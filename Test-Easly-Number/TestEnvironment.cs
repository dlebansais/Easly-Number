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
        }
        #endregion

        #region Environment
        [Test, Order(1)]
        [Category("Coverage")]
        public void TestCreate()
        {
            TestContext.Progress.WriteLine($"NaN = {double.NaN}");
            TestContext.Progress.WriteLine($"PositiveInfinity = {double.PositiveInfinity}");
            TestContext.Progress.WriteLine($"NegativeInfinity = {double.NegativeInfinity}");
        }
        #endregion
    }
}
