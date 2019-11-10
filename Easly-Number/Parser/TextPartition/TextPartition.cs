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
        /// True if the partition includes a suffix part.
        /// </summary>
        public bool HasRadixPrefix { get { return RadixPrefix >= 0; } }

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
        /// True if the partition includes a fractional part.
        /// </summary>
        public bool HasFractionalPart { get { return FirstFractionalPartIndex >= 0 && FirstFractionalPartIndex < LastFractionalPartIndex; } }

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
        /// True if the partition includes a fractional part.
        /// </summary>
        public bool HasExponentPart { get { return FirstExponentPartIndex >= 0 && FirstExponentPartIndex < LastExponentPartIndex; } }

        /// <summary>
        /// The exponent part (if any) after the exponent character and optional sign.
        /// </summary>
        public string ExponentPart { get { return FirstExponentPartIndex < 0 ? string.Empty : Text.Substring(FirstExponentPartIndex, LastExponentPartIndex - FirstExponentPartIndex); } }

        /// <summary>
        /// True if the partition includes a suffix part.
        /// </summary>
        public bool HasRadixSuffix { get { return RadixSuffix >= 0; } }

        /// <summary>
        /// Index of the suffix indicating a radix, -1 if not parsed.
        /// </summary>
        public int RadixSuffix { get; set; } = -1;

        /// <summary>
        /// Index of the first invalid character, -1 if not parsed.
        /// </summary>
        public int FirstInvalidCharacterIndex { get; set; } = -1;

        /// <summary>
        /// True if the partition represents a valid number.
        /// </summary>
        public bool IsValid { get { return Text.Length > (LastLeadingSpaceIndex + 1) && FirstInvalidCharacterIndex < 0; } }

        /// <summary>
        /// True if the partition represents a valid number, possibly followed by an invalid part.
        /// </summary>
        public bool IsPartiallyValid { get { return Text.Length > 0 && FirstInvalidCharacterIndex != 0; } }

        /// <summary>
        /// The remaining part of the string that is not parsed in the number.
        /// </summary>
        public string InvalidPart { get { return FirstInvalidCharacterIndex < 0 ? string.Empty : Text.Substring(FirstInvalidCharacterIndex); } }

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
        /// Index to use for partition comparison.
        /// </summary>
        public abstract int ComparisonIndex { get; }

        /// <summary>
        /// Converts the parsed partition to bit fields.
        /// </summary>
        /// <param name="significandPrecision">The number of bits in the significand.</param>
        /// <param name="exponentPrecision">The number of bits in the exponent.</param>
        /// <param name="integerField">The bit field of the integer part upon return.</param>
        /// <param name="fractionalField">The bit field of the fractional part upon return.</param>
        /// <param name="exponentField">The bit field of the exponent part upon return.</param>
        public abstract void ConvertToBitField(long significandPrecision, long exponentPrecision, out BitField integerField, out BitField fractionalField, out BitField exponentField);

        /// <summary>
        /// Gets the decimal separator character for the current culture.
        /// </summary>
        protected void InitCultureSeparator()
        {
            string CurrentCultureSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            Debug.Assert(CurrentCultureSeparator.Length == 1);
            CultureDecimalSeparator = CurrentCultureSeparator[0];
        }

        /// <summary>
        /// Delegate type of a method that validates a digit.
        /// </summary>
        /// <param name="digit">The digit to validate.</param>
        /// <param name="value">The digit value, if valid.</param>
        /// <returns>True if valid; Otherwise, false.</returns>
        public delegate bool IsValidDigitHandler(char digit, out int value);

        /// <summary>
        /// Delegate type of a method that validates a digit.
        /// </summary>
        /// <param name="value">The value to turn convert to a digit.</param>
        /// <returns>The digit corresponding to <paramref name="value"/>.</returns>
        public delegate char ToDigitHandler(int value);

        /// <summary>
        /// Returns the input number divided by two.
        /// </summary>
        /// <param name="text">The number to divide.</param>
        /// <param name="radix">The radix to use.</param>
        /// <param name="validityHandler">The handler to use to validate digits.</param>
        /// <param name="digitHandler">The handler to use to convert to digits.</param>
        /// <param name="hasCarry">True upon return if <paramref name="text"/> is odd.</param>
        internal static string DividedByTwo(string text, int radix, IsValidDigitHandler validityHandler, ToDigitHandler digitHandler, out bool hasCarry)
        {
            string Result = string.Empty;
            int Carry = 0;

            for (int i = 0; i < text.Length; i++)
            {
                bool IsValid = validityHandler(text[i], out int Value);
                Debug.Assert(IsValid);

                Value += Carry;
                char Digit = digitHandler(Value / 2);

                if (Digit != '0' || i > 0 || text.Length == 1)
                    Result += Digit;

                Carry = Value % 2 != 0 ? radix : 0;
            }

            hasCarry = Carry != 0;

            return Result;
        }

        /// <summary>
        /// Returns the input number multiplied by two, with an optional carry to add.
        /// </summary>
        /// <param name="text">The number to multiply.</param>
        /// <param name="radix">The radix to use.</param>
        /// <param name="validityHandler">The handler to use to validate digits.</param>
        /// <param name="digitHandler">The handler to use to convert to digits.</param>
        /// <param name="addCarry">True if a carry should be added.</param>
        internal static string MultipliedByTwo(string text, int radix, IsValidDigitHandler validityHandler, ToDigitHandler digitHandler, bool addCarry)
        {
            string Result = string.Empty;
            int Carry = addCarry ? 1 : 0;

            for (int i = 0; i < text.Length; i++)
            {
                bool IsValid = validityHandler(text[text.Length - 1 - i], out int Value);
                Debug.Assert(IsValid);

                Value = (Value * 2) + Carry;
                if (Value >= radix)
                {
                    Value -= radix;
                    Carry = 1;
                }
                else
                    Carry = 0;

                Result = digitHandler(Value) + Result;
            }

            if (Carry > 0)
                Result = digitHandler(Carry) + Result;

            return Result;
        }

        /// <summary>
        /// Returns the input number rounded to the nearest number ending with digit 0.
        /// If exactly in the middle, round to lower.
        /// </summary>
        /// <param name="text">The number to multiply.</param>
        /// <param name="radix">The radix to use.</param>
        /// <param name="validityHandler">The handler to use to validate digits.</param>
        /// <param name="digitHandler">The handler to use to convert to digits.</param>
        /// <param name="includeEndingZeroes">True to add ending zeroes.</param>
        internal static string RoundedToNearest(string text, int radix, IsValidDigitHandler validityHandler, ToDigitHandler digitHandler, bool includeEndingZeroes)
        {
            Debug.Assert(text.Length > 0);

            int DigitIndex = text.Length;
            int Value;

            --DigitIndex;
            bool IsValid = validityHandler(text[DigitIndex], out Value);
            Debug.Assert(IsValid);

            bool RoundDown = Value < (radix / 2);
            bool IsMiddle = Value == (radix / 2);

            while (Value == radix - 1 && DigitIndex > 0)
            {
                --DigitIndex;
                IsValid = validityHandler(text[DigitIndex], out Value);
                Debug.Assert(IsValid);
            }

            string Result;

            if (Value == radix - 1)
            {
                Debug.Assert(DigitIndex == 0);
                Result = "1";
            }
            else if (RoundDown)
            {
                Result = text.Substring(0, DigitIndex);
            }
            else if (IsMiddle)
            {
                Result = text;
            }
            else
            {
                char RoundedDigit = digitHandler(Value + 1);

                Result = text.Substring(0, DigitIndex);
                Result += RoundedDigit;
                DigitIndex++;
            }

            if (includeEndingZeroes)
                for (int i = DigitIndex + 1; i < text.Length; i++)
                    Result += "0";

            return Result;
        }
    }
}
