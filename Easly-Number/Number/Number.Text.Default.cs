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
            FixedPointIntegerPart(ref numberString, exponent, out string IntegerPart);
            FixedPointFractionalPart(displayFormat, ref numberString, exponent, out string Separator, out string FractionalPart);

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

        private void FixedPointFractionalPart(DisplayFormat displayFormat, ref string numberString, int exponent, out string separator, out string fractionalPart)
        {
            separator = displayFormat.NumberFormatInfo.NumberDecimalSeparator;
            fractionalPart = numberString;

            while (fractionalPart.Length > 0 && fractionalPart[fractionalPart.Length - 1] == '0')
                fractionalPart = fractionalPart.Substring(0, fractionalPart.Length - 1);

            if (fractionalPart.Length > displayFormat.PrecisionSpecifier)
                fractionalPart = fractionalPart.Substring(0, displayFormat.PrecisionSpecifier);

            if (fractionalPart.Length == 0)
                separator = string.Empty;
        }

        private void FixedPointIntegerPart(ref string numberString, int exponent, out string integerPart)
        {
            int SignificantDigitCount = exponent;

            if (SignificantDigitCount <= numberString.Length)
            {
                if (SignificantDigitCount > 0)
                {
                    integerPart = numberString.Substring(0, SignificantDigitCount);
                    numberString = numberString.Substring(SignificantDigitCount);
                }
                else
                {
                    integerPart = "0";

                    while (SignificantDigitCount < 0)
                    {
                        SignificantDigitCount++;
                        numberString = "0" + numberString.Substring(0, numberString.Length - 1);
                    }
                }
            }
            else
            {
                integerPart = numberString;
                numberString = string.Empty;
            }
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
    }
}
