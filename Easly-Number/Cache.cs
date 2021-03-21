namespace EaslyNumber
{
    using System.Threading;
    using static NativeMethods;

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
}
