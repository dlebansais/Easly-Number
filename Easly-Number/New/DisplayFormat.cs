namespace EaslyNumber2
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    internal class DisplayFormat
    {
        public static DisplayFormat Empty { get; } = new DisplayFormat();

        private DisplayFormat()
        {
            NumericFormat = NumericFormat.Default;
            IsExponentUpperCase = false;
            PrecisionSpecifier = 0;
            NumberFormatInfo = NumberFormatInfo.CurrentInfo;
        }

        public DisplayFormat(NumericFormat numericFormat, bool isExponentUpperCase, int precisionSpecifier, NumberFormatInfo numberFormatInfo)
        {
            NumericFormat = numericFormat;
            IsExponentUpperCase = isExponentUpperCase;
            PrecisionSpecifier = precisionSpecifier;
            NumberFormatInfo = numberFormatInfo;
        }

        public NumericFormat NumericFormat { get; }
        public bool IsExponentUpperCase { get; }
        public int PrecisionSpecifier { get; }
        public NumberFormatInfo NumberFormatInfo { get; }

        public static bool Parse(string? format, IFormatProvider? provider, out DisplayFormat displayFormat)
        {
            char FormatCharacter = (format == null || format.Length == 0) ? 'G' : format[0];
            bool IsExponentUpperCase = char.IsUpper(FormatCharacter);
            NumberFormatInfo NumberFormatInfo = (provider != null && provider.GetFormat(typeof(NumberFormatInfo)) is NumberFormatInfo AsNumberFormatInfo) ? AsNumberFormatInfo : NumberFormatInfo.CurrentInfo;

            NumericFormat NumericFormat;
            int PrecisionSpecifier;

            switch (FormatCharacter)
            {
                case 'G':
                case 'g':
                    NumericFormat = NumericFormat.Default;
                    PrecisionSpecifier = 15;
                    break;

                case 'E':
                case 'e':
                    NumericFormat = NumericFormat.Exponential;
                    PrecisionSpecifier = 6;
                    break;

                case 'F':
                case 'f':
                    NumericFormat = NumericFormat.FixedPoint;
                    PrecisionSpecifier = NumberFormatInfo.NumberDecimalDigits;
                    break;

                default:
                    displayFormat = DisplayFormat.Empty;
                    return false;
            }

            if (format != null && format.Length > 1)
            {
                if (!int.TryParse(format.Substring(1), out PrecisionSpecifier))
                {
                    displayFormat = DisplayFormat.Empty;
                    return false;
                }

                if (PrecisionSpecifier < 0 || PrecisionSpecifier > 99)
                {
                    displayFormat = DisplayFormat.Empty;
                    return false;
                }
            }

            Debug.Assert(NumericFormat == NumericFormat.Default || NumericFormat == NumericFormat.Exponential || NumericFormat == NumericFormat.FixedPoint);
            Debug.Assert(PrecisionSpecifier >= 0 && PrecisionSpecifier <= 99);

            displayFormat = new DisplayFormat(NumericFormat, IsExponentUpperCase, PrecisionSpecifier, NumberFormatInfo);
            return true;
        }
    }
}
