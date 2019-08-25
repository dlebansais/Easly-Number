namespace EaslyNumber
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    /// <summary>
    /// Specifies how operations on instances of <see cref="Number"/> objects are performed.
    /// </summary>
    public static class Arithmetic
    {
        #region Constants
        /// <summary>
        /// The default significand precision until set by the client.
        /// </summary>
        public const long DefaultSignificandPrecision = 53;

        /// <summary>
        /// The default exponent precision until set by the client.
        /// </summary>
        public const long DefaultExponentPrecision = 53;

        /// <summary>
        /// The default rounding until set by the client.
        /// </summary>
        public const Rounding DefaultRounding = Rounding.ToNearest;

        /// <summary>
        /// The default value for <see cref="EnableInfinitePrecision"/> until set by the client.
        /// </summary>
        public const bool DefaultEnableInfinitePrecision = false;
        #endregion

        #region Init
        static Arithmetic()
        {
            ThreadLocalSignificandPrecision = new ThreadLocal<long>();
            ThreadLocalExponentPrecision = new ThreadLocal<long>();
            ThreadLocalFlags = new ThreadLocal<Flags>();
            ThreadLocalRounding = new ThreadLocal<Rounding>();
            ThreadLocalEnableInfinitePrecision = new ThreadLocal<bool>();

            Debug.Assert(IsSignificandPrecisionValid(DefaultSignificandPrecision));
            Debug.Assert(IsExponentPrecisionValid(DefaultExponentPrecision));
        }
        #endregion

        #region Properties
        /// <summary>
        /// The number of bits in the significand when numbers are created.
        /// </summary>
        public static long SignificandPrecision
        {
            get
            {
                if (!ThreadLocalSignificandPrecision.IsValueCreated)
                    ThreadLocalSignificandPrecision.Value = DefaultSignificandPrecision;

                return ThreadLocalSignificandPrecision.Value;
            }
            set
            {
                if (!IsSignificandPrecisionValid(value))
                    throw new ArgumentOutOfRangeException(nameof(value));

                ThreadLocalSignificandPrecision.Value = value;
            }
        }
        private static ThreadLocal<long> ThreadLocalSignificandPrecision;

        /// <summary>
        /// The number of bits in the exponent when numbers are created.
        /// </summary>
        public static long ExponentPrecision
        {
            get
            {
                if (!ThreadLocalExponentPrecision.IsValueCreated)
                    ThreadLocalExponentPrecision.Value = DefaultExponentPrecision;

                return ThreadLocalExponentPrecision.Value;
            }
            set
            {
                if (!IsExponentPrecisionValid(value))
                    throw new ArgumentOutOfRangeException(nameof(value));

                ThreadLocalExponentPrecision.Value = value;
            }
        }
        private static ThreadLocal<long> ThreadLocalExponentPrecision;

        /// <summary>
        /// The rounding mode to use when performing operations.
        /// </summary>
        public static Rounding Rounding
        {
            get
            {
                if (!ThreadLocalRounding.IsValueCreated)
                    ThreadLocalRounding.Value = DefaultRounding;

                return ThreadLocalRounding.Value;
            }
            set
            {
                ThreadLocalRounding.Value = value;
            }
        }
        private static ThreadLocal<Rounding> ThreadLocalRounding;

        /// <summary>
        /// True if operations that can be exact can increase the precision of the resulting number automatically.
        /// </summary>
        public static bool EnableInfinitePrecision
        {
            get
            {
                if (!ThreadLocalEnableInfinitePrecision.IsValueCreated)
                    ThreadLocalEnableInfinitePrecision.Value = DefaultEnableInfinitePrecision;

                return ThreadLocalEnableInfinitePrecision.Value;
            }
            set
            {
                ThreadLocalEnableInfinitePrecision.Value = value;
            }
        }
        private static ThreadLocal<bool> ThreadLocalEnableInfinitePrecision;

        /// <summary>
        /// Flag containing information about the result of operations.
        /// </summary>
        public static Flags Flags
        {
            get
            {
                if (!ThreadLocalFlags.IsValueCreated)
                    ThreadLocalFlags.Value = new Flags();

                return ThreadLocalFlags.Value;
            }
        }
        private static ThreadLocal<Flags> ThreadLocalFlags;
        #endregion

        #region Client Interface
        /// <summary>
        /// Tests if the given significand precision value is valid.
        /// </summary>
        /// <param name="precision">The precision value.</param>
        /// <returns>True if valid; Otherwise, false.</returns>
        public static bool IsSignificandPrecisionValid(long precision)
        {
            return precision > 0;
        }

        /// <summary>
        /// Tests if the given exponent precision value is valid.
        /// </summary>
        /// <param name="precision">The precision value.</param>
        /// <returns>True if valid; Otherwise, false.</returns>
        public static bool IsExponentPrecisionValid(long precision)
        {
            return precision > 0;
        }
        #endregion
    }
}
