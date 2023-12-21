using System.Runtime.InteropServices;

namespace DXCompiler.NET;


// Callback for local file inclusion 
public delegate string IncludeFileDelegate(string filename);


public class IncludeHandler : NativeResourceHandle
{
    [StructLayout(LayoutKind.Sequential)]
    private struct IncludeContext
    {
        public IncludeFileDelegate includeFile;
    }


    private delegate NativeBuffer NativeHandlerDelegate(IntPtr context, IntPtr filenameutf8);
    private static NativeBuffer IncludeHandlerNative(IntPtr contextPtr, IntPtr filenameutf8)
    {
        string? filename = Marshal.PtrToStringUTF8(filenameutf8);

        if (filename == null)
            return NativeBuffer.CreateFromString("");

        IncludeContext context = Marshal.PtrToStructure<IncludeContext>(contextPtr);

        string file = context.includeFile(filename);

        return NativeBuffer.CreateFromString(file);
    }

    private static IntPtr IncludeHandlerNativePtr => Marshal.GetFunctionPointerForDelegate<NativeHandlerDelegate>(IncludeHandlerNative);


    // Keep a reference so the GC does not deallocate the delegate's internal context
    IncludeFileDelegate delegateReference;
    IntPtr contextPtr;


    // Free with DeleteIncludeHandler
    [DllImport(Global.LibraryPath, CallingConvention = CallingConvention.Cdecl)]
    private static extern IntPtr CreateIncludeHandler(IntPtr ctx, IntPtr delegatePtr);


    [DllImport(Global.LibraryPath, CallingConvention = CallingConvention.Cdecl)]
    private static extern void DeleteIncludeHandler(IntPtr handler);


    public IncludeHandler(IncludeFileDelegate? includeDelegate = null)
    {
        delegateReference = includeDelegate ?? DefaultIncludeHandler;

        IncludeContext context = new IncludeContext()
        {
            includeFile = delegateReference
        };

        contextPtr = Marshal.AllocHGlobal(Marshal.SizeOf<IncludeContext>());
        Marshal.StructureToPtr(context, contextPtr, false);

        handle = CreateIncludeHandler(contextPtr, IncludeHandlerNativePtr);
    }


    private string DefaultIncludeHandler(string filename)
    {   
        try
        {
            using FileStream fs = File.Open(filename, FileMode.Open);
            using StreamReader reader = new StreamReader(fs);
            return reader.ReadToEnd();
        }
        catch (FileNotFoundException) {} 
        
        return "";
    }


    protected override bool ReleaseHandle()
    {
        Marshal.FreeHGlobal(contextPtr);
        DeleteIncludeHandler(handle);

        return true;
    }
}