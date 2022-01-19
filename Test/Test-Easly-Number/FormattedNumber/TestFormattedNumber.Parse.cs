namespace TestFormattedNumber;

using EaslyNumber;
using NUnit.Framework;

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

#if !DEBUG
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { FormattedNumber.Parse(NullString); });
#endif
    }
}
