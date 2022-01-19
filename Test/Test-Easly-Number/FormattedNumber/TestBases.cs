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
    }
}
