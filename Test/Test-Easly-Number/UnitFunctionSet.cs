namespace TestEaslyNumber
{
    using EaslyNumber;
    using NUnit.Framework;
    using System.Diagnostics;

    public class UnitFunctionSet
    {
        [Test]
        [Category("aUnit")]
        public void TestIncremented()
        {
            //Debug.Assert(false);

            string Text;

            Text = NumberTextPartition.Incremented("0", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "1");

            Text = NumberTextPartition.Incremented("1", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "2");

            Text = NumberTextPartition.Incremented("8", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "9");

            Text = NumberTextPartition.Incremented("9", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "10");

            Text = NumberTextPartition.Incremented("10", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "11");

            Text = NumberTextPartition.Incremented("11", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "12");

            Text = NumberTextPartition.Incremented("18", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "19");

            Text = NumberTextPartition.Incremented("19", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "20");

            Text = NumberTextPartition.Incremented("20", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "21");

            Text = NumberTextPartition.Incremented("21", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "22");

            Text = NumberTextPartition.Incremented("28", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "29");

            Text = NumberTextPartition.Incremented("99", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "100");

            Text = NumberTextPartition.Incremented("999", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "1000");

            Text = NumberTextPartition.Incremented("1000", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "1001");

            for (int i = 1; i < 1000; i++)
            {
                Text = NumberTextPartition.Incremented(i.ToString(), Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
                Assert.That(Text == (i + 1).ToString());
            }
        }

        [Test]
        [Category("aUnit")]
        public void TestDecremented()
        {
            //Debug.Assert(false);

            string Text;

            Text = NumberTextPartition.Decremented("1", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "0");

            Text = NumberTextPartition.Decremented("2", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "1");

            Text = NumberTextPartition.Decremented("9", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "8");

            Text = NumberTextPartition.Decremented("10", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "9");

            Text = NumberTextPartition.Decremented("11", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "10");

            Text = NumberTextPartition.Decremented("12", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "11");

            Text = NumberTextPartition.Decremented("19", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "18");

            Text = NumberTextPartition.Decremented("20", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "19");

            Text = NumberTextPartition.Decremented("21", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "20");

            Text = NumberTextPartition.Decremented("22", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "21");

            Text = NumberTextPartition.Decremented("29", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "28");

            Text = NumberTextPartition.Decremented("100", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "99");

            Text = NumberTextPartition.Decremented("1000", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "999");

            Text = NumberTextPartition.Decremented("1001", Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
            Assert.That(Text == "1000");

            for (int i = 1; i < 1000; i++)
            {
                Text = NumberTextPartition.Decremented(i.ToString(), Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit);
                Assert.That(Text == (i - 1).ToString());
            }
        }
    }
}
