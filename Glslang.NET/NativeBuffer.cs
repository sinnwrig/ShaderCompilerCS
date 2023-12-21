using System.Runtime.InteropServices;
using System.Text;

namespace DXCompiler.NET;


[StructLayout(LayoutKind.Sequential)]
public struct NativeBuffer
{
    public IntPtr Buf;
    public nint Len;
    public uint Encoding;


    const uint UTF8 = 65001;
    const uint UTF16 = 1200;
    const uint UTF32 = 12000;
    const uint ACP = 0;


    public static NativeBuffer CreateFromString(string bufferString)
    {
        NativeBuffer buffer;
        buffer.Buf = StringUtility.GetUtf8Ptr(bufferString, out uint len);
        buffer.Len = (nint)len;
        buffer.Encoding = UTF8;
        return buffer;
    }


    public static NativeBuffer CreateFromBytes(byte[] bytes)
    {
        NativeBuffer buffer;
        buffer.Len = bytes.Length;
        buffer.Buf = Marshal.AllocHGlobal(buffer.Len);
        buffer.Encoding = ACP;
        Marshal.Copy(bytes, 0, buffer.Buf, (int)buffer.Len);
        return buffer;
    }


    public void FreeBuffer()
    {
        if (Buf == IntPtr.Zero)
            return;
        
        Marshal.FreeHGlobal(Buf);
        Len = 0;
        Encoding = 0;
    }


    public bool TryGetBytes(out byte[] blobBytes)
    {
        blobBytes = null!;
        try
        {
            if (Len <= 0 || Buf == IntPtr.Zero)
                return false;
            blobBytes = new byte[Len];
            Marshal.Copy(Buf, blobBytes, 0, (int)Len);
            return true;
        }
        catch
        {
            return false;
        }
    }


    public bool TryGetString(out string blobString)
    {
        blobString = null!;
        try
        {   
            if (!TryGetBytes(out byte[] bytes))
                return false;

            Encoding encoding = System.Text.Encoding.GetEncoding((int)Encoding);
            blobString = encoding.GetString(bytes);
            return true;
        }
        catch
        {
            return false;
        }
    }
}