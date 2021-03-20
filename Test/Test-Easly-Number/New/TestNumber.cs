namespace TestEaslyNumber2
{
    using EaslyNumber2;
    using NUnit.Framework;
    using System;
    using System.Globalization;

    [TestFixture]
    public partial class TestNumber
    {
        [OneTimeSetUp]
        public static void InitTestSession()
        {
            TestEaslyNumber.TestEnvironment.InitTestSession();

            NL = Environment.NewLine;
            SP = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        }

        private static string NL = string.Empty;
        private static string SP = string.Empty;
    }
}
