namespace EaslyNumber;

using System.Threading;
using static Interop.Mpfr.NativeMethods;

#pragma warning disable SA1600 // Elements should be documented
internal class Cache : ThreadLocal<string>
{
    public Cache()
        : base(() => { return "Thread" + Thread.CurrentThread.ManagedThreadId; })
    {
    }

    protected override void Dispose(bool disposing)
    {
        if (!IsValueCreated)
        {
            _ = Value;
            mpfr_free_cache2(1);
        }

        base.Dispose(disposing);
    }
}
#pragma warning restore SA1600 // Elements should be documented
