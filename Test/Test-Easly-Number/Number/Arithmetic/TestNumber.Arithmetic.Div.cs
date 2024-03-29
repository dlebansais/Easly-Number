﻿namespace TestNumber;

using EaslyNumber;
using NUnit.Framework;

[TestFixture]
public partial class TestNumberDiv
{
    [Test]
    [Category("Coverage")]
    public static void Div0()
    {
        double d1 = 125478;
        double d2 = 5.478231405e-3;

        Number Value1 = new Number(d1);
        Number Value2 = new Number(d2);

        double d = d1 / d2;
        Number Value = Value1 / Value2;

        string DoubleString = d.ToString("G17").Substring(0, 15);
        string NumberString = Value.ToString("G17").Substring(0, 15);

        Assert.AreEqual(DoubleString, NumberString);

        Value = Number.Divide(Value1, Value2, Value1.Precision, Rounding.Nearest);
        NumberString = Value.ToString("G17").Substring(0, 15);

        Assert.AreEqual(DoubleString, NumberString);
    }

    [Test]
    [Category("Coverage")]
    public static void Div1()
    {
        Number Value1 = new Number(125478.0);
        ulong n2 = 5;
        Number Value2 = new Number(n2);

        Number Value3 = Value1 / n2;
        string NumberString3 = Value3.ToString();
        Number Value4 = Number.Divide(Value1, Value2, Value1.Precision, Rounding.Nearest);
        string NumberString4 = Value4.ToString();

        Assert.AreEqual(NumberString3, NumberString4);
    }

    [Test]
    [Category("Coverage")]
    public static void Div2()
    {
        Number Value1 = new Number(125478.0);
        ulong n2 = 5;
        Number Value2 = new Number(n2);

        Number Value3 = n2 / Value1;
        string NumberString3 = Value3.ToString();
        Number Value4 = Number.Divide(Value2, Value1, Value2.Precision, Rounding.Nearest);
        string NumberString4 = Value4.ToString();

        Assert.AreEqual(NumberString3, NumberString4);
    }

    [Test]
    [Category("Coverage")]
    public static void Div3()
    {
        Number Value1 = new Number(125478.0);
        long n2 = -5;
        Number Value2 = new Number(n2);

        Number Value3 = Value1 / n2;
        string NumberString3 = Value3.ToString();
        Number Value4 = Number.Divide(Value1, Value2, Value1.Precision, Rounding.Nearest);
        string NumberString4 = Value4.ToString();

        Assert.AreEqual(NumberString3, NumberString4);
    }

    [Test]
    [Category("Coverage")]
    public static void Div4()
    {
        Number Value1 = new Number(125478.0);
        long n2 = -5;
        Number Value2 = new Number(n2);

        Number Value3 = n2 / Value1;
        string NumberString3 = Value3.ToString();
        Number Value4 = Number.Divide(Value2, Value1, Value2.Precision, Rounding.Nearest);
        string NumberString4 = Value4.ToString();

        Assert.AreEqual(NumberString3, NumberString4);
    }

    [Test]
    [Category("Coverage")]
    public static void Div5()
    {
        Number Value1 = new Number(125478.0);
        double d2 = 5.0;
        Number Value2 = new Number(d2);

        Number Value3 = Value1 / d2;
        string NumberString3 = Value3.ToString();
        Number Value4 = Number.Divide(Value1, Value2, Value1.Precision, Rounding.Nearest);
        string NumberString4 = Value4.ToString();

        Assert.AreEqual(NumberString3, NumberString4);
    }

    [Test]
    [Category("Coverage")]
    public static void Div6()
    {
        Number Value1 = new Number(125478.0);
        double d2 = 5.0;
        Number Value2 = new Number(d2);

        Number Value3 = d2 / Value1;
        string NumberString3 = Value3.ToString();
        Number Value4 = Number.Divide(Value2, Value1, Value2.Precision, Rounding.Nearest);
        string NumberString4 = Value4.ToString();

        Assert.AreEqual(NumberString3, NumberString4);
    }
}
