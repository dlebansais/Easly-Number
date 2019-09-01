namespace EaslyNumber
{
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    /// The partition of a string into different components of a number.
    /// </summary>
    internal abstract class TextPartition
    {
        /// <summary>
        /// Parses a new character.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="index">The position of the character to parse in <paramref name="text"/>.</param>
        public abstract void Parse(string text, int index);

        /// <summary>
        /// The parser current state.
        /// </summary>
        protected ParsingState State { get; set; } = ParsingState.Init;

        /// <summary>
        /// The decimal separator character for the current culture.
        /// </summary>
        protected char CultureDecimalSeparator { get; set; }

        /// <summary>
        /// Index of the last optional space character, -1 if not parsed.
        /// </summary>
        public int LastLeadingSpaceIndex { get; set; } = -1;

        /// <summary>
        /// Index of the last optional zero character, -1 if not parsed.
        /// </summary>
        public int LastLeadingZeroIndex { get; set; } = -1;

        /// <summary>
        /// Sign of the significand, None if not parsed.
        /// </summary>
        public OptionalSign SignificandSign { get; set; } = OptionalSign.None;

        /// <summary>
        /// Index of the prefix indicating a base, -1 if not parsed.
        /// </summary>
        public int BasePrefix { get; set; } = -1;

        /// <summary>
        /// Index of the integer part, -1 if not parsed.
        /// </summary>
        public int FirstIntegerPartIndex { get; set; } = -1;

        /// <summary>
        /// Index of the decimal separator, -1 if not parsed.
        /// </summary>
        public int DecimalSeparatorIndex { get; set; } = -1;

        /// <summary>
        /// Index of the exponent character, -1 if not parsed.
        /// </summary>
        public int ExponentIndex { get; set; } = -1;

        /// <summary>
        /// Index of the last optional zero character, -1 if not parsed.
        /// </summary>
        public OptionalExponent Exponent { get; set; } = OptionalExponent.None;

        /// <summary>
        /// Sign of the exponent, None if not parsed.
        /// </summary>
        public OptionalSign ExponentSign { get; set; } = OptionalSign.None;

        /// <summary>
        /// Index of the exponent part, -1 if not parsed.
        /// </summary>
        public int FirstExponentPartIndex { get; set; } = -1;

        /// <summary>
        /// Index of the first invalid character, -1 if not parsed.
        /// </summary>
        public int FirstInvalidCharacterIndex { get; set; } = -1;

        /// <summary>
        /// Gets the decimal separator character for the current culture.
        /// </summary>
        protected void InitCultureSeparator()
        {
            string CurrentCultureSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            Debug.Assert(CurrentCultureSeparator.Length == 1);
            CultureDecimalSeparator = CurrentCultureSeparator[0];
        }
    }
}
