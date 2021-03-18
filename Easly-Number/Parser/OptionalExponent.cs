namespace EaslyNumber
{
    /// <summary>
    /// Optional exponent in a number.
    /// </summary>
    internal enum OptionalExponent
    {
        /// <summary>
        /// No exponent (the number uses fixed-point notation).
        /// </summary>
        None,

        /// <summary>
        /// 'E' character.
        /// </summary>
        UpperCaseE,

        /// <summary>
        /// 'e' character.
        /// </summary>
        LowerCaseE,
    }
}
