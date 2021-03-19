namespace EaslyNumber2
{
    using System.Threading;
    using static NativeMethods;

    internal class Cache : ThreadLocal<bool>
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing && IsValueCreated)
                mpfr_free_cache2(1);

            base.Dispose(disposing);
        }
    }
}
