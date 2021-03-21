namespace TestEaslyNumber
{
    using EaslyNumber;
    using NUnit.Framework;
    using System;
    using System.Globalization;

    [TestFixture]
    public partial class TestNumber
    {
        [Test]
        [Category("Coverage")]
        public static void TestCopyNaN()
        {
            Number n = Number.NaN;

            Assert.IsFalse(n.IsInfinite);
            Assert.IsFalse(n.IsInteger);
            Assert.IsTrue(n.IsNaN);
            Assert.IsFalse(n.IsNegativeInfinity);
            Assert.IsFalse(n.IsPositiveInfinity);
            Assert.IsTrue(n.IsSpecial);
            Assert.IsFalse(n.IsZero);

            string Text = n.ToString();
            Assert.That(Text == TestEnvironment.TextNaN, $"Expected: {TestEnvironment.TextNaN}, got: {Text}");
        }

        [Test]
        [Category("Coverage")]
        public static void TestCopyPositiveInfinity()
        {
            Number n = Number.PositiveInfinity;

            Assert.IsTrue(n.IsInfinite);
            Assert.IsFalse(n.IsInteger);
            Assert.IsFalse(n.IsNaN);
            Assert.IsFalse(n.IsNegativeInfinity);
            Assert.IsTrue(n.IsPositiveInfinity);
            Assert.IsTrue(n.IsSpecial);
            Assert.IsFalse(n.IsZero);

            string Text = n.ToString();
            Assert.That(Text == TestEaslyNumber.TestEnvironment.TextPositiveInfinity, $"Expected: {TestEaslyNumber.TestEnvironment.TextPositiveInfinity}, got: {Text}");
        }

        [Test]
        [Category("Coverage")]
        public static void TestCopyNegativeInfinity()
        {
            Number n = Number.NegativeInfinity;

            Assert.IsTrue(n.IsInfinite);
            Assert.IsFalse(n.IsInteger);
            Assert.IsFalse(n.IsNaN);
            Assert.IsTrue(n.IsNegativeInfinity);
            Assert.IsFalse(n.IsPositiveInfinity);
            Assert.IsTrue(n.IsSpecial);
            Assert.IsFalse(n.IsZero);

            string Text = n.ToString();
            Assert.That(Text == TestEaslyNumber.TestEnvironment.TextNegativeInfinity, $"Expected: {TestEaslyNumber.TestEnvironment.TextNegativeInfinity}, got: {Text}");
        }

        [Test]
        [Category("Coverage")]
        public static void TestCopyZero()
        {
            Number n = Number.Zero;

            Assert.IsFalse(n.IsInfinite);
            Assert.IsTrue(n.IsInteger);
            Assert.IsFalse(n.IsNaN);
            Assert.IsFalse(n.IsNegativeInfinity);
            Assert.IsFalse(n.IsPositiveInfinity);
            Assert.IsFalse(n.IsSpecial);
            Assert.IsTrue(n.IsZero);

            string Text = n.ToString();
            Assert.That(Text == "0", $"Expected: 0, got: {Text}");
        }

        [Test]
        [Category("Coverage")]
        public static void TestInit()
        {
            Number n = new Number();

            Assert.IsFalse(n.IsInfinite);
            Assert.IsFalse(n.IsInteger);
            Assert.IsTrue(n.IsNaN);
            Assert.IsFalse(n.IsNegativeInfinity);
            Assert.IsFalse(n.IsPositiveInfinity);
            Assert.IsTrue(n.IsSpecial);
            Assert.IsFalse(n.IsZero);

            string Text = n.ToString();
            Assert.That(Text == TestEaslyNumber.TestEnvironment.TextNaN, $"Expected: {TestEaslyNumber.TestEnvironment.TextNaN}, got: {Text}");
        }

        [Test]
        [Category("Coverage")]
        public static void TestInitTextNaN()
        {
            Number n = new Number(TestEaslyNumber.TestEnvironment.TextNaN);

            Assert.IsFalse(n.IsInfinite);
            Assert.IsFalse(n.IsInteger);
            Assert.IsTrue(n.IsNaN);
            Assert.IsFalse(n.IsNegativeInfinity);
            Assert.IsFalse(n.IsPositiveInfinity);
            Assert.IsTrue(n.IsSpecial);
            Assert.IsFalse(n.IsZero);

            string Text = n.ToString();
            Assert.That(Text == TestEaslyNumber.TestEnvironment.TextNaN, $"Expected: {TestEaslyNumber.TestEnvironment.TextNaN}, got: {Text}");
        }

        [Test]
        [Category("Coverage")]
        public static void TestInitTextPositiveInfinity()
        {
            Number n = new Number(TestEaslyNumber.TestEnvironment.TextPositiveInfinity);

            Assert.IsTrue(n.IsInfinite);
            Assert.IsFalse(n.IsInteger);
            Assert.IsFalse(n.IsNaN);
            Assert.IsFalse(n.IsNegativeInfinity);
            Assert.IsTrue(n.IsPositiveInfinity);
            Assert.IsTrue(n.IsSpecial);
            Assert.IsFalse(n.IsZero);

            string Text = n.ToString();
            Assert.That(Text == TestEaslyNumber.TestEnvironment.TextPositiveInfinity, $"Expected: {TestEaslyNumber.TestEnvironment.TextPositiveInfinity}, got: {Text}");
        }

        [Test]
        [Category("Coverage")]
        public static void TestInitTextNegativeInfinity()
        {
            Number n = new Number(TestEaslyNumber.TestEnvironment.TextNegativeInfinity);

            Assert.IsTrue(n.IsInfinite);
            Assert.IsFalse(n.IsInteger);
            Assert.IsFalse(n.IsNaN);
            Assert.IsTrue(n.IsNegativeInfinity);
            Assert.IsFalse(n.IsPositiveInfinity);
            Assert.IsTrue(n.IsSpecial);
            Assert.IsFalse(n.IsZero);

            string Text = n.ToString();
            Assert.That(Text == TestEaslyNumber.TestEnvironment.TextNegativeInfinity, $"Expected: {TestEaslyNumber.TestEnvironment.TextNegativeInfinity}, got: {Text}");
        }

        [Test]
        [Category("Coverage")]
        public static void TestInitTextZero()
        {
            Number n = new Number("0");

            Assert.IsFalse(n.IsInfinite);
            Assert.IsTrue(n.IsInteger);
            Assert.IsFalse(n.IsNaN);
            Assert.IsFalse(n.IsNegativeInfinity);
            Assert.IsFalse(n.IsPositiveInfinity);
            Assert.IsFalse(n.IsSpecial);
            Assert.IsTrue(n.IsZero);

            string Text = n.ToString();
            Assert.That(Text == "0", $"Expected: 0, got: {Text}");
        }

        [Test]
        [Category("Coverage")]
        public static void TestInitTextCombo()
        {
            TestInitTextCombo("");
            TestInitTextCombo(" ");
            TestInitTextCombo("  ");
        }

        private static void TestInitTextCombo(string prefix)
        {
            TestInitTextCombo(prefix, "");
            TestInitTextCombo(prefix, "-");
            TestInitTextCombo(prefix, "+");
        }

        private static void TestInitTextCombo(string prefix, string sign)
        {
            TestInitTextCombo(prefix, sign, "");
            TestInitTextCombo(prefix, sign, "0");
            TestInitTextCombo(prefix, sign, "01");
            TestInitTextCombo(prefix, sign, "1");
            TestInitTextCombo(prefix, sign, "10");
            TestInitTextCombo(prefix, sign, "9");
            TestInitTextCombo(prefix, sign, $"9{CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator}0");
            TestInitTextCombo(prefix, sign, $"9 0");
        }

        private static void TestInitTextCombo(string prefix, string sign, string integral)
        {
            TestInitTextCombo(prefix, sign, integral, true);
            TestInitTextCombo(prefix, sign, integral, false);
        }

        private static void TestInitTextCombo(string prefix, string sign, string integral, bool hasFractional)
        {
            if (integral.Length > 0)
                TestInitTextCombo(prefix, sign, integral, hasFractional, "");

            if (hasFractional)
            {
                TestInitTextCombo(prefix, sign, integral, hasFractional, "0");
                TestInitTextCombo(prefix, sign, integral, hasFractional, "01");
                TestInitTextCombo(prefix, sign, integral, hasFractional, "1");
                TestInitTextCombo(prefix, sign, integral, hasFractional, "10");
                TestInitTextCombo(prefix, sign, integral, hasFractional, "9");
            }
        }

        private static void TestInitTextCombo(string prefix, string sign, string integral, bool hasFractional, string fractional)
        {
            TestInitTextCombo(prefix, sign, integral, hasFractional, fractional, true);
            TestInitTextCombo(prefix, sign, integral, hasFractional, fractional, false);
        }

        private static void TestInitTextCombo(string prefix, string sign, string integral, bool hasFractional, string fractional, bool hasExponent)
        {
            TestInitTextCombo(prefix, sign, integral, hasFractional, fractional, hasExponent, "");

            if (hasExponent)
            {
                TestInitTextCombo(prefix, sign, integral, hasFractional, fractional, hasExponent, "-");
                TestInitTextCombo(prefix, sign, integral, hasFractional, fractional, hasExponent, "+");
            }
        }

        private static void TestInitTextCombo(string prefix, string sign, string integral, bool hasFractional, string fractional, bool hasExponent, string exponentSign)
        {
            TestInitTextCombo(prefix, sign, integral, hasFractional, fractional, hasExponent, exponentSign, "0");
            TestInitTextCombo(prefix, sign, integral, hasFractional, fractional, hasExponent, exponentSign, "1");
            TestInitTextCombo(prefix, sign, integral, hasFractional, fractional, hasExponent, exponentSign, "10");
            TestInitTextCombo(prefix, sign, integral, hasFractional, fractional, hasExponent, exponentSign, "01");
            TestInitTextCombo(prefix, sign, integral, hasFractional, fractional, hasExponent, exponentSign, "9");
        }

        private static void TestInitTextCombo(string prefix, string sign, string integral, bool hasFractional, string fractional, bool hasExponent, string exponentSign, string exponent)
        {
            TestInitTextCombo(prefix, sign, integral, hasFractional, fractional, hasExponent, exponentSign, exponent, "");
            TestInitTextCombo(prefix, sign, integral, hasFractional, fractional, hasExponent, exponentSign, exponent, " ");
            TestInitTextCombo(prefix, sign, integral, hasFractional, fractional, hasExponent, exponentSign, exponent, "  ");
        }

        private static void TestInitTextCombo(string prefix, string sign, string integral, bool hasFractional, string fractional, bool hasExponent, string exponentSign, string exponent, string suffix)
        {
            string Text = $"{prefix}{sign}{integral}";

            if (hasFractional)
                Text += $"{CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator}{fractional}";

            if (hasExponent)
                Text += $"E{exponentSign}{exponent}";

            Text += suffix;

            TestInitTextFormatted(Text);
        }

        private static void TestInitTextFormatted(string text)
        {
            Number n = new Number(text);

            Assert.IsFalse(n.IsInfinite);
            Assert.IsFalse(n.IsNaN);
            Assert.IsFalse(n.IsNegativeInfinity);
            Assert.IsFalse(n.IsPositiveInfinity);
            Assert.IsFalse(n.IsSpecial);
        }
    }
}
