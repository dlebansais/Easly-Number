namespace EaslyNumber
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

#pragma warning disable SA1600 // Elements should be documented
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

        private static bool ParseFormatCharacter(char formatCharacter, NumberFormatInfo numberFormatInfo, out NumericFormat numericFormat, out int precisionSpecifier)
        {
            numericFormat = NumericFormat.Default;
            precisionSpecifier = 0;

            switch (formatCharacter)
            {
                case 'G':
                case 'g':
                    break;

                case 'E':
                case 'e':
                    numericFormat = NumericFormat.Exponential;
                    precisionSpecifier = 6;
                    break;

                case 'F':
                case 'f':
                    numericFormat = NumericFormat.FixedPoint;
                    precisionSpecifier = numberFormatInfo.NumberDecimalDigits;
                    break;

                default:
                    return false;
            }

            return true;
        }

        private static bool ParsePrecisionSpecifier(string? format, ref int precisionSpecifier)
        {
            if (format is not null && format.Length > 1)
            {
                if (!int.TryParse(format.Substring(1), out precisionSpecifier))
                    return false;

                if (precisionSpecifier < 0 || precisionSpecifier > 99)
                    return false;
            }

            return true;
        }

        public static bool Parse(string? format, IFormatProvider? provider, ulong precision, out DisplayFormat displayFormat)
        {
            char FormatCharacter = (format is null || format.Length == 0) ? 'G' : format[0];
            bool IsExponentUpperCase = char.IsUpper(FormatCharacter);
            NumberFormatInfo NumberFormatInfo = (provider is not null && provider.GetFormat(typeof(NumberFormatInfo)) is NumberFormatInfo AsNumberFormatInfo) ? AsNumberFormatInfo : NumberFormatInfo.CurrentInfo;

            if (!ParseFormatCharacter(FormatCharacter, NumberFormatInfo, out NumericFormat NumericFormat, out int PrecisionSpecifier))
            {
                displayFormat = Empty;
                return false;
            }

            if (!ParsePrecisionSpecifier(format, ref PrecisionSpecifier))
            {
                displayFormat = Empty;
                return false;
            }

            if (NumericFormat == NumericFormat.Default && PrecisionSpecifier == 0)
                if (precision <= 24)
                    PrecisionSpecifier = 7 + 1;
                else
                    PrecisionSpecifier = 15 + 2;

            Debug.Assert(NumericFormat == NumericFormat.Default || NumericFormat == NumericFormat.Exponential || NumericFormat == NumericFormat.FixedPoint);
            Debug.Assert(PrecisionSpecifier >= 0);
            Debug.Assert(PrecisionSpecifier <= 99);

            displayFormat = new DisplayFormat(NumericFormat, IsExponentUpperCase, PrecisionSpecifier, NumberFormatInfo);
            return true;
        }
    }
#pragma warning restore SA1600 // Elements should be documented
}
