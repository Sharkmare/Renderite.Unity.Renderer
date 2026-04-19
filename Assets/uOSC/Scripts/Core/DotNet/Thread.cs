#if !NETFX_CORE

using UnityEngine;
using System;

namespace uOSC.DotNet
{

public class Thread : uOSC.Thread
{
    System.Threading.Thread thread_;
    bool isRunning_ = false;
    Action loopFunc_ = null;

    public override void Start(Action loopFunc)
    {
        if (isRunning_ || loopFunc == null) return;

        isRunning_ = true;
        loopFunc_ = loopFunc;

        // OSC-025: mark as background so this thread does not prevent process exit
        // when Unity quits before OnDisable fires (crash / Environment.Exit).
        // AboveNormal priority ensures OSC haptic/tracking data is not preempted
        // by normal-priority work threads during heavy frame loads.
        thread_ = new System.Threading.Thread(ThreadLoop);
        thread_.IsBackground = true;
        thread_.Priority = System.Threading.ThreadPriority.AboveNormal;
        thread_.Start();
    }

    void ThreadLoop()
    {
        while (isRunning_)
        {
            try
            {
                loopFunc_();
                System.Threading.Thread.Sleep(IntervalMillisec);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
                Debug.LogError(e.StackTrace);
            }
        }
    }

    public override void Stop(int timeoutMilliseconds = 3000)
    {
        if (!isRunning_) return;

        isRunning_ = false;

        if (thread_.IsAlive)
        {
            thread_.Join(timeoutMilliseconds);
            if (thread_.IsAlive)
            {
                thread_.Abort();
            }
        }
    }
}

}

#endif