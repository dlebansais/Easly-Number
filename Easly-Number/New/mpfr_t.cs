namespace EaslyNumber2
{
    using System.Threading;
    using static NativeMethods;

#pragma warning disable SA1300 // Element should begin with upper-case letter
    internal class mpfr_t
    {
        public mpfr_t()
        {
            ulong Precision = mpfr_get_default_prec();
            mpfr_init2(ref MpfrStruct, Precision);

            InitCacheManagement();
        }

        public mpfr_t(ulong precision)
        {
            mpfr_init2(ref MpfrStruct, precision);

            InitCacheManagement();
        }

        private void InitCacheManagement()
        {
            if (!ObjectCount.IsValueCreated)
                ObjectCount.Value = 0;
            else
                ObjectCount.Value++;

            IsCacheInitialized = true;
        }

        private void DisposeCache()
        {
            ObjectCount.Value--;

            if (ObjectCount.Value == 0)
                mpfr_free_cache();
        }

        private static ThreadLocal<ulong> ObjectCount = new ThreadLocal<ulong>();

        public static ulong LiveObjectCount()
        {
            return ObjectCount.Value;
        }

        public bool IsCacheInitialized { get; private set; }

        ~mpfr_t()
        {
            mpfr_clear(ref MpfrStruct);

            DisposeCache();
        }

#pragma warning disable SA1401 // Fields should be private
        public __mpfr_t MpfrStruct;
#pragma warning restore SA1401 // Fields should be private
    }
#pragma warning restore SA1300 // Element should begin with upper-case letter
}
