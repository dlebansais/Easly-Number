namespace TestEaslyNumber
{
    using EaslyNumber;
    using NUnit.Framework;

    [TestFixture]
    public partial class TestNumber
    {
        [Test]
        [Category("Coverage")]
        public static void Greater0()
        {
            double d1 = 125478;
            double d2 = 5.478231405e-3;

            Number Value1 = new Number(d1);
            Number Value2= new Number(d2);

            bool IsDoubleGreater = d1 > d2;
            bool IsNumberGreater = Value1 > Value2;

            Assert.AreEqual(IsDoubleGreater, IsNumberGreater);
        }

        [Test]
        [Category("Coverage")]
        public static void GreaterEqual0()
        {
            double d1 = 125478;
            double d2 = 5.478231405e-3;

            Number Value1 = new Number(d1);
            Number Value2 = new Number(d2);

            bool IsDoubleGreaterEqual = d1 >= d2;
            bool IsNumberGreaterEqual = Value1 >= Value2;

            Assert.AreEqual(IsDoubleGreaterEqual, IsNumberGreaterEqual);
        }

        [Test]
        [Category("Coverage")]
        public static void Lesser0()
        {
            double d1 = 125478;
            double d2 = 5.478231405e-3;

            Number Value1 = new Number(d1);
            Number Value2 = new Number(d2);

            bool IsDoubleLesser = d2 < d1;
            bool IsNumberLesser = Value2 < Value1;

            Assert.AreEqual(IsDoubleLesser, IsNumberLesser);
        }

        [Test]
        [Category("Coverage")]
        public static void LesserEqual0()
        {
            double d1 = 125478;
            double d2 = 5.478231405e-3;

            Number Value1 = new Number(d1);
            Number Value2 = new Number(d2);

            bool IsDoubleLesserEqual = d2 <= d1;
            bool IsNumberLesserEqual = Value2 <= Value1;

            Assert.AreEqual(IsDoubleLesserEqual, IsNumberLesserEqual);
        }

        [Test]
        [Category("Coverage")]
        public static void Equal0()
        {
            double d1 = 125478;
            double d2 = 5.478231405e-3;

            Number Value1 = new Number(d1);
            Number Value2 = new Number(d2);

            bool IsDoubleEqual = d1 == d2;
            bool IsNumberEqual = Value1 == Value2;

            Assert.AreEqual(IsDoubleEqual, IsNumberEqual);
        }

        [Test]
        [Category("Coverage")]
        public static void Different0()
        {
            double d1 = 125478;
            double d2 = 5.478231405e-3;

            Number Value1 = new Number(d1);
            Number Value2 = new Number(d2);

            bool IsDoubleDifferent = d1 != d2;
            bool IsNumberDifferent = Value1 != Value2;

            Assert.AreEqual(IsDoubleDifferent, IsNumberDifferent);
        }
    }
}
