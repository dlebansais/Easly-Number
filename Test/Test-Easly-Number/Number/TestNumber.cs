namespace TestNumber;

using NUnit.Framework;
using System.Diagnostics;

[TestFixture]
public partial class TestNumber
{
    [OneTimeSetUp]
    public static void InitTestSession()
    {
        Debug.Assert(TestEnvironment.NL.Length > 0);
        NL = TestEnvironment.NL;

        Debug.Assert(TestEnvironment.SP.Length > 0);
        SP = TestEnvironment.SP;
    }

    public static string NL = string.Empty;
    public static string SP = string.Empty;
}
