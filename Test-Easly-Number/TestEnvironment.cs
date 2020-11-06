namespace TestEaslyNumber
{
    using NUnit.Framework;
    using System.Globalization;
    using System.Reflection;
    using System.Threading;

    [TestFixture]
    public class TestEnvironment
    {
        #region Setup
        [OneTimeSetUp]
        public static void InitTestSession()
        {
            CultureInfo frFR = CultureInfo.CreateSpecificCulture("fr-FR");
            CultureInfo.DefaultThreadCurrentCulture = frFR;
            CultureInfo.DefaultThreadCurrentUICulture = frFR;
            Thread.CurrentThread.CurrentCulture = frFR;
            Thread.CurrentThread.CurrentUICulture = frFR;

            Assembly? NumberAssembly;

            try
            {
                NumberAssembly = Assembly.Load("Easly-Number");
            }
            catch
            {
                NumberAssembly = null;
            }
            Assume.That(NumberAssembly != null);

            if (TextNaN.Length == 0)
            {
                TextNaN = double.NaN.ToString();
                TestContext.Progress.WriteLine($"              NaN = {TextNaN}");
            }

            if (TextPositiveInfinity.Length == 0)
            {
                TextPositiveInfinity = double.PositiveInfinity.ToString();
                TestContext.Progress.WriteLine($"Positive Infinity = {TextPositiveInfinity}");
            }

            if (TextNegativeInfinity.Length == 0)
            {
                TextNegativeInfinity = double.NegativeInfinity.ToString();
                TestContext.Progress.WriteLine($"Negative Infinity = {TextNegativeInfinity}");
            }

            if (SP.Length == 0)
            {
                SP = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                TestContext.Progress.WriteLine($"Decimal Separator = {SP}");
            }
        }

        private static string TextNaN = string.Empty;
        private static string TextPositiveInfinity = string.Empty;
        private static string TextNegativeInfinity = string.Empty;
        private static string SP = string.Empty;
        #endregion

        #region Tools
        public const string FormatDouble = "E16";

        public static string DoubleString(double d)
        {
            return d.ToString(FormatDouble);
        }
        #endregion
    }
}
