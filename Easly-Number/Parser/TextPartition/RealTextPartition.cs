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
            {
                ExponentString = Text.Substring(FirstExponentPartIndex, LastExponentPartIndex - FirstExponentPartIndex);
                while (ExponentString.Length > 1 && ExponentString[0] == '0')
                    ExponentString = ExponentString.Substring(1);
            }
            else
                ExponentString = "0";

            string PowerOfTwo;

            if (Text != "0")
            {
                Normalize(ref IntegerString, ref FractionalString, ref ExponentString, ExponentSign == OptionalSign.Negative);
                FindBestPowerOfTwo(ref IntegerString, ref FractionalString, ref ExponentString, out PowerOfTwo);
            }
            else
                PowerOfTwo = "0";

            exponentField = new BitField();

            if (PowerOfTwo != "0")
                ConvertExponentToBitField(PowerOfTwo, exponentPrecision, ref exponentField);
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
        /// Changes the string representation of a number to ensure that either the exponent part, the integer part or the fractional part is equal to zero.
        /// </summary>
        /// <param name="integerString">The integer part of the significand.</param>
        /// <param name="fractionalString">The fractional part of the significand.</param>
        /// <param name="exponentString">The exponent part of the significand.</param>
        /// <param name="isExponentNegative">True if the exponent is negative.</param>
        private static void Normalize(ref string integerString, ref string fractionalString, ref string exponentString, bool isExponentNegative)
        {
            if (isExponentNegative)
                NormalizeIncreaseExponent(ref integerString, ref fractionalString, ref exponentString);
            else
                NormalizeDecreaseExponent(ref integerString, ref fractionalString, ref exponentString);
        }

        /// <summary>
        /// Changes the string representation of a number to ensure that either the exponent part, the integer part or the fractional part is equal to zero by moving digits from the integer part to the fractional part.
        /// </summary>
        /// <param name="integerString">The integer part of the significand.</param>
        /// <param name="fractionalString">The fractional part of the significand.</param>
        /// <param name="exponentString">The exponent part of the significand.</param>
        private static void NormalizeIncreaseExponent(ref string integerString, ref string fractionalString, ref string exponentString)
        {
            if (integerString.Length == 0)
                integerString = "0";
            if (fractionalString.Length == 0)
                fractionalString = "0";
            if (exponentString.Length == 0)
                exponentString = "0";

            while (exponentString != "0" && integerString != "0" && fractionalString != "0")
                IncreaseExponent(ref integerString, ref fractionalString, ref exponentString);

            Debug.Assert(integerString.Length >= 1);
            Debug.Assert(fractionalString.Length >= 1);
            Debug.Assert(exponentString.Length >= 1);
            Debug.Assert(exponentString == "0" || integerString == "0" || fractionalString == "0");
        }

        /// <summary>
        /// Changes the string representation of a number by moving one digit from the integer part to the fractional part and increasing the exponent.
        /// </summary>
        /// <param name="integerString">The integer part of the significand.</param>
        /// <param name="fractionalString">The fractional part of the significand.</param>
        /// <param name="exponentString">The exponent part of the significand.</param>
        private static void IncreaseExponent(ref string integerString, ref string fractionalString, ref string exponentString)
        {
            Debug.Assert(integerString.Length > 0);
            Debug.Assert(fractionalString.Length > 0);
            Debug.Assert(fractionalString.Length > 0 && exponentString != "0");

            char LastDigit;

            if (integerString != "0")
            {
                LastDigit = integerString[integerString.Length - 1];

                if (integerString.Length > 1)
                    integerString = integerString.Substring(0, integerString.Length - 1);
                else
                    integerString = "0";
            }
            else
                LastDigit = '0';

            if (fractionalString == "0")
                fractionalString = LastDigit.ToString();
            else
                fractionalString = $"{LastDigit}{fractionalString}";

            exponentString = Incremented(exponentString, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
        }

        /// <summary>
        /// Changes the string representation of a number to ensure that either the exponent part, the integer part or the fractional part is equal to zero by moving digits from the fractional part to the integer part.
        /// </summary>
        /// <param name="integerString">The integer part of the significand.</param>
        /// <param name="fractionalString">The fractional part of the significand.</param>
        /// <param name="exponentString">The exponent part of the significand.</param>
        private static void NormalizeDecreaseExponent(ref string integerString, ref string fractionalString, ref string exponentString)
        {
            if (integerString.Length == 0)
                integerString = "0";
            if (fractionalString.Length == 0)
                fractionalString = "0";
            if (exponentString.Length == 0)
                exponentString = "0";

            while (exponentString != "0" && integerString != "0" && fractionalString != "0")
                DecreaseExponent(ref integerString, ref fractionalString, ref exponentString);

            Debug.Assert(integerString.Length >= 1);
            Debug.Assert(fractionalString.Length >= 1);
            Debug.Assert(exponentString.Length >= 1);
            Debug.Assert(exponentString == "0" || integerString == "0" || fractionalString == "0");
        }

        /// <summary>
        /// Changes the string representation of a number by moving one digit from the fractional part to the integer part and increasing the exponent.
        /// </summary>
        /// <param name="integerString">The integer part of the significand.</param>
        /// <param name="fractionalString">The fractional part of the significand.</param>
        /// <param name="exponentString">The exponent part of the significand.</param>
        private static void DecreaseExponent(ref string integerString, ref string fractionalString, ref string exponentString)
        {
            Debug.Assert(integerString.Length > 0);
            Debug.Assert(fractionalString.Length > 0);
            Debug.Assert(fractionalString.Length > 0 && exponentString != "0");

            char FirstDigit;

            if (fractionalString != "0")
            {
                FirstDigit = fractionalString[0];

                if (fractionalString.Length > 1)
                    fractionalString = fractionalString.Substring(1);
                else
                    fractionalString = "0";
            }
            else
                FirstDigit = '0';

            if (integerString == "0")
                integerString = FirstDigit.ToString();
            else
                integerString = $"{integerString}{FirstDigit}";

            exponentString = Decremented(exponentString, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
        }

        private static void FindBestPowerOfTwo(ref string integerString, ref string fractionalString, ref string exponentString, out string powerOfTwo)
        {
            if (exponentString == "0")
            {
                powerOfTwo = "0";
                return;
            }

            if (fractionalString == "0")
            {
                int Base2Digits;

                if (int.TryParse(exponentString, out int ExponentValue))
                {
                    double Base10Digits = integerString.Length + ExponentValue;
                    Base2Digits = (int)(Base10Digits * Math.Log(10) / Math.Log(2));
                }
                else
                {
                    Base2Digits = 0; // TODO
                    ExponentValue = 0;
                }

                string RootBase2 = "1";

                for (int i = 0; i < Base2Digits; i++)
                    RootBase2 = MultipliedByTwo(RootBase2, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit, false);

                while (RootBase2.Length > integerString.Length + ExponentValue)
                {
                    RootBase2 = DividedByTwo(RootBase2, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit, out _);
                    Base2Digits--;
                }

                string RootBase2Short = RootBase2.Substring(0, integerString.Length);

                while (string.Compare(RootBase2Short, integerString, StringComparison.InvariantCulture) > 0)
                {
                    RootBase2 = DividedByTwo(RootBase2, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit, out _);
                    Base2Digits--;

                    RootBase2Short = RootBase2.Substring(0, integerString.Length);
                }

                powerOfTwo = Base2Digits.ToString(CultureInfo.InvariantCulture);
                return;
            }

            if (integerString == "0")
            {
                int Base2Digits;

                if (int.TryParse(exponentString, out int ExponentValue))
                {
                    double Base10Digits = fractionalString.Length + ExponentValue;
                    Base2Digits = (int)(Base10Digits * Math.Log(10) / Math.Log(2));
                }
                else
                {
                    Base2Digits = 0; // TODO
                    ExponentValue = 0;
                }

                string RootBase2 = "1";

                for (int i = 0; i < Base2Digits; i++)
                    RootBase2 = MultipliedByTwo(RootBase2, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit, false);

                while (RootBase2.Length > fractionalString.Length + ExponentValue)
                {
                    RootBase2 = DividedByTwo(RootBase2, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit, out _);
                    Base2Digits--;
                }

                string RootBase2Short = RootBase2.Substring(0, fractionalString.Length);

                while (string.Compare(RootBase2Short, fractionalString, StringComparison.InvariantCulture) > 0)
                {
                    RootBase2 = DividedByTwo(RootBase2, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit, out _);
                    Base2Digits--;

                    RootBase2Short = RootBase2.Substring(0, fractionalString.Length);
                }

                powerOfTwo = Base2Digits.ToString(CultureInfo.InvariantCulture);
                return;
            }

            powerOfTwo = "0";
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
