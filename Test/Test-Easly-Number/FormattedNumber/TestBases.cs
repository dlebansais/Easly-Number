namespace TestFormattedNumber;

using EaslyNumber;
using NUnit.Framework;
using System;

[TestFixture]
public partial class TestBases
{
    [Test]
    [Category("Coverage")]
    public static void Binary()
    {
        BinaryIntegerBase Base = new();

        Assert.AreEqual(0, Base.ToValue('0'));
        Assert.AreEqual(1, Base.ToValue('1'));

        Assert.Throws<ArgumentException>(() => { Base.ToValue('/'); });
        Assert.Throws<ArgumentException>(() => { Base.ToValue('2'); });

        Assert.AreEqual('0', Base.ToDigit(0));
        Assert.AreEqual('1', Base.ToDigit(1));

        Assert.Throws<ArgumentException>(() => { Base.ToDigit(-1); });
        Assert.Throws<ArgumentException>(() => { Base.ToDigit(2); });
    }

    [Test]
    [Category("Coverage")]
    public static void Hexadecimal()
    {
        HexadecimalIntegerBase Base = new();

        Assert.AreEqual(0, Base.ToValue('0'));
        Assert.AreEqual(1, Base.ToValue('1'));
        Assert.AreEqual(2, Base.ToValue('2'));
        Assert.AreEqual(3, Base.ToValue('3'));
        Assert.AreEqual(4, Base.ToValue('4'));
        Assert.AreEqual(5, Base.ToValue('5'));
        Assert.AreEqual(6, Base.ToValue('6'));
        Assert.AreEqual(7, Base.ToValue('7'));
        Assert.AreEqual(8, Base.ToValue('8'));
        Assert.AreEqual(9, Base.ToValue('9'));
        Assert.AreEqual(10, Base.ToValue('A'));
        Assert.AreEqual(11, Base.ToValue('B'));
        Assert.AreEqual(12, Base.ToValue('C'));
        Assert.AreEqual(13, Base.ToValue('D'));
        Assert.AreEqual(14, Base.ToValue('E'));
        Assert.AreEqual(15, Base.ToValue('F'));
        Assert.AreEqual(10, Base.ToValue('a'));
        Assert.AreEqual(11, Base.ToValue('b'));
        Assert.AreEqual(12, Base.ToValue('c'));
        Assert.AreEqual(13, Base.ToValue('d'));
        Assert.AreEqual(14, Base.ToValue('e'));
        Assert.AreEqual(15, Base.ToValue('f'));

        Assert.Throws<ArgumentException>(() => { Base.ToValue('/'); });
        Assert.Throws<ArgumentException>(() => { Base.ToValue(':'); });
        Assert.Throws<ArgumentException>(() => { Base.ToValue('@'); });
        Assert.Throws<ArgumentException>(() => { Base.ToValue('G'); });
        Assert.Throws<ArgumentException>(() => { Base.ToValue('`'); });
        Assert.Throws<ArgumentException>(() => { Base.ToValue('g'); });

        Assert.AreEqual('0', Base.ToDigit(0));
        Assert.AreEqual('1', Base.ToDigit(1));
        Assert.AreEqual('2', Base.ToDigit(2));
        Assert.AreEqual('3', Base.ToDigit(3));
        Assert.AreEqual('4', Base.ToDigit(4));
        Assert.AreEqual('5', Base.ToDigit(5));
        Assert.AreEqual('6', Base.ToDigit(6));
        Assert.AreEqual('7', Base.ToDigit(7));
        Assert.AreEqual('8', Base.ToDigit(8));
        Assert.AreEqual('9', Base.ToDigit(9));
        Assert.AreEqual('A', Base.ToDigit(10));
        Assert.AreEqual('B', Base.ToDigit(11));
        Assert.AreEqual('C', Base.ToDigit(12));
        Assert.AreEqual('D', Base.ToDigit(13));
        Assert.AreEqual('E', Base.ToDigit(14));
        Assert.AreEqual('F', Base.ToDigit(15));

        Assert.Throws<ArgumentException>(() => { Base.ToDigit(-1); });
        Assert.Throws<ArgumentException>(() => { Base.ToDigit(16); });
    }

    [Test]
    [Category("Coverage")]
    public static void Decimal()
    {
        DecimalIntegerBase Base = new();

        Assert.AreEqual(0, Base.ToValue('0'));
        Assert.AreEqual(1, Base.ToValue('1'));
        Assert.AreEqual(2, Base.ToValue('2'));
        Assert.AreEqual(3, Base.ToValue('3'));
        Assert.AreEqual(4, Base.ToValue('4'));
        Assert.AreEqual(5, Base.ToValue('5'));
        Assert.AreEqual(6, Base.ToValue('6'));
        Assert.AreEqual(7, Base.ToValue('7'));
        Assert.AreEqual(8, Base.ToValue('8'));
        Assert.AreEqual(9, Base.ToValue('9'));

        Assert.Throws<ArgumentException>(() => { Base.ToValue('/'); });
        Assert.Throws<ArgumentException>(() => { Base.ToValue(':'); });

        Assert.AreEqual('0', Base.ToDigit(0));
        Assert.AreEqual('1', Base.ToDigit(1));
        Assert.AreEqual('2', Base.ToDigit(2));
        Assert.AreEqual('3', Base.ToDigit(3));
        Assert.AreEqual('4', Base.ToDigit(4));
        Assert.AreEqual('5', Base.ToDigit(5));
        Assert.AreEqual('6', Base.ToDigit(6));
        Assert.AreEqual('7', Base.ToDigit(7));
        Assert.AreEqual('8', Base.ToDigit(8));
        Assert.AreEqual('9', Base.ToDigit(9));

        Assert.Throws<ArgumentException>(() => { Base.ToDigit(-1); });
        Assert.Throws<ArgumentException>(() => { Base.ToDigit(10); });
    }

    [Test]
    [Category("Coverage")]
    public static void Octal()
    {
        OctalIntegerBase Base = new();

        Assert.AreEqual(0, Base.ToValue('0'));
        Assert.AreEqual(1, Base.ToValue('1'));
        Assert.AreEqual(2, Base.ToValue('2'));
        Assert.AreEqual(3, Base.ToValue('3'));
        Assert.AreEqual(4, Base.ToValue('4'));
        Assert.AreEqual(5, Base.ToValue('5'));
        Assert.AreEqual(6, Base.ToValue('6'));
        Assert.AreEqual(7, Base.ToValue('7'));

        Assert.Throws<ArgumentException>(() => { Base.ToValue('/'); });
        Assert.Throws<ArgumentException>(() => { Base.ToValue('8'); });

        Assert.AreEqual('0', Base.ToDigit(0));
        Assert.AreEqual('1', Base.ToDigit(1));
        Assert.AreEqual('2', Base.ToDigit(2));
        Assert.AreEqual('3', Base.ToDigit(3));
        Assert.AreEqual('4', Base.ToDigit(4));
        Assert.AreEqual('5', Base.ToDigit(5));
        Assert.AreEqual('6', Base.ToDigit(6));
        Assert.AreEqual('7', Base.ToDigit(7));

        Assert.Throws<ArgumentException>(() => { Base.ToDigit(-1); });
        Assert.Throws<ArgumentException>(() => { Base.ToDigit(8); });
    }
}
