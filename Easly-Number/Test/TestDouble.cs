[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Test-Easly-Number")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Test-Feature")]

namespace EaslyNumber
{
    using System;
    using System.Collections.Generic;

#pragma warning disable SA1600 // Elements should be documented
    internal class TestDouble
    {
        public static void GenerateDoubleOutput(List<OutputDouble> testList)
        {
            double PositiveNaN = double.NaN;
            double NegativeNaN = double.NaN;
            GenerateDoubleOutput(testList, PositiveNaN, NegativeNaN);

            double PositiveInfinity = double.PositiveInfinity;
            double NegativeInfinity = double.NegativeInfinity;
            GenerateDoubleOutput(testList, PositiveInfinity, NegativeInfinity);

            double PositiveZero = 0;
            double NegativeZero = -PositiveZero;
            GenerateDoubleOutput(testList, PositiveZero, NegativeZero);

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

                GenerateDoubleOutput(testList, ExponentChoice);
            }
        }

        private static void GenerateDoubleOutput(List<OutputDouble> testList, uint exponentChoice)
        {
            for (int SignificandIndex = 0; SignificandIndex <= 11; SignificandIndex++)
            {
                ulong SignificandChoice;

                switch (SignificandIndex)
                {
                    default:
                    case 0:
                        SignificandChoice = 0x0000000000000;
                        break;
                    case 1:
                        SignificandChoice = 0x0000000000001;
                        break;
                    case 2:
                        SignificandChoice = 0x0000000000002;
                        break;
                    case 3:
                        SignificandChoice = 0x0000000000100;
                        break;
                    case 4:
                        SignificandChoice = 0x000000000FF00;
                        break;
                    case 5:
                        SignificandChoice = 0x0000000FF0000;
                        break;
                    case 6:
                        SignificandChoice = 0x0FF0000000000;
                        break;
                    case 7:
                        SignificandChoice = 0xFF00000000000;
                        break;
                    case 8:
                        SignificandChoice = 0x8000000000000;
                        break;
                    case 9:
                        SignificandChoice = 0xC000000000000;
                        break;
                    case 10:
                        SignificandChoice = 0xFFFFFFFFFFFFE;
                        break;
                    case 11:
                        SignificandChoice = 0xFFFFFFFFFFFFF;
                        break;
                }

                GenerateDoubleOutput(testList, exponentChoice, SignificandChoice);
            }
        }

        private static void GenerateDoubleOutput(List<OutputDouble> testList, uint exponentChoice, ulong significandChoice)
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

            GenerateDoubleOutput(testList, PositiveDouble, NegativeDouble);
        }

        private static void GenerateDoubleOutput(List<OutputDouble> testList, double positiveDouble, double negativeDouble)
        {
            GenerateDoubleOutput(testList, positiveDouble, negativeDouble, null);

            GenerateDoubleOutput(testList, positiveDouble, negativeDouble, string.Empty);
            GenerateDoubleOutput(testList, positiveDouble, negativeDouble, "E");
            GenerateDoubleOutput(testList, positiveDouble, negativeDouble, "F");
            GenerateDoubleOutput(testList, positiveDouble, negativeDouble, "G");

            for (int i = 0; i <= 99; i++)
            {
                GenerateDoubleOutput(testList, positiveDouble, negativeDouble, $"E{i}");
                GenerateDoubleOutput(testList, positiveDouble, negativeDouble, $"F{i}");
                GenerateDoubleOutput(testList, positiveDouble, negativeDouble, $"G{i}");
            }
        }

        private static void GenerateDoubleOutput(List<OutputDouble> testList, double positiveDouble, double negativeDouble, string? format)
        {
            string DSP, DSN;

            if (format is null)
            {
                DSP = positiveDouble.ToString();
                DSN = negativeDouble.ToString();
            }
            else
            {
                DSP = positiveDouble.ToString(format);
                DSN = negativeDouble.ToString(format);
            }

            testList.Add(new OutputDouble() { Positive = positiveDouble, Negative = negativeDouble, Format = format, PositiveString = DSP, NegativeString = DSN });
        }
    }
#pragma warning restore SA1600 // Elements should be documented
}
