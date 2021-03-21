namespace TestEaslyNumber
{
    using EaslyNumber;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    [TestFixture]
    public partial class TestNumber
    {
        [Test]
        [Category("Coverage")]
        public static void TestDoubleToString()
        {
            List<OutputDouble> TestList = new();
            TestDouble.GenerateDoubleOutput(TestList);

            Assembly CurrentAssembly = Assembly.GetExecutingAssembly();
            using Stream TestDataStream = CurrentAssembly.GetManifestResourceStream("TestEaslyNumber.DoubleOutput.txt")!;
            using StreamReader Reader = new StreamReader(TestDataStream);

            string DSP;
            string DSN;

            for (int i = 0; i < TestList.Count; i++)
            {
                DSP = Reader.ReadLine()!;
                DSN = Reader.ReadLine()!;
                TestDoubleToString(TestList[i], DSP, DSN);
            }
        }

        private static void TestDoubleToString(OutputDouble item, string positiveDoubleString, string negativeDoubleString)
        {
            string? Format = item.Format;
            Number PositiveNumber = new Number(item.Positive);
            Number NegativeNumber = new Number(item.Negative);
            const int RequiredPrecision = 14;

            string PositiveNumberString, NegativeNumberString;

            if (Format == null)
            {
                PositiveNumberString = PositiveNumber.ToString();
                NegativeNumberString = NegativeNumber.ToString();
            }
            else
            {
                PositiveNumberString = PositiveNumber.ToString(Format);
                NegativeNumberString = NegativeNumber.ToString(Format);
            }

            bool IsSPEqual = IsNumberStringEqual(positiveDoubleString, PositiveNumberString, RequiredPrecision);
            bool IsSNEqual = IsNumberStringEqual(negativeDoubleString, NegativeNumberString, RequiredPrecision);

            if (!IsSPEqual || !IsSNEqual)
            {
            }

            Assert.That(IsSPEqual, $"Expected: {positiveDoubleString}, got: {PositiveNumberString}");
            Assert.That(IsSNEqual, $"Expected: {negativeDoubleString}, got: {NegativeNumberString}");

            Number CopyP = new Number(PositiveNumberString);
            Number CopyN = new Number(NegativeNumberString);
            Assert.AreEqual(PositiveNumber.IsNaN, CopyP.IsNaN);
            Assert.AreEqual(NegativeNumber.IsNaN, CopyN.IsNaN);
        }

        [Test]
        [Category("Coverage")]
        public static void TestSingleToString()
        {
            List<OutputSingle> TestList = new();
            TestSingle.GenerateSingleOutput(TestList);

            Assembly CurrentAssembly = Assembly.GetExecutingAssembly();
            using Stream TestDataStream = CurrentAssembly.GetManifestResourceStream("TestEaslyNumber.SingleOutput.txt")!;
            using StreamReader Reader = new StreamReader(TestDataStream);

            string DSP;
            string DSN;

            for (int i = 0; i < TestList.Count; i++)
            {
                DSP = Reader.ReadLine()!;
                DSN = Reader.ReadLine()!;
                TestSingleToString(TestList[i], DSP, DSN);
            }
        }

        private static void TestSingleToString(OutputSingle item, string positiveSingleString, string negativeSingleString)
        {
            string? Format = item.Format;
            Number PositiveNumber = new Number(item.Positive);
            Number NegativeNumber = new Number(item.Negative);
            const int RequiredPrecision = 5;

            string PositiveNumberString, NegativeNumberString;

            if (Format == null)
            {
                PositiveNumberString = PositiveNumber.ToString();
                NegativeNumberString = NegativeNumber.ToString();
            }
            else
            {
                PositiveNumberString = PositiveNumber.ToString(Format);
                NegativeNumberString = NegativeNumber.ToString(Format);
            }

            bool IsSPEqual = IsNumberStringEqual(positiveSingleString, PositiveNumberString, RequiredPrecision);
            bool IsSNEqual = IsNumberStringEqual(negativeSingleString, NegativeNumberString, RequiredPrecision);

            if (!IsSPEqual || !IsSNEqual)
            {
            }

            Assert.That(IsSPEqual, $"Expected: {positiveSingleString}, got: {PositiveNumberString}");
            Assert.That(IsSNEqual, $"Expected: {negativeSingleString}, got: {NegativeNumberString}");

            Number CopyP = new Number(PositiveNumberString);
            Number CopyN = new Number(NegativeNumberString);
            Assert.AreEqual(PositiveNumber.IsNaN, CopyP.IsNaN);
            Assert.AreEqual(NegativeNumber.IsNaN, CopyN.IsNaN);
        }

        private static bool IsNumberStringEqual(string doubleString, string numberString, int requiredPrecision)
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

                if (DoubleExponentPartIndex > DoubleFractionalPartIndex + requiredPrecision)
                {
                    if (IsPartEqual(DoubleFractionalPart, NumberFractionalPart, requiredPrecision))
                        return true;
                }

                IsFractionalPartEqual = DoubleFractionalPart == NumberFractionalPart;
            }

            if (DoubleFractionalPartIndex < 0)
                DoubleFractionalPartIndex = doubleString.Length;
            if (NumberFractionalPartIndex < 0)
                NumberFractionalPartIndex = numberString.Length;

            if (IsFractionalPartEqual && DoubleFractionalPartIndex > requiredPrecision && NumberFractionalPartIndex > requiredPrecision)
            {
                string DoubleIntegerPart = doubleString.Substring(0, DoubleFractionalPartIndex);
                string NumberIntegerPart = numberString.Substring(0, NumberFractionalPartIndex);

                if (IsPartEqual(DoubleIntegerPart, NumberIntegerPart, requiredPrecision))
                    return true;
            }

            return false;
        }

        private static bool IsPartEqual(string doublePart, string numberPart, int requiredPrecision)
        {
            if (doublePart.Length > requiredPrecision)
                doublePart = doublePart.Substring(0, requiredPrecision);
            if (numberPart.Length > requiredPrecision)
                numberPart = numberPart.Substring(0, requiredPrecision);

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
    }
}
