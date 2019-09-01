namespace EaslyNumber
{
    /// <summary>
    /// Optional separator between the decimal part and the fractionla part of a significand.
    /// </summary>
    internal enum OptionalSeparator
    {
        /// <summary>
        /// No separator (the number is an integer).
        /// </summary>
        None,

        /// <summary>
        /// English dot.
        /// </summary>
        Normalized,

        /// <summary>
        /// The separator character specific to a culture.
        /// </summary>
        CultureSpecific,
    }
}
