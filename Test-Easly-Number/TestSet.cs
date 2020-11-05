namespace TestEaslyNumber
{
    using EaslyNumber;
    using NUnit.Framework;
    using System;
    using System.Diagnostics;

    [TestFixture]
    public class TestSet
    {
        #region Setup
        [OneTimeSetUp]
        public static void InitTestSession()
        {
            TestEnvironment.InitTestSession();
        }

        static bool SkipFullParse = true;
        #endregion

        #region Basic Tests
        [Test]
        [Category("Coverage")]
        public static void TestDigits()
        {
            int Value;
            Assert.That(Number.IsValidBinaryDigit('0', out Value) && Value == 0);
            Assert.That(Number.IsValidBinaryDigit('1', out Value) && Value == 1);
            Assert.That(!Number.IsValidBinaryDigit('2', out Value));
            Assert.That(Number.IsValidOctalDigit('0', out Value) && Value == 0);
            Assert.That(Number.IsValidOctalDigit('1', out Value) && Value == 1);
            Assert.That(Number.IsValidOctalDigit('2', out Value) && Value == 2);
            Assert.That(Number.IsValidOctalDigit('3', out Value) && Value == 3);
            Assert.That(Number.IsValidOctalDigit('4', out Value) && Value == 4);
            Assert.That(Number.IsValidOctalDigit('5', out Value) && Value == 5);
            Assert.That(Number.IsValidOctalDigit('6', out Value) && Value == 6);
            Assert.That(Number.IsValidOctalDigit('7', out Value) && Value == 7);
            Assert.That(!Number.IsValidOctalDigit('8', out Value));
            Assert.That(Number.IsValidDecimalDigit('0', out Value) && Value == 0);
            Assert.That(Number.IsValidDecimalDigit('1', out Value) && Value == 1);
            Assert.That(Number.IsValidDecimalDigit('2', out Value) && Value == 2);
            Assert.That(Number.IsValidDecimalDigit('3', out Value) && Value == 3);
            Assert.That(Number.IsValidDecimalDigit('4', out Value) && Value == 4);
            Assert.That(Number.IsValidDecimalDigit('5', out Value) && Value == 5);
            Assert.That(Number.IsValidDecimalDigit('6', out Value) && Value == 6);
            Assert.That(Number.IsValidDecimalDigit('7', out Value) && Value == 7);
            Assert.That(Number.IsValidDecimalDigit('8', out Value) && Value == 8);
            Assert.That(Number.IsValidDecimalDigit('9', out Value) && Value == 9);
            Assert.That(!Number.IsValidDecimalDigit('a', out Value));
            Assert.That(Number.IsValidHexadecimalDigit('0', out Value) && Value == 0);
            Assert.That(Number.IsValidHexadecimalDigit('1', out Value) && Value == 1);
            Assert.That(Number.IsValidHexadecimalDigit('2', out Value) && Value == 2);
            Assert.That(Number.IsValidHexadecimalDigit('3', out Value) && Value == 3);
            Assert.That(Number.IsValidHexadecimalDigit('4', out Value) && Value == 4);
            Assert.That(Number.IsValidHexadecimalDigit('5', out Value) && Value == 5);
            Assert.That(Number.IsValidHexadecimalDigit('6', out Value) && Value == 6);
            Assert.That(Number.IsValidHexadecimalDigit('7', out Value) && Value == 7);
            Assert.That(Number.IsValidHexadecimalDigit('8', out Value) && Value == 8);
            Assert.That(Number.IsValidHexadecimalDigit('9', out Value) && Value == 9);
            Assert.That(Number.IsValidHexadecimalDigit('a', out Value) && Value == 10);
            Assert.That(Number.IsValidHexadecimalDigit('b', out Value) && Value == 11);
            Assert.That(Number.IsValidHexadecimalDigit('c', out Value) && Value == 12);
            Assert.That(Number.IsValidHexadecimalDigit('d', out Value) && Value == 13);
            Assert.That(Number.IsValidHexadecimalDigit('e', out Value) && Value == 14);
            Assert.That(Number.IsValidHexadecimalDigit('f', out Value) && Value == 15);
            Assert.That(Number.IsValidHexadecimalDigit('A', out Value) && Value == 10);
            Assert.That(Number.IsValidHexadecimalDigit('B', out Value) && Value == 11);
            Assert.That(Number.IsValidHexadecimalDigit('C', out Value) && Value == 12);
            Assert.That(Number.IsValidHexadecimalDigit('D', out Value) && Value == 13);
            Assert.That(Number.IsValidHexadecimalDigit('E', out Value) && Value == 14);
            Assert.That(Number.IsValidHexadecimalDigit('F', out Value) && Value == 15);
            Assert.That(!Number.IsValidHexadecimalDigit('g', out Value));
            Assert.That(!Number.IsValidHexadecimalDigit('G', out Value));

            Assert.That(Number.ToBinaryDigit(0) == '0');
            Assert.Throws<ArgumentOutOfRangeException>(() => Number.ToBinaryDigit(2));

            Assert.That(Number.ToOctalDigit(0) == '0');
            Assert.That(Number.ToOctalDigit(1) == '1');
            Assert.That(Number.ToOctalDigit(2) == '2');
            Assert.That(Number.ToOctalDigit(3) == '3');
            Assert.That(Number.ToOctalDigit(4) == '4');
            Assert.That(Number.ToOctalDigit(5) == '5');
            Assert.That(Number.ToOctalDigit(6) == '6');
            Assert.That(Number.ToOctalDigit(7) == '7');
            Assert.Throws<ArgumentOutOfRangeException>(() => Number.ToOctalDigit(8));

            Assert.That(Number.ToDecimalDigit(0) == '0');
            Assert.That(Number.ToDecimalDigit(1) == '1');
            Assert.That(Number.ToDecimalDigit(2) == '2');
            Assert.That(Number.ToDecimalDigit(3) == '3');
            Assert.That(Number.ToDecimalDigit(4) == '4');
            Assert.That(Number.ToDecimalDigit(5) == '5');
            Assert.That(Number.ToDecimalDigit(6) == '6');
            Assert.That(Number.ToDecimalDigit(7) == '7');
            Assert.That(Number.ToDecimalDigit(8) == '8');
            Assert.That(Number.ToDecimalDigit(9) == '9');
            Assert.Throws<ArgumentOutOfRangeException>(() => Number.ToDecimalDigit(10));

            Assert.That(Number.ToHexadecimalDigit(0, false) == '0');
            Assert.That(Number.ToHexadecimalDigit(0, true) == '0');
            Assert.That(Number.ToHexadecimalDigit(1, false) == '1');
            Assert.That(Number.ToHexadecimalDigit(1, true) == '1');
            Assert.That(Number.ToHexadecimalDigit(2, false) == '2');
            Assert.That(Number.ToHexadecimalDigit(2, true) == '2');
            Assert.That(Number.ToHexadecimalDigit(3, false) == '3');
            Assert.That(Number.ToHexadecimalDigit(3, true) == '3');
            Assert.That(Number.ToHexadecimalDigit(4, false) == '4');
            Assert.That(Number.ToHexadecimalDigit(4, true) == '4');
            Assert.That(Number.ToHexadecimalDigit(5, false) == '5');
            Assert.That(Number.ToHexadecimalDigit(5, true) == '5');
            Assert.That(Number.ToHexadecimalDigit(6, false) == '6');
            Assert.That(Number.ToHexadecimalDigit(6, true) == '6');
            Assert.That(Number.ToHexadecimalDigit(7, false) == '7');
            Assert.That(Number.ToHexadecimalDigit(7, true) == '7');
            Assert.That(Number.ToHexadecimalDigit(8, false) == '8');
            Assert.That(Number.ToHexadecimalDigit(8, true) == '8');
            Assert.That(Number.ToHexadecimalDigit(9, false) == '9');
            Assert.That(Number.ToHexadecimalDigit(9, true) == '9');
            Assert.That(Number.ToHexadecimalDigit(10, false) == 'A');
            Assert.That(Number.ToHexadecimalDigit(10, true) == 'a');
            Assert.That(Number.ToHexadecimalDigit(11, false) == 'B');
            Assert.That(Number.ToHexadecimalDigit(11, true) == 'b');
            Assert.That(Number.ToHexadecimalDigit(12, false) == 'C');
            Assert.That(Number.ToHexadecimalDigit(12, true) == 'c');
            Assert.That(Number.ToHexadecimalDigit(13, false) == 'D');
            Assert.That(Number.ToHexadecimalDigit(13, true) == 'd');
            Assert.That(Number.ToHexadecimalDigit(14, false) == 'E');
            Assert.That(Number.ToHexadecimalDigit(14, true) == 'e');
            Assert.That(Number.ToHexadecimalDigit(15, false) == 'F');
            Assert.That(Number.ToHexadecimalDigit(15, true) == 'f');
            Assert.Throws<ArgumentOutOfRangeException>(() => Number.ToHexadecimalDigit(16, false));
            Assert.Throws<ArgumentOutOfRangeException>(() => Number.ToHexadecimalDigit(16, true));
        }

        [Test]
        [Category("Coverage")]
        public static void TestBinary()
        {
            Assert.That(!Number.IsValidBinaryNumber(""));
            Assert.That(Number.IsValidBinaryNumber("0b0"));
            //Debug.Assert(false);
            Assert.That(Number.IsValidBinaryNumber("0b1"));
            Assert.That(!Number.IsValidBinaryNumber("0b2"));
            Assert.That(!Number.IsValidBinaryNumber("0b0120"));
            Assert.That(!Number.IsValidBinaryNumber("010"));
            Assert.That(!Number.IsValidBinaryNumber("011"));
            Assert.That(Number.IsValidBinaryNumber("0b111"));
            Assert.That(Number.IsValidBinaryNumber("0b110"));
            Assert.That(Number.IsValidBinaryNumber("0:B"));
            Assert.That(Number.IsValidBinaryNumber("1:B"));
            Assert.That(!Number.IsValidBinaryNumber("2:B"));
            Assert.That(!Number.IsValidBinaryNumber("0120:B"));
            Assert.That(Number.IsValidBinaryNumber("111:B"));
            Assert.That(Number.IsValidBinaryNumber("110:B"));
            Assert.That(!Number.IsValidBinaryNumber(" "));
            Assert.That(Number.IsValidBinaryNumber(" 0b0"));
            Assert.That(Number.IsValidBinaryNumber("   0b1"));
            Assert.That(Number.IsValidBinaryNumber(" 0:B"));
            Assert.That(Number.IsValidBinaryNumber("   1:B"));
            Assert.That(!Number.IsValidBinaryNumber("1:Bx"));
        }

        [Test]
        [Category("Coverage")]
        public static void TestOctal()
        {
            Assert.That(!Number.IsValidOctalNumber(""));
            Assert.That(Number.IsValidOctalNumber("0:O"));
            Assert.That(Number.IsValidOctalNumber("01:O"));
            Assert.That(!Number.IsValidOctalNumber("8"));
            Assert.That(!Number.IsValidOctalNumber("180:O"));
            //Debug.Assert(false);
            Assert.That(!Number.IsValidOctalNumber("10"));
            Assert.That(!Number.IsValidOctalNumber("11"));
            Assert.That(Number.IsValidOctalNumber("111:O"));
            Assert.That(Number.IsValidOctalNumber("110:O"));
            Assert.That(!Number.IsValidOctalNumber(" "));
            Assert.That(Number.IsValidOctalNumber(" 0:O"));
            Assert.That(Number.IsValidOctalNumber("   7:O"));
            Assert.That(!Number.IsValidOctalNumber("1:Ox"));
        }

        [Test]
        [Category("Coverage")]
        public static void TestHexadecimal()
        {
            //Debug.Assert(false);
            Assert.That(!Number.IsValidHexadecimalNumber(""));
            Assert.That(Number.IsValidHexadecimalNumber("0x0"));
            Assert.That(Number.IsValidHexadecimalNumber("0x1"));
            Assert.That(!Number.IsValidHexadecimalNumber("g"));
            Assert.That(!Number.IsValidHexadecimalNumber("01g0"));
            Assert.That(!Number.IsValidHexadecimalNumber("010"));
            Assert.That(!Number.IsValidHexadecimalNumber("011"));
            Assert.That(Number.IsValidHexadecimalNumber("0x111"));
            Assert.That(Number.IsValidHexadecimalNumber("0x110"));
            Assert.That(Number.IsValidHexadecimalNumber("11F:H"));
            Assert.That(Number.IsValidHexadecimalNumber("11f:H"));
            Assert.That(Number.IsValidHexadecimalNumber("110:H"));
            Assert.That(!Number.IsValidHexadecimalNumber(" "));
            Assert.That(Number.IsValidHexadecimalNumber(" 0x0"));
            Assert.That(Number.IsValidHexadecimalNumber("   0xF"));
            Assert.That(Number.IsValidHexadecimalNumber("   0xf"));
            Assert.That(Number.IsValidHexadecimalNumber(" 0:H"));
            Assert.That(Number.IsValidHexadecimalNumber("   f:H"));
            Assert.That(Number.IsValidHexadecimalNumber("   F:H"));
            Assert.That(!Number.IsValidHexadecimalNumber("f:Hx"));
            Assert.That(!Number.IsValidHexadecimalNumber("F:Hx"));
        }

        [Test]
        [Category("Coverage")]
        public static void SimpleParse()
        {
            FormattedNumber FormattedNumber;

            FormattedNumber = new FormattedNumber(string.Empty);
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsNaN && FormattedNumber.InvalidPart == string.Empty);
            FormattedNumber = new FormattedNumber("0");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "0" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("0:B");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "0" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix == ":B" && FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("0b0");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.Prefix == "0b" && FormattedNumber.BeforeExponent == "0" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("0:O");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "0" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix == ":O" && FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("0:H");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "0" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix == ":H" && FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("0x0");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.Prefix == "0x" && FormattedNumber.BeforeExponent == "0" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("5");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "5" && FormattedNumber.Exponent.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("1:B");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "1" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix == ":B" && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            //Debug.Assert(false);
            FormattedNumber = new FormattedNumber("-1:B");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "-1" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix == ":B" && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("+1:B");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "+1" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix == ":B" && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("0b1");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.Prefix == "0b" && FormattedNumber.BeforeExponent == "1" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("5:O");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "5" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix == ":O" && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("F:H");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "F" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix == ":H" && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("+F:H");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "+F" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix == ":H" && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("-F:H");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "-F" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix == ":H" && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("0xF");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.Prefix == "0x" && FormattedNumber.BeforeExponent == "F" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("468F3ECF:H");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "468F3ECF" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix == ":H" && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("0x468F3ECF");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.Prefix == "0x" && FormattedNumber.BeforeExponent == "468F3ECF" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("468F3xECF:H");
            Assert.That(!FormattedNumber.IsValid && FormattedNumber.BeforeExponent == "468" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && FormattedNumber.InvalidPart == "F3xECF:H", $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("123456789ABCDEF123456789ABCDEF123456789ABCDEF123456789ABCDEF123456789ABCDEF123456789ABCDEF123456789ABCDEF123456789ABCDEF123456789ABCDEF:H");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "123456789ABCDEF123456789ABCDEF123456789ABCDEF123456789ABCDEF123456789ABCDEF123456789ABCDEF123456789ABCDEF123456789ABCDEF123456789ABCDEF" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix == ":H" && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
        }

        [Test]
        [Category("Coverage")]
        public static void SimpleRealParse()
        {
            string TextNaN = double.NaN.ToString();
            string TextPositiveInfinity = double.PositiveInfinity.ToString();
            string TextNegativeInfinity = double.NegativeInfinity.ToString();

            FormattedNumber FormattedNumber;

            FormattedNumber = new FormattedNumber("1");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "1" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("1.");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "1." && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("1,");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "1," && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("01");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.DiscardedProlog == "0" && FormattedNumber.BeforeExponent == "1" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("01.");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.DiscardedProlog == "0" && FormattedNumber.BeforeExponent == "1." && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("01,");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.DiscardedProlog == "0" && FormattedNumber.BeforeExponent == "1," && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("-1");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "-1" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("+1");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "+1" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber(".0");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == ".0" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber(".0001");
            Assert.That(FormattedNumber.IsValid && !FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == ".0001" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("1.0");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "1.0" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("1,0");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "1,0" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("1.0e0");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "1.0e" && FormattedNumber.Exponent == "0" && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("1.0e1");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "1.0e" && FormattedNumber.Exponent == "1" && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            //Debug.Assert(false);
            FormattedNumber = new FormattedNumber("1.0E10");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "1.0E" && FormattedNumber.Exponent == "10" && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("1.0xe1");
            Assert.That(!FormattedNumber.IsValid, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("1.0e1x");
            Assert.That(!FormattedNumber.IsValid, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("1e1");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "1e" && FormattedNumber.Exponent == "1" && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("001.0");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.DiscardedProlog == "00" && FormattedNumber.BeforeExponent == "1.0" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("00e0");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.DiscardedProlog == "0" && FormattedNumber.BeforeExponent == "0e" && FormattedNumber.Exponent == "0" && FormattedNumber.Suffix.Length == 0 && FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("1e+1");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "1e" && FormattedNumber.Exponent == "+1" && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("1e-1");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "1e" && FormattedNumber.Exponent == "-1" && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("-e1");
            Assert.That(!FormattedNumber.IsValid, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("0ex1");
            Assert.That(!FormattedNumber.IsValid, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("1.0e++1");
            Assert.That(!FormattedNumber.IsValid, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber(" 0");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.DiscardedProlog == " " && FormattedNumber.BeforeExponent == "0" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber(" 1");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.DiscardedProlog == " " && FormattedNumber.BeforeExponent == "1" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber(" -1");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.DiscardedProlog == " " && FormattedNumber.BeforeExponent == "-1" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber(" +1");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.DiscardedProlog == " " && FormattedNumber.BeforeExponent == "+1" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber(" 1.");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.DiscardedProlog == " " && FormattedNumber.BeforeExponent == "1." && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber(" 1,");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.DiscardedProlog == " " && FormattedNumber.BeforeExponent == "1," && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber(" 01");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.DiscardedProlog == " 0" && FormattedNumber.BeforeExponent == "1" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber(" 01.");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.DiscardedProlog == " 0" && FormattedNumber.BeforeExponent == "1." && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber(" 01,");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.DiscardedProlog == " 0" && FormattedNumber.BeforeExponent == "1," && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("0.123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890");
            Assert.That(FormattedNumber.IsValid && !FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "0.123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber("1e23456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsInteger && FormattedNumber.BeforeExponent == "1e" && FormattedNumber.Exponent == "23456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890" && FormattedNumber.Suffix.Length == 0 && !FormattedNumber.Value.IsZero, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber(TextNaN);
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsSpecial && FormattedNumber.BeforeExponent == "NaN" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && FormattedNumber.Value.IsNaN, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber($" {TextNaN}");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsSpecial && FormattedNumber.DiscardedProlog == " " && FormattedNumber.BeforeExponent == "NaN" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && FormattedNumber.Value.IsNaN, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber($"{TextNaN}x");
            Assert.That(!FormattedNumber.IsValid, $"Result: {FormattedNumber}");
            //Debug.Assert(false);
            FormattedNumber = new FormattedNumber($"+{TextNaN}");
            Assert.That(!FormattedNumber.IsValid, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber($"-{TextNaN}");
            Assert.That(!FormattedNumber.IsValid, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber(TextPositiveInfinity);
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsSpecial && FormattedNumber.BeforeExponent == TextPositiveInfinity && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && FormattedNumber.Value.IsPositiveInfinity, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber($" {TextPositiveInfinity}");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsSpecial && FormattedNumber.DiscardedProlog == " " && FormattedNumber.BeforeExponent == TextPositiveInfinity && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && FormattedNumber.Value.IsPositiveInfinity, $"Result: {FormattedNumber}");

            if (TextPositiveInfinity[0] != '+')
            {
                FormattedNumber = new FormattedNumber($"+{TextPositiveInfinity}");
                Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsSpecial && FormattedNumber.BeforeExponent == $"+{TextPositiveInfinity}" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && FormattedNumber.Value.IsPositiveInfinity, $"Result: {FormattedNumber}");

                FormattedNumber = new FormattedNumber($" +{TextPositiveInfinity}");
                Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsSpecial && FormattedNumber.DiscardedProlog == " " && FormattedNumber.BeforeExponent == $"+{TextPositiveInfinity}" && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && FormattedNumber.Value.IsPositiveInfinity, $"Result: {FormattedNumber}");
            }

            FormattedNumber = new FormattedNumber(TextNegativeInfinity);
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsSpecial && FormattedNumber.BeforeExponent == TextNegativeInfinity && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && FormattedNumber.Value.IsNegativeInfinity, $"Result: {FormattedNumber}");
            FormattedNumber = new FormattedNumber($" {TextNegativeInfinity}");
            Assert.That(FormattedNumber.IsValid && FormattedNumber.Value.IsSpecial && FormattedNumber.DiscardedProlog == " " && FormattedNumber.BeforeExponent == TextNegativeInfinity && FormattedNumber.Exponent.Length == 0 && FormattedNumber.Suffix.Length == 0 && FormattedNumber.Value.IsNegativeInfinity, $"Result: {FormattedNumber}");
        }

        [Test]
        [Category("Coverage")]
        public static void FullParse()
        {
            if (SkipFullParse)
                return;

            FormattedNumber FormattedNumber;

            string Charset = "01.e-+";
            long N = Charset.Length;
            long T = N * N * N * N * N * N * N * N * N * N;
            //Debug.WriteLine($"T = {T}");
            double Percent = 1.0;
            for (long n = 0; n < T; n++)
            {
                string s = GenerateNumber(Charset, n);
                try
                {
                    FormattedNumber = new FormattedNumber(s);
                    Debug.Assert(FormattedNumber.ToString() == s || !FormattedNumber.IsValid);
                    Assert.That(FormattedNumber.ToString() == s || !FormattedNumber.IsValid, $"#n: {FormattedNumber}, Source={s}");
                }
                catch (Exception e)
                {
                    Debug.Assert(false);
                    Assert.That(false, $"#n: Source={s}\n\n{e.Message}");
                }

                double d = (100.0 * ((double)n)) / ((double)T);
                if (d >= Percent)
                {
                    //Debug.WriteLine(((int)Percent).ToString() + "%");
                    Percent += 1.0;
                }
            }
        }

        private static string GenerateNumber(string charset, long pattern)
        {
            if (pattern == 0)
                return "";

            string s = charset.Substring((int)(pattern % charset.Length), 1);
            s += GenerateNumber(charset, pattern / charset.Length);

            return s;
        }
        #endregion

        #region ToString tests
        [Test]
        [Category("Coverage")]
        public static void TestToString()
        {
            double PositiveNaN = double.NaN;
            double NegativeNaN = double.NaN;
            TestToString(PositiveNaN, NegativeNaN);

            double PositiveInfinity = double.PositiveInfinity;
            double NegativeInfinity = double.NegativeInfinity;
            TestToString(PositiveInfinity, NegativeInfinity);

            double PositiveZero = 0;
            double NegativeZero = -PositiveZero;
            TestToString(PositiveZero, NegativeZero);

            for (int ExponentIndex = 0; ExponentIndex <= 6; ExponentIndex++)
            {
                uint ExponentChoice;

                switch (ExponentIndex)
                {
                    default:
                    case 0:
                        ExponentChoice = 1;
                        break;
                    case 1:
                        ExponentChoice = 2;
                        break;
                    case 2:
                        ExponentChoice = 1022;
                        break;
                    case 3:
                        ExponentChoice = 1023;
                        break;
                    case 4:
                        ExponentChoice = 1024;
                        break;
                    case 5:
                        ExponentChoice = 2045;
                        break;
                    case 6:
                        ExponentChoice = 2046;
                        break;
                }

                TestToString(ExponentChoice);
            }
        }

        public static void TestToString(uint exponentChoice)
        {
            for (int SignificandIndex = 0; SignificandIndex <= 11; SignificandIndex++)
            {
                ulong SignificandChoice;

                switch (SignificandIndex)
                {
                    default:
                    case 0:
                        SignificandChoice = 0x0000000;
                        break;
                    case 1:
                        SignificandChoice = 0x0000001;
                        break;
                    case 2:
                        SignificandChoice = 0x0000002;
                        break;
                    case 3:
                        SignificandChoice = 0x0000100;
                        break;
                    case 4:
                        SignificandChoice = 0x000FF00;
                        break;
                    case 5:
                        SignificandChoice = 0x00FF000;
                        break;
                    case 6:
                        SignificandChoice = 0x0FF0000;
                        break;
                    case 7:
                        SignificandChoice = 0xFF00000;
                        break;
                    case 8:
                        SignificandChoice = 0x8000000;
                        break;
                    case 9:
                        SignificandChoice = 0xC000000;
                        break;
                    case 10:
                        SignificandChoice = 0xFFFFFFE;
                        break;
                    case 11:
                        SignificandChoice = 0xFFFFFFF;
                        break;
                }

                TestToString(exponentChoice, SignificandChoice);
            }
        }

        public static void TestToString(uint exponentChoice, ulong significandChoice)
        {
            byte[] Content = new byte[8];

            byte[] ExponentBytes = BitConverter.GetBytes(exponentChoice << 4);

            Content[7] = ExponentBytes[1];
            Content[6] = ExponentBytes[0];

            byte[] SignificandBytes = BitConverter.GetBytes(significandChoice);

            Content[6] |= SignificandBytes[6];
            Content[5] = SignificandBytes[5];
            Content[4] = SignificandBytes[4];
            Content[3] = SignificandBytes[3];
            Content[2] = SignificandBytes[2];
            Content[1] = SignificandBytes[1];
            Content[0] = SignificandBytes[0];

            double PositiveDouble = BitConverter.ToDouble(Content, 0);
            Content[7] |= 0x80;
            double NegativeDouble = BitConverter.ToDouble(Content, 0);

            TestToString(PositiveDouble, NegativeDouble);
        }

        public static void TestToString(double positiveDouble, double negativeDouble)
        {
            Number PositiveDoubleNumber = new Number(positiveDouble);
            Number NegativeDoubleNumber = new Number(negativeDouble);

            TestToString(positiveDouble, PositiveDoubleNumber, negativeDouble, NegativeDoubleNumber);

            TestToString(positiveDouble, PositiveDoubleNumber, negativeDouble, NegativeDoubleNumber, "E");
            TestToString(positiveDouble, PositiveDoubleNumber, negativeDouble, NegativeDoubleNumber, "F");
            TestToString(positiveDouble, PositiveDoubleNumber, negativeDouble, NegativeDoubleNumber, "G");

            for (int i = 0; i <= 99; i++)
            {
                TestToString(positiveDouble, PositiveDoubleNumber, negativeDouble, NegativeDoubleNumber, $"E{i}");
                TestToString(positiveDouble, PositiveDoubleNumber, negativeDouble, NegativeDoubleNumber, $"F{i}");
                TestToString(positiveDouble, PositiveDoubleNumber, negativeDouble, NegativeDoubleNumber, $"G{i}");
            }
        }

        public static void TestToString(double positiveDouble, Number positiveDoubleNumber, double negativeDouble, Number negativeDoubleNumber)
        {
            string DSP = positiveDouble.ToString();
            string NSP = positiveDoubleNumber.ToString();
            string DSN = negativeDouble.ToString();
            string NSN = negativeDoubleNumber.ToString();
            Assert.That(DSP == NSP, $"Expected: {DSP}, got: {NSP}");
            Assert.That(DSN == NSN, $"Expected: {DSN}, got: {NSN}");
        }

        public static void TestToString(double positiveDouble, Number positiveDoubleNumber, double negativeDouble, Number negativeDoubleNumber, string format)
        {
            string DSP = positiveDouble.ToString(format);
            string NSP = positiveDoubleNumber.ToString(format);
            string DSN = negativeDouble.ToString(format);
            string NSN = negativeDoubleNumber.ToString(format);
            Assert.That(DSP == NSP, $"Expected: {DSP}, got: {NSP}");
            Assert.That(DSN == NSN, $"Expected: {DSN}, got: {NSN}");
        }

        [Test]
        [Category("Coverage")]
        public static void TestInvalidFormats()
        {
            //string NullString = null;

            //TestInvalidFormats(NullString);
            TestInvalidFormats("");
            TestInvalidFormats("x");
            TestInvalidFormats("Ex");
            TestInvalidFormats("E-1");
            TestInvalidFormats("E100");
            TestInvalidFormats("Fx");
            TestInvalidFormats("F-1");
            TestInvalidFormats("F100");
            TestInvalidFormats("Gx");
            TestInvalidFormats("G-1");
            TestInvalidFormats("G100");
        }

        public static void TestInvalidFormats(string format)
        {
            double d = 0;
            Number n = new Number(d);

            Exception exDouble;
            Exception exNumber;

            exDouble = Assert.Throws<FormatException>(() => d.ToString(format));
            exNumber = Assert.Throws<FormatException>(() => n.ToString(format));
            Assert.That(exDouble.Message == exNumber.Message, $"Expected: {exDouble.Message}, got: {exNumber.Message}");
        }
        #endregion

        #region Arithmetic Tests
        [Test]
        [Category("Coverage")]
        public static void Add0()
        {
            double d1 = 1.2547856e2;
            double d2 = 5.478231405e-3;

            string Text1 = TestEnvironment.DoubleString(d1);
            string Text2 = TestEnvironment.DoubleString(d2);

            //Debug.Assert(false);
            FormattedNumber FormattedNumber1 = new FormattedNumber(Text1);
            FormattedNumber FormattedNumber2 = new FormattedNumber(Text2);

            Number Value1 = FormattedNumber1.Value;
            Number Value2 = FormattedNumber2.Value;
            Assert.That(Value1.CheatDouble == d1);
            Assert.That(Value2.CheatDouble == d2);
            Assert.That(Value1.ToString() == Text1, $"Result={Value1}, Expected={Text1}");
            Assert.That(Value2.ToString() == Text2, $"Result={Value2}, Expected={Text2}");

            Number Result = FormattedNumber1.Value + FormattedNumber2.Value;

            double d = d1 + d2;

            string ExpectedText = TestEnvironment.DoubleString(d);

            if (ExpectedText.Length > 4)
                ExpectedText = ExpectedText.Substring(0, ExpectedText.Length - 1);

            string ResultText = Result.ToString();
            if (ResultText.Length > ExpectedText.Length)
                ResultText = ResultText.Substring(0, ExpectedText.Length);

            Assert.That(ResultText == ExpectedText, $"Result={ResultText}, Expected={ExpectedText}");
        }

        [Test]
        [Category("Coverage")]
        public static void Subtract0()
        {
            double d1 = 1.2547856e2;
            double d2 = 5.478231405e-3;

            string Text1 = TestEnvironment.DoubleString(d1);
            string Text2 = TestEnvironment.DoubleString(d2);

            //Debug.Assert(false);
            FormattedNumber FormattedNumber1 = new FormattedNumber(Text1);
            FormattedNumber FormattedNumber2 = new FormattedNumber(Text2);

            Number Value1 = FormattedNumber1.Value;
            Number Value2 = FormattedNumber2.Value;
            Assert.That(Value1.CheatDouble == d1);
            Assert.That(Value2.CheatDouble == d2);
            Assert.That(Value1.ToString() == Text1, $"Result={Value1}, Expected={Text1}");
            Assert.That(Value2.ToString() == Text2, $"Result={Value2}, Expected={Text2}");

            Number Result = FormattedNumber1.Value - FormattedNumber2.Value;

            double d = d1 - d2;

            string ExpectedText = TestEnvironment.DoubleString(d);

            if (ExpectedText.Length > 4)
                ExpectedText = ExpectedText.Substring(0, ExpectedText.Length - 1);

            string ResultText = Result.ToString();
            if (ResultText.Length > ExpectedText.Length)
                ResultText = ResultText.Substring(0, ExpectedText.Length);

            Assert.That(ResultText == ExpectedText, $"Result={ResultText}, Expected={ExpectedText}");
        }

        [Test]
        [Category("Coverage")]
        public static void Multiply0()
        {
            double d1 = 1.2547856e2;
            double d2 = 5.478231405e-3;

            string Text1 = TestEnvironment.DoubleString(d1);
            string Text2 = TestEnvironment.DoubleString(d2);

            //Debug.Assert(false);
            FormattedNumber FormattedNumber1 = new FormattedNumber(Text1);
            FormattedNumber FormattedNumber2 = new FormattedNumber(Text2);

            Number Value1 = FormattedNumber1.Value;
            Number Value2 = FormattedNumber2.Value;
            Assert.That(Value1.CheatDouble == d1);
            Assert.That(Value2.CheatDouble == d2);
            Assert.That(Value1.ToString() == Text1, $"Result={Value1}, Expected={Text1}");
            Assert.That(Value2.ToString() == Text2, $"Result={Value2}, Expected={Text2}");

            Number Result = FormattedNumber1.Value * FormattedNumber2.Value;

            double d = d1 * d2;

            string ExpectedText = TestEnvironment.DoubleString(d);

            if (ExpectedText.Length > 4)
                ExpectedText = ExpectedText.Substring(0, ExpectedText.Length - 1);

            string ResultText = Result.ToString();
            if (ResultText.Length > ExpectedText.Length)
                ResultText = ResultText.Substring(0, ExpectedText.Length);

            Assert.That(ResultText == ExpectedText, $"Result={ResultText}, Expected={ExpectedText}");
        }

        [Test]
        [Category("Coverage")]
        public static void Divide0()
        {
            double d1 = 1.2547856e2;
            double d2 = 5.478231405e-3;

            string Text1 = TestEnvironment.DoubleString(d1);
            string Text2 = TestEnvironment.DoubleString(d2);

            //Debug.Assert(false);
            FormattedNumber FormattedNumber1 = new FormattedNumber(Text1);
            FormattedNumber FormattedNumber2 = new FormattedNumber(Text2);

            Number Value1 = FormattedNumber1.Value;
            Number Value2 = FormattedNumber2.Value;
            Assert.That(Value1.CheatDouble == d1);
            Assert.That(Value2.CheatDouble == d2);
            Assert.That(Value1.ToString() == Text1, $"Result={Value1}, Expected={Text1}");
            Assert.That(Value2.ToString() == Text2, $"Result={Value2}, Expected={Text2}");

            Number Result = FormattedNumber1.Value / FormattedNumber2.Value;

            double d = d1 / d2;

            string ExpectedText = TestEnvironment.DoubleString(d);

            if (ExpectedText.Length > 4)
                ExpectedText = ExpectedText.Substring(0, ExpectedText.Length - 1);

            string ResultText = Result.ToString();
            if (ResultText.Length > ExpectedText.Length)
                ResultText = ResultText.Substring(0, ExpectedText.Length);

            Assert.That(ResultText == ExpectedText, $"Result={ResultText}, Expected={ExpectedText}");
        }

        [Test]
        [Category("Coverage")]
        public static void Negate0()
        {
            double d1 = 1.2547856e2;

            string Text1 = TestEnvironment.DoubleString(d1);

            //Debug.Assert(false);
            FormattedNumber FormattedNumber1 = new FormattedNumber(Text1);

            Number Value1 = FormattedNumber1.Value;
            Assert.That(Value1.CheatDouble == d1);
            Assert.That(Value1.ToString() == Text1, $"Result={Value1}, Expected={Text1}");

            Number Result = -FormattedNumber1.Value;

            double d = -d1;

            string ExpectedText = TestEnvironment.DoubleString(d);

            if (ExpectedText.Length > 4)
                ExpectedText = ExpectedText.Substring(0, ExpectedText.Length - 1);

            string ResultText = Result.ToString();
            if (ResultText.Length > ExpectedText.Length)
                ResultText = ResultText.Substring(0, ExpectedText.Length);

            Assert.That(ResultText == ExpectedText, $"Result={ResultText}, Expected={ExpectedText}");
        }

        [Test]
        [Category("Coverage")]
        public static void Abs0()
        {
            double d1 = 1.2547856e2;

            string Text1 = TestEnvironment.DoubleString(d1);

            //Debug.Assert(false);
            FormattedNumber FormattedNumber1 = new FormattedNumber(Text1);

            Number Value1 = FormattedNumber1.Value;
            Assert.That(Value1.CheatDouble == d1);
            Assert.That(Value1.ToString() == Text1, $"Result={Value1}, Expected={Text1}");

            Number Result = FormattedNumber1.Value.Abs();

            double d = Math.Abs(d1);

            string ExpectedText = TestEnvironment.DoubleString(d);

            if (ExpectedText.Length > 4)
                ExpectedText = ExpectedText.Substring(0, ExpectedText.Length - 1);

            string ResultText = Result.ToString();
            if (ResultText.Length > ExpectedText.Length)
                ResultText = ResultText.Substring(0, ExpectedText.Length);

            Assert.That(ResultText == ExpectedText, $"Result={ResultText}, Expected={ExpectedText}");
        }

        [Test]
        [Category("Coverage")]
        public static void Exp0()
        {
            double d1 = 1.2547856e2;

            string Text1 = TestEnvironment.DoubleString(d1);

            //Debug.Assert(false);
            FormattedNumber FormattedNumber1 = new FormattedNumber(Text1);

            Number Value1 = FormattedNumber1.Value;
            Assert.That(Value1.CheatDouble == d1);
            Assert.That(Value1.ToString() == Text1, $"Result={Value1}, Expected={Text1}");

            Number Result = FormattedNumber1.Value.Exp();

            double d = Math.Exp(d1);

            string ExpectedText = TestEnvironment.DoubleString(d);

            string ResultText = Result.ToString();
            if (ResultText.Length > ExpectedText.Length)
                ResultText = ResultText.Substring(0, ExpectedText.Length);

            Assert.That(ResultText == ExpectedText, $"Result={ResultText}, Expected={ExpectedText}");
        }

        [Test]
        [Category("Coverage")]
        public static void Log0()
        {
            double d1 = 1.2547856e2;

            string Text1 = TestEnvironment.DoubleString(d1);

            //Debug.Assert(false);
            FormattedNumber FormattedNumber1 = new FormattedNumber(Text1);

            Number Value1 = FormattedNumber1.Value;
            Assert.That(Value1.CheatDouble == d1);
            Assert.That(Value1.ToString() == Text1, $"Result={Value1}, Expected={Text1}");

            Number Result = FormattedNumber1.Value.Log();

            double d = Math.Log(d1);

            string ExpectedText = TestEnvironment.DoubleString(d);

            if (ExpectedText.Length > 4)
                ExpectedText = ExpectedText.Substring(0, ExpectedText.Length - 1);

            string ResultText = Result.ToString();
            if (ResultText.Length > ExpectedText.Length)
                ResultText = ResultText.Substring(0, ExpectedText.Length);

            Assert.That(ResultText == ExpectedText, $"Result={ResultText}, Expected={ExpectedText}");
        }

        [Test]
        [Category("Coverage")]
        public static void Log10_0()
        {
            double d1 = 1.2547856e2;

            string Text1 = TestEnvironment.DoubleString(d1);

            //Debug.Assert(false);
            FormattedNumber FormattedNumber1 = new FormattedNumber(Text1);

            Number Value1 = FormattedNumber1.Value;
            Assert.That(Value1.CheatDouble == d1);
            Assert.That(Value1.ToString() == Text1, $"Result={Value1}, Expected={Text1}");

            Number Result = FormattedNumber1.Value.Log10();

            double d = Math.Log10(d1);

            string ExpectedText = TestEnvironment.DoubleString(d);

            if (ExpectedText.Length > 4)
                ExpectedText = ExpectedText.Substring(0, ExpectedText.Length - 1);

            string ResultText = Result.ToString();
            if (ResultText.Length > ExpectedText.Length)
                ResultText = ResultText.Substring(0, ExpectedText.Length);

            Assert.That(ResultText == ExpectedText, $"Result={ResultText}, Expected={ExpectedText}");
        }

        [Test]
        [Category("Coverage")]
        public static void Pow0()
        {
            double d1 = 1.2547856e2;
            double d2 = 5.478231405e-3;

            string Text1 = TestEnvironment.DoubleString(d1);
            string Text2 = TestEnvironment.DoubleString(d2);

            //Debug.Assert(false);
            FormattedNumber FormattedNumber1 = new FormattedNumber(Text1);
            FormattedNumber FormattedNumber2 = new FormattedNumber(Text2);

            Number Value1 = FormattedNumber1.Value;
            Number Value2 = FormattedNumber2.Value;
            Assert.That(Value1.CheatDouble == d1);
            Assert.That(Value2.CheatDouble == d2);
            Assert.That(Value1.ToString() == Text1, $"Result={Value1}, Expected={Text1}");
            Assert.That(Value2.ToString() == Text2, $"Result={Value2}, Expected={Text2}");

            Number Result = FormattedNumber1.Value.Pow(FormattedNumber2.Value);

            double d = Math.Pow(d1, d2);

            string ExpectedText = TestEnvironment.DoubleString(d);

            if (ExpectedText.Length > 4)
                ExpectedText = ExpectedText.Substring(0, ExpectedText.Length - 1);

            string ResultText = Result.ToString();
            if (ResultText.Length > ExpectedText.Length)
                ResultText = ResultText.Substring(0, ExpectedText.Length);

            Assert.That(ResultText == ExpectedText, $"Result={ResultText}, Expected={ExpectedText}");
        }

        [Test]
        [Category("Coverage")]
        public static void Sqrt0()
        {
            double d1 = 1.2547856e2;

            string Text1 = TestEnvironment.DoubleString(d1);

            //Debug.Assert(false);
            FormattedNumber FormattedNumber1 = new FormattedNumber(Text1);

            Number Value1 = FormattedNumber1.Value;
            Assert.That(Value1.CheatDouble == d1);
            Assert.That(Value1.ToString() == Text1, $"Result={Value1}, Expected={Text1}");

            Number Result = FormattedNumber1.Value.Sqrt();

            double d = Math.Sqrt(d1);

            string ExpectedText = TestEnvironment.DoubleString(d);

            if (ExpectedText.Length > 4)
                ExpectedText = ExpectedText.Substring(0, ExpectedText.Length - 1);

            string ResultText = Result.ToString();
            if (ResultText.Length > ExpectedText.Length)
                ResultText = ResultText.Substring(0, ExpectedText.Length);

            Assert.That(ResultText == ExpectedText, $"Result={ResultText}, Expected={ExpectedText}");
        }

        [Test]
        [Category("Coverage")]
        public static void ShiftLeft0()
        {
            double d1 = 125478;
            double d2 = 5;

            string Text1 = TestEnvironment.DoubleString(d1);
            string Text2 = TestEnvironment.DoubleString(d2);

            //Debug.Assert(false);
            FormattedNumber FormattedNumber1 = new FormattedNumber(Text1);
            FormattedNumber FormattedNumber2 = new FormattedNumber(Text2);

            Number Value1 = FormattedNumber1.Value;
            Number Value2 = FormattedNumber2.Value;
            Assert.That(Value1.CheatDouble == d1);
            Assert.That(Value2.CheatDouble == d2);
            Assert.That(Value1.ToString() == Text1, $"Result={Value1}, Expected={Text1}");
            Assert.That(Value2.ToString() == Text2, $"Result={Value2}, Expected={Text2}");

            Number Result = FormattedNumber1.Value.ShiftLeft(FormattedNumber2.Value);

            double d = (int)d1 << (int)d2;

            string ExpectedText = TestEnvironment.DoubleString(d);

            if (ExpectedText.Length > 4)
                ExpectedText = ExpectedText.Substring(0, ExpectedText.Length - 1);

            string ResultText = Result.ToString();
            if (ResultText.Length > ExpectedText.Length)
                ResultText = ResultText.Substring(0, ExpectedText.Length);

            Assert.That(ResultText == ExpectedText, $"Result={ResultText}, Expected={ExpectedText}");
        }

        [Test]
        [Category("Coverage")]
        public static void ShiftRight0()
        {
            double d1 = 125478;
            double d2 = 5;

            string Text1 = TestEnvironment.DoubleString(d1);
            string Text2 = TestEnvironment.DoubleString(d2);

            //Debug.Assert(false);
            FormattedNumber FormattedNumber1 = new FormattedNumber(Text1);
            FormattedNumber FormattedNumber2 = new FormattedNumber(Text2);

            Number Value1 = FormattedNumber1.Value;
            Number Value2 = FormattedNumber2.Value;
            Assert.That(Value1.CheatDouble == d1);
            Assert.That(Value2.CheatDouble == d2);
            Assert.That(Value1.ToString() == Text1, $"Result={Value1}, Expected={Text1}");
            Assert.That(Value2.ToString() == Text2, $"Result={Value2}, Expected={Text2}");

            Number Result = FormattedNumber1.Value.ShiftRight(FormattedNumber2.Value);

            double d = (int)d1 >> (int)d2;

            string ExpectedText = TestEnvironment.DoubleString(d);

            if (ExpectedText.Length > 4)
                ExpectedText = ExpectedText.Substring(0, ExpectedText.Length - 1);

            string ResultText = Result.ToString();
            if (ResultText.Length > ExpectedText.Length)
                ResultText = ResultText.Substring(0, ExpectedText.Length);

            Assert.That(ResultText == ExpectedText, $"Result={ResultText}, Expected={ExpectedText}");
        }

        [Test]
        [Category("Coverage")]
        public static void Remainder0()
        {
            double d1 = 1.2547856e2;
            double d2 = 5.478231405e-3;

            string Text1 = TestEnvironment.DoubleString(d1);
            string Text2 = TestEnvironment.DoubleString(d2);

            //Debug.Assert(false);
            FormattedNumber FormattedNumber1 = new FormattedNumber(Text1);
            FormattedNumber FormattedNumber2 = new FormattedNumber(Text2);

            Number Value1 = FormattedNumber1.Value;
            Number Value2 = FormattedNumber2.Value;
            Assert.That(Value1.CheatDouble == d1);
            Assert.That(Value2.CheatDouble == d2);
            Assert.That(Value1.ToString() == Text1, $"Result={Value1}, Expected={Text1}");
            Assert.That(Value2.ToString() == Text2, $"Result={Value2}, Expected={Text2}");

            Number Result = FormattedNumber1.Value.Remainder(FormattedNumber2.Value);

            double d = Math.IEEERemainder(d1, d2);

            string ExpectedText = TestEnvironment.DoubleString(d);

            if (ExpectedText.Length > 4)
                ExpectedText = ExpectedText.Substring(0, ExpectedText.Length - 1);

            string ResultText = Result.ToString();
            if (ResultText.Length > ExpectedText.Length)
                ResultText = ResultText.Substring(0, ExpectedText.Length);

            Assert.That(ResultText == ExpectedText, $"Result={ResultText}, Expected={ExpectedText}");
        }

        [Test]
        [Category("Coverage")]
        public static void BitwiseAnd0()
        {
            double d1 = 125478;
            double d2 = 5;

            string Text1 = TestEnvironment.DoubleString(d1);
            string Text2 = TestEnvironment.DoubleString(d2);

            //Debug.Assert(false);
            FormattedNumber FormattedNumber1 = new FormattedNumber(Text1);
            FormattedNumber FormattedNumber2 = new FormattedNumber(Text2);

            Number Value1 = FormattedNumber1.Value;
            Number Value2 = FormattedNumber2.Value;
            Assert.That(Value1.CheatDouble == d1);
            Assert.That(Value2.CheatDouble == d2);
            Assert.That(Value1.ToString() == Text1, $"Result={Value1}, Expected={Text1}");
            Assert.That(Value2.ToString() == Text2, $"Result={Value2}, Expected={Text2}");

            Number Result = FormattedNumber1.Value.BitwiseAnd(FormattedNumber2.Value);

            double d = (int)d1 & (int)d2;

            string ExpectedText = TestEnvironment.DoubleString(d);

            if (ExpectedText.Length > 4)
                ExpectedText = ExpectedText.Substring(0, ExpectedText.Length - 1);

            string ResultText = Result.ToString();
            if (ResultText.Length > ExpectedText.Length)
                ResultText = ResultText.Substring(0, ExpectedText.Length);

            Assert.That(ResultText == ExpectedText, $"Result={ResultText}, Expected={ExpectedText}");
        }

        [Test]
        [Category("Coverage")]
        public static void BitwiseOr0()
        {
            double d1 = 125478;
            double d2 = 5;

            string Text1 = TestEnvironment.DoubleString(d1);
            string Text2 = TestEnvironment.DoubleString(d2);

            //Debug.Assert(false);
            FormattedNumber FormattedNumber1 = new FormattedNumber(Text1);
            FormattedNumber FormattedNumber2 = new FormattedNumber(Text2);

            Number Value1 = FormattedNumber1.Value;
            Number Value2 = FormattedNumber2.Value;
            Assert.That(Value1.CheatDouble == d1);
            Assert.That(Value2.CheatDouble == d2);
            Assert.That(Value1.ToString() == Text1, $"Result={Value1}, Expected={Text1}");
            Assert.That(Value2.ToString() == Text2, $"Result={Value2}, Expected={Text2}");

            Number Result = FormattedNumber1.Value.BitwiseOr(FormattedNumber2.Value);

            double d = (int)d1 | (int)d2;

            string ExpectedText = TestEnvironment.DoubleString(d);

            if (ExpectedText.Length > 4)
                ExpectedText = ExpectedText.Substring(0, ExpectedText.Length - 1);

            string ResultText = Result.ToString();
            if (ResultText.Length > ExpectedText.Length)
                ResultText = ResultText.Substring(0, ExpectedText.Length);

            Assert.That(ResultText == ExpectedText, $"Result={ResultText}, Expected={ExpectedText}");
        }

        [Test]
        [Category("Coverage")]
        public static void BitwiseXor0()
        {
            double d1 = 125478;
            double d2 = 5;

            string Text1 = TestEnvironment.DoubleString(d1);
            string Text2 = TestEnvironment.DoubleString(d2);

            //Debug.Assert(false);
            FormattedNumber FormattedNumber1 = new FormattedNumber(Text1);
            FormattedNumber FormattedNumber2 = new FormattedNumber(Text2);

            Number Value1 = FormattedNumber1.Value;
            Number Value2 = FormattedNumber2.Value;
            Assert.That(Value1.CheatDouble == d1);
            Assert.That(Value2.CheatDouble == d2);
            Assert.That(Value1.ToString() == Text1, $"Result={Value1}, Expected={Text1}");
            Assert.That(Value2.ToString() == Text2, $"Result={Value2}, Expected={Text2}");

            Number Result = FormattedNumber1.Value.BitwiseXor(FormattedNumber2.Value);

            double d = (int)d1 ^ (int)d2;

            string ExpectedText = TestEnvironment.DoubleString(d);

            if (ExpectedText.Length > 4)
                ExpectedText = ExpectedText.Substring(0, ExpectedText.Length - 1);

            string ResultText = Result.ToString();
            if (ResultText.Length > ExpectedText.Length)
                ResultText = ResultText.Substring(0, ExpectedText.Length);

            Assert.That(ResultText == ExpectedText, $"Result={ResultText}, Expected={ExpectedText}");
        }

        [Test]
        [Category("Coverage")]
        public static void TryParseInt0()
        {
            double d1 = 125478;
            double d2 = 5.478231405e-3;

            string Text1 = TestEnvironment.DoubleString(d1);
            string Text2 = TestEnvironment.DoubleString(d2);

            //Debug.Assert(false);
            FormattedNumber FormattedNumber1 = new FormattedNumber(Text1);
            FormattedNumber FormattedNumber2 = new FormattedNumber(Text2);

            Number Value1 = FormattedNumber1.Value;
            Number Value2 = FormattedNumber2.Value;
            Assert.That(Value1.CheatDouble == d1);
            Assert.That(Value2.CheatDouble == d2);
            Assert.That(Value1.ToString() == Text1, $"Result={Value1}, Expected={Text1}");
            Assert.That(Value2.ToString() == Text2, $"Result={Value2}, Expected={Text2}");

            bool IsParsed1 = Value1.TryParseInt(out int Result1);
            bool IsParsed2 = Value2.TryParseInt(out int Result2);

            Assert.That(IsParsed1 && Result1 == (int)d1);
            Assert.That(!IsParsed1);
        }
        #endregion
    }
}
