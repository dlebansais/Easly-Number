﻿namespace TestEaslyNumber
{
    using EaslyNumber;
    using NUnit.Framework;
    using System;
    using System.Globalization;

    [TestFixture]
    public class CoverageSet
    {
        #region Setup
        [OneTimeSetUp]
        public static void InitTestSession()
        {
            TestEnvironment.InitTestSession();

            NL = Environment.NewLine;
            SP = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        }

        private static string NL = string.Empty;
        private static string SP = string.Empty;
        #endregion

        #region BitField
        [Test]
        [Category("Coverage")]
        public void TestBitField()
        {
            BitField IntegerField = new BitField();
            IntegerField.SetZero();
            Assert.That(IntegerField.IsZero);

            IntegerField = CreateBitFieldFromString("123456789012345678901234567890", out long BitIndex);

            Assert.That(IntegerField.Equals(IntegerField));

            BitField OtherField = new BitField();
            Assert.That(OtherField.Equals(OtherField));
            Assert.That(!IntegerField.Equals(OtherField));
            Assert.That(!OtherField.Equals(IntegerField));

            OtherField = (BitField)IntegerField.Clone();
            Assert.That(IntegerField.Equals(OtherField));
            Assert.That(!IntegerField.Equals(BitField.Empty));
            Assert.That(!(IntegerField == BitField.Empty));
            Assert.That(IntegerField != BitField.Empty);

            bool OldBit = OtherField.GetBit(0);
            Assert.That(OldBit == false);

            OtherField.SetBit(0, !OldBit);
            Assert.That(!IntegerField.Equals(OtherField));

            int HashCode1 = IntegerField.GetHashCode();
            int HashCode2 = OtherField.GetHashCode();
            Assert.That(HashCode1 != HashCode2);

            Assert.That(IntegerField < OtherField);
            Assert.That(OtherField > IntegerField);

            for (int i = 0; i < BitIndex; i++)
                IntegerField.DecreasePrecision();

            Assert.That(IntegerField.SignificantBits == 0);
            Assert.That(IntegerField.ShiftBits == BitIndex);
            Assert.That(IntegerField.GetBit(0) == false);
        }

        [Test]
        [Category("Coverage")]
        public void TestBitFieldComparison()
        {
            BitField IntegerField1 = CreateBitFieldFromString("123456789012345678901234567890", out long BitIndex1);
            BitField IntegerField2 = CreateBitFieldFromString("1234567890123456789012345678", out long BitIndex2);

            Assert.That(!IntegerField1.Equals(IntegerField2));
            Assert.That(IntegerField1 > IntegerField2);
            Assert.That(IntegerField2 < IntegerField1);

            BitField IntegerField3 = new BitField();
            BitField IntegerField4 = new BitField();

            Assert.That(IntegerField1 > IntegerField3);
            Assert.That(IntegerField3 < IntegerField1);
            Assert.That(IntegerField2 > IntegerField3);
            Assert.That(IntegerField3 < IntegerField2);
            Assert.That(!(IntegerField1 < IntegerField3));
            Assert.That(!(IntegerField3 > IntegerField1));
            Assert.That(!(IntegerField3 < IntegerField4));
            Assert.That(!(IntegerField3 > IntegerField4));
            Assert.That(!(IntegerField4 < IntegerField3));
            Assert.That(!(IntegerField4 > IntegerField3));

            BitField IntegerField5 = CreateBitFieldFromString("123456789012345678901234567891", out long BitIndex5);
            Assert.That(IntegerField1 < IntegerField5);
            Assert.That(IntegerField5 > IntegerField1);
            Assert.That(!(IntegerField1 > IntegerField5));
            Assert.That(!(IntegerField5 < IntegerField1));

            BitField IntegerField6 = CreateBitFieldFromString("123456789012345678901234567890", out long BitIndex6);

            Assert.That(!(IntegerField1 < IntegerField6));
            Assert.That(!(IntegerField1 > IntegerField6));
            Assert.That(!(IntegerField6 < IntegerField1));
            Assert.That(!(IntegerField6 > IntegerField1));

            BitField IntegerField7 = new BitField();
            IntegerField7.SetZero();
            BitField IntegerField8 = new BitField();
            IntegerField8.SetZero();
            Assert.That(!(IntegerField7 < IntegerField8));
            Assert.That(!(IntegerField7 > IntegerField8));
        }

        private BitField CreateBitFieldFromString(string integerString, out long bitIndex)
        {
            BitField IntegerField = new BitField();
            bitIndex = 0;

            do
            {
                integerString = NumberTextPartition.DividedByTwo(integerString, 10, IsValidDecimalDigit, ToDecimalDigit, out bool HasCarry);
                IntegerField.SetBit(bitIndex, HasCarry);
                Assert.That(IntegerField.GetBit(bitIndex) == HasCarry);
                bitIndex++;
            }
            while (integerString != "0");

            return IntegerField;
        }

        [Test]
        [Category("Coverage")]
        public void TestBitField_byte()
        {
            BitField_byte IntegerField = new BitField_byte();
            IntegerField.SetZero();
            Assert.That(IntegerField.IsZero);

            IntegerField = CreateBitField_byteFromString("123456789012345678901234567890", out long BitIndex);

            Assert.That(IntegerField.Equals(IntegerField));
            Assert.That(BitField_byte.Equals(IntegerField, IntegerField));
            Assert.That(BitField_byte.Equals(BitField.Empty, BitField.Empty));
            Assert.That(!BitField_byte.Equals(IntegerField, BitField.Empty));
            Assert.That(!BitField_byte.Equals(BitField.Empty, IntegerField));

            BitField_byte OtherField = new BitField_byte();
            Assert.That(OtherField.Equals(OtherField));
            Assert.That(!IntegerField.Equals(OtherField));
            Assert.That(!OtherField.Equals(IntegerField));

            OtherField = (BitField_byte)IntegerField.Clone();
            Assert.That(IntegerField.Equals(OtherField));
            Assert.That(!IntegerField.Equals(BitField.Empty));
            Assert.That(!(IntegerField == BitField.Empty));
            Assert.That(IntegerField != BitField.Empty);

            BitField_byte NullField1 = BitField.Empty;
            BitField_byte NullField2 = BitField.Empty;
            Assert.That(NullField1 == NullField2);
            Assert.That(NullField1 != IntegerField);
            Assert.That(IntegerField != NullField1);

            bool OldBit = OtherField.GetBit(0);
            Assert.That(OldBit == false);

            OtherField.SetBit(0, !OldBit);
            Assert.That(!IntegerField.Equals(OtherField));

            int HashCode1 = IntegerField.GetHashCode();
            int HashCode2 = OtherField.GetHashCode();
            Assert.That(HashCode1 != HashCode2);

            HashCode1 = BitField_byte.GetHashCode(IntegerField);
            HashCode2 = BitField_byte.GetHashCode(OtherField);
            Assert.That(HashCode1 != HashCode2);

            int HashCodeNull = BitField_byte.GetHashCode(BitField_byte.Empty);
            Assert.That(HashCodeNull == 0);

            Assert.That(IntegerField < OtherField);
            Assert.That(OtherField > IntegerField);

            for (int i = 0; i < BitIndex; i++)
                IntegerField.DecreasePrecision();

            Assert.That(IntegerField.SignificantBits == 0);
            Assert.That(IntegerField.ShiftBits == BitIndex);
            Assert.That(IntegerField.GetBit(0) == false);
        }

        [Test]
        [Category("Coverage")]
        public void TestBitField_byteComparison()
        {
            BitField_byte IntegerField1 = CreateBitField_byteFromString("123456789012345678901234567890", out long BitIndex1);
            BitField_byte IntegerField2 = CreateBitField_byteFromString("1234567890123456789012345678", out long BitIndex2);

            Assert.That(!IntegerField1.Equals(IntegerField2));
            Assert.That(IntegerField1 > IntegerField2);
            Assert.That(IntegerField2 < IntegerField1);

            BitField_byte IntegerField3 = new BitField_byte();
            BitField_byte IntegerField4 = new BitField_byte();

            Assert.That(IntegerField1 > IntegerField3);
            Assert.That(IntegerField3 < IntegerField1);
            Assert.That(IntegerField2 > IntegerField3);
            Assert.That(IntegerField3 < IntegerField2);
            Assert.That(!(IntegerField1 < IntegerField3));
            Assert.That(!(IntegerField3 > IntegerField1));
            Assert.That(!(IntegerField3 < IntegerField4));
            Assert.That(!(IntegerField3 > IntegerField4));
            Assert.That(!(IntegerField4 < IntegerField3));
            Assert.That(!(IntegerField4 > IntegerField3));

            BitField_byte IntegerField5 = CreateBitField_byteFromString("123456789012345678901234567891", out long BitIndex5);
            Assert.That(IntegerField1 < IntegerField5);
            Assert.That(IntegerField5 > IntegerField1);
            Assert.That(!(IntegerField1 > IntegerField5));
            Assert.That(!(IntegerField5 < IntegerField1));

            BitField_byte IntegerField6 = CreateBitField_byteFromString("123456789012345678901234567890", out long BitIndex6);

            Assert.That(!(IntegerField1 < IntegerField6));
            Assert.That(!(IntegerField1 > IntegerField6));
            Assert.That(!(IntegerField6 < IntegerField1));
            Assert.That(!(IntegerField6 > IntegerField1));

            BitField_byte IntegerField7 = new BitField_byte();
            IntegerField7.SetZero();
            BitField_byte IntegerField8 = new BitField_byte();
            IntegerField8.SetZero();
            Assert.That(!(IntegerField7 < IntegerField8));
            Assert.That(!(IntegerField7 > IntegerField8));
        }

        private BitField_byte CreateBitField_byteFromString(string integerString, out long bitIndex)
        {
            BitField_byte IntegerField = new BitField_byte();
            bitIndex = 0;

            do
            {
                integerString = NumberTextPartition.DividedByTwo(integerString, 10, IsValidDecimalDigit, ToDecimalDigit, out bool HasCarry);
                IntegerField.SetBit(bitIndex, HasCarry);
                Assert.That(IntegerField.GetBit(bitIndex) == HasCarry);
                bitIndex++;
            }
            while (integerString != "0");

            return IntegerField;
        }

        [Test]
        [Category("Coverage")]
        public void TestBitField_uint()
        {
            BitField_uint IntegerField = new BitField_uint();
            IntegerField.SetZero();
            Assert.That(IntegerField.IsZero);

            IntegerField = CreateBitField_uintFromString("123456789012345678901234567890", out long BitIndex);

            Assert.That(IntegerField.Equals(IntegerField));
            Assert.That(BitField_uint.Equals(IntegerField, IntegerField));
            Assert.That(BitField_uint.Equals(BitField.Empty, BitField.Empty));
            Assert.That(!BitField_uint.Equals(IntegerField, BitField.Empty));
            Assert.That(!BitField_uint.Equals(BitField.Empty, IntegerField));

            BitField_uint OtherField = new BitField_uint();
            Assert.That(OtherField.Equals(OtherField));
            Assert.That(!IntegerField.Equals(OtherField));
            Assert.That(!OtherField.Equals(IntegerField));

            OtherField = (BitField_uint)IntegerField.Clone();
            Assert.That(IntegerField.Equals(OtherField));
            Assert.That(!IntegerField.Equals(BitField_uint.Empty));
            Assert.That(!(IntegerField == BitField_uint.Empty));
            Assert.That(IntegerField != BitField_uint.Empty);

            BitField_uint NullField1 = BitField_uint.Empty;
            BitField_uint NullField2 = BitField_uint.Empty;
            Assert.That(NullField1 == NullField2);
            Assert.That(NullField1 != IntegerField);
            Assert.That(IntegerField != NullField1);

            bool OldBit = OtherField.GetBit(0);
            Assert.That(OldBit == false);

            OtherField.SetBit(0, !OldBit);
            Assert.That(!IntegerField.Equals(OtherField));

            int HashCode1 = IntegerField.GetHashCode();
            int HashCode2 = OtherField.GetHashCode();
            Assert.That(HashCode1 != HashCode2);

            HashCode1 = BitField_uint.GetHashCode(IntegerField);
            HashCode2 = BitField_uint.GetHashCode(OtherField);
            Assert.That(HashCode1 != HashCode2);

            int HashCodeNull = BitField_uint.GetHashCode(BitField_uint.Empty);
            Assert.That(HashCodeNull == 0);

            Assert.That(IntegerField < OtherField);
            Assert.That(OtherField > IntegerField);

            for (int i = 0; i < BitIndex; i++)
                IntegerField.DecreasePrecision();

            Assert.That(IntegerField.SignificantBits == 0);
            Assert.That(IntegerField.ShiftBits == BitIndex);
            Assert.That(IntegerField.GetBit(0) == false);
        }

        [Test]
        [Category("Coverage")]
        public void TestBitField_uintComparison()
        {
            BitField_uint IntegerField1 = CreateBitField_uintFromString("123456789012345678901234567890", out long BitIndex1);
            BitField_uint IntegerField2 = CreateBitField_uintFromString("1234567890123456789012345678", out long BitIndex2);

            Assert.That(!IntegerField1.Equals(IntegerField2));
            Assert.That(IntegerField1 > IntegerField2);
            Assert.That(IntegerField2 < IntegerField1);

            BitField_uint IntegerField3 = new BitField_uint();
            BitField_uint IntegerField4 = new BitField_uint();

            Assert.That(IntegerField1 > IntegerField3);
            Assert.That(IntegerField3 < IntegerField1);
            Assert.That(IntegerField2 > IntegerField3);
            Assert.That(IntegerField3 < IntegerField2);
            Assert.That(!(IntegerField1 < IntegerField3));
            Assert.That(!(IntegerField3 > IntegerField1));
            Assert.That(!(IntegerField3 < IntegerField4));
            Assert.That(!(IntegerField3 > IntegerField4));
            Assert.That(!(IntegerField4 < IntegerField3));
            Assert.That(!(IntegerField4 > IntegerField3));

            BitField_uint IntegerField5 = CreateBitField_uintFromString("123456789012345678901234567891", out long BitIndex5);
            Assert.That(IntegerField1 < IntegerField5);
            Assert.That(IntegerField5 > IntegerField1);
            Assert.That(!(IntegerField1 > IntegerField5));
            Assert.That(!(IntegerField5 < IntegerField1));

            BitField_uint IntegerField6 = CreateBitField_uintFromString("123456789012345678901234567890", out long BitIndex6);

            Assert.That(!(IntegerField1 < IntegerField6));
            Assert.That(!(IntegerField1 > IntegerField6));
            Assert.That(!(IntegerField6 < IntegerField1));
            Assert.That(!(IntegerField6 > IntegerField1));

            BitField_uint IntegerField7 = new BitField_uint();
            IntegerField7.SetZero();
            BitField_uint IntegerField8 = new BitField_uint();
            IntegerField8.SetZero();
            Assert.That(!(IntegerField7 < IntegerField8));
            Assert.That(!(IntegerField7 > IntegerField8));
        }

        private BitField_uint CreateBitField_uintFromString(string integerString, out long bitIndex)
        {
            BitField_uint IntegerField = new BitField_uint();
            bitIndex = 0;

            do
            {
                integerString = NumberTextPartition.DividedByTwo(integerString, 10, IsValidDecimalDigit, ToDecimalDigit, out bool HasCarry);
                IntegerField.SetBit(bitIndex, HasCarry);
                Assert.That(IntegerField.GetBit(bitIndex) == HasCarry);
                bitIndex++;
            }
            while (integerString != "0");

            return IntegerField;
        }

        public static bool IsValidDecimalDigit(char digit, out int value)
        {
            if (digit >= '0' && digit <= '9')
            {
                value = digit - '0';
                return true;
            }
            else
            {
                value = -1;
                return false;
            }
        }

        public static char ToDecimalDigit(int value)
        {
            if (value >= 0 && value < 10)
                return (char)('0' + value);
            else
                throw new ArgumentOutOfRangeException(nameof(value));
        }
        #endregion

        #region Rounding
        [Test]
        [Category("Coverage")]
        public void TestRounding()
        {
            double[] TestArray = new double[]
            {
                1.2547856e2,
                1.2547856,
                1.2547857,
                1.2547858,
                1.2547859,
                9.9999,
                1.2547850,
                1.0000001,
            };

            //Debug.Assert(false);

            for (int i = 0; i < TestArray.Length; i++)
            {
                double d = TestArray[i];
                string Text = TestEnvironment.DoubleString(d);

                FormattedNumber FormattedNumber = new FormattedNumber(Text);

                Number Value = FormattedNumber.Value;
                string ValueString = Value.ToString(TestEnvironment.FormatDouble);
                Assert.That(IsEqualRepresentation(ValueString, Text), $"Result #{i}={ValueString}, Expected={Text}");
            }
        }

        [Test]
        [Category("Coverage")]
        public void TestRoundingText()
        {
            string[] TestArray = new string[]
            {
                "2547856",
                "254785600",
                "2547857",
                "254785700",
                "2547858",
                "254785800",
                "2547859",
                "254785900",
                "9999",
                "999900",
                "2547850",
                "254785000",
                "0000001",
                "0000001000",
            };

            //Debug.Assert(false);

            string FractionalString;
            foreach (string s in TestArray)
            {
                FractionalString = RealTextPartition.RoundedToNearest(s, 10, IsValidDecimalDigit, ToDecimalDigit, false);
                FractionalString = RealTextPartition.RoundedToNearest(s, 10, IsValidDecimalDigit, ToDecimalDigit, true);
            }
        }
        #endregion

        #region Precision
        [Test]
        [Category("Coverage")]
        public void TestPrecision()
        {
            double[] TestArray = new double[]
            {
                1.2547856e2,
                1.2547856,
                1.2547857,
                1.2547858,
                1.2547859,
                9.9999,
                1.2547850,
                1.0000001,
            };

            Assert.That(Arithmetic.SignificandPrecision == Arithmetic.DefaultSignificandPrecision);
            Arithmetic.SignificandPrecision = Arithmetic.DefaultSignificandPrecision;

            Assert.That(Arithmetic.ExponentPrecision == Arithmetic.DefaultExponentPrecision);
            Arithmetic.ExponentPrecision = Arithmetic.DefaultExponentPrecision;

            Assert.That(Arithmetic.Rounding == Rounding.ToNearest);
            Arithmetic.Rounding = Rounding.ToNearest;

            Assert.That(!Arithmetic.EnableInfinitePrecision);
            Arithmetic.EnableInfinitePrecision = false;

            //System.Diagnostics.Debug.Assert(false);

            for (int i = 0; i < TestArray.Length; i++)
            {
                double d = TestArray[i];
                string Text = TestEnvironment.DoubleString(d);

                FormattedNumber FormattedNumber = new FormattedNumber(Text);

                Number Value = FormattedNumber.Value;
                string ValueString = Value.ToString(TestEnvironment.FormatDouble);
                Assert.That(IsEqualRepresentation(ValueString, Text), $"Result #{i}={ValueString}, Expected={Text}");
            }

            Flags Flags = Arithmetic.Flags;
            Assert.That(!Flags.DivideByZero, $"Flags.DivideByZero={Flags.DivideByZero}, Expected=false");
            Assert.That(!Flags.Inexact, $"Flags.Inexact={Flags.Inexact}, Expected=false");

            Flags.SetDivideByZero();
            Assert.That(Flags.DivideByZero);

            Flags.SetInexact();
            Assert.That(Flags.Inexact);

            Flags.Clear();
            Assert.That(!Flags.DivideByZero);
            Assert.That(!Flags.Inexact);

            Exception ex;

            ex = Assert.Throws<ArgumentOutOfRangeException>(() => Arithmetic.SignificandPrecision = 0);
            Assert.That(ex.Message == $"Specified argument was out of the range of valid values.{NL}Parameter name: value", ex.Message);

            ex = Assert.Throws<ArgumentOutOfRangeException>(() => Arithmetic.ExponentPrecision = 0);
            Assert.That(ex.Message == $"Specified argument was out of the range of valid values.{NL}Parameter name: value", ex.Message);
        }

        private static bool IsEqualRepresentation(string s1, string s2)
        {
            if (s1.Length != s2.Length)
                return false;

            int Index1 = s1.ToUpperInvariant().LastIndexOf('E');
            int Index2 = s2.ToUpperInvariant().LastIndexOf('E');

            if (Index1 != Index2)
                return false;

            if (Index1 > 0)
            {
                char c1 = s1[Index1 - 1];
                char c2 = s2[Index2 - 1];
                if (c1 >= '0' && c1 <= '9' && c2 >= '0' && c2 <= '9')
                {
                    int digit1 = c1 - '0';
                    int digit2 = c2 - '0';

                    if (digit1 != digit2 && digit1 + 1 != digit2)
                        return false;

                    if (digit1 + 1 == digit2)
                        s2 = s2.Substring(0, Index2 - 1) + c1 + s2.Substring(Index2);
                }
            }

            return s1 == s2;
        }
        #endregion

        #region Misc
        [Test]
        [Category("Coverage")]
        public void TestTryParse()
        {
            Assert.That(!Number.TryParse("", out Number n1));
            //Debug.Assert(false);
            Assert.That(Number.TryParse("0", out Number n2) && n2.IsZero && n2.ToString() == "0" && n2.CheatDouble == 0, $"Result: {n2}, expected: 0");
            Assert.That(Number.TryParse("1", out Number n3) && !n3.IsZero && n3.ToString() == "1" && n3.CheatDouble == 1, $"Result: {n3}, expected: 1");
        }

        [Test]
        [Category("Coverage")]
        public void TestCreate()
        {
            //Debug.Assert(false);
            Exception ex;
            Number n1;

            string TextNaN = double.NaN.ToString();
            string TextPositiveInfinity = double.PositiveInfinity.ToString();
            string TextNegativeInfinity = double.NegativeInfinity.ToString();

            ex = Assert.Throws<ArgumentException>(() => n1 = new Number(""));
            Assert.That(ex.Message == $"text is not a valid number.", ex.Message);

            Number n2 = new Number("0");
            Assert.That(n2.IsZero && n2.ToString() == "0" && n2.CheatDouble == 0, $"Result: {n2}, expected: 0");

            Number n3 = new Number("1");
            Assert.That(!n3.IsZero && n3.ToString() == "1" && n3.CheatDouble == 1, $"Result: {n3}, expected: 1");

            Number n4 = new Number(TextNaN);
            Assert.That(n4.IsNaN && n4.ToString() == TextNaN && double.IsNaN(n4.CheatDouble), $"Result: {n4}, expected: {TextNaN}");

            Number n5 = new Number(TextPositiveInfinity);
            Assert.That(n5.IsPositiveInfinity && n5.ToString() == TextPositiveInfinity && double.IsPositiveInfinity(n5.CheatDouble), $"Result: {n5}, expected: {TextPositiveInfinity}");

            if (TextPositiveInfinity[0] != '+')
            {
                Number n6 = new Number($"+{TextPositiveInfinity}");
                Assert.That(n6.IsPositiveInfinity && n6.ToString() == TextPositiveInfinity && double.IsPositiveInfinity(n6.CheatDouble), $"Result: {n6}, expected: {TextPositiveInfinity}");
            }

            Number n7 = new Number(TextNegativeInfinity);
            Assert.That(n7.IsNegativeInfinity && n7.ToString() == TextNegativeInfinity && double.IsNegativeInfinity(n7.CheatDouble), $"Result: {n7}, expected: {TextNegativeInfinity}");

            ex = Assert.Throws<ArgumentException>(() => n4 = new Number($" {TextNaN}"));
            Assert.That(ex.Message == $"partition does not represent a valid number.", ex.Message);

            ex = Assert.Throws<ArgumentException>(() => n4 = new Number($"{TextNaN}x"));
            Assert.That(ex.Message == $"partition does not represent a valid number.", ex.Message);

            Number n8 = new Number("0xFF");
            Assert.That(!n8.IsZero && n8.ToString() == "255" && n8.CheatDouble == 255, $"Result: {n8}, expected: 255");

            ex = Assert.Throws<ArgumentException>(() => n8 = new Number(" 0xFF"));
            Assert.That(ex.Message == $"partition does not represent a valid number.", ex.Message);

            ex = Assert.Throws<ArgumentException>(() => n8 = new Number("0xFFx"));
            Assert.That(ex.Message == $"partition does not represent a valid number.", ex.Message);

            Number n9 = new Number("FF:H");
            Assert.That(!n9.IsZero && n9.ToString() == "255" && n9.CheatDouble == 255, $"Result: {n9}, expected: 255");

            ex = Assert.Throws<ArgumentException>(() => n9 = new Number(" FF:H"));
            Assert.That(ex.Message == $"partition does not represent a valid number.", ex.Message);

            ex = Assert.Throws<ArgumentException>(() => n9 = new Number("FF:Hx"));
            Assert.That(ex.Message == $"partition does not represent a valid number.", ex.Message);

            //System.Diagnostics.Debug.Assert(false);
            Number n10 = new Number("1.2e3");
            Assert.That(n10.ToString() == $"1{SP}2E3" && n10.CheatDouble == 1.2e3, $"Result: {n10}, expected: 1{SP}2E3");

            n10 = new Number("1.2E3");
            Assert.That(n10.ToString() == $"1{SP}2E3" && n10.CheatDouble == 1.2E3, $"Result: {n10}, expected: 1{SP}2E3");

            ex = Assert.Throws<ArgumentException>(() => n10 = new Number(" 1.2e3"));
            Assert.That(ex.Message == $"partition does not represent a valid number.", ex.Message);

            ex = Assert.Throws<ArgumentException>(() => n10 = new Number("1.2e3x"));
            Assert.That(ex.Message == $"partition does not represent a valid number.", ex.Message);
        }

        [Test]
        [Category("Coverage")]
        public void TestConversion()
        {
            //System.Diagnostics.Debug.Assert(false);
            Number n1 = new Number(1.0F);
            Assert.That(n1.ToString() == "1" && n1.CheatSingle == 1, $"Result: {n1}, expected: 1");

            Number n2 = new Number(2.0);
            Assert.That(n2.ToString() == "2" && n2.CheatDouble == 2, $"Result: {n2}, expected: 2");

            Number n3 = new Number(3.0M);
            Assert.That(n3.ToString() == "3" && n3.CheatDouble == 3, $"Result: {n3}, expected: 3");

            Number n4 = new Number(4);
            Assert.That(n4.ToString() == "4" && n4.CheatDouble == 4, $"Result: {n4}, expected: 4");

            Number n5 = new Number(5U);
            Assert.That(n5.ToString() == "5" && n5.CheatDouble == 5, $"Result: {n5}, expected: 5");

            Number n6 = new Number(6L);
            Assert.That(n6.ToString() == "6" && n6.CheatDouble == 6, $"Result: {n6}, expected: 6");

            Number n7 = new Number(7UL);
            Assert.That(n7.ToString() == "7" && n7.CheatDouble == 7, $"Result: {n7}, expected: 7");
        }
        #endregion

        #region Comparison
        [Test]
        [Category("Coverage")]
        public void TestEqual()
        {
            CheckSameEqual(0);
            CheckSameEqual(1);
            CheckSameEqual(-1);
            //Debug.Assert(false);
            CheckSameEqual(double.NaN);
            CheckSameEqual(double.PositiveInfinity);
            CheckSameEqual(double.NegativeInfinity);

            Number n1, n2;

            n1 = new Number(double.NaN);
            n2 = new Number(double.PositiveInfinity);
            Assert.That(!n1.Equals(n2), $"Failed !n1.Equals(n2), for n1: {n1}, n2: {n2}");
            Assert.That(!n2.Equals(n1), $"Failed !n2.Equals(n1), for n1: {n1}, n2: {n2}");

            n1 = new Number(double.NaN);
            n2 = new Number(double.NegativeInfinity);
            Assert.That(!n1.Equals(n2), $"Failed !n1.Equals(n2), for n1: {n1}, n2: {n2}");
            Assert.That(!n2.Equals(n1), $"Failed !n2.Equals(n1), for n1: {n1}, n2: {n2}");

            n1 = new Number(double.PositiveInfinity);
            n2 = new Number(double.NegativeInfinity);
            Assert.That(!n1.Equals(n2), $"Failed !n1.Equals(n2), for n1: {n1}, n2: {n2}");
            Assert.That(!n2.Equals(n1), $"Failed !n2.Equals(n1), for n1: {n1}, n2: {n2}");

            n1 = new Number(0);
            n2 = new Number(double.NaN);
            Assert.That(!n1.Equals(n2), $"Failed !n1.Equals(n2), for n1: {n1}, n2: {n2}");
            Assert.That(!n2.Equals(n1), $"Failed !n2.Equals(n1), for n1: {n1}, n2: {n2}");

            n1 = new Number(0);
            n2 = new Number(double.PositiveInfinity);
            Assert.That(!n1.Equals(n2), $"Failed !n1.Equals(n2), for n1: {n1}, n2: {n2}");
            Assert.That(!n2.Equals(n1), $"Failed !n2.Equals(n1), for n1: {n1}, n2: {n2}");

            n1 = new Number(0);
            n2 = new Number(double.NegativeInfinity);
            Assert.That(!n1.Equals(n2), $"Failed !n1.Equals(n2), for n1: {n1}, n2: {n2}");
            Assert.That(!n2.Equals(n1), $"Failed !n2.Equals(n1), for n1: {n1}, n2: {n2}");

            n1 = new Number(0);
            n2 = new Number(1);
            Assert.That(!n1.Equals(n2), $"Failed !n1.Equals(n2), for n1: {n1}, n2: {n2}");
            Assert.That(!n2.Equals(n1), $"Failed !n2.Equals(n1), for n1: {n1}, n2: {n2}");

            n1 = new Number(1);
            n2 = new Number(1);
            Assert.That(n1.Equals(n2), $"Failed n1.Equals(n2), for n1: {n1}, n2: {n2}");
            Assert.That(n2.Equals(n1), $"Failed n2.Equals(n1), for n1: {n1}, n2: {n2}");

            n1 = new Number(1.0F);
            n2 = new Number(1.0F);
            Assert.That(n1 == n2, $"Failed n1 == n2, for n1: {n1}, n2: {n2}");
            Assert.That(n1.Equals(n2), $"Failed n1.Equals(n2), for n1: {n1}, n2: {n2}");
            Assert.That(n1.GetHashCode() == n2.GetHashCode(), $"Failed n1.GetHashCode() [{n1.GetHashCode()}] == n2.GetHashCode() [{n2.GetHashCode()}], for n1: {n1}, n2: {n2}");
            //Assert.That(!n1.Equals(null), $"Failed !n1.Equals(null), for n1: {n1}");
            Assert.That(n1 != Number.PositiveInfinity, $"Failed n1 != Number.PositiveInfinity, for n1: {n1}");

            n2 = new Number(-1.0F);
            Assert.That(n1 != n2, $"Failed n1 != n2, for n1: {n1}, n2: {n2}");
        }

        private void CheckSameEqual(double d)
        {
            double d1 = d;
            double d2 = d;
            Number n1 = new Number(d1);
            Number n2 = new Number(d2);

#pragma warning disable CS1718
            bool EqualsNumber = n1.Equals(n1);
            bool EqualsDouble = d1.Equals(d1);
            bool IdenticalNumber = n1 == n1;
            bool IdenticalDouble = d1 == d1;
#pragma warning restore CS1718

            Assert.That(EqualsNumber == EqualsDouble, $"Failed #1 EqualsNumber == EqualsDouble, for d: {d}");
            Assert.That(IdenticalNumber == IdenticalDouble, $"Failed #2 IdenticalNumber == IdenticalDouble, for d: {d}");

            EqualsNumber = n1.Equals(n2);
            EqualsDouble = d1.Equals(d2);
            IdenticalNumber = n1 == n2;
            IdenticalDouble = d1 == d2;

            Assert.That(EqualsNumber == EqualsDouble, $"Failed #3 EqualsNumber == EqualsDouble, for d: {d}");
            Assert.That(IdenticalNumber == IdenticalDouble, $"Failed #4 IdenticalNumber == IdenticalDouble, for d: {d}");
        }

        [Test]
        [Category("Coverage")]
        public void TestLower()
        {
            Number n1, n2;

            n1 = new Number(double.NaN);
            n2 = new Number(double.PositiveInfinity);
            Assert.That(!(n1 < n2));
            Assert.That(!(n1 > n2));

            Exception ex;

            ex = Assert.Throws<ArgumentException>(() => Number.Compare(n1, n2));
            Assert.That(ex.Message == $"x is not allowed to be NaN", ex.Message);

            ex = Assert.Throws<ArgumentException>(() => Number.Compare(n2, n1));
            Assert.That(ex.Message == $"y is not allowed to be NaN", ex.Message);

            n1 = new Number(double.PositiveInfinity);
            n2 = new Number(double.PositiveInfinity);
            Assert.That(Number.Compare(n1, n2) == 0);

            n1 = new Number(double.NegativeInfinity);
            n2 = new Number(double.NegativeInfinity);
            Assert.That(Number.Compare(n1, n2) == 0);

            n1 = new Number(double.PositiveInfinity);
            n2 = new Number(double.NegativeInfinity);
            Assert.That(Number.Compare(n1, n2) > 0);
            Assert.That(Number.Compare(n2, n1) < 0);

            n1 = Number.Zero;
            n2 = new Number(11.0F);
            Assert.That(Number.Compare(n1, n1) == 0);
            Assert.That(Number.Compare(n1, n2) < 0);
            Assert.That(Number.Compare(n2, n1) > 0);

            n1 = new Number(1.0F);
            n2 = new Number(-1.0F);
            Assert.That(n1.CheatSingle == 1.0F);
            Assert.That(n2.CheatSingle == -1.0F);

            Assert.That(n1 < Number.PositiveInfinity);
            Assert.That(n1 > Number.NegativeInfinity);
            Assert.That(!(n1 > Number.PositiveInfinity));
            Assert.That(!(n1 < Number.NegativeInfinity));
            Assert.That(Number.NegativeInfinity < n1);
            Assert.That(Number.PositiveInfinity > n1);
            Assert.That(!(Number.NegativeInfinity > n1));
            Assert.That(!(Number.PositiveInfinity < n1));

            Assert.That(n1 > Number.Zero);
            Assert.That(n2 < Number.Zero);
            Assert.That(n2 < n1);
            Assert.That(!(n2 > n1));
            Assert.That(n1 > n2);
            Assert.That(!(n1 < n2));
            Assert.That(Number.Compare(n1, n2) > 0);
            Assert.That(Number.Compare(n2, n1) < 0);

            n1 = new Number(2.0F);
            n2 = new Number(1.0F);
            Assert.That(n1.CheatSingle == 2.0F);
            Assert.That(n2.CheatSingle == 1.0F);
            Assert.That(n2 < n1);
            Assert.That(Number.Compare(n1, n2) > 0);
            Assert.That(Number.Compare(n2, n1) < 0);

            //System.Diagnostics.Debug.Assert(false);
            n1 = new Number(1.2F);
            n2 = new Number(1.1F);
            Assert.That(n1.CheatSingle == 1.2F);
            Assert.That(n2.CheatSingle == 1.1F);
            Assert.That(n2 < n1);
            Assert.That(Number.Compare(n1, n2) > 0);
            Assert.That(Number.Compare(n2, n1) < 0);

            n1 = new Number("1.0e2");
            n2 = new Number("1.0e1");
            Assert.That(n1.CheatDouble == 4);
            Assert.That(n2.CheatDouble == 2);
            Assert.That(n2 < n1);
            Assert.That(n1 > n2);
            Assert.That(!(n2 > n1));
            Assert.That(!(n1 < n2));
            Assert.That(Number.Compare(n1, n2) > 0);
            Assert.That(Number.Compare(n2, n1) < 0);

            n1 = new Number("1.0");
            n2 = new Number("1.0");
            Assert.That(n1.CheatDouble == 1);
            Assert.That(n2.CheatDouble == 1);
            Assert.That(!(n2 < n1));
            Assert.That(!(n2 > n1));
            Assert.That(Number.Compare(n1, n2) == 0);
            Assert.That(Number.Compare(n2, n1) == 0);
        }
        #endregion
    }
}
