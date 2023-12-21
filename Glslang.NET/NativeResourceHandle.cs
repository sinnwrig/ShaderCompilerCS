namespace DXCompiler.NET;


public abstract class NativeResourceHandle : IDisposable
{
    protected IntPtr handle;

    protected abstract bool ReleaseHandle();


    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }


    public IntPtr GetHandle() => handle;


    ~NativeResourceHandle()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Warning: Native handle for {GetType().Name} was not properly deallocated with a using statement.");
        Console.ForegroundColor = ConsoleColor.White;

        Dispose();
    }

}