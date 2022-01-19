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
        FormattedNumber EmptyNumber = FormattedNumber.Parse(string.Empty);
        Assert.IsFalse(EmptyNumber.IsValid);

        FormattedNumber BadBaseNumber1 = FormattedNumber.Parse(":H");
        Assert.IsFalse(BadBaseNumber1.IsValid);

        FormattedNumber BadBaseNumber2 = FormattedNumber.Parse(":O");
        Assert.IsFalse(BadBaseNumber2.IsValid);

        FormattedNumber BadBaseNumber3 = FormattedNumber.Parse(":B");
        Assert.IsFalse(BadBaseNumber3.IsValid);

        FormattedNumber BadBaseNumber4 = FormattedNumber.Parse("10*");
        Assert.IsFalse(BadBaseNumber4.IsValid);

        FormattedNumber BadBaseNumber5 = FormattedNumber.Parse("01*");
        Assert.IsFalse(BadBaseNumber5.IsValid);

        FormattedNumber BadBaseNumber6 = FormattedNumber.Parse(":*");
        Assert.IsFalse(BadBaseNumber6.IsValid);

        FormattedNumber BadBaseNumber7 = FormattedNumber.Parse(":");
        Assert.IsFalse(BadBaseNumber7.IsValid);

        FormattedNumber BadBaseNumber8 = FormattedNumber.Parse("0*");
        Assert.IsFalse(BadBaseNumber8.IsValid);

#if !DEBUG
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { FormattedNumber.Parse(NullString); });
#endif
    }

    [Test]
    [Category("Coverage")]
    public static void ParseSpecial()
    {
        FormattedNumber NaNNumber = FormattedNumber.Parse(double.NaN.ToString());
        Assert.IsFalse(NaNNumber.IsValid);

        FormattedNumber PositiveInfinityNumber = FormattedNumber.Parse(double.PositiveInfinity.ToString());
        Assert.IsFalse(PositiveInfinityNumber.IsValid);

        FormattedNumber NegativeInfinityNumber = FormattedNumber.Parse(double.NegativeInfinity.ToString());
        Assert.IsFalse(NegativeInfinityNumber.IsValid);
    }

    [Test]
    [Category("Coverage")]
    public static void ParseIntegers()
    {
        FormattedNumber ZeroNumber = FormattedNumber.Parse("0");
        Assert.IsTrue(ZeroNumber.IsValid);

        FormattedNumber Number1 = FormattedNumber.Parse("1");
        Assert.IsTrue(Number1.IsValid);

        FormattedNumber Number2 = FormattedNumber.Parse("0:H");
        Assert.IsTrue(Number2.IsValid);

        FormattedNumber Number3 = FormattedNumber.Parse("0:O");
        Assert.IsTrue(Number3.IsValid);

        FormattedNumber Number4 = FormattedNumber.Parse("0:B");
        Assert.IsTrue(Number4.IsValid);

        FormattedNumber Number5 = FormattedNumber.Parse("1:H");
        Assert.IsTrue(Number5.IsValid);

        FormattedNumber Number6 = FormattedNumber.Parse("1:O");
        Assert.IsTrue(Number6.IsValid);

        FormattedNumber Number7 = FormattedNumber.Parse("1:B");
        Assert.IsTrue(Number7.IsValid);

        FormattedNumber Number8 = FormattedNumber.Parse("1234567890");
        Assert.IsTrue(Number8.IsValid);

        FormattedNumber Number9 = FormattedNumber.Parse("123456789ABCDEF0:H");
        Assert.IsTrue(Number9.IsValid);

        FormattedNumber Number10 = FormattedNumber.Parse("12345670:O");
        Assert.IsTrue(Number10.IsValid);

        FormattedNumber Number11 = FormattedNumber.Parse("10:B");
        Assert.IsTrue(Number11.IsValid);

        FormattedNumber Number12 = FormattedNumber.Parse("01234567890");
        Assert.IsTrue(Number12.IsValid);

        FormattedNumber Number13 = FormattedNumber.Parse("0123456789ABCDEF0:H");
        Assert.IsTrue(Number13.IsValid);

        FormattedNumber Number14 = FormattedNumber.Parse("012345670:O");
        Assert.IsTrue(Number14.IsValid);

        FormattedNumber Number15 = FormattedNumber.Parse("010:B");
        Assert.IsTrue(Number15.IsValid);
    }

    [Test]
    [Category("Coverage")]
    public static void ParseSignDecimal()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("+0");
        Assert.IsTrue(Number1.IsValid);

        FormattedNumber Number2 = FormattedNumber.Parse("-0");
        Assert.IsTrue(Number2.IsValid);

        FormattedNumber Number3 = FormattedNumber.Parse("+1234567890");
        Assert.IsTrue(Number3.IsValid);

        FormattedNumber Number4 = FormattedNumber.Parse("-1234567890");
        Assert.IsTrue(Number4.IsValid);

        FormattedNumber Number5 = FormattedNumber.Parse("+01");
        Assert.IsTrue(Number5.IsValid);

        FormattedNumber Number6 = FormattedNumber.Parse("-01");
        Assert.IsTrue(Number6.IsValid);
    }

    [Test]
    [Category("Coverage")]
    public static void ParseSignHexadecimal()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("+0:H");
        Assert.IsTrue(Number1.IsValid);

        FormattedNumber Number2 = FormattedNumber.Parse("-0:H");
        Assert.IsTrue(Number2.IsValid);

        FormattedNumber Number3 = FormattedNumber.Parse("+123456789ABCDEF0:H");
        Assert.IsTrue(Number3.IsValid);

        FormattedNumber Number4 = FormattedNumber.Parse("-123456789ABCDEF0:H");
        Assert.IsTrue(Number4.IsValid);

        FormattedNumber Number5 = FormattedNumber.Parse("+01:H");
        Assert.IsTrue(Number5.IsValid);

        FormattedNumber Number6 = FormattedNumber.Parse("-01:H");
        Assert.IsTrue(Number6.IsValid);
    }

    [Test]
    [Category("Coverage")]
    public static void ParseSignOctal()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("+0:O");
        Assert.IsTrue(Number1.IsValid);

        FormattedNumber Number2 = FormattedNumber.Parse("-0:O");
        Assert.IsTrue(Number2.IsValid);

        FormattedNumber Number3 = FormattedNumber.Parse("+1234567:O");
        Assert.IsTrue(Number3.IsValid);

        FormattedNumber Number4 = FormattedNumber.Parse("-1234567:O");
        Assert.IsTrue(Number4.IsValid);

        FormattedNumber Number5 = FormattedNumber.Parse("+01:O");
        Assert.IsTrue(Number5.IsValid);

        FormattedNumber Number6 = FormattedNumber.Parse("-01:O");
        Assert.IsTrue(Number6.IsValid);
    }

    [Test]
    [Category("Coverage")]
    public static void ParseSignBinary()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("+0:B");
        Assert.IsTrue(Number1.IsValid);

        FormattedNumber Number2 = FormattedNumber.Parse("-0:B");
        Assert.IsTrue(Number2.IsValid);

        FormattedNumber Number3 = FormattedNumber.Parse("+1:B");
        Assert.IsTrue(Number3.IsValid);

        FormattedNumber Number4 = FormattedNumber.Parse("-1:B");
        Assert.IsTrue(Number4.IsValid);

        FormattedNumber Number5 = FormattedNumber.Parse("+01:B");
        Assert.IsTrue(Number5.IsValid);

        FormattedNumber Number6 = FormattedNumber.Parse("-01:B");
        Assert.IsTrue(Number6.IsValid);
    }

    [Test]
    [Category("Coverage")]
    public static void ParseRealZero()
    {
        FormattedNumber ZeroNumber1 = FormattedNumber.Parse("0.0");
        Assert.IsTrue(ZeroNumber1.IsValid);

        FormattedNumber ZeroNumber2 = FormattedNumber.Parse("0.");
        Assert.IsTrue(ZeroNumber2.IsValid);

        FormattedNumber ZeroNumber3 = FormattedNumber.Parse(".0");
        Assert.IsTrue(ZeroNumber3.IsValid);

        FormattedNumber ZeroNumber4 = FormattedNumber.Parse("0.0e0");
        Assert.IsTrue(ZeroNumber4.IsValid);

        FormattedNumber ZeroNumber5 = FormattedNumber.Parse("0.0e1");
        Assert.IsTrue(ZeroNumber5.IsValid);

        FormattedNumber ZeroNumber6 = FormattedNumber.Parse("0.0e-1");
        Assert.IsTrue(ZeroNumber6.IsValid);

        FormattedNumber ZeroNumber7 = FormattedNumber.Parse("+0.0");
        Assert.IsTrue(ZeroNumber7.IsValid);

        FormattedNumber ZeroNumber8 = FormattedNumber.Parse("+0.");
        Assert.IsTrue(ZeroNumber8.IsValid);

        FormattedNumber ZeroNumber9 = FormattedNumber.Parse("+.0");
        Assert.IsTrue(ZeroNumber9.IsValid);

    }
}
