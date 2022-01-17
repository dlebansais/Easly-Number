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
            Assert.That(Text == TestEnvironment.TextPositiveInfinity, $"Expected: {TestEnvironment.TextPositiveInfinity}, got: {Text}");
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
            Assert.That(Text == TestEnvironment.TextNegativeInfinity, $"Expected: {TestEnvironment.TextNegativeInfinity}, got: {Text}");
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
            Assert.That(Text == TestEnvironment.TextNaN, $"Expected: {TestEnvironment.TextNaN}, got: {Text}");
        }

        [Test]
        [Category("Coverage")]
        public static void TestInitTextNaN()
        {
            Number n = new Number(TestEnvironment.TextNaN);

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
        public static void TestInitTextPositiveInfinity()
        {
            Number n = new Number(TestEnvironment.TextPositiveInfinity);

            Assert.IsTrue(n.IsInfinite);
            Assert.IsFalse(n.IsInteger);
            Assert.IsFalse(n.IsNaN);
            Assert.IsFalse(n.IsNegativeInfinity);
            Assert.IsTrue(n.IsPositiveInfinity);
            Assert.IsTrue(n.IsSpecial);
            Assert.IsFalse(n.IsZero);

            string Text = n.ToString();
            Assert.That(Text == TestEnvironment.TextPositiveInfinity, $"Expected: {TestEnvironment.TextPositiveInfinity}, got: {Text}");
        }

        [Test]
        [Category("Coverage")]
        public static void TestInitTextNegativeInfinity()
        {
            Number n = new Number(TestEnvironment.TextNegativeInfinity);

            Assert.IsTrue(n.IsInfinite);
            Assert.IsFalse(n.IsInteger);
            Assert.IsFalse(n.IsNaN);
            Assert.IsTrue(n.IsNegativeInfinity);
            Assert.IsFalse(n.IsPositiveInfinity);
            Assert.IsTrue(n.IsSpecial);
            Assert.IsFalse(n.IsZero);

            string Text = n.ToString();
            Assert.That(Text == TestEnvironment.TextNegativeInfinity, $"Expected: {TestEnvironment.TextNegativeInfinity}, got: {Text}");
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
            Assert.IsTrue(n.Sign == 0);

            string Text = n.ToString();
            Assert.That(Text == "0", $"Expected: 0, got: {Text}");
        }

        [Test]
        [Category("Coverage")]
        public static void TestInitTryParse()
        {
            bool Success = Number.TryParse("0", out Number n0);
            Assert.IsTrue(Success);

            Assert.IsFalse(n0.IsInfinite);
            Assert.IsTrue(n0.IsInteger);
            Assert.IsFalse(n0.IsNaN);
            Assert.IsFalse(n0.IsNegativeInfinity);
            Assert.IsFalse(n0.IsPositiveInfinity);
            Assert.IsFalse(n0.IsSpecial);
            Assert.IsTrue(n0.IsZero);
            Assert.IsTrue(n0.Sign == 0);

            string Text = n0.ToString();
            Assert.That(Text == "0", $"Expected: 0, got: {Text}");

            Success = Number.TryParse("!", out _);
            Assert.IsFalse(Success);

            Success = Number.TryParse(CultureInfo.CurrentCulture.NumberFormat.NaNSymbol, out Number n2);
            Assert.IsTrue(Success);
            Assert.IsTrue(n2.IsNaN);

            Success = Number.TryParse(CultureInfo.CurrentCulture.NumberFormat.PositiveInfinitySymbol, out Number n3);
            Assert.IsTrue(Success);
            Assert.IsTrue(n3.IsPositiveInfinity);

            Success = Number.TryParse(CultureInfo.CurrentCulture.NumberFormat.NegativeInfinitySymbol, out Number n4);
            Assert.IsTrue(Success);
            Assert.IsTrue(n4.IsNegativeInfinity);

            Success = Number.TryParse("0", out Number n5, precision: 53, rounding: Rounding.TowardZero);
            Assert.IsTrue(Success);
            Assert.IsTrue(n5.IsZero);

            Success = Number.TryParse(string.Empty, out _);
            Assert.IsFalse(Success);
        }

        [Test]
        [Category("Coverage")]
        public static void TestToString()
        {
            double d = 1.2547856e2;
            Number Value = new Number(d);

            double d1 = Math.Exp(d);
            Number Value1 = Value.Exp();

            string DoubleString0 = d1.ToString("G17").Substring(0, 15);
            string NumberString0 = Value1.ToString("G17").Substring(0, 15);

            Assert.AreEqual(DoubleString0, NumberString0);

            string DoubleString1 = d1.ToString("g17").Substring(0, 15);
            string NumberString1 = Value1.ToString("g17").Substring(0, 15);

            Assert.AreEqual(DoubleString1, NumberString1);

            string DoubleString2 = d1.ToString("E17").Substring(0, 15);
            string NumberString2 = Value1.ToString("E17").Substring(0, 15);

            Assert.AreEqual(DoubleString2, NumberString2);

            string DoubleString3 = d1.ToString("e17").Substring(0, 15);
            string NumberString3 = Value1.ToString("e17").Substring(0, 15);

            Assert.AreEqual(DoubleString3, NumberString3);

            string NumberString4 = Value1.ToString(null, CultureInfo.InvariantCulture);
        }

        [Test]
        [Category("Coverage")]
        public static void TestInitInteger0()
        {
            int p = 0;
            Number n = new Number(p);

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
        public static void TestInitInteger1()
        {
            uint p = 0;
            Number n = new Number(p);

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
        public static void TestInitInteger2()
        {
            long p = 0;
            Number n = new Number(p);

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
        public static void TestInitInteger3()
        {
            ulong p = 0;
            Number n = new Number(p);

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
        public static void TestPrecision()
        {
            ulong DefaultPrecision = Number.DefaultPrecision;
            Assert.That(DefaultPrecision == 53, $"Expected: 53, got: {DefaultPrecision}");

            Number.DefaultPrecision = 64;

            DefaultPrecision = Number.DefaultPrecision;
            Assert.That(DefaultPrecision == 64, $"Expected: 64, got: {DefaultPrecision}");

            Number n = new Number(1.0);
            Assert.That(n.Precision == 53, $"Expected: 53, got: {n.Precision}");

            n = new Number("1");
            Assert.That(n.Precision == 64, $"Expected: 64, got: {n.Precision}");

            n.Precision = 53;
            Assert.That(n.Precision == 53, $"Expected: 53, got: {n.Precision}");

            Number.ResetDefaultPrecision();

            DefaultPrecision = Number.DefaultPrecision;
            Assert.That(DefaultPrecision == 53, $"Expected: 53, got: {DefaultPrecision}");

            n = new Number("1");
            Assert.That(n.Precision == 53, $"Expected: 53, got: {n.Precision}");
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
