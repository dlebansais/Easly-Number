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

            Assembly NumberAssembly;

            try
            {
                NumberAssembly = Assembly.Load("Easly-Number");
            }
            catch
            {
                NumberAssembly = null;
            }
            Assume.That(NumberAssembly != null);

            if (TextNaN == null)
            {
                TextNaN = double.NaN.ToString();
                TestContext.Progress.WriteLine($"              NaN = {TextNaN}");
            }

            if (TextPositiveInfinity == null)
            {
                TextPositiveInfinity = double.PositiveInfinity.ToString();
                TestContext.Progress.WriteLine($"Positive Infinity = {TextPositiveInfinity}");
            }

            if (TextNegativeInfinity == null)
            {
                TextNegativeInfinity = double.NegativeInfinity.ToString();
                TestContext.Progress.WriteLine($"Negative Infinity = {TextNegativeInfinity}");
            }

            if (SP == null)
            {
                SP = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                TestContext.Progress.WriteLine($"Decimal Separator = {SP}");
            }
        }

        private static string TextNaN;
        private static string TextPositiveInfinity;
        private static string TextNegativeInfinity;
        private static string SP;
        #endregion
    }
}
