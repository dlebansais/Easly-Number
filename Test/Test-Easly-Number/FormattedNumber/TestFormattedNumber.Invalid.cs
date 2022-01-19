namespace TestFormattedNumber;

using EaslyNumber;
using NUnit.Framework;
using System;

[TestFixture]
public partial class TestFormattedNumber
{
    [Test]
    [Category("Coverage")]
    public static void ParseInvalid()
    {
        FormattedNumber Number;

        Number = FormattedNumber.Parse(string.Empty);
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse(":H");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse(":O");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse(":B");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("10*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("01*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse(":*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse(":");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("0*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("1:*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("1:H*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("1:O*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("1:B*");
        Assert.IsFalse(Number.IsValid);

#if !DEBUG
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { FormattedNumber.Parse(NullString); });
#endif
    }

    [Test]
    [Category("Coverage")]
    public static void ParseRealInvalid()
    {
        FormattedNumber Number;

        Number = FormattedNumber.Parse("0.*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse(".*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse(".0*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("0.0*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("+0.*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("+.");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("+.0*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("+0.0*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("-0.*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("-.");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("-.0*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("-0.0*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("0.0e*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("0.e*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse(".0e*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("0e*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("0.0e+*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("0.0e-*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("0.0e0*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("0.0e01");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("0.e0*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse(".0e0*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("e0*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("1e0*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("01e0*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("0.0e1*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("0.0e+1*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("0.0e-1*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse(".0e1*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse(".0e+1*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse(".0e-1*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("0.e1*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("0.e+1*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("0.e-1*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("0e1*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("0e+1*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("0e-1*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("1e1*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("1e+1*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("1e-1*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("01e1*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("01e+1*");
        Assert.IsFalse(Number.IsValid);

        Number = FormattedNumber.Parse("01e-1*");
        Assert.IsFalse(Number.IsValid);
    }
}
