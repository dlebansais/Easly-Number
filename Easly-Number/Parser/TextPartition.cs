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
        /// Initializes a new instance of the <see cref="TextPartition"/> class.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="radix">The radix to use.</param>
        public TextPartition(string text, int radix)
        {
            Text = text;
            Radix = radix;
        }

        /// <summary>
        /// The string to parse.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// The radix for digits.
        /// </summary>
        public int Radix { get; }

        /// <summary>
        /// Index of the last optional space character, -1 if not parsed.
        /// </summary>
        public int LastLeadingSpaceIndex { get; set; } = -1;

        /// <summary>
        /// The beginning of <see cref="Text"/> that can be ignored.
        /// </summary>
        public string DiscardedProlog { get { return LastLeadingSpaceIndex < 0 ? string.Empty : Text.Substring(0, LastLeadingSpaceIndex); } }

        /// <summary>
        /// Index of the last optional zero character, -1 if not parsed.
        /// </summary>
        public int LastLeadingZeroIndex { get; set; } = -1;

        /// <summary>
        /// Sign of the significand, None if not parsed.
        /// </summary>
        public OptionalSign SignificandSign { get; set; } = OptionalSign.None;

        /// <summary>
        /// Index of the prefix indicating a radix, -1 if not parsed.
        /// </summary>
        public int RadixPrefix { get; set; } = -1;

        /// <summary>
        /// Index of the integer part, -1 if not parsed.
        /// </summary>
        public int FirstIntegerPartIndex { get; set; } = -1;

        /// <summary>
        /// Index of the first character after the integer part, -1 if not parsed.
        /// </summary>
        public int LastIntegerPartIndex { get; set; } = -1;

        /// <summary>
        /// The integer part in front of the decimal separator (if any).
        /// </summary>
        public string IntegerPart { get { return FirstIntegerPartIndex < 0 ? string.Empty : Text.Substring(FirstIntegerPartIndex, LastIntegerPartIndex - FirstIntegerPartIndex); } }

        /// <summary>
        /// Index of the decimal separator, -1 if not parsed.
        /// </summary>
        public int DecimalSeparatorIndex { get; set; } = -1;

        /// <summary>
        /// Index of the fractional part, -1 if not parsed.
        /// </summary>
        public int FirstFractionalPartIndex { get { return DecimalSeparatorIndex < 0 ? -1 : DecimalSeparatorIndex + 1; } }

        /// <summary>
        /// Index of the first character after the fractional part, -1 if not parsed.
        /// </summary>
        public int LastFractionalPartIndex { get; set; } = -1;

        /// <summary>
        /// The fractional part after the decimal separator (if any).
        /// </summary>
        public string FractionalPart { get { return FirstFractionalPartIndex < 0 ? string.Empty : Text.Substring(FirstFractionalPartIndex, LastFractionalPartIndex - FirstFractionalPartIndex); } }

        /// <summary>
        /// The optional separator, if any.
        /// </summary>
        public abstract OptionalSeparator Separator { get; }

        /// <summary>
        /// Index of the exponent character, -1 if not parsed.
        /// </summary>
        public int ExponentIndex { get; set; } = -1;

        /// <summary>
        /// Type of the optional exponent character.
        /// </summary>
        public OptionalExponent ExponentCharacter { get; set; } = OptionalExponent.None;

        /// <summary>
        /// Sign of the exponent, None if not parsed.
        /// </summary>
        public OptionalSign ExponentSign { get; set; } = OptionalSign.None;

        /// <summary>
        /// Index of the exponent part, -1 if not parsed.
        /// </summary>
        public int FirstExponentPartIndex { get; set; } = -1;

        /// <summary>
        /// Index of the first character after the exponent part, -1 if not parsed.
        /// </summary>
        public int LastExponentPartIndex { get; set; } = -1;

        /// <summary>
        /// The exponent part (if any) after the exponent character and optional sign.
        /// </summary>
        public string ExponentPart { get { return FirstExponentPartIndex < 0 ? string.Empty : Text.Substring(FirstExponentPartIndex, LastExponentPartIndex - FirstExponentPartIndex); } }

        /// <summary>
        /// Index of the first invalid character, -1 if not parsed.
        /// </summary>
        public int FirstInvalidCharacterIndex { get; set; } = -1;

        /// <summary>
        /// Index of the first invalid character, -1 if not parsed.
        /// </summary>
        public bool IsValid { get { return FirstInvalidCharacterIndex < 0; } }

        /// <summary>
        /// The remaining part of the string that is not parsed in the number.
        /// </summary>
        public string InvalidPart { get { return FirstInvalidCharacterIndex < 0 ? string.Empty : Text.Substring(FirstInvalidCharacterIndex); } }

        /// <summary>
        /// The binary data corresponding to the integer part.
        /// </summary>
        public BitField IntegerField { get; private set; } = new BitField();

        /// <summary>
        /// The binary data corresponding to the fractional part.
        /// </summary>
        public BitField FractionalField { get; private set; } = new BitField();

        /// <summary>
        /// The binary data corresponding to the exponent part.
        /// </summary>
        public BitField ExponentField { get; private set; } = new BitField();

        /// <summary>
        /// The parser current state.
        /// </summary>
        protected ParsingState State { get; set; } = ParsingState.Init;

        /// <summary>
        /// The decimal separator character for the current culture.
        /// </summary>
        protected char CultureDecimalSeparator { get; set; }

        /// <summary>
        /// Parses a new character.
        /// </summary>
        /// <param name="index">The position of the character to parse in <see cref="Text"/>.</param>
        public abstract void Parse(int index);

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
