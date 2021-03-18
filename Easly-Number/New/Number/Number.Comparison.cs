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
        /// Compares two instances.
        /// </summary>
        /// <param name="other">The other instance.</param>
        /// <returns>Negative value if the current instance is less then <paramref name="other"/>, positive value if the current instance is greated then <paramref name="other"/>; otherwise, zero.</returns>
        public int CompareTo(Number other)
        {
            Consolidate();
            other.Consolidate();

            return mpfr_cmp(ref Proxy.MpfrStruct, ref other.Proxy.MpfrStruct);
        }

        /// <summary>
        /// Compares two values.
        /// </summary>
        /// <param name="other">The other value.</param>
        /// <returns>Negative value if the current instance is less then <paramref name="other"/>, positive value if the current instance is greated then <paramref name="other"/>; otherwise, zero.</returns>
        public int CompareTo(ulong other)
        {
            Consolidate();

            return mpfr_cmp_ui(ref Proxy.MpfrStruct, other);
        }

        /// <summary>
        /// Compares two values.
        /// </summary>
        /// <param name="other">The other value.</param>
        /// <returns>Negative value if the current instance is less then <paramref name="other"/>, positive value if the current instance is greated then <paramref name="other"/>; otherwise, zero.</returns>
        public int CompareTo(long other)
        {
            Consolidate();

            return mpfr_cmp_si(ref Proxy.MpfrStruct, other);
        }

        /// <summary>
        /// Compares two values.
        /// </summary>
        /// <param name="other">The other value.</param>
        /// <returns>Negative value if the current instance is less then <paramref name="other"/>, positive value if the current instance is greated then <paramref name="other"/>; otherwise, zero.</returns>
        public int CompareTo(double other)
        {
            Consolidate();

            return mpfr_cmp_d(ref Proxy.MpfrStruct, other);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        public override bool Equals(object? obj)
        {
            if (obj is Number other)
            {
                Consolidate();
                other.Consolidate();

                return mpfr_equal_p(ref Proxy.MpfrStruct, ref other.Proxy.MpfrStruct) == 0;
            }
            else
                return false;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
        {
            Consolidate();

            double d = mpfr_get_d(ref Proxy.MpfrStruct, DefaultRounding);
            return d.GetHashCode();
        }
    }
}
