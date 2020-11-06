namespace EaslyNumber
{
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
    }
}
