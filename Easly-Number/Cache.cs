namespace EaslyNumber;

using System.Diagnostics;
using System.Threading;
using static Interop.Mpfr.NativeMethods;

/// <summary>
/// Handles cleanup of the MPFR library cache.
/// </summary>
internal class Cache : ThreadLocal<string>
{
    #region Init
    /// <summary>
    /// Initializes a new instance of the <see cref="Cache"/> class.
    /// </summary>
    public Cache()
        : base(() => { return "Thread" + Thread.CurrentThread.ManagedThreadId; })
    {
    }
    #endregion

    #region Overrides
    /// <summary>
    /// Releases the resources used by this <see cref="Cache"/> instance.
    /// </summary>
    /// <param name="disposing">A Boolean value that indicates whether this method is being called due to a call to <see cref="Cache.Dispose(bool)"/>.</param>
    protected override void Dispose(bool disposing)
    {
        // Since this override is called from the implementation of ThreadLocal<>, it is always called only once.
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
    #endregion

    #region Overrides
    /// <summary>
    /// Gets the number of calls to <see cref="Cache.Dispose(bool)"/> that cleared the cache.
    /// </summary>
    internal static long FreeCount
    {
        get { return FreeCountInternal; }
    }

    private static long FreeCountInternal;
    #endregion
}
