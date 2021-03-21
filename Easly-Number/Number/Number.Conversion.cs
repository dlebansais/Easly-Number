namespace EaslyNumber
{
    using System;
    using static EaslyNumber.NativeMethods;

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

            if (mpfr_fits_sint_p(ref Proxy.MpfrStruct, Rounding) == 0)
                return false;

            long Result = mpfr_get_si(ref Proxy.MpfrStruct, Rounding);

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

            if (mpfr_fits_uint_p(ref Proxy.MpfrStruct, Rounding) == 0)
                return false;

            ulong Result = mpfr_get_ui(ref Proxy.MpfrStruct, Rounding);

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

            if (mpfr_cmp(ref Proxy.MpfrStruct, ref LongMinValue.Proxy.MpfrStruct) < 0 || mpfr_cmp(ref Proxy.MpfrStruct, ref LongMaxValue.Proxy.MpfrStruct) > 0)
                return false;

            __mpz_t IntValue = new() { Limbs = IntPtr.Zero };
            mpz_init(ref IntValue);
            mpfr_get_z(ref IntValue, ref Proxy.MpfrStruct, Rounding.Nearest);

            byte[] Bytes = new byte[16];

            long countp;
            mpz_export(Bytes, out countp, -1, sizeof(byte), 0, 0, ref IntValue);

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

            if (mpfr_sgn(ref Proxy.MpfrStruct) < 0 || mpfr_cmp(ref Proxy.MpfrStruct, ref ULongMaxValue.Proxy.MpfrStruct) > 0)
                return false;

            __mpz_t IntValue = new() { Limbs = IntPtr.Zero };
            mpz_init(ref IntValue);
            mpfr_get_z(ref IntValue, ref Proxy.MpfrStruct, Rounding.Nearest);

            byte[] Bytes = new byte[16];

            long countp;
            mpz_export(Bytes, out countp, -1, sizeof(byte), 0, 0, ref IntValue);

            value = BitConverter.ToUInt64(Bytes, 0);
            return true;
        }
    }
}
