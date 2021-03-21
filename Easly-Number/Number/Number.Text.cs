namespace EaslyNumber
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text;
    using static EaslyNumber.NativeMethods;

    /// <summary>
    /// Represents numbers with arbitrary precision.
    /// </summary>
    public partial struct Number : IFormattable
    {
        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance.</returns>
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the value of this instance as specified by provider.</returns>
        public string ToString(IFormatProvider provider)
        {
            return ToString("G", provider);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation, using the specified format.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <returns>The string representation of the value of this instance as specified by format.</returns>
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the value of this instance as specified by format and provider.</returns>
        public string ToString(string? format, IFormatProvider? provider)
        {
            if (format == null)
                format = "G";

            if (!DisplayFormat.Parse(format, provider, Precision, out DisplayFormat Format))
            {
                if (format.Length == 1)
                    throw new FormatException("Format specifier was invalid.");
                else
                    return format;
            }

            if (IsNaN)
                return Format.NumberFormatInfo.NaNSymbol;
            else if (IsPositiveInfinity)
                return Format.NumberFormatInfo.PositiveInfinitySymbol;
            else if (IsNegativeInfinity)
                return Format.NumberFormatInfo.NegativeInfinitySymbol;
            else
                return ToString(Format);
        }

        private string ToString(DisplayFormat format)
        {
            Consolidate();

            string Result = string.Empty;

            switch (format.NumericFormat)
            {
                case NumericFormat.Default:
                    Result = ToStringDefaultFormat(format);
                    break;
                case NumericFormat.Exponential:
                    Result = ToStringExponentialFormat(format);
                    break;
                case NumericFormat.FixedPoint:
                    Result = ToStringFixedPointFormat(format);
                    break;
            }

            Debug.Assert(Result.Length > 0);

            return Result;
        }

        private string ToStringDefaultFormat(DisplayFormat displayFormat)
        {
            GetNumberString((ulong)displayFormat.PrecisionSpecifier, out string NumberString, out int Exponent, out string NegativeSign, out bool IsZero);

            if (IsZero)
                return NegativeSign + "0";

            if (Exponent > -5 && Exponent <= displayFormat.PrecisionSpecifier)
                return ToStringDefaultFormatFixedPoint(displayFormat, NumberString, Exponent, NegativeSign);
            else
                return ToStringDefaultFormatScientific(displayFormat, NumberString, Exponent, NegativeSign);
        }

        private string ToStringDefaultFormatFixedPoint(DisplayFormat displayFormat, string numberString, int exponent, string negativeSign)
        {
            int SignificantDigitCount = exponent;

            string IntegerPart;
            if (SignificantDigitCount <= numberString.Length)
            {
                if (SignificantDigitCount > 0)
                {
                    IntegerPart = numberString.Substring(0, SignificantDigitCount);
                    numberString = numberString.Substring(SignificantDigitCount);
                }
                else
                {
                    IntegerPart = "0";

                    while (SignificantDigitCount < 0)
                    {
                        SignificantDigitCount++;
                        numberString = "0" + numberString.Substring(0, numberString.Length - 1);
                    }
                }
            }
            else
            {
                IntegerPart = numberString;
                numberString = string.Empty;
            }

            string Separator = displayFormat.NumberFormatInfo.NumberDecimalSeparator;

            string FractionalPart = numberString;

            while (FractionalPart.Length > 0 && FractionalPart[FractionalPart.Length - 1] == '0')
                FractionalPart = FractionalPart.Substring(0, FractionalPart.Length - 1);

            if (FractionalPart.Length > displayFormat.PrecisionSpecifier)
                FractionalPart = FractionalPart.Substring(0, displayFormat.PrecisionSpecifier);

            if (FractionalPart.Length == 0)
                Separator = string.Empty;

            string Digits = $"{IntegerPart}{FractionalPart}";

            bool IsZero = true;
            for (int i = 0; i < Digits.Length; i++)
                if (Digits[i] != '0')
                {
                    IsZero = false;
                    break;
                }

            if (IsZero)
                negativeSign = string.Empty;

            string Result = $"{negativeSign}{IntegerPart}{Separator}{FractionalPart}";

            return Result;
        }

        private string ToStringDefaultFormatScientific(DisplayFormat displayFormat, string numberString, int exponent, string negativeSign)
        {
            bool IsFirstDigitZero;
            string FirstDigit;
            if (numberString.Length > 0)
            {
                FirstDigit = numberString.Substring(0, 1);
                numberString = numberString.Substring(1);
                IsFirstDigitZero = FirstDigit == "0";
            }
            else
            {
                FirstDigit = "0";
                IsFirstDigitZero = true;
            }

            string Separator = displayFormat.NumberFormatInfo.NumberDecimalSeparator;

            string OtherDigits;

            if (numberString.Length > displayFormat.PrecisionSpecifier)
                OtherDigits = numberString.Substring(0, displayFormat.PrecisionSpecifier);
            else
                OtherDigits = numberString;

            while (OtherDigits.Length > 0 && OtherDigits[OtherDigits.Length - 1] == '0')
                OtherDigits = OtherDigits.Substring(0, OtherDigits.Length - 1);

            if (OtherDigits.Length == 0)
                Separator = string.Empty;

            string ExponentCharacter = displayFormat.IsExponentUpperCase ? "E" : "e";
            string ExponentSign;
            string ExponentString;

            int ExponentDigitCount = Precision <= 24 ? 2 : 3;
            string ExponentDigitFormat = $"D0{ExponentDigitCount}";

            if (IsFirstDigitZero)
            {
                ExponentSign = exponent >= 0 ? "+" : "-";
                ExponentString = (exponent >= 0 ? exponent : -exponent).ToString(ExponentDigitFormat);
            }
            else
            {
                exponent--;

                ExponentSign = exponent >= 0 ? "+" : "-";
                ExponentString = (exponent >= 0 ? exponent : -exponent).ToString(ExponentDigitFormat);
            }

            string Result = $"{negativeSign}{FirstDigit}{Separator}{OtherDigits}{ExponentCharacter}{ExponentSign}{ExponentString}";

            return Result;
        }

        private string ToStringExponentialFormat(DisplayFormat displayFormat)
        {
            GetNumberString((ulong)(displayFormat.PrecisionSpecifier + 1), out string NumberString, out int Exponent, out string NegativeSign, out _);

            bool IsFirstDigitZero;
            string FirstDigit;
            if (NumberString.Length > 0)
            {
                FirstDigit = NumberString.Substring(0, 1);
                NumberString = NumberString.Substring(1);
                IsFirstDigitZero = FirstDigit == "0";
            }
            else
            {
                FirstDigit = "0";
                IsFirstDigitZero = true;
            }

            string Separator = displayFormat.NumberFormatInfo.NumberDecimalSeparator;

            string OtherDigits;

            if (NumberString.Length > displayFormat.PrecisionSpecifier)
                OtherDigits = NumberString.Substring(0, displayFormat.PrecisionSpecifier);
            else
            {
                OtherDigits = NumberString;
                while (OtherDigits.Length < displayFormat.PrecisionSpecifier)
                    OtherDigits += "0";
            }

            if (OtherDigits.Length == 0)
                Separator = string.Empty;

            string ExponentCharacter = displayFormat.IsExponentUpperCase ? "E" : "e";
            string ExponentSign;
            string ExponentString;

            if (IsFirstDigitZero)
            {
                ExponentSign = Exponent >= 0 ? "+" : "-";
                ExponentString = (Exponent >= 0 ? Exponent : -Exponent).ToString("D03");
            }
            else
            {
                Exponent--;

                ExponentSign = Exponent >= 0 ? "+" : "-";
                ExponentString = (Exponent >= 0 ? Exponent : -Exponent).ToString("D03");
            }

            string Result = $"{NegativeSign}{FirstDigit}{Separator}{OtherDigits}{ExponentCharacter}{ExponentSign}{ExponentString}";

            return Result;
        }

        private string ToStringFixedPointFormat(DisplayFormat displayFormat)
        {
            string Format = $"%.{displayFormat.PrecisionSpecifier}RNF";
            int DigitCount = mpfr_snprintf(IntPtr.Zero, 0, Format, ref Proxy.MpfrStruct, IntPtr.Zero);
            int SizeInDigits = DigitCount + 2;

            StringBuilder Data = new StringBuilder(SizeInDigits);

            mpfr_sprintf(Data, Format, ref Proxy.MpfrStruct, IntPtr.Zero);

            string NumberString = Data.ToString();
            NumberString = NumberString.Replace(".", displayFormat.NumberFormatInfo.NumberDecimalSeparator);

            return NumberString;
        }

        private void GetNumberString(ulong precision, out string numberString, out int exponent, out string negativeSign, out bool isZero)
        {
            const int Resultbase = 10;

            ulong SizeInDigits = precision > 0 ? precision : 1UL;

            StringBuilder Data = new StringBuilder((int)(SizeInDigits + 2));
            Rounding StringRounding = Rounding.Nearest;

            mpfr_get_str(Data, out exponent, Resultbase, SizeInDigits, ref Proxy.MpfrStruct, StringRounding);
            string Result = Data.ToString();

            if (Result.Length > 0 && Result[0] == '-')
            {
                negativeSign = "-";
                numberString = Result.Substring(1);
            }
            else
            {
                negativeSign = string.Empty;
                numberString = Result;
            }

            isZero = true;
            for (int i = 0; i < numberString.Length; i++)
                if (numberString[i] != '0')
                {
                    isZero = false;
                    break;
                }
        }
    }
}
