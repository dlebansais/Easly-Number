namespace EaslyNumber2
{
    using static NativeMethods;

#pragma warning disable SA1300 // Element should begin with upper-case letter
    internal class mpfr_t
    {
        public mpfr_t()
        {
            ulong Precision = mpfr_get_default_prec();
            mpfr_init2(ref MpfrStruct, Precision);
        }

        ~mpfr_t()
        {
            mpfr_clear(ref MpfrStruct);
        }

#pragma warning disable SA1401 // Fields should be private
        public __mpfr_t MpfrStruct;
#pragma warning restore SA1401 // Fields should be private
    }
#pragma warning restore SA1300 // Element should begin with upper-case letter
}
