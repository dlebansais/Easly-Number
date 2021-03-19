namespace TestEaslyNumber2
{
    using EaslyNumber2;
    using NUnit.Framework;
    using System;
    using System.Globalization;

    [TestFixture]
    public partial class TestNumber
    {
        #region Setup
        [OneTimeSetUp]
        public static void InitTestSession()
        {
            TestEaslyNumber.TestEnvironment.InitTestSession();

            NL = Environment.NewLine;
            SP = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
        }

        private static string NL = string.Empty;
        private static string SP = string.Empty;
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

            TestToString(positiveDouble, PositiveDoubleNumber, negativeDouble, NegativeDoubleNumber, "");
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

            bool IsSPEqual = IsNumberStringEqual(DSP, NSP);
            bool IsSNEqual = IsNumberStringEqual(DSN, NSN);

            if (!IsSPEqual || !IsSNEqual)
            {
            }

            Assert.That(IsSPEqual, $"Expected: {DSP}, got: {NSP}");
            Assert.That(IsSNEqual, $"Expected: {DSN}, got: {NSN}");
        }

        public static void TestToString(double positiveDouble, Number positiveDoubleNumber, double negativeDouble, Number negativeDoubleNumber, string format)
        {
            string DSP = positiveDouble.ToString(format);
            string NSP = positiveDoubleNumber.ToString(format);
            string DSN = negativeDouble.ToString(format);
            string NSN = negativeDoubleNumber.ToString(format);

            bool IsSPEqual = IsNumberStringEqual(DSP, NSP);
            bool IsSNEqual = IsNumberStringEqual(DSN, NSN);

            if (!IsSPEqual || !IsSNEqual)
            {
            }

            Assert.That(IsSPEqual, $"Expected: {DSP}, got: {NSP}");
            Assert.That(IsSNEqual, $"Expected: {DSN}, got: {NSN}");
        }

        const int RequiredPrecision = 7;

        private static bool IsNumberStringEqual(string doubleString, string numberString)
        {
            if (doubleString == numberString)
                return true;

            int DoubleFractionalPartIndex = doubleString.IndexOf(SP);
            int NumberFractionalPartIndex = numberString.IndexOf(SP);

            if (DoubleFractionalPartIndex != NumberFractionalPartIndex)
                return false;

            bool IsFractionalPartEqual = true;

            if (DoubleFractionalPartIndex >= 0)
            {
                int DoubleExponentPartIndex = doubleString.ToUpper().IndexOf("E", DoubleFractionalPartIndex + 1);
                int NumberExponentPartIndex = numberString.ToUpper().IndexOf("E", NumberFractionalPartIndex + 1);

                if (DoubleExponentPartIndex < 0)
                    DoubleExponentPartIndex = doubleString.Length;
                if (NumberExponentPartIndex < 0)
                    NumberExponentPartIndex = numberString.Length;

                string DoubleFractionalPart = doubleString.Substring(DoubleFractionalPartIndex + 1, DoubleExponentPartIndex - DoubleFractionalPartIndex - 1);
                string NumberFractionalPart = numberString.Substring(NumberFractionalPartIndex + 1, NumberExponentPartIndex - NumberFractionalPartIndex - 1);

                if (DoubleExponentPartIndex > DoubleFractionalPartIndex + RequiredPrecision)
                {
                    if (IsPartEqual(DoubleFractionalPart, NumberFractionalPart))
                    {
                        /*System.Diagnostics.Debug.WriteLine(doubleString);
                        System.Diagnostics.Debug.WriteLine(numberString);
                        System.Diagnostics.Debug.WriteLine("");*/
                        return true;
                    }
                }

                IsFractionalPartEqual = DoubleFractionalPart == NumberFractionalPart;
            }

            if (DoubleFractionalPartIndex < 0)
                DoubleFractionalPartIndex = doubleString.Length;
            if (NumberFractionalPartIndex < 0)
                NumberFractionalPartIndex = numberString.Length;

            if (IsFractionalPartEqual && DoubleFractionalPartIndex > RequiredPrecision && NumberFractionalPartIndex > RequiredPrecision)
            {
                string DoubleIntegerPart = doubleString.Substring(0, DoubleFractionalPartIndex);
                string NumberIntegerPart = numberString.Substring(0, NumberFractionalPartIndex);

                if (IsPartEqual(DoubleIntegerPart, NumberIntegerPart))
                {
                    /*System.Diagnostics.Debug.WriteLine(doubleString);
                    System.Diagnostics.Debug.WriteLine(numberString);
                    System.Diagnostics.Debug.WriteLine("");*/
                    return true;
                }
            }

            return false;
        }

        private static bool IsPartEqual(string doublePart, string numberPart)
        {
            if (doublePart.Length > RequiredPrecision)
                doublePart = doublePart.Substring(0, RequiredPrecision);
            if (numberPart.Length > RequiredPrecision)
                numberPart = numberPart.Substring(0, RequiredPrecision);

            while (doublePart.Length < numberPart.Length)
                doublePart += "0";
            while (numberPart.Length < doublePart.Length)
                numberPart += "0";

            if (long.TryParse(doublePart, out long DoubleFractional) && long.TryParse(numberPart, out long NumberFractional))
            {
                long Diff = DoubleFractional - NumberFractional;
                if (Diff < 10)
                    return true;
            }

            return false;
        }

        [Test]
        [Category("Coverage")]
        public static void TestCopiedFormats()
        {
            TestCopiedFormats("Ex");
            TestCopiedFormats("E-1");
            TestCopiedFormats("E100");
            TestCopiedFormats("Fx");
            TestCopiedFormats("F-1");
            TestCopiedFormats("F100");
            TestCopiedFormats("Gx");
            TestCopiedFormats("G-1");
            TestCopiedFormats("G100");
            TestCopiedFormats("zz");
        }

        public static void TestCopiedFormats(string format)
        {
            double d = 0;
            Number n = new Number(d);

            string s = "*";

            s = d.ToString(format);
            Assert.That(s == format, $"Double: format '{format}' was expected to give {s}");

            s = n.ToString(format);
            Assert.That(s == format, $"Number: format '{format}' was expected to give {s}");
        }

        [Test]
        [Category("Coverage")]
        public static void TestInvalidFormats()
        {
            TestInvalidFormats("X");
            TestInvalidFormats("x");
            TestInvalidFormats("D");
            TestInvalidFormats("d");
            TestInvalidFormats("Z");
            TestInvalidFormats("z");
        }

        public static void TestInvalidFormats(string format)
        {
            double d = 0;
            Number n = new Number(d);

            Exception? exDouble;
            Exception? exNumber;

            string s = "*";

            try
            {
                s = d.ToString(format);
                Assert.That(s == "*", $"Double: format '{format}' did not throw an exception, but gave {s}");
            }
            catch
            {
            }

            try
            {
                s = n.ToString(format);
                Assert.That(s == "*", $"Number: format '{format}' did not throw an exception, but gave {s}");
            }
            catch
            {
            }

            exDouble = Assert.Throws<FormatException>(() => d.ToString(format));
            exNumber = Assert.Throws<FormatException>(() => n.ToString(format));
            Assert.That(exDouble?.Message == exNumber?.Message, $"Expected: '{exDouble?.Message}', got: '{exNumber?.Message}'. Format: {format}");
        }
        #endregion
    }
}
