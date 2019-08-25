namespace EaslyNumber
{
    /// <summary>
    /// The rounding mode.
    /// </summary>
    public enum Rounding
    {
        /// <summary>
        /// Round to nearest.
        /// </summary>
        ToNearest,

        /// <summary>
        /// Round toward zero.
        /// </summary>
        TowardZero,

        /// <summary>
        /// Round toward +inf.
        /// </summary>
        TowardPositiveInfinity,

        /// <summary>
        /// Round toward -inf.
        /// </summary>
        TowardNegativeInfinity,

        /// <summary>
        /// Round away from zero.
        /// </summary>
        AwayFromZero,
    }
}
