namespace EaslyNumber
{
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    /// Describes and manipulates real numbers with arbitrary precision.
    /// </summary>
    public partial struct Number
    {
        #region Parser
        /// <summary>
        /// Parses a string to extract relevant parts for a number.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="discardedProlog">The beginning of the string that can be ignored.</param>
        /// <param name="significandSign">The optional sign of the significand.</param>
        /// <param name="integerPart">The integer part in front of the separator (if any).</param>
        /// <param name="separator">The optional separator.</param>
        /// <param name="fractionalPart">The fractional part after the separator (if any).</param>
        /// <param name="exponentCharacter">The optional exponent character.</param>
        /// <param name="exponentSign">The optional exponent sign.</param>
        /// <param name="exponentPart">The exponent part (if any).</param>
        /// <param name="invalidPart">The remaining part of the string that is not parsed in the number.</param>
        internal static void Parse(string text, out string discardedProlog, out OptionalSign significandSign, out string integerPart, out OptionalSeparator separator, out string fractionalPart, out OptionalExponent exponentCharacter, out OptionalSign exponentSign, out string exponentPart, out string invalidPart)
        {
            discardedProlog = null;
            significandSign = OptionalSign.None;
            integerPart = null;
            separator = OptionalSeparator.None;
            fractionalPart = null;
            exponentCharacter = OptionalExponent.None;
            exponentSign = OptionalSign.None;
            exponentPart = null;
            invalidPart = null;

            Debug.Assert(integerPart.Length > 0 || fractionalPart.Length > 0);
            Debug.Assert(exponentSign == OptionalSign.None || exponentCharacter != OptionalExponent.None);
            Debug.Assert(exponentPart.Length == 0 || exponentCharacter != OptionalExponent.None);
        }

        /// <summary>
        /// Returns the text of an optional sign.
        /// </summary>
        /// <param name="sign">The optional sign.</param>
        internal static string SignText(OptionalSign sign)
        {
            if (sign == OptionalSign.Positive)
                return "+";
            else if (sign == OptionalSign.Negative)
                return "-";
            else
                return string.Empty;
        }

        /// <summary>
        /// Returns the text of an optional separator.
        /// </summary>
        /// <param name="separator">The optional separator.</param>
        internal static string SeparatorText(OptionalSeparator separator)
        {
            if (separator == OptionalSeparator.Normalized)
                return ".";
            else if (separator == OptionalSeparator.CultureSpecific)
                return CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            else
                return string.Empty;
        }

        /// <summary>
        /// Returns the text of an optional exponent character.
        /// </summary>
        /// <param name="exponentCharacter">The optional exponent character.</param>
        internal static string ExponentCharacterText(OptionalExponent exponentCharacter)
        {
            if (exponentCharacter == OptionalExponent.UpperCaseE)
                return "E";
            else if (exponentCharacter == OptionalExponent.LowerCaseE)
                return "e";
            else
                return string.Empty;
        }
        #endregion
    }
}
