﻿namespace EaslyNumber;

using System;
using Interop.Mpfr;
using static Interop.Mpfr.NativeMethods;

/// <summary>
/// Represents numbers with arbitrary precision.
/// </summary>
public partial struct Number : IFormattable
{
    /// <summary>
    /// Gets the value if it can be represented with a <see cref="int"/>.
    /// </summary>
    /// <param name="value">The value upon return.</param>
    public bool TryParseInt(out int value)
    {
        value = 0;

        Consolidate();

        if (!IsInteger)
            return false;

        if (mpfr_fits_sint_p(ref Proxy.MpfrStruct, (mpfr_rnd_t)Rounding) == 0)
            return false;

        long Result = mpfr_get_si(ref Proxy.MpfrStruct, (mpfr_rnd_t)Rounding);

        value = (int)Result;
        return true;
    }

    /// <summary>
    /// Gets the value if it can be represented with a <see cref="uint"/>.
    /// </summary>
    /// <param name="value">The value upon return.</param>
    public bool TryParseUInt(out uint value)
    {
        value = 0;

        Consolidate();

        if (!IsInteger)
            return false;

        if (mpfr_fits_uint_p(ref Proxy.MpfrStruct, (mpfr_rnd_t)Rounding) == 0)
            return false;

        ulong Result = mpfr_get_ui(ref Proxy.MpfrStruct, (mpfr_rnd_t)Rounding);

        value = (uint)Result;
        return true;
    }

    /// <summary>
    /// Gets the value if it can be represented with a <see cref="long"/>.
    /// </summary>
    /// <param name="value">The value upon return.</param>
    public bool TryParseLong(out long value)
    {
        value = 0;

        Consolidate();

        if (!IsInteger)
            return false;

        if (mpfr_cmp(ref Proxy.MpfrStruct, ref LongMinValue.Proxy.MpfrStruct) < 0)
            return false;

        if (mpfr_cmp(ref Proxy.MpfrStruct, ref LongMaxValue.Proxy.MpfrStruct) > 0)
            return false;

        Interop.Mpir.NativeMethods.__mpz_t IntValue = new() { Limbs = IntPtr.Zero };
        Interop.Mpir.NativeMethods.mpz_init(ref IntValue);
        mpfr_get_z(ref IntValue, ref Proxy.MpfrStruct, mpfr_rnd_t.MPFR_RNDN);

        byte[] Bytes = new byte[16];

        Interop.Mpir.NativeMethods.size_t countp;
        Interop.Mpir.NativeMethods.mpz_export(Bytes, out countp, -1, (Interop.Mpir.NativeMethods.size_t)sizeof(byte), 0, (Interop.Mpir.NativeMethods.size_t)0, ref IntValue);

        value = BitConverter.ToInt64(Bytes, 0);
        return true;
    }

    /// <summary>
    /// Gets the value if it can be represented with a <see cref="ulong"/>.
    /// </summary>
    /// <param name="value">The value upon return.</param>
    public bool TryParseULong(out ulong value)
    {
        value = 0;

        Consolidate();

        if (!IsInteger)
            return false;

        if (mpfr_sgn(ref Proxy.MpfrStruct) < 0)
            return false;

        if (mpfr_cmp(ref Proxy.MpfrStruct, ref ULongMaxValue.Proxy.MpfrStruct) > 0)
            return false;

        Interop.Mpir.NativeMethods.__mpz_t IntValue = new() { Limbs = IntPtr.Zero };
        Interop.Mpir.NativeMethods.mpz_init(ref IntValue);
        mpfr_get_z(ref IntValue, ref Proxy.MpfrStruct, mpfr_rnd_t.MPFR_RNDN);

        byte[] Bytes = new byte[16];

        Interop.Mpir.NativeMethods.size_t countp;
        Interop.Mpir.NativeMethods.mpz_export(Bytes, out countp, -1, (Interop.Mpir.NativeMethods.size_t)sizeof(byte), 0, (Interop.Mpir.NativeMethods.size_t)0, ref IntValue);

        value = BitConverter.ToUInt64(Bytes, 0);
        return true;
    }
}
