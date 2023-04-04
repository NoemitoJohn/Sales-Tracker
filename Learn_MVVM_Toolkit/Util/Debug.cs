using System.Diagnostics;
namespace Learn_MVVM_Toolkit.Util;

public class Debug
{
    public static void Log(object? v)
    {
        Trace.WriteLine(v);
    }
}
