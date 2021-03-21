namespace TestEaslyNumber
{
    using EaslyNumber;
    using NUnit.Framework;

    [TestFixture]
    public partial class TestNumber
    {
        [Test]
        [Category("Coverage")]
        public static void Add0()
        {
            double d1 = 125478;
            double d2 = 5.478231405e-3;

            Number Value1 = new Number(d1);
            Number Value2= new Number(d2);

            double d = d1 + d2;
            Number Value = Value1 + Value2;

            string DoubleString = d.ToString("G17").Substring(0, 15);
            string NumberString = Value.ToString("G17").Substring(0, 15);

            Assert.AreEqual(DoubleString, NumberString);

            Value = Number.Add(Value1, Value2, Value1.Precision, Rounding.Nearest);
            NumberString = Value.ToString("G17").Substring(0, 15);

            Assert.AreEqual(DoubleString, NumberString);
        }

        [Test]
        [Category("Coverage")]
        public static void Sub0()
        {
            double d1 = 125478;
            double d2 = 5.478231405e-3;

            Number Value1 = new Number(d1);
            Number Value2 = new Number(d2);

            double d = d1 - d2;
            Number Value = Value1 - Value2;

            string DoubleString = d.ToString("G17").Substring(0, 15);
            string NumberString = Value.ToString("G17").Substring(0, 15);

            Assert.AreEqual(DoubleString, NumberString);

            Value = Number.Subtract(Value1, Value2, Value1.Precision, Rounding.Nearest);
            NumberString = Value.ToString("G17").Substring(0, 15);

            Assert.AreEqual(DoubleString, NumberString);
        }

        [Test]
        [Category("Coverage")]
        public static void Mul0()
        {
            double d1 = 125478;
            double d2 = 5.478231405e-3;

            Number Value1 = new Number(d1);
            Number Value2 = new Number(d2);

            double d = d1 * d2;
            Number Value = Value1 * Value2;

            string DoubleString = d.ToString("G17").Substring(0, 15);
            string NumberString = Value.ToString("G17").Substring(0, 15);

            Assert.AreEqual(DoubleString, NumberString);

            Value = Number.Multiply(Value1, Value2, Value1.Precision, Rounding.Nearest);
            NumberString = Value.ToString("G17").Substring(0, 15);

            Assert.AreEqual(DoubleString, NumberString);
        }

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
    }
}
