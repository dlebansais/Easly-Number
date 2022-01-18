namespace EaslyNumber;

using System.Threading;
using static Interop.Mpfr.NativeMethods;

#pragma warning disable SA1600 // Elements should be documented
internal class Cache : ThreadLocal<bool>
{
    protected override void Dispose(bool disposing)
    {
        if (!IsCacheCleared)
        {
            IsCacheCleared = true;
            mpfr_free_cache2(1);
        }

        base.Dispose(disposing);
    }

    private bool IsCacheCleared;
}
#pragma warning restore SA1600 // Elements should be documented
