namespace TestNumber;

using EaslyNumber;
using NUnit.Framework;
using System;

[TestFixture]
public partial class TestFormattedNumber
{
    [Test]
    [Category("Coverage")]
    public static void Conversion()
    {
        FormattedNumber Formatted = FormattedNumber.Parse("01.2e3");
        Assert.That(Formatted is FormattedReal);

        FormattedReal NumberAsReal = (FormattedReal)Formatted;
        Assert.AreEqual(OptionalSign.None, NumberAsReal.Sign);
        Assert.AreEqual(1, NumberAsReal.LeadingZeroCount);
        Assert.AreEqual("1", NumberAsReal.IntegerText);
        Assert.AreEqual('.', NumberAsReal.SeparatorCharacter);
        Assert.AreEqual("2", NumberAsReal.FractionalText);
        Assert.AreEqual('e', NumberAsReal.ExponentCharacter);
        Assert.AreEqual(OptionalSign.None, NumberAsReal.ExponentSign);
        Assert.AreEqual("3", NumberAsReal.ExponentText);
        Assert.AreEqual("01.2e", NumberAsReal.SignificandPart);
        Assert.AreEqual("3", NumberAsReal.ExponentPart);
        Assert.True(NumberAsReal.IsValid);
        Assert.AreEqual(string.Empty, NumberAsReal.InvalidText);
        Assert.AreEqual("/0/1/46/2/101//3/", NumberAsReal.Diagnostic);

        Number Number = Formatted.Value;
        Assert.AreEqual("1200", Number.ToString());
    }
}
