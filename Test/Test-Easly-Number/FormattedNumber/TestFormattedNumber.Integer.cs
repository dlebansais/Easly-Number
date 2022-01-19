namespace TestFormattedNumber;

using EaslyNumber;
using NUnit.Framework;
using System;

[TestFixture]
public partial class TestFormattedNumber
{
    [Test]
    [Category("Coverage")]
    public static void ParseIntegers()
    {
        FormattedNumber Number;

        Number = FormattedNumber.Parse("0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("0:H");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("0:O");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("0:B");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("1:H");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("1:O");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("1:B");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("1234567890");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("123456789ABCDEF0:H");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("12345670:O");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("10:B");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("01234567890");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("0123456789ABCDEF0:H");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("012345670:O");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("010:B");
        Assert.IsTrue(Number.IsValid);
    }

    [Test]
    [Category("Coverage")]
    public static void ParseSignDecimal()
    {
        FormattedNumber Number;

        Number = FormattedNumber.Parse("+0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("-0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("+1234567890");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("-1234567890");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("+01");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("-01");
        Assert.IsTrue(Number.IsValid);
    }

    [Test]
    [Category("Coverage")]
    public static void ParseSignHexadecimal()
    {
        FormattedNumber Number;

        Number = FormattedNumber.Parse("+0:H");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("-0:H");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("+123456789ABCDEF0:H");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("-123456789ABCDEF0:H");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("+01:H");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("-01:H");
        Assert.IsTrue(Number.IsValid);
    }

    [Test]
    [Category("Coverage")]
    public static void ParseSignOctal()
    {
        FormattedNumber Number;

        Number = FormattedNumber.Parse("+0:O");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("-0:O");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("+1234567:O");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("-1234567:O");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("+01:O");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("-01:O");
        Assert.IsTrue(Number.IsValid);
    }

    [Test]
    [Category("Coverage")]
    public static void ParseSignBinary()
    {
        FormattedNumber Number;

        Number = FormattedNumber.Parse("+0:B");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("-0:B");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("+1:B");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("-1:B");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("+01:B");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("-01:B");
        Assert.IsTrue(Number.IsValid);
    }
}
