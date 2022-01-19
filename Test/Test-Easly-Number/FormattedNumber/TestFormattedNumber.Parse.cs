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

    [Test]
    [Category("Coverage")]
    public static void ParseRealZero()
    {
        FormattedNumber Number;

        Number = FormattedNumber.Parse("0.0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("0.");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse(".0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("0.0e0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("0.0e1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("0.0e-1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("+0.0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("+0.");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("+.0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("0e0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("0e1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("0e-1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse("00.0");
        Assert.IsTrue(Number.IsValid);
    }

    [Test]
    [Category("Coverage")]
    public static void ParseReal()
    {
        ParseRealWithExponent('e');
        ParseRealWithExponent('E');
    }

    private static void ParseRealWithExponent(char exponentCharacter)
    {
        FormattedNumber Number;

        Number = FormattedNumber.Parse($"123.456{exponentCharacter}0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"+123.456{exponentCharacter}0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"-123.456{exponentCharacter}0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"0123.456{exponentCharacter}0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"+0123.456{exponentCharacter}0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"-0123.456{exponentCharacter}0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"123.456{exponentCharacter}1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"123.456{exponentCharacter}+1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"123.456{exponentCharacter}-1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"123456{exponentCharacter}0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"123456{exponentCharacter}1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"123456{exponentCharacter}+1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"123456{exponentCharacter}-1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"123456.{exponentCharacter}0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"123456.{exponentCharacter}1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"123456.{exponentCharacter}+1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"123456.{exponentCharacter}-1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"123456.0{exponentCharacter}0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"123456.0{exponentCharacter}1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"123456.0{exponentCharacter}+1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"123456.0{exponentCharacter}-1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"0.123456{exponentCharacter}0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"0.123456{exponentCharacter}1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"0.123456{exponentCharacter}+1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"0.123456{exponentCharacter}-1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($".123456{exponentCharacter}0");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($".123456{exponentCharacter}1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($".123456{exponentCharacter}+1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($".123456{exponentCharacter}-1");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"123456.10{exponentCharacter}1234567890");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"123456{exponentCharacter}1234567890");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"123456.0{exponentCharacter}1234567890");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($"123456.{exponentCharacter}1234567890");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($".0{exponentCharacter}1234567890");
        Assert.IsTrue(Number.IsValid);

        Number = FormattedNumber.Parse($".10{exponentCharacter}1234567890");
        Assert.IsTrue(Number.IsValid);
    }
}
