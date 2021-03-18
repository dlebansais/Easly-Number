namespace EaslyNumber2
{
    /// <summary>
    /// Rounding mode for operations on numbers.
    /// </summary>
    public enum Rounding
    {
        /// <summary>
        /// Round to nearest, with ties away from zero (mpfr_round).
        /// </summary>
        NearestAwayFromZero = -1,

        /// <summary>
        /// Round to nearest, with ties to even.
        /// </summary>
        Nearest = 0,

        /// <summary>
        /// Round toward zero.
        /// </summary>
        TowardZero,

        /// <summary>
        /// Round toward +Inf.
        /// </summary>
        TowardPositiveInfinity,

        /// <summary>
        /// Round toward -Inf.
        /// </summary>
        TowardNegativeInfinity,

        /// <summary>
        /// Round away from zero.
        /// </summary>
        AwayFromZero,

        /// <summary>
        /// Faithful rounding.
        /// </summary>
        Faithful,
    }
}
