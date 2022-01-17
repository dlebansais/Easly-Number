namespace EaslyNumber
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text;
    using Interop.Mpfr;
    using static Interop.Mpfr.NativeMethods;

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
            if (format is null)
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

        private string ToStringExponentialFormat(DisplayFormat displayFormat)
        {
            GetNumberString((ulong)(displayFormat.PrecisionSpecifier + 1), out string NumberString, out int Exponent, out string NegativeSign, out _);

            Debug.Assert(NumberString.Length > 0);

            string FirstDigit = NumberString.Substring(0, 1);
            NumberString = NumberString.Substring(1);
            bool IsFirstDigitZero = FirstDigit == "0";

            string Separator = displayFormat.NumberFormatInfo.NumberDecimalSeparator;

            string OtherDigits = NumberString;

            if (OtherDigits.Length == 0)
                Separator = string.Empty;

            string ExponentCharacter = displayFormat.IsExponentUpperCase ? "E" : "e";
            string ExponentSign;
            string ExponentString;

            if (IsFirstDigitZero)
            {
                Debug.Assert(Exponent == 0);
                ExponentSign = "+";
                ExponentString = Exponent.ToString("D03");
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

            Debug.Assert(precision > 0);
            ulong SizeInDigits = precision;

            StringBuilder Data = new StringBuilder((int)(SizeInDigits + 2));
            mpfr_rnd_t StringRounding = mpfr_rnd_t.MPFR_RNDN;

            mpfr_get_str(Data, out exponent, Resultbase, SizeInDigits, ref Proxy.MpfrStruct, StringRounding);
            string Result = Data.ToString();

            Debug.Assert(Result.Length > 0);

            if (Result[0] == '-')
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
