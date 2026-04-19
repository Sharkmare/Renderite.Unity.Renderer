using System;
using System.Text;

namespace uOSC
{

public static class Reader
{
    public static string ParseString(byte[] buf, ref int pos)
    {
        int size = 0;
        int bufSize = buf.Length;
        for (; buf[pos + size] != 0; ++size);
        var value = Encoding.UTF8.GetString(buf, pos, size);
        pos += Util.GetStringAlignedSize(size);
        return value;
    }

    public static int ParseInt(byte[] buf, ref int pos)
    {
        // OSC-012: BinaryPrimitives reads big-endian directly — does NOT mutate the input buffer
        // (Array.Reverse was destructive, causing silent corruption if the buffer was reused).
        var value = System.Buffers.Binary.BinaryPrimitives.ReadInt32BigEndian(
            new System.ReadOnlySpan<byte>(buf, pos, 4));
        pos += 4;
        return value;
    }

    public static float ParseFloat(byte[] buf, ref int pos)
    {
        // OSC-013: read big-endian int bits then reinterpret as float — non-mutating and
        // allocation-free. ReadSingleBigEndian is .NET 5+ only, so we use the uint path.
        var bits = System.Buffers.Binary.BinaryPrimitives.ReadUInt32BigEndian(
            new System.ReadOnlySpan<byte>(buf, pos, 4));
        pos += 4;
        unsafe { return *(float*)&bits; }
    }

    public static byte[] ParseBlob(byte[] buf, ref int pos)
    {
        var size = ParseInt(buf, ref pos);
        var value = new byte[size];
        Buffer.BlockCopy(buf, pos, value, 0, size);
        pos += Util.GetBufferAlignedSize(size);
        return value;
    }

    public static ulong ParseTimetag(byte[] buf, ref int pos)
    {
        // OSC-014: BinaryPrimitives reads big-endian UInt64 — non-mutating.
        var value = System.Buffers.Binary.BinaryPrimitives.ReadUInt64BigEndian(
            new System.ReadOnlySpan<byte>(buf, pos, 8));
        pos += 8;
        return value;
    }
}

}