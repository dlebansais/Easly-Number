namespace TestEaslyNumber2
{
    using EaslyNumber2;
    using NUnit.Framework;

    [TestFixture]
    public partial class TestNumber
    {
        [Test]
        [Category("Coverage")]
        public static void Ceil0()
        {
            Ceil(0.1);
            Ceil(0.5);
            Ceil(0.6);
            Ceil(1);
            Ceil(1.1);
            Ceil(1.5);

            Ceil(-0.1);
            Ceil(-0.5);
            Ceil(-0.6);
            Ceil(-1);
            Ceil(-1.1);
            Ceil(-1.5);
        }

        private static void Ceil(double d1)
        {
            Number Value1 = new Number(d1);

            double d = System.Math.Ceiling(d1);
            Number Value = Value1.Ceil();

            string DoubleString = GetString(d);
            string NumberString = GetString(Value);

            Assert.AreEqual(DoubleString, NumberString);

            Value = Value1.Round(Rounding.TowardPositiveInfinity);
            NumberString = GetString(Value);

            Assert.AreEqual(DoubleString, NumberString);
        }

        [Test]
        [Category("Coverage")]
        public static void Floor0()
        {
            Floor(0.1);
            Floor(0.5);
            Floor(0.6);
            Floor(1);
            Floor(1.1);
            Floor(1.5);

            Floor(-0.1);
            Floor(-0.5);
            Floor(-0.6);
            Floor(-1);
            Floor(-1.1);
            Floor(-1.5);
        }

        private static void Floor(double d1)
        {
            Number Value1 = new Number(d1);

            double d = System.Math.Floor(d1);
            Number Value = Value1.Floor();

            string DoubleString = GetString(d);
            string NumberString = GetString(Value);

            Assert.AreEqual(DoubleString, NumberString);

            Value = Value1.Round(Rounding.TowardNegativeInfinity);
            NumberString = GetString(Value);

            Assert.AreEqual(DoubleString, NumberString);
        }

        [Test]
        [Category("Coverage")]
        public static void RoundAwayFromZero0()
        {
            RoundAwayFromZero(0.1);
            RoundAwayFromZero(0.5);
            RoundAwayFromZero(0.6);
            RoundAwayFromZero(1);
            RoundAwayFromZero(1.1);
            RoundAwayFromZero(1.5);

            RoundAwayFromZero(-0.1);
            RoundAwayFromZero(-0.5);
            RoundAwayFromZero(-0.6);
            RoundAwayFromZero(-1);
            RoundAwayFromZero(-1.1);
            RoundAwayFromZero(-1.5);
        }

        private static void RoundAwayFromZero(double d1)
        {
            Number Value1 = new Number(d1);

            double d = System.Math.Round(d1, System.MidpointRounding.AwayFromZero);
            Number Value = Value1.Round();

            string DoubleString = GetString(d);
            string NumberString = GetString(Value);

            Assert.AreEqual(DoubleString, NumberString);

            Value = Value1.Round(Rounding.NearestAwayFromZero);
            NumberString = GetString(Value);

            Assert.AreEqual(DoubleString, NumberString);
        }

        [Test]
        [Category("Coverage")]
        public static void RoundToEven0()
        {
            RoundToEven(0.1);
            RoundToEven(0.5);
            RoundToEven(0.6);
            RoundToEven(1);
            RoundToEven(1.1);
            RoundToEven(1.5);

            RoundToEven(-0.1);
            RoundToEven(-0.5);
            RoundToEven(-0.6);
            RoundToEven(-1);
            RoundToEven(-1.1);
            RoundToEven(-1.5);
        }

        private static void RoundToEven(double d1)
        {
            Number Value1 = new Number(d1);

            double d = System.Math.Round(d1, System.MidpointRounding.ToEven);
            Number Value = Value1.RoundEven();

            string DoubleString = GetString(d);
            string NumberString = GetString(Value);

            Assert.AreEqual(DoubleString, NumberString);

            Value = Value1.Round(Rounding.Nearest);
            NumberString = GetString(Value);

            Assert.AreEqual(DoubleString, NumberString);
        }

        [Test]
        [Category("Coverage")]
        public static void Trunc0()
        {
            Trunc(0.1);
            Trunc(0.5);
            Trunc(0.6);
            Trunc(1);
            Trunc(1.1);
            Trunc(1.5);

            Trunc(-0.1);
            Trunc(-0.5);
            Trunc(-0.6);
            Trunc(-1);
            Trunc(-1.1);
            Trunc(-1.5);
        }

        private static void Trunc(double d1)
        {
            Number Value1 = new Number(d1);

            double d = System.Math.Truncate(d1);
            Number Value = Value1.Trunc();

            string DoubleString = GetString(d);
            string NumberString = GetString(Value);

            Assert.AreEqual(DoubleString, NumberString);

            Value = Value1.Round(Rounding.TowardZero);
            NumberString = GetString(Value);

            Assert.AreEqual(DoubleString, NumberString);
        }

        private static string GetString(double d)
        {
            string DoubleString = d.ToString();
            return GetFixedString(DoubleString);
        }

        private static string GetString(Number value)
        {
            string NumberString = value.ToString();
            return GetFixedString(NumberString);
        }

        private static string GetFixedString(string s)
        {
            if (s == "-0")
                s = "0";

            return s;
        }
    }
}
