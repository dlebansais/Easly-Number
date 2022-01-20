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

    [Test]
    [Category("Coverage")]
    public static void Sub()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("3");
        FormattedNumber Number2 = FormattedNumber.Parse("1");

        FormattedNumber Number = Number1 - Number2;
        Assert.IsTrue(Number.IsValid);
        Assert.AreEqual("2e0", Number.ToString());
    }

    [Test]
    [Category("Coverage")]
    public static void Mul()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("2");
        FormattedNumber Number2 = FormattedNumber.Parse("1");

        FormattedNumber Number = Number1 * Number2;
        Assert.IsTrue(Number.IsValid);
        Assert.AreEqual("2e0", Number.ToString());
    }

    [Test]
    [Category("Coverage")]
    public static void Div()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("2");
        FormattedNumber Number2 = FormattedNumber.Parse("1");

        FormattedNumber Number = Number1 / Number2;
        Assert.IsTrue(Number.IsValid);
        Assert.AreEqual("2e0", Number.ToString());
    }

    [Test]
    [Category("Coverage")]
    public static void Neg()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("-2");

        FormattedNumber Number = -Number1;
        Assert.IsTrue(Number.IsValid);
        Assert.AreEqual("2e0", Number.ToString());
    }

    [Test]
    [Category("Coverage")]
    public static void Abs()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("-2");

        FormattedNumber Number = Number1.Abs();
        Assert.IsTrue(Number.IsValid);
        Assert.AreEqual("2e0", Number.ToString());
    }

    [Test]
    [Category("Coverage")]
    public static void ExpLog()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("2");
        FormattedNumber Number;

        Number = Number1.Exp();
        Assert.IsTrue(Number.IsValid);

        Number = Number.Log();
        Assert.AreEqual("2e0", Number.ToString());
    }

    [Test]
    [Category("Coverage")]
    public static void Log10()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("100");

        FormattedNumber Number = Number1.Log10();
        Assert.IsTrue(Number.IsValid);
        Assert.AreEqual("2e0", Number.ToString());
    }

    [Test]
    [Category("Coverage")]
    public static void Pow()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("2");
        FormattedNumber Number2 = FormattedNumber.Parse("1");

        FormattedNumber Number = Number1.Pow(Number2);
        Assert.IsTrue(Number.IsValid);
        Assert.AreEqual("2e0", Number.ToString());
    }

    [Test]
    [Category("Coverage")]
    public static void Sqrt()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("4");

        FormattedNumber Number = Number1.Sqrt();
        Assert.IsTrue(Number.IsValid);
        Assert.AreEqual("2e0", Number.ToString());
    }

    [Test]
    [Category("Coverage")]
    public static void ShiftLeft()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("1");
        FormattedNumber Number2 = FormattedNumber.Parse("1");

        FormattedNumber Number = Number1.ShiftLeft(Number2);
        Assert.IsTrue(Number.IsValid);
        Assert.AreEqual("2e0", Number.ToString());
    }

    [Test]
    [Category("Coverage")]
    public static void ShiftRight()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("4");
        FormattedNumber Number2 = FormattedNumber.Parse("1");

        FormattedNumber Number = Number1.ShiftRight(Number2);
        Assert.IsTrue(Number.IsValid);
        Assert.AreEqual("2e0", Number.ToString());
    }

    [Test]
    [Category("Coverage")]
    public static void Remainder()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("4");
        FormattedNumber Number2 = FormattedNumber.Parse("3");

        FormattedNumber Number = Number1.Remainder(Number2);
        Assert.IsTrue(Number.IsValid);
        Assert.AreEqual("1e0", Number.ToString());
    }

    [Test]
    [Category("Coverage")]
    public static void BitwiseAnd()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("2");
        FormattedNumber Number2 = FormattedNumber.Parse("3");

        FormattedNumber Number = Number1.BitwiseAnd(Number2);
        Assert.IsTrue(Number.IsValid);
        Assert.AreEqual("2e0", Number.ToString());
    }

    [Test]
    [Category("Coverage")]
    public static void BitwiseOr()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("2");
        FormattedNumber Number2 = FormattedNumber.Parse("0");

        FormattedNumber Number = Number1.BitwiseOr(Number2);
        Assert.IsTrue(Number.IsValid);
        Assert.AreEqual("2e0", Number.ToString());
    }

    [Test]
    [Category("Coverage")]
    public static void BitwiseXor()
    {
        FormattedNumber Number1 = FormattedNumber.Parse("6");
        FormattedNumber Number2 = FormattedNumber.Parse("4");

        FormattedNumber Number = Number1.BitwiseXor(Number2);
        Assert.IsTrue(Number.IsValid);
        Assert.AreEqual("2e0", Number.ToString());
    }
}
