﻿namespace EaslyNumber;

using Interop.Mpfr;
using System.Threading;
using static Interop.Mpfr.NativeMethods;

#pragma warning disable SA1300 // Element should begin with upper-case letter
#pragma warning disable SA1600 // Elements should be documented
internal class mpfr_t
{
    public mpfr_t()
        : this(mpfr_get_default_prec())
    {
    }

    public mpfr_t(ulong precision)
    {
        mpfr_init2(ref MpfrStruct, precision);

        InitCacheManagement();
    }

    public mpfr_t(__mpfr_t mpfrStruct)
    {
        MpfrStruct = mpfrStruct;

        InitCacheManagement();
    }

    private void InitCacheManagement()
    {
        _ = LibraryCache.Value;
    }

    internal static Cache LibraryCache { get; } = new Cache();

    ~mpfr_t()
    {
        mpfr_clear(ref MpfrStruct);
    }

#pragma warning disable SA1401 // Fields should be private
    public __mpfr_t MpfrStruct;
#pragma warning restore SA1401 // Fields should be private
}
#pragma warning restore SA1300 // Element should begin with upper-case letter
#pragma warning restore SA1600 // Elements should be documented
