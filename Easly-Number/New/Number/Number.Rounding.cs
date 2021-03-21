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
        /// Returns the number rounded using <paramref name="rounding"/> mode.
        /// </summary>
        /// <param name="rounding">The rounding mode.</param>
        public Number Round(Rounding rounding)
        {
            Consolidate();

            Number z = new Number(Precision, Rounding);

            mpfr_rint(ref z.Proxy.MpfrStruct, ref Proxy.MpfrStruct, rounding);

            return z;
        }

        /// <summary>
        /// Returns the next higher or equal representable integer.
        /// </summary>
        public Number Ceil()
        {
            Consolidate();

            Number z = new Number(Precision, Rounding);

            mpfr_ceil(ref z.Proxy.MpfrStruct, ref Proxy.MpfrStruct);

            return z;
        }

        /// <summary>
        /// Returns the next lower or equal representable integer.
        /// </summary>
        public Number Floor()
        {
            Consolidate();

            Number z = new Number(Precision, Rounding);

            mpfr_floor(ref z.Proxy.MpfrStruct, ref Proxy.MpfrStruct);

            return z;
        }

        /// <summary>
        /// Returns the nearest representable integer, rounding halfway cases away from zero.
        /// </summary>
        public Number Round()
        {
            Consolidate();

            Number z = new Number(Precision, Rounding);

            mpfr_round(ref z.Proxy.MpfrStruct, ref Proxy.MpfrStruct);

            return z;
        }

        /// <summary>
        /// Returns the nearest representable integer, rounding halfway cases with the even-rounding rule.
        /// </summary>
        public Number RoundEven()
        {
            Consolidate();

            Number z = new Number(Precision, Rounding);

            mpfr_roundeven(ref z.Proxy.MpfrStruct, ref Proxy.MpfrStruct);

            return z;
        }

        /// <summary>
        /// Returns the nearest representable integer, rounding toward zero.
        /// </summary>
        public Number Trunc()
        {
            Consolidate();

            Number z = new Number(Precision, Rounding);

            mpfr_trunc(ref z.Proxy.MpfrStruct, ref Proxy.MpfrStruct);

            return z;
        }
    }
}
