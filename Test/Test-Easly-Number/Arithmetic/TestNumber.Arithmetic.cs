namespace TestEaslyNumber
{
    using EaslyNumber;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public partial class TestNumberArithmetic
    {
        [Test]
        [Category("Coverage")]
        public static void Neg0()
        {
            double d1 = 5.478231405e-3;

            Number Value1 = new Number(d1);

            double d = -d1;
            Number Value = -Value1;

            string DoubleString = d.ToString("G17").Substring(0, 15);
            string NumberString = Value.ToString("G17").Substring(0, 15);

            Assert.AreEqual(DoubleString, NumberString);

            Value = Number.Negate(Value1, Value1.Precision, Rounding.Nearest);
            NumberString = Value.ToString("G17").Substring(0, 15);

            Assert.AreEqual(DoubleString, NumberString);
        }

        [Test]
        [Category("Coverage")]
        public static void Abs0()
        {
            double d1 = -5.478231405e-3;

            Number Value1 = new Number(d1);

            double d = System.Math.Abs(d1);
            Number Value = Value1.Abs();

            string DoubleString = d.ToString("G17").Substring(0, 15);
            string NumberString = Value.ToString("G17").Substring(0, 15);

            Assert.AreEqual(DoubleString, NumberString);
        }

        [Test]
        [Category("Coverage")]
        public static void Exp0()
        {
            double d1 = 1.2547856e2;

            Number Value1 = new Number(d1);

            double d = System.Math.Exp(d1);
            Number Value = Value1.Exp();

            string DoubleString = d.ToString("G17").Substring(0, 15);
            string NumberString = Value.ToString("G17").Substring(0, 15);

            Assert.AreEqual(DoubleString, NumberString);
        }

        [Test]
        [Category("Coverage")]
        public static void Log0()
        {
            double d1 = 1.2547856e2;

            Number Value1 = new Number(d1);

            double d = System.Math.Log(d1);
            Number Value = Value1.Log();

            string DoubleString = d.ToString("G17").Substring(0, 15);
            string NumberString = Value.ToString("G17").Substring(0, 15);

            Assert.AreEqual(DoubleString, NumberString);
        }

        [Test]
        [Category("Coverage")]
        public static void Log10_0()
        {
            double d1 = 1.2547856e2;

            Number Value1 = new Number(d1);

            double d = System.Math.Log10(d1);
            Number Value = Value1.Log10();

            string DoubleString = d.ToString("G17").Substring(0, 15);
            string NumberString = Value.ToString("G17").Substring(0, 15);

            Assert.AreEqual(DoubleString, NumberString);
        }

        [Test]
        [Category("Coverage")]
        public static void Pow0()
        {
            double d1 = 1.2547856e2;
            double d2 = 5.478231405e-3;

            Number Value1 = new Number(d1);
            Number Value2 = new Number(d2);

            double d = System.Math.Pow(d1, d2);
            Number Value = Value1.Pow(Value2);

            string DoubleString = d.ToString("G17").Substring(0, 15);
            string NumberString = Value.ToString("G17").Substring(0, 15);

            Assert.AreEqual(DoubleString, NumberString);
        }

        [Test]
        [Category("Coverage")]
        public static void Sqrt0()
        {
            double d1 = 1.2547856e2;

            Number Value1 = new Number(d1);

            double d = System.Math.Sqrt(d1);
            Number Value = Value1.Sqrt();

            string DoubleString = d.ToString("G17").Substring(0, 15);
            string NumberString = Value.ToString("G17").Substring(0, 15);

            Assert.AreEqual(DoubleString, NumberString);
        }

        [Test]
        [Category("Coverage")]
        public static void Remainder0()
        {
            double d1 = 1.2547856e2;
            double d2 = 5.478231405e-3;

            Number Value1 = new Number(d1);
            Number Value2 = new Number(d2);

            double d = System.Math.IEEERemainder(d1, d2);
            Number Value = Value1.Remainder(Value2);

            string DoubleString = d.ToString("G17").Substring(0, 15);
            string NumberString = Value.ToString("G17").Substring(0, 15);

            Assert.AreEqual(DoubleString, NumberString);
        }

        [Test]
        [Category("Coverage")]
        public static void ShiftLeft()
        {
            int n1 = 123456;
            int n2 = 5;

            Number Value1 = new Number(n1);

            double d = n1 << n2;
            Number Value = Value1 << n2;

            string DoubleString = d.ToString();
            string NumberString = Value.ToString();

            Assert.AreEqual(DoubleString, NumberString);

            Value = Number.ShiftLeft(Value1, n2, Value1.Precision, Rounding.Nearest);
            NumberString = Value.ToString();

            Assert.AreEqual(DoubleString, NumberString);
        }

        [Test]
        [Category("Coverage")]
        public static void ShiftRight()
        {
            int n1 = 123456;
            int n2 = 5;

            Number Value1 = new Number(n1);

            double d = n1 >> n2;
            Number Value = Value1 >> n2;

            string DoubleString = d.ToString();
            string NumberString = Value.ToString();

            Assert.AreEqual(DoubleString, NumberString);

            Value = Number.ShiftRight(Value1, n2, Value1.Precision, Rounding.Nearest);
            NumberString = Value.ToString();

            Assert.AreEqual(DoubleString, NumberString);
        }

        [Test]
        [Category("Coverage")]
        public static void BitwiseAnd()
        {
            int n1 = 12345;
            int n2 = 25;

            Number Value1 = new Number(n1);
            Number Value2 = new Number(n2);

            int n = n1 & n2;
            Number Value = Value1 & Value2;

            string IntString = n.ToString();
            string NumberString = Value.ToString();

            Assert.AreEqual(IntString, NumberString);
        }

        [Test]
        [Category("Coverage")]
        public static void BitwiseOr()
        {
            int n1 = 12345;
            int n2 = 25;

            Number Value1 = new Number(n1);
            Number Value2 = new Number(n2);

            int n = n1 | n2;
            Number Value = Value1 | Value2;

            string IntString = n.ToString();
            string NumberString = Value.ToString();

            Assert.AreEqual(IntString, NumberString);
        }

        [Test]
        [Category("Coverage")]
        public static void BitwiseXor()
        {
            int n1 = 12345;
            int n2 = 25;

            Number Value1 = new Number(n1);
            Number Value2 = new Number(n2);

            int n = n1 ^ n2;
            Number Value = Value1 ^ Value2;

            string IntString = n.ToString();
            string NumberString = Value.ToString();

            Assert.AreEqual(IntString, NumberString);
        }

        [Test]
        [Category("Coverage")]
        public static void Bitwise()
        {
            double d1 = 1.2547856e2;
            double d2 = 5.478231405e-3;

            Number Value1 = new Number(d1);
            Number Value2 = new Number(d2);

            Exception? exNumber;
            string Message = "Value does not fall within the expected range.";

            exNumber = Assert.Throws<ArgumentException>(() => { Number Value = Value1 & Value2; });
            Assert.That(exNumber?.Message == Message, $"Expected: '{Message}', got: '{exNumber?.Message}'");

            exNumber = Assert.Throws<ArgumentException>(() => { Number Value = Value1 | Value2; });
            Assert.That(exNumber?.Message == Message, $"Expected: '{Message}', got: '{exNumber?.Message}'");

            exNumber = Assert.Throws<ArgumentException>(() => { Number Value = Value1 ^ Value2; });
            Assert.That(exNumber?.Message == Message, $"Expected: '{Message}', got: '{exNumber?.Message}'");
        }
    }
}
