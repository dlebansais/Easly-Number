namespace TestNumber;

using EaslyNumber;
using NUnit.Framework;
using System;
using System.Globalization;
using System.Reflection;
using System.Threading;

[SetUpFixture]
public class TestEnvironment
{
    [OneTimeSetUp]
    public static void InitTestSession()
    {
        // Called first, before all tests in the TestNumber namespace.
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
        Assume.That(NumberAssembly is not null);

        TextNaN = double.NaN.ToString();
        TestContext.Progress.WriteLine($"              NaN = {TextNaN}");

        TextPositiveInfinity = double.PositiveInfinity.ToString();
        TestContext.Progress.WriteLine($"Positive Infinity = {TextPositiveInfinity}");

        TextNegativeInfinity = double.NegativeInfinity.ToString();
        TestContext.Progress.WriteLine($"Negative Infinity = {TextNegativeInfinity}");

        NL = Environment.NewLine;
        string UnescapedNL = NL.Replace("\r", "\\r").Replace("\n", "\\n");
        TestContext.Progress.WriteLine($"         New Line = {UnescapedNL}");

        SP = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        TestContext.Progress.WriteLine($"Decimal Separator = {SP}");
    }

    public static string TextNaN = string.Empty;
    public static string TextPositiveInfinity = string.Empty;
    public static string TextNegativeInfinity = string.Empty;
    public static string NL = string.Empty;
    public static string SP = string.Empty;
}
