namespace EaslyNumber2
{
    using System;
    using static EaslyNumber2.NativeMethods;

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

            if (mpfr_fits_sint_p(ref Proxy.MpfrStruct, Rounding) == 0)
                return false;

            long Result = mpfr_get_si(ref Proxy.MpfrStruct, Rounding);

            if (value < int.MinValue || value > int.MaxValue)
                return false;

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

            if (mpfr_fits_uint_p(ref Proxy.MpfrStruct, Rounding) == 0)
                return false;

            ulong Result = mpfr_get_ui(ref Proxy.MpfrStruct, Rounding);

            if (value > uint.MaxValue)
                return false;

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

            if (mpfr_fits_sint_p(ref Proxy.MpfrStruct, Rounding) == 0)
                return false;

            value = mpfr_get_si(ref Proxy.MpfrStruct, Rounding);
            return true;
        }

        /// <summary>
        /// Gets the value if it can be represented with a <see cref="ulong"/>.
        /// </summary>
        /// <param name="value">The value upon return.</param>
        public bool TryParseUInt(out ulong value)
        {
            value = 0;

            Consolidate();

            if (mpfr_fits_uint_p(ref Proxy.MpfrStruct, Rounding) == 0)
                return false;

            value = mpfr_get_ui(ref Proxy.MpfrStruct, Rounding);
            return true;
        }
    }
}
