namespace TestFormattedNumber;

using EaslyNumber;
using NUnit.Framework;
using System;

[TestFixture]
public partial class TestFormattedNumber
{
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
    public static void ParseRealZero()
    {
        FormattedNumber Number;

        Number = FormattedNumber.Parse("0.0");
        Assert.IsTrue(Number.IsValid);

        Assert.IsTrue(Number is FormattedReal);
        FormattedReal RealNumber = (FormattedReal)Number;
        Assert.AreEqual(OptionalSign.None, RealNumber.Sign);
        Assert.AreEqual(0, RealNumber.LeadingZeroCount);
        Assert.AreEqual("0", RealNumber.IntegerText);
        Assert.AreEqual('.', RealNumber.SeparatorCharacter);
        Assert.AreEqual("0", RealNumber.FractionalText);
        Assert.AreEqual(OptionalSign.None, RealNumber.ExponentSign);
        Assert.AreEqual(string.Empty, RealNumber.ExponentText);
        Assert.AreEqual("0.0", RealNumber.SignificandPart);
        Assert.AreEqual(string.Empty, RealNumber.ExponentPart);
        Assert.IsTrue(RealNumber.IsValid);
        Assert.AreEqual("//0/46/0/0///", RealNumber.Diagnostic);

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
