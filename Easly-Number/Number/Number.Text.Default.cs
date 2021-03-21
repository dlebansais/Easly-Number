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

        private string ToStringDefaultFormatScientific(DisplayFormat displayFormat, string numberString, int exponent, string negativeSign)
        {
            ScientificFirstDigit(ref numberString, out bool IsFirstDigitZero, out string FirstDigit);
            ScientificOtherDigits(displayFormat, ref numberString, out string Separator, out string OtherDigits);
            ScientificExponent(displayFormat, IsFirstDigitZero, exponent, out string ExponentCharacter, out string ExponentSign, out string ExponentString);

            string Result = $"{negativeSign}{FirstDigit}{Separator}{OtherDigits}{ExponentCharacter}{ExponentSign}{ExponentString}";

            return Result;
        }

        private void ScientificFirstDigit(ref string numberString, out bool isFirstDigitZero, out string firstDigit)
        {
            if (numberString.Length > 0)
            {
                firstDigit = numberString.Substring(0, 1);
                numberString = numberString.Substring(1);
                isFirstDigitZero = firstDigit == "0";
            }
            else
            {
                firstDigit = "0";
                isFirstDigitZero = true;
            }
        }

        private void ScientificOtherDigits(DisplayFormat displayFormat, ref string numberString, out string separator, out string otherDigits)
        {
            separator = displayFormat.NumberFormatInfo.NumberDecimalSeparator;

            if (numberString.Length > displayFormat.PrecisionSpecifier)
                otherDigits = numberString.Substring(0, displayFormat.PrecisionSpecifier);
            else
                otherDigits = numberString;

            while (otherDigits.Length > 0 && otherDigits[otherDigits.Length - 1] == '0')
                otherDigits = otherDigits.Substring(0, otherDigits.Length - 1);

            if (otherDigits.Length == 0)
                separator = string.Empty;
        }

        private void ScientificExponent(DisplayFormat displayFormat, bool isFirstDigitZero, int exponent, out string exponentCharacter, out string exponentSign, out string exponentString)
        {
            exponentCharacter = displayFormat.IsExponentUpperCase ? "E" : "e";

            int ExponentDigitCount = Precision <= 24 ? 2 : 3;
            string ExponentDigitFormat = $"D0{ExponentDigitCount}";

            if (isFirstDigitZero)
            {
                exponentSign = exponent >= 0 ? "+" : "-";
                exponentString = (exponent >= 0 ? exponent : -exponent).ToString(ExponentDigitFormat);
            }
            else
            {
                exponent--;

                exponentSign = exponent >= 0 ? "+" : "-";
                exponentString = (exponent >= 0 ? exponent : -exponent).ToString(ExponentDigitFormat);
            }
        }
    }
}
