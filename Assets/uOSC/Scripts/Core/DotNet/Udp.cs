#if !NETFX_CORE

using UnityEngine;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace uOSC.DotNet
{

public class Udp : uOSC.Udp
{
    enum State
    {
        Stop,
        Server,
        Client,
    }
    State state_ = State.Stop;

    Queue<byte[]> messageQueue_ = new Queue<byte[]>();
    object lockObject_ = new object();

    UdpClient udpClient_;
    IPEndPoint endPoint_;
    Thread thread_ = new Thread();

    public override int messageCount
    {
        get { return messageQueue_.Count; }
    }

    public override void StartServer(int port)
    {
        Stop();
        state_ = State.Server;

        endPoint_ = new IPEndPoint(IPAddress.Any, port);
        udpClient_ = new UdpClient(endPoint_);
        // OSC-024: set a 1 ms socket receive timeout so the thread blocks in the kernel
        // instead of calling Available every millisecond and immediately going back to sleep.
        // Previous pattern (Available-poll + Thread.Sleep(1)) caused ~1000 context switches
        // per second with zero data flowing. Now the thread wakes only when a datagram
        // arrives or after 1 ms, whichever comes first.
        udpClient_.Client.ReceiveTimeout = 1;
        thread_.Start(() =>
        {
            try
            {
                // OSC-024: block up to ReceiveTimeout (1 ms) waiting for a datagram.
                var buffer = udpClient_.Receive(ref endPoint_);
                lock (lockObject_) { messageQueue_.Enqueue(buffer); }
                // drain any additional datagrams that arrived while we were processing
                while (udpClient_.Available > 0)
                {
                    buffer = udpClient_.Receive(ref endPoint_);
                    lock (lockObject_) { messageQueue_.Enqueue(buffer); }
                }
            }
            catch (System.Net.Sockets.SocketException)
            {
                // OSC-024: timeout expired with no data — normal, loop back
            }
        });
    }

    public override void StartClient(string address, int port)
    {
        Stop();
        state_ = State.Client;

        var ip = IPAddress.Parse(address);
        endPoint_ = new IPEndPoint(ip, port);
        udpClient_ = new UdpClient();
    }

    public override void Stop()
    {
        if (state_ == State.Stop) return;

        thread_.Stop();
        udpClient_.Close();
        state_ = State.Stop;
    }

    public override void Send(byte[] data, int size)
    {
        udpClient_.Send(data, size, endPoint_);
    }

    public override byte[] Receive()
    {
        byte[] buffer;
        lock (lockObject_)
        {
            buffer = messageQueue_.Dequeue();
        }
        return buffer;
    }
}

}

#endif