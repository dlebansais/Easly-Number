﻿namespace EaslyNumber
{
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
            ConvertIntegerToBitField(significandPrecision, out integerField);
            ConvertFractionalToBitField(significandPrecision, integerField.SignificantBits, out fractionalField);
            ConvertExponentToBitField(exponentPrecision, out exponentField);
        }

        /// <summary>
        /// Converts the parsed partition to bit fields.
        /// </summary>
        /// <param name="significandPrecision">The number of bits in the significand.</param>
        /// <param name="integerField">The bit field of the integer part upon return.</param>
        private void ConvertIntegerToBitField(long significandPrecision, out BitField integerField)
        {
            integerField = new BitField();

            if (FirstIntegerPartIndex >= 0)
            {
                string IntegerString = Text.Substring(FirstIntegerPartIndex, LastIntegerPartIndex - FirstIntegerPartIndex);
                long BitIndex = 0;

                do
                {
                    if (BitIndex >= significandPrecision)
                        integerField.DecreasePrecision();

                    IntegerString = DividedByTwo(IntegerString, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit, out bool HasCarry);
                    integerField.SetBit(BitIndex++, HasCarry);
                }
                while (IntegerString != "0");
            }
            else
                integerField.SetZero();
        }

        /// <summary>
        /// Converts the parsed partition to bit fields.
        /// </summary>
        /// <param name="significandPrecision">The number of bits in the significand.</param>
        /// <param name="integerBitIndex">The number of significant bits in the integer field part.</param>
        /// <param name="fractionalField">The bit field of the fractional part upon return.</param>
        private void ConvertFractionalToBitField(long significandPrecision, long integerBitIndex, out BitField fractionalField)
        {
            fractionalField = new BitField();

            if (HasFractionalPart)
            {
                string FractionalString = Text.Substring(FirstFractionalPartIndex, LastFractionalPartIndex - FirstFractionalPartIndex);
                long BitIndex = 0;
                int StartingLength = FractionalString.Length;

#if IGNORE
                bool DebugString = false; // FractionalString == "47856";

                if (DebugString)
                {
                    Debug.Assert(false);
                    Debug.WriteLine(FractionalString);
                }
#endif

                do
                {
                    if (integerBitIndex + BitIndex >= significandPrecision)
                        break;

                    FractionalString = MultipliedByTwo(FractionalString, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit, false);

                    bool HasCarry = FractionalString.Length > StartingLength;
                    fractionalField.SetBit(BitIndex++, HasCarry);

                    if (HasCarry)
                        FractionalString = FractionalString.Substring(1);

#if IGNORE
                    if (DebugString)
                        Debug.WriteLine((HasCarry ? "(1) " : "(0) ") + FractionalString);
#endif
                }
                while (FractionalString != "0");
            }
        }

        /// <summary>
        /// Converts the parsed partition to bit fields.
        /// </summary>
        /// <param name="exponentPrecision">The number of bits in the exponent.</param>
        /// <param name="exponentField">The bit field of the exponent part upon return.</param>
        private void ConvertExponentToBitField(long exponentPrecision, out BitField exponentField)
        {
            exponentField = new BitField();

            if (HasExponentPart)
            {
                string ExponentString = Text.Substring(FirstExponentPartIndex, LastExponentPartIndex - FirstExponentPartIndex);
                long BitIndex = 0;

                do
                {
                    if (BitIndex >= exponentPrecision)
                        exponentField.DecreasePrecision();

                    ExponentString = DividedByTwo(ExponentString, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit, out bool HasCarry);
                    exponentField.SetBit(BitIndex++, HasCarry);
                }
                while (ExponentString != "0");
            }
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
