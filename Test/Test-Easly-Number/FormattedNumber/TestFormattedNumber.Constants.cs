namespace TestFormattedNumber;

using EaslyNumber;
using NUnit.Framework;
using System;

[TestFixture]
public partial class TestFormattedNumber
{
    [Test]
    [Category("Coverage")]
    public static void Constants()
    {
        FormattedNumber NaNFormatted = FormattedNumber.NaN;
        FormattedNumber PositiveInfinityFormatted = FormattedNumber.PositiveInfinity;
        FormattedNumber NegativeInfinityFormatted = FormattedNumber.NegativeInfinity;

        CanonicalNumber NaNCanonical = CanonicalNumber.NaN;
        CanonicalNumber PositiveInfinityCanonical = CanonicalNumber.PositiveInfinity;
        CanonicalNumber NegativeInfinityCanonical = CanonicalNumber.NegativeInfinity;

        Assert.AreEqual(NaNFormatted, FormattedNumber.FromCanonical(NaNCanonical));
        Assert.AreEqual(PositiveInfinityFormatted, FormattedNumber.FromCanonical(PositiveInfinityCanonical));
        Assert.AreEqual(NegativeInfinityFormatted, FormattedNumber.FromCanonical(NegativeInfinityCanonical));

        Assert.AreEqual(NaNFormatted.Canonical, NaNCanonical);
        Assert.AreEqual(PositiveInfinityFormatted.Canonical, PositiveInfinityCanonical);
        Assert.AreEqual(NegativeInfinityFormatted.Canonical, NegativeInfinityCanonical);
    }
}
