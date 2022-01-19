namespace TestFormattedNumber;

using EaslyNumber;
using NUnit.Framework;
using System;

[TestFixture]
public partial class TestFormattedNumber
{
    [Test]
    [Category("Coverage")]
    public static void Add()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("1");
        FormattedNumber Number2 = FormattedNumber.Parse("1");

        FormattedNumber Number = Number1 + Number2;
        Assert.IsTrue(Number.IsValid);
        Assert.AreEqual("2e0", Number.ToString());
    }
}
