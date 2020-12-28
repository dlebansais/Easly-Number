namespace TestEaslyNumber
{
    using EaslyNumber;
    using System;
    using System.Diagnostics;
    using System.Globalization;

    public static class Program
    {
        private static string NL = Environment.NewLine;
        private static string SP = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        public static int Main(string[] args)
        {
            Number n10 = new Number("1.2e3");
            Debug.Assert(n10.ToString() == $"1{SP}2E3" && n10.CheatDouble == 1.2e3, $"Result: {n10}, expected: 1{SP}2E3");

            return 0;
        }
    }
}
