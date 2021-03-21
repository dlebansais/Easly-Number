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

            bool IsGreater = Value1.CompareTo(Value2) > 0;
            Assert.IsTrue(IsGreater);

            ulong n1 = 5;
            long n2 = 5;

            IsGreater = Value1 > n1;
            Assert.IsTrue(IsGreater);

            IsGreater = n1 > Value1;
            Assert.IsFalse(IsGreater);

            IsGreater = Value1 > n2;
            Assert.IsTrue(IsGreater);

            IsGreater = n2 > Value1;
            Assert.IsFalse(IsGreater);

            IsGreater = Value1 > d2;
            Assert.IsTrue(IsGreater);

            IsGreater = d2 > Value1;
            Assert.IsFalse(IsGreater);
        }

        [Test]
        [Category("Coverage")]
        public static void Greater1()
        {
            double d1 = 125478;
            double d2 = 5.478231405e-3;
            ulong n1 = 5;
            long n2 = 5;

            Number Value1 = new Number(d1);

            bool IsGreater;

            IsGreater = Value1.CompareTo(d2) > 0;
            Assert.IsTrue(IsGreater);

            IsGreater = Value1.CompareTo(n1) > 0;
            Assert.IsTrue(IsGreater);

            IsGreater = Value1.CompareTo(n2) > 0;
            Assert.IsTrue(IsGreater);
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

            bool IsGreater = Value1.CompareTo(Value2) >= 0;
            Assert.IsTrue(IsGreater);

            ulong n1 = 5;
            long n2 = 5;

            IsGreater = Value1 >= n1;
            Assert.IsTrue(IsGreater);

            IsGreater = n1 >= Value1;
            Assert.IsFalse(IsGreater);

            IsGreater = Value1 >= n2;
            Assert.IsTrue(IsGreater);

            IsGreater = n2 >= Value1;
            Assert.IsFalse(IsGreater);

            IsGreater = Value1 >= d2;
            Assert.IsTrue(IsGreater);

            IsGreater = d2 >= Value1;
            Assert.IsFalse(IsGreater);
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

            bool IsLesser = Value1.CompareTo(Value2) < 0;
            Assert.False(IsLesser);

            ulong n1 = 5;
            long n2 = 5;

            IsLesser = Value1 < n1;
            Assert.IsFalse(IsLesser);

            IsLesser = n1 < Value1;
            Assert.IsTrue(IsLesser);

            IsLesser = Value1 < n2;
            Assert.IsFalse(IsLesser);

            IsLesser = n2 < Value1;
            Assert.IsTrue(IsLesser);

            IsLesser = Value1 < d2;
            Assert.IsFalse(IsLesser);

            IsLesser = d2 < Value1;
            Assert.IsTrue(IsLesser);
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

            bool IsLesser = Value1.CompareTo(Value2) <= 0;
            Assert.False(IsLesser);

            ulong n1 = 5;
            long n2 = 5;

            IsLesser = Value1 <= n1;
            Assert.IsFalse(IsLesser);

            IsLesser = n1 <= Value1;
            Assert.IsTrue(IsLesser);

            IsLesser = Value1 <= n2;
            Assert.IsFalse(IsLesser);

            IsLesser = n2 <= Value1;
            Assert.IsTrue(IsLesser);

            IsLesser = Value1 <= d2;
            Assert.IsFalse(IsLesser);

            IsLesser = d2 <= Value1;
            Assert.IsTrue(IsLesser);
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

            bool IsEqual = Value1.CompareTo(Value2) == 0;
            Assert.IsFalse(IsEqual);

            ulong n1 = 5;
            long n2 = 5;

            IsEqual = Value1 == n1;
            Assert.IsFalse(IsEqual);

            IsEqual = n1 == Value1;
            Assert.IsFalse(IsEqual);

            IsEqual = Value1 == n2;
            Assert.IsFalse(IsEqual);

            IsEqual = n2 == Value1;
            Assert.IsFalse(IsEqual);

            IsEqual = Value1 == d2;
            Assert.IsFalse(IsEqual);

            IsEqual = d2 == Value1;
            Assert.IsFalse(IsEqual);
        }

        [Test]
        [Category("Coverage")]
        public static void Equal1()
        {
            double d1 = 125478;
            double d2 = 5.478231405e-3;

            Number Value1 = new Number(d1);

            bool IsEqual;

            IsEqual = Value1.Equals(d1);
            Assert.IsFalse(IsEqual);

            IsEqual = Value1.Equals(d2);
            Assert.IsFalse(IsEqual);

            Number Value2 = new Number(d2);

            IsEqual = Value1.Equals(Value2);
            Assert.IsFalse(IsEqual);

            IsEqual = Value1.Equals(Value1);
            Assert.IsTrue(IsEqual);

            int HashCodeDouble = d1.GetHashCode();
            int HashCodeNumber = Value1.GetHashCode();
            Assert.AreNotEqual(HashCodeDouble, HashCodeNumber);
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

            bool IsDifferent = Value1.CompareTo(Value2) != 0;
            Assert.IsTrue(IsDifferent);

            ulong n1 = 5;
            long n2 = 5;

            IsDifferent = Value1 != n1;
            Assert.IsTrue(IsDifferent);

            IsDifferent = n1 != Value1;
            Assert.IsTrue(IsDifferent);

            IsDifferent = Value1 != n2;
            Assert.IsTrue(IsDifferent);

            IsDifferent = n2 != Value1;
            Assert.IsTrue(IsDifferent);

            IsDifferent = Value1 != d2;
            Assert.IsTrue(IsDifferent);

            IsDifferent = d2 != Value1;
            Assert.IsTrue(IsDifferent);
        }
    }
}
