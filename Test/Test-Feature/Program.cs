namespace TestEaslyNumber
{
    using EaslyNumber2;
    using System;
    using System.Diagnostics;
    using System.Globalization;

    public static class Program
    {
        private static string NL = Environment.NewLine;
        private static string SP = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        public static int Main(string[] args)
        {
            Number n = new Number();
            Number x = -n;
            bool b = n.IsNaN;
            bool c = n.IsNegativeInfinity;

            Number n10 = new Number("1.2e3");
            double d = 1.2e3;
            string ds = d.ToString();

            Debug.Assert(n10.ToString() == $"1{SP}2E3", $"Result: {n10}, expected: 1{SP}2E3");

            return 0;
        }
    }
}
