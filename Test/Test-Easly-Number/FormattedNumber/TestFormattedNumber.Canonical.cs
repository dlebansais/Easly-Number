namespace TestFormattedNumber;

using EaslyNumber;
using NUnit.Framework;
using System;

[TestFixture]
public partial class TestFormattedNumber
{
    [Test]
    [Category("Coverage")]
    public static void Canonical()
    {
        CanonicalNumber Canonical;
        FormattedNumber Formatted;

        Canonical = new CanonicalNumber(OptionalSign.None, "1");
        Formatted = FormattedNumber.FromCanonical(Canonical);
        Assert.IsTrue(Formatted.IsValid);

        Canonical = new CanonicalNumber(OptionalSign.None, "0", OptionalSign.None, "0");
        Formatted = FormattedNumber.FromCanonical(Canonical);
        Assert.IsTrue(Formatted.IsValid);

        Canonical = new CanonicalNumber(OptionalSign.None, "1", OptionalSign.None, "0");
        Formatted = FormattedNumber.FromCanonical(Canonical);
        Assert.IsTrue(Formatted.IsValid);

        Canonical = new CanonicalNumber(0);
        Formatted = FormattedNumber.FromCanonical(Canonical);
        Assert.IsTrue(Formatted.IsValid);

        Canonical = new CanonicalNumber(-1);
        Formatted = FormattedNumber.FromCanonical(Canonical);
        Assert.IsTrue(Formatted.IsValid);

        Canonical = new CanonicalNumber(+1);
        Formatted = FormattedNumber.FromCanonical(Canonical);
        Assert.IsTrue(Formatted.IsValid);

        Canonical = CanonicalNumber.FromEFloat(Canonical.NumberFloat);
        Formatted = FormattedNumber.FromCanonical(Canonical);
        Assert.IsTrue(Formatted.IsValid);

        Canonical = CanonicalNumber.FromEFloat(Number.NaN);
        Formatted = FormattedNumber.FromCanonical(Canonical);
        Assert.AreEqual(FormattedNumber.NaN, Formatted);

        Canonical = CanonicalNumber.FromEFloat(Number.PositiveInfinity);
        Formatted = FormattedNumber.FromCanonical(Canonical);
        Assert.AreEqual(FormattedNumber.PositiveInfinity, Formatted);

        Canonical = CanonicalNumber.FromEFloat(Number.NegativeInfinity);
        Formatted = FormattedNumber.FromCanonical(Canonical);
        Assert.AreEqual(FormattedNumber.NegativeInfinity, Formatted);
    }

    [Test]
    [Category("Coverage")]
    public static void CanonicalComparison()
    {
        CanonicalNumber Canonical1 = new CanonicalNumber(OptionalSign.None, "1");
        CanonicalNumber Canonical2 = new CanonicalNumber(OptionalSign.None, "2");
        CanonicalNumber Canonical3 = new CanonicalNumber(OptionalSign.None, "1");

        Assert.IsFalse(Canonical1.IsEqual(Canonical2));
        Assert.IsTrue(Canonical1.IsEqual(Canonical3));

        Assert.IsFalse(Canonical1 > Canonical2);
        Assert.IsTrue(Canonical1 < Canonical2);
    }
}
