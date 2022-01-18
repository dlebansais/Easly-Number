namespace TestEaslyNumber;

using NUnit.Framework;
using System;
using System.Globalization;

[TestFixture]
public partial class TestNumber
{
    [OneTimeSetUp]
    public static void InitTestSession()
    {
        TestEnvironment.InitTestSession();

        NL = Environment.NewLine;
        SP = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
    }

    public static string NL = string.Empty;
    public static string SP = string.Empty;
}
