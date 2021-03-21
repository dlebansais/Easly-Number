namespace TestEaslyNumber
{
    using EaslyNumber;
    using NUnit.Framework;

    [TestFixture]
    public partial class TestNumberParse
    {
        [Test]
        [Category("Coverage")]
        public static void TryParse0()
        {
            double d1 = 125478;
            double d2 = 5.478231405e-3;

            Number Value1 = new Number(d1);
            Number Value2= new Number(d2);

            bool IsParsed1 = Value1.TryParseInt(out int Result1);
            bool IsParsed2 = Value2.TryParseInt(out int Result2);
            bool IsParsed3 = Value2.TryParseUInt(out uint Result3);
            bool IsParsed4 = Value2.TryParseLong(out long Result4);
            bool IsParsed5 = Value2.TryParseULong(out ulong Result5);

            Assert.That(IsParsed1 && Result1 == (int)d1);
            Assert.That(!IsParsed2);
            Assert.That(!IsParsed3);
            Assert.That(!IsParsed4);
            Assert.That(!IsParsed5);
        }

        [Test]
        [Category("Coverage")]
        public static void TryParse1()
        {
            ulong n = 2178483647;
            Number Value = new Number(n);
            Assert.IsTrue(Value.IsInteger);

            bool IsParsedAsInt = Value.TryParseInt(out int AsInt);
            Assert.IsFalse(IsParsedAsInt);

            bool IsParsedAsUInt = Value.TryParseUInt(out uint AsUInt);
            Assert.IsTrue(IsParsedAsUInt);

            long p = (uint)n;
            Assert.AreEqual(AsUInt, p);
        }

        [Test]
        [Category("Coverage")]
        public static void TryParse2()
        {
            ulong n = 95120485000;
            Number Value = new Number(n);
            Assert.IsTrue(Value.IsInteger);

            bool IsParsedAsInt = Value.TryParseInt(out int AsInt);
            Assert.IsFalse(IsParsedAsInt);

            bool IsParsedAsUInt = Value.TryParseUInt(out uint AsUInt);
            Assert.IsFalse(IsParsedAsUInt);

            bool IsParsedAsLong = Value.TryParseLong(out long AsLong);
            Assert.IsTrue(IsParsedAsLong);

            long p = (long)n;
            Assert.AreEqual(AsLong, p);
        }

        [Test]
        [Category("Coverage")]
        public static void TryParse3()
        {
            ulong n = 9228972036854775807;
            Number Value = new Number(n);
            Assert.IsTrue(Value.IsInteger);

            bool IsParsedAsInt = Value.TryParseInt(out int AsInt);
            Assert.IsFalse(IsParsedAsInt);

            bool IsParsedAsUInt = Value.TryParseUInt(out uint AsUInt);
            Assert.IsFalse(IsParsedAsUInt);

            bool IsParsedAsLong = Value.TryParseLong(out long AsLong);
            Assert.IsFalse(IsParsedAsLong);

            bool IsParsedAsULong = Value.TryParseULong(out ulong AsULong);
            Assert.IsTrue(IsParsedAsULong);

            Assert.AreEqual(AsULong, n);
        }

        [Test]
        [Category("Coverage")]
        public static void TryParse4()
        {
            Number Value = new Number("11119228972036854775807");
            Assert.IsTrue(Value.IsInteger);

            bool IsParsedAsInt = Value.TryParseInt(out int AsInt);
            Assert.IsFalse(IsParsedAsInt);

            bool IsParsedAsUInt = Value.TryParseUInt(out uint AsUInt);
            Assert.IsFalse(IsParsedAsUInt);

            bool IsParsedAsLong = Value.TryParseLong(out long AsLong);
            Assert.IsFalse(IsParsedAsLong);

            bool IsParsedAsULong = Value.TryParseULong(out ulong AsULong);
            Assert.IsFalse(IsParsedAsULong);
        }
    }
}
