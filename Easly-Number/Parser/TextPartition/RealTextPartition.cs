namespace EaslyNumber
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    /// The partition of a string into different components of a real number.
    /// </summary>
    internal partial class RealTextPartition : NumberTextPartition
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="RealTextPartition"/> class.
        /// <param name="text">The string to parse.</param>
        /// </summary>
        public RealTextPartition(string text)
            : base(text)
        {
        }

        /// <summary>
        /// Gets the decimal separator character for the current culture.
        /// </summary>
        protected void InitCultureSeparator()
        {
            string CurrentCultureSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            Debug.Assert(CurrentCultureSeparator.Length == 1);
            CultureDecimalSeparator = CurrentCultureSeparator[0];
        }
        #endregion

        #region Properties
        /// <summary>
        /// The optional separator.
        /// </summary>
        public OptionalSeparator Separator
        {
            get
            {
                if (DecimalSeparatorIndex < 0)
                    return OptionalSeparator.None;
                else if (Text[DecimalSeparatorIndex] != CultureDecimalSeparator)
                    return OptionalSeparator.Normalized;
                else
                    return OptionalSeparator.CultureSpecific;
            }
        }

        /// <summary>
        /// True if the partition includes an integer part.
        /// </summary>
        public bool HasIntegerPart { get { return FirstIntegerPartIndex >= 0 && FirstIntegerPartIndex < LastIntegerPartIndex; } }

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
        /// True if the parsed number is zero.
        /// </summary>
        public bool IsZero
        {
            get
            {
                if (FirstIntegerPartIndex >= 0)
                {
                    for (int i = FirstIntegerPartIndex; i < LastIntegerPartIndex; i++)
                        if (Text[i] != '0')
                            return false;
                }

                if (HasFractionalPart)
                {
                    for (int i = FirstFractionalPartIndex; i < LastFractionalPartIndex; i++)
                        if (Text[i] != '0')
                            return false;
                }

                return true;
            }
        }

        /// <summary>
        /// The decimal separator character for the current culture.
        /// </summary>
        protected char CultureDecimalSeparator { get; set; }

        /// <summary>
        /// Index to use for partition comparison.
        /// </summary>
        public override int ComparisonIndex
        {
            get { return FirstInvalidCharacterIndex < 0 ? Text.Length : FirstInvalidCharacterIndex; }
        }
        #endregion

        #region Bit Field
        /// <summary>
        /// Converts the parsed partition to bit fields.
        /// </summary>
        /// <param name="significandPrecision">The number of bits in the significand.</param>
        /// <param name="exponentPrecision">The number of bits in the exponent.</param>
        /// <param name="integerField">The bit field of the integer part upon return.</param>
        /// <param name="fractionalField">The bit field of the fractional part upon return.</param>
        /// <param name="exponentField">The bit field of the exponent part upon return.</param>
        public virtual void ConvertToBitField(long significandPrecision, long exponentPrecision, out BitField integerField, out BitField fractionalField, out BitField exponentField)
        {
            string IntegerString;
            string FractionalString;
            string ExponentString;

            if (HasIntegerPart)
                IntegerString = Text.Substring(FirstIntegerPartIndex, LastIntegerPartIndex - FirstIntegerPartIndex);
            else
                IntegerString = "0";

            if (HasFractionalPart)
                FractionalString = Text.Substring(FirstFractionalPartIndex, LastFractionalPartIndex - FirstFractionalPartIndex);
            else
                FractionalString = "0";

            if (HasExponentPart)
                ExponentString = Text.Substring(FirstExponentPartIndex, LastExponentPartIndex - FirstExponentPartIndex);
            else
                ExponentString = "0";

            Normalize(ref IntegerString, ref FractionalString, ref ExponentString, ExponentSign == OptionalSign.Negative);
            FindBestPowerOfTwo(ExponentString, out ExponentString);

            exponentField = new BitField();

            if (ExponentString != "0")
                ConvertExponentToBitField(ExponentString, exponentPrecision, ref exponentField);
            else
                exponentField.SetZero();

            integerField = new BitField();
            fractionalField = new BitField();

            if (IntegerString != "0")
                ConvertIntegerToBitField(IntegerString, significandPrecision, ref integerField);
            else
                integerField.SetZero();

            if (FractionalString != "0")
                ConvertFractionalToBitField(FractionalString, significandPrecision, integerField.SignificantBits, ref fractionalField);
            else
                fractionalField.SetZero();
        }

        /// <summary>
        /// Changes the string representation of a number to ensure the integer part is zero and the fractional part does not start with a zero.
        /// </summary>
        /// <param name="integerString">The integer part of the significand.</param>
        /// <param name="fractionalString">The fractional part of the significand.</param>
        /// <param name="exponentString">The exponent part of the significand.</param>
        /// <param name="isExponentNegative">True if the exponent is negative.</param>
        private static void Normalize(ref string integerString, ref string fractionalString, ref string exponentString, bool isExponentNegative)
        {
            if (integerString.Length > 1 || (integerString.Length == 1 && integerString[0] != '0'))
                NormalizeIncreaseExponent(ref integerString, ref fractionalString, ref exponentString, isExponentNegative);
            else if (fractionalString[0] == '0')
                NormalizeDecreaseExponent(ref fractionalString, ref exponentString, isExponentNegative);
        }

        /// <summary>
        /// Changes the string representation of a number to ensure the integer part is zero by moving digits from the integer part to the fractional part.
        /// </summary>
        /// <param name="integerString">The integer part of the significand.</param>
        /// <param name="fractionalString">The fractional part of the significand.</param>
        /// <param name="exponentString">The exponent part of the significand.</param>
        /// <param name="isExponentNegative">True if the exponent is negative.</param>
        private static void NormalizeIncreaseExponent(ref string integerString, ref string fractionalString, ref string exponentString, bool isExponentNegative)
        {
            while (integerString.Length > 1)
                IncreaseExponents(ref integerString, ref fractionalString, ref exponentString, isExponentNegative);

            Debug.Assert(integerString.Length == 1);

            if (integerString != "0")
            {
                IncreaseExponents(ref integerString, ref fractionalString, ref exponentString, isExponentNegative);
                Debug.Assert(integerString.Length == 0);

                integerString = "0";
            }
        }

        /// <summary>
        /// Changes the string representation of a number to move one digit to the fractional, increasing the exponent.
        /// </summary>
        /// <param name="integerString">The integer part of the significand.</param>
        /// <param name="fractionalString">The fractional part of the significand.</param>
        /// <param name="exponentString">The exponent part of the significand.</param>
        /// <param name="isExponentNegative">True if the exponent is negative.</param>
        private static void IncreaseExponents(ref string integerString, ref string fractionalString, ref string exponentString, bool isExponentNegative)
        {
            char LastDigit = integerString[integerString.Length - 1];
            integerString = integerString.Substring(0, integerString.Length - 1);
            fractionalString = $"{LastDigit}{fractionalString}";

            if (isExponentNegative)
            {
                if (exponentString == "0")
                {
                    exponentString = "1";
                    isExponentNegative = false;
                }
                else
                    exponentString = Decremented(exponentString, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            }
            else
                exponentString = Incremented(exponentString, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
        }

        /// <summary>
        /// Changes the string representation of a number to ensure the fractional part does not start with zero by removing digits from the fractional part.
        /// </summary>
        /// <param name="fractionalString">The fractional part of the significand.</param>
        /// <param name="exponentString">The exponent part of the significand.</param>
        /// <param name="isExponentNegative">True if the exponent is negative.</param>
        private static void NormalizeDecreaseExponent(ref string fractionalString, ref string exponentString, bool isExponentNegative)
        {
            while (fractionalString.Length > 1 && fractionalString[0] == '0')
                DecreaseExponents(ref fractionalString, ref exponentString, isExponentNegative);

            Debug.Assert(fractionalString.Length >= 1);
            Debug.Assert(fractionalString[0] != '0');
        }

        /// <summary>
        /// Changes the string representation of a number to remove the first digit of the fractional part, decreasing the exponent.
        /// </summary>
        /// <param name="fractionalString">The fractional part of the significand.</param>
        /// <param name="exponentString">The exponent part of the significand.</param>
        /// <param name="isExponentNegative">True if the exponent is negative.</param>
        private static void DecreaseExponents(ref string fractionalString, ref string exponentString, bool isExponentNegative)
        {
            Debug.Assert(fractionalString.Length > 1 && fractionalString[0] == '0');

            fractionalString = fractionalString.Substring(1);

            if (isExponentNegative)
                exponentString = Incremented(exponentString, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            else
            {
                if (exponentString == "0")
                {
                    exponentString = "1";
                    isExponentNegative = true;
                }
                else
                    exponentString = Decremented(exponentString, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            }
        }

        private static void FindBestPowerOfTwo(string exponentString, out string powerOfTwo)
        {
            if (exponentString.Length < 15)
            {
                if (double.TryParse(exponentString, out double ExponentValue))
                {
                    ExponentValue *= Math.Log(10) / Math.Log(2);
                    powerOfTwo = ((ulong)ExponentValue).ToString(CultureInfo.CurrentCulture);
                    return;
                }
            }

            // TODO: multiply by Log(10) / Log(2).
            powerOfTwo = string.Empty;
        }

        private static bool PowerOfTwoIsGreater(ulong powerOfTwo, string exponentString)
        {
            string PowerString = PowerOfTwoToString(powerOfTwo);

            return (PowerString.Length > exponentString.Length) || (PowerString.Length == exponentString.Length && string.Compare(PowerString, exponentString, StringComparison.InvariantCulture) > 0);
        }

        private static bool PowerOfTwoIsLower(ulong powerOfTwo, string exponentString)
        {
            string PowerString = PowerOfTwoToString(powerOfTwo);

            return (PowerString.Length < exponentString.Length) || (PowerString.Length == exponentString.Length && string.Compare(PowerString, exponentString, StringComparison.InvariantCulture) < 0);
        }

        private static string PowerOfTwoToString(ulong powerOfTwo)
        {
            string Result = "1";

            while (powerOfTwo > 0)
            {
                Result = MultipliedByTwo(Result, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit, false);
                powerOfTwo--;
            }

            return Result;
        }

        /// <summary>
        /// Converts the parsed partition to bit fields.
        /// </summary>
        /// <param name="integerString">The string representing the integer part of the significand.</param>
        /// <param name="significandPrecision">The number of bits in the significand.</param>
        /// <param name="integerField">The bit field of the integer part upon return.</param>
        private static void ConvertIntegerToBitField(string integerString, long significandPrecision, ref BitField integerField)
        {
            long BitIndex = 0;

            do
            {
                if (BitIndex >= significandPrecision)
                    integerField.DecreasePrecision();

                integerString = DividedByTwo(integerString, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit, out bool HasCarry);
                integerField.SetBit(BitIndex++, HasCarry);
            }
            while (integerString != "0");
        }

        /// <summary>
        /// Converts the parsed partition to bit fields.
        /// </summary>
        /// <param name="fractionalString">The string representing the fractional part of the significand.</param>
        /// <param name="significandPrecision">The number of bits in the significand.</param>
        /// <param name="integerBitIndex">The number of significant bits in the integer field part.</param>
        /// <param name="fractionalField">The bit field of the fractional part upon return.</param>
        private static void ConvertFractionalToBitField(string fractionalString, long significandPrecision, long integerBitIndex, ref BitField fractionalField)
        {
            long BitIndex = 0;
            int StartingLength = fractionalString.Length;
            do
            {
                if (integerBitIndex + BitIndex >= significandPrecision)
                    break;

                fractionalString = MultipliedByTwo(fractionalString, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit, false);

                bool HasCarry = fractionalString.Length > StartingLength;
                fractionalField.SetBit(BitIndex++, HasCarry);

                if (HasCarry)
                    fractionalString = fractionalString.Substring(1);
            }
            while (fractionalString != "0");
        }

        /// <summary>
        /// Converts the parsed partition to bit fields.
        /// </summary>
        /// <param name="exponentString">The string representing the exponent part.</param>
        /// <param name="exponentPrecision">The number of bits in the exponent.</param>
        /// <param name="exponentField">The bit field of the exponent part upon return.</param>
        private static void ConvertExponentToBitField(string exponentString, long exponentPrecision, ref BitField exponentField)
        {
            long BitIndex = 0;

            do
            {
                if (BitIndex >= exponentPrecision)
                    exponentField.DecreasePrecision();

                exponentString = DividedByTwo(exponentString, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit, out bool HasCarry);
                exponentField.SetBit(BitIndex++, HasCarry);
            }
            while (exponentString != "0");
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
            {
                for (int i = DigitIndex + 1; i < text.Length; i++)
                    Result += "0";
            }
            else
            {
                while (Result.Length > 1 && Result[Result.Length - 1] == '0')
                    Result = Result.Substring(0, Result.Length - 1);
            }

            return Result;
        }
        #endregion
    }
}
