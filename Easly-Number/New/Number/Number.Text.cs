namespace EaslyNumber2
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text;
    using static EaslyNumber2.NativeMethods;

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
            Consolidate();

            if (!DisplayFormat.Parse(format, provider, out DisplayFormat Format))
                throw new FormatException("Parameter format is invalid");

            string? Result = null;

            if (IsNaN)
                Result = Format.NumberFormatInfo.NaNSymbol;
            else if (IsPositiveInfinity)
                Result = Format.NumberFormatInfo.PositiveInfinitySymbol;
            else if (IsNegativeInfinity)
                Result = Format.NumberFormatInfo.NegativeInfinitySymbol;
            else
            {
                switch (Format.NumericFormat)
                {
                    case NumericFormat.Default:
                        Result = ToStringDefaultFormat(Format);
                        break;
                    case NumericFormat.Exponential:
                        Result = ToStringExponentialFormat(Format);
                        break;
                    case NumericFormat.FixedPoint:
                        Result = ToStringFixedPointFormat(Format);
                        break;
                }
            }

            Debug.Assert(Result != null);

            return Result;
        }

        private string ToStringDefaultFormat(DisplayFormat displayFormat)
        {
            ulong SizeInDigits = Precision;
            int Resultbase = 10;
            StringBuilder Data = new StringBuilder((int)(SizeInDigits + 2));

            int Exponent;
            mpfr_get_str(Data, out Exponent, Resultbase, SizeInDigits, ref Proxy.MpfrStruct, Rounding);

            string Result = Data.ToString();
            bool IsNegative = Result.Length > 0 && Result[0] == '-';

            bool IsZero = true;
            for (int i = IsNegative ? 1 : 0; i < Result.Length; i++)
                if (Result[i] != '0')
                {
                    IsZero = false;
                    break;
                }
            if (IsZero)
                return IsNegative ? "-0" : "0";

            int FractionalIndex = IsNegative ? 2 : 1;

            if (FractionalIndex > Result.Length)
                return Result;

            string IntegerPart = Result.Substring(0, FractionalIndex);
            string FractionalPart = Result.Substring(FractionalIndex);

            int LastNonZero = FractionalPart.Length;
            while (LastNonZero > 0 && FractionalPart[LastNonZero - 1] == '0')
                LastNonZero--;

            FractionalPart = FractionalPart.Substring(0, LastNonZero);

            if (FractionalPart.Length > 0)
                FractionalPart = "." + FractionalPart;

            string ExponentPart = (Exponent - 1).ToString();
            if (Exponent > 0)
                ExponentPart = "+" + ExponentPart;

            Result = $"{IntegerPart}{FractionalPart}E{ExponentPart}";

            return Result;
        }

        private string ToStringExponentialFormat(DisplayFormat displayFormat)
        {
            return string.Empty;
        }

        private string ToStringFixedPointFormat(DisplayFormat displayFormat)
        {
            return string.Empty;
        }
    }
}
