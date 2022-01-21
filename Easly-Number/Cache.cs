namespace EaslyNumber;

using System;
using System.Diagnostics;
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
        Debug.Assert(!IsCacheDisposed);

        IsCacheDisposed = true;

        if (IsValueCreated)
        {
            Interlocked.Increment(ref FreeCountInternal);
            mpfr_free_cache2(1);
        }

        base.Dispose(disposing);
    }

    private bool IsCacheDisposed;

    internal static long FreeCount
    {
        get { return FreeCountInternal; }
    }

    private static long FreeCountInternal;
}
#pragma warning restore SA1600 // Elements should be documented
