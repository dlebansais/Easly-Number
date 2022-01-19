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

        Assert.AreEqual(":B", Base.Suffix);

        Assert.AreEqual(0, Base.ToValue('0'));
        Assert.AreEqual(1, Base.ToValue('1'));

        Assert.Throws<ArgumentException>(() => { Base.ToValue('/'); });
        Assert.Throws<ArgumentException>(() => { Base.ToValue('2'); });

        Assert.AreEqual('0', Base.ToDigit(0));
        Assert.AreEqual('1', Base.ToDigit(1));

        Assert.Throws<ArgumentException>(() => { Base.ToDigit(-1); });
        Assert.Throws<ArgumentException>(() => { Base.ToDigit(2); });

        Assert.IsFalse(Base.IsValidNumber(string.Empty));
        Assert.IsTrue(Base.IsValidNumber("0", true));
        Assert.IsTrue(Base.IsValidNumber("0", false));
        Assert.IsTrue(Base.IsValidNumber("1", true));
        Assert.IsTrue(Base.IsValidNumber("1", false));
        Assert.IsTrue(Base.IsValidNumber("10", true));
        Assert.IsTrue(Base.IsValidNumber("10", false));
        Assert.IsTrue(Base.IsValidNumber("01", true));
        Assert.IsFalse(Base.IsValidNumber("01", false));
        Assert.IsFalse(Base.IsValidNumber("*", false));

        Assert.IsFalse(Base.IsValidSignificand(string.Empty));
        Assert.IsTrue(Base.IsValidSignificand("0"));
        Assert.IsTrue(Base.IsValidSignificand("1"));
        Assert.IsFalse(Base.IsValidSignificand("2"));
        Assert.IsTrue(Base.IsValidSignificand("101"));
        Assert.IsFalse(Base.IsValidSignificand("10"));
        Assert.IsFalse(Base.IsValidSignificand("01"));

        bool HasCarry;
        string DivideResult;

        DivideResult = Base.DividedByTwo("10", out HasCarry);
        Assert.AreEqual("1", DivideResult);
        Assert.IsFalse(HasCarry);
        Assert.AreEqual("10", Base.MultipliedByTwo(DivideResult, HasCarry));

        DivideResult = Base.DividedByTwo("11", out HasCarry);
        Assert.AreEqual("1", DivideResult);
        Assert.IsTrue(HasCarry);
        Assert.AreEqual("11", Base.MultipliedByTwo(DivideResult, HasCarry));

        DivideResult = Base.DividedByTwo("1010", out HasCarry);
        Assert.AreEqual("101", DivideResult);
        Assert.IsFalse(HasCarry);
        Assert.AreEqual("1010", Base.MultipliedByTwo(DivideResult, HasCarry));

        Assert.Throws<ArgumentException>(() => { Base.DividedByTwo(string.Empty, out _); });
        Assert.Throws<ArgumentException>(() => { Base.MultipliedByTwo(string.Empty, true); });
        Assert.Throws<ArgumentException>(() => { Base.MultipliedByTwo(string.Empty, false); });

#if !DEBUG
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { Base.IsValidNumber(NullString); });
        Assert.Throws<ArgumentNullException>(() => { Base.IsValidSignificand(NullString); });
        Assert.Throws<ArgumentNullException>(() => { Base.DividedByTwo(NullString, out _); });
        Assert.Throws<ArgumentNullException>(() => { Base.MultipliedByTwo(NullString, false); });
#endif
    }

    [Test]
    [Category("Coverage")]
    public static void Hexadecimal()
    {
        HexadecimalIntegerBase Base = new();

        Assert.AreEqual(":H", Base.Suffix);

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

        Assert.IsFalse(Base.IsValidNumber(string.Empty));
        Assert.IsTrue(Base.IsValidNumber("0", true));
        Assert.IsTrue(Base.IsValidNumber("0", false));
        Assert.IsTrue(Base.IsValidNumber("9", true));
        Assert.IsTrue(Base.IsValidNumber("9", false));
        Assert.IsTrue(Base.IsValidNumber("a", true));
        Assert.IsTrue(Base.IsValidNumber("a", false));
        Assert.IsTrue(Base.IsValidNumber("f", true));
        Assert.IsTrue(Base.IsValidNumber("f", false));
        Assert.IsTrue(Base.IsValidNumber("A", true));
        Assert.IsTrue(Base.IsValidNumber("A", false));
        Assert.IsTrue(Base.IsValidNumber("F", true));
        Assert.IsTrue(Base.IsValidNumber("F", false));
        Assert.IsTrue(Base.IsValidNumber("123456789ABCDEFabcdef0", true));
        Assert.IsTrue(Base.IsValidNumber("123456789ABCDEFabcdef0", false));
        Assert.IsTrue(Base.IsValidNumber("0123456789ABCDEFabcdef0", true));
        Assert.IsFalse(Base.IsValidNumber("01", false));
        Assert.IsFalse(Base.IsValidNumber("*", false));

        Assert.IsFalse(Base.IsValidSignificand(string.Empty));
        Assert.IsTrue(Base.IsValidSignificand("0"));
        Assert.IsTrue(Base.IsValidSignificand("9"));
        Assert.IsTrue(Base.IsValidSignificand("a"));
        Assert.IsTrue(Base.IsValidSignificand("f"));
        Assert.IsTrue(Base.IsValidSignificand("A"));
        Assert.IsTrue(Base.IsValidSignificand("F"));
        Assert.IsFalse(Base.IsValidSignificand("."));
        Assert.IsTrue(Base.IsValidSignificand("109"));
        Assert.IsTrue(Base.IsValidSignificand("10a"));
        Assert.IsTrue(Base.IsValidSignificand("10f"));
        Assert.IsTrue(Base.IsValidSignificand("10A"));
        Assert.IsTrue(Base.IsValidSignificand("10F"));
        Assert.IsFalse(Base.IsValidSignificand("10"));
        Assert.IsFalse(Base.IsValidSignificand("01"));
        Assert.IsFalse(Base.IsValidSignificand("90"));
        Assert.IsFalse(Base.IsValidSignificand("09"));
        Assert.IsFalse(Base.IsValidSignificand("a0"));
        Assert.IsFalse(Base.IsValidSignificand("0a"));
        Assert.IsFalse(Base.IsValidSignificand("f0"));
        Assert.IsFalse(Base.IsValidSignificand("0f"));
        Assert.IsFalse(Base.IsValidSignificand("A0"));
        Assert.IsFalse(Base.IsValidSignificand("0A"));
        Assert.IsFalse(Base.IsValidSignificand("F0"));
        Assert.IsFalse(Base.IsValidSignificand("0F"));

        bool HasCarry;
        string DivideResult;

        DivideResult = Base.DividedByTwo("A", out HasCarry);
        Assert.AreEqual("5", DivideResult);
        Assert.IsFalse(HasCarry);
        Assert.AreEqual("A", Base.MultipliedByTwo(DivideResult, HasCarry));

        DivideResult = Base.DividedByTwo("B", out HasCarry);
        Assert.AreEqual("5", DivideResult);
        Assert.IsTrue(HasCarry);
        Assert.AreEqual("B", Base.MultipliedByTwo(DivideResult, HasCarry));

        DivideResult = Base.DividedByTwo("123456789ABCDEF", out HasCarry);
        Assert.AreEqual("91A2B3C4D5E6F7", DivideResult);
        Assert.IsTrue(HasCarry);
        Assert.AreEqual("123456789ABCDEF", Base.MultipliedByTwo(DivideResult, HasCarry));

        Assert.Throws<ArgumentException>(() => { Base.DividedByTwo(string.Empty, out _); });
        Assert.Throws<ArgumentException>(() => { Base.MultipliedByTwo(string.Empty, true); });
        Assert.Throws<ArgumentException>(() => { Base.MultipliedByTwo(string.Empty, false); });

#if !DEBUG
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { Base.IsValidNumber(NullString); });
        Assert.Throws<ArgumentNullException>(() => { Base.IsValidSignificand(NullString); });
        Assert.Throws<ArgumentNullException>(() => { Base.DividedByTwo(NullString, out _); });
        Assert.Throws<ArgumentNullException>(() => { Base.MultipliedByTwo(NullString, false); });
#endif
    }

    [Test]
    [Category("Coverage")]
    public static void Decimal()
    {
        DecimalIntegerBase Base = new();

        Assert.AreEqual(string.Empty, Base.Suffix);

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

        Assert.IsFalse(Base.IsValidNumber(string.Empty));
        Assert.IsTrue(Base.IsValidNumber("0", true));
        Assert.IsTrue(Base.IsValidNumber("0", false));
        Assert.IsTrue(Base.IsValidNumber("9", true));
        Assert.IsTrue(Base.IsValidNumber("9", false));
        Assert.IsFalse(Base.IsValidSignificand("A"));
        Assert.IsTrue(Base.IsValidNumber("123456789", true));
        Assert.IsTrue(Base.IsValidNumber("123456789", false));
        Assert.IsTrue(Base.IsValidNumber("0123456789", true));
        Assert.IsFalse(Base.IsValidNumber("01", false));
        Assert.IsFalse(Base.IsValidNumber("*", false));

        Assert.IsFalse(Base.IsValidSignificand(string.Empty));
        Assert.IsTrue(Base.IsValidSignificand("0"));
        Assert.IsTrue(Base.IsValidSignificand("9"));
        Assert.IsTrue(Base.IsValidSignificand("109"));
        Assert.IsFalse(Base.IsValidSignificand("10"));
        Assert.IsFalse(Base.IsValidSignificand("01"));
        Assert.IsFalse(Base.IsValidSignificand("90"));
        Assert.IsFalse(Base.IsValidSignificand("09"));

        bool HasCarry;
        string DivideResult;

        DivideResult = Base.DividedByTwo("4", out HasCarry);
        Assert.AreEqual("2", DivideResult);
        Assert.IsFalse(HasCarry);
        Assert.AreEqual("4", Base.MultipliedByTwo(DivideResult, HasCarry));

        DivideResult = Base.DividedByTwo("5", out HasCarry);
        Assert.AreEqual("2", DivideResult);
        Assert.IsTrue(HasCarry);
        Assert.AreEqual("5", Base.MultipliedByTwo(DivideResult, HasCarry));

        DivideResult = Base.DividedByTwo("123456789", out HasCarry);
        Assert.AreEqual("61728394", DivideResult);
        Assert.IsTrue(HasCarry);
        Assert.AreEqual("123456789", Base.MultipliedByTwo(DivideResult, HasCarry));

        Assert.Throws<ArgumentException>(() => { Base.DividedByTwo(string.Empty, out _); });
        Assert.Throws<ArgumentException>(() => { Base.MultipliedByTwo(string.Empty, true); });
        Assert.Throws<ArgumentException>(() => { Base.MultipliedByTwo(string.Empty, false); });

#if !DEBUG
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { Base.IsValidNumber(NullString); });
        Assert.Throws<ArgumentNullException>(() => { Base.IsValidSignificand(NullString); });
        Assert.Throws<ArgumentNullException>(() => { Base.DividedByTwo(NullString, out _); });
        Assert.Throws<ArgumentNullException>(() => { Base.MultipliedByTwo(NullString, false); });
#endif
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

        Assert.IsFalse(Base.IsValidNumber(string.Empty));
        Assert.IsTrue(Base.IsValidNumber("0", true));
        Assert.IsTrue(Base.IsValidNumber("0", false));
        Assert.IsTrue(Base.IsValidNumber("7", true));
        Assert.IsTrue(Base.IsValidNumber("7", false));
        Assert.IsFalse(Base.IsValidSignificand("8"));
        Assert.IsTrue(Base.IsValidNumber("1234567", true));
        Assert.IsTrue(Base.IsValidNumber("1234567", false));
        Assert.IsTrue(Base.IsValidNumber("01234567", true));
        Assert.IsFalse(Base.IsValidNumber("01", false));
        Assert.IsFalse(Base.IsValidNumber("*", false));

        Assert.IsFalse(Base.IsValidSignificand(string.Empty));
        Assert.IsTrue(Base.IsValidSignificand("0"));
        Assert.IsTrue(Base.IsValidSignificand("7"));
        Assert.IsTrue(Base.IsValidSignificand("107"));
        Assert.IsFalse(Base.IsValidSignificand("10"));
        Assert.IsFalse(Base.IsValidSignificand("01"));
        Assert.IsFalse(Base.IsValidSignificand("70"));
        Assert.IsFalse(Base.IsValidSignificand("07"));

        bool HasCarry;
        string DivideResult;

        DivideResult = Base.DividedByTwo("4", out HasCarry);
        Assert.AreEqual("2", DivideResult);
        Assert.IsFalse(HasCarry);
        Assert.AreEqual("4", Base.MultipliedByTwo(DivideResult, HasCarry));

        DivideResult = Base.DividedByTwo("5", out HasCarry);
        Assert.AreEqual("2", DivideResult);
        Assert.IsTrue(HasCarry);
        Assert.AreEqual("5", Base.MultipliedByTwo(DivideResult, HasCarry));

        DivideResult = Base.DividedByTwo("1234567", out HasCarry);
        Assert.AreEqual("516273", DivideResult);
        Assert.IsTrue(HasCarry);
        Assert.AreEqual("1234567", Base.MultipliedByTwo(DivideResult, HasCarry));

        Assert.Throws<ArgumentException>(() => { Base.DividedByTwo(string.Empty, out _); });
        Assert.Throws<ArgumentException>(() => { Base.MultipliedByTwo(string.Empty, true); });
        Assert.Throws<ArgumentException>(() => { Base.MultipliedByTwo(string.Empty, false); });

#if !DEBUG
        string NullString = null!;
        Assert.Throws<ArgumentNullException>(() => { Base.IsValidNumber(NullString); });
        Assert.Throws<ArgumentNullException>(() => { Base.IsValidSignificand(NullString); });
        Assert.Throws<ArgumentNullException>(() => { Base.DividedByTwo(NullString, out _); });
        Assert.Throws<ArgumentNullException>(() => { Base.MultipliedByTwo(NullString, false); });
#endif
    }
}
