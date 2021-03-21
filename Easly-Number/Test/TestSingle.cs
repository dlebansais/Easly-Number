[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Test-Easly-Number")]
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Test-Feature")]

namespace EaslyNumber
{
    using System;
    using System.Collections.Generic;

    internal class TestSingle
    {
        public static void GenerateSingleOutput(List<OutputSingle> testList)
        {
            float PositiveNaN = float.NaN;
            float NegativeNaN = float.NaN;
            GenerateSingleOutput(testList, PositiveNaN, NegativeNaN);

            float PositiveInfinity = float.PositiveInfinity;
            float NegativeInfinity = float.NegativeInfinity;
            GenerateSingleOutput(testList, PositiveInfinity, NegativeInfinity);

            float PositiveZero = 0;
            float NegativeZero = -PositiveZero;
            GenerateSingleOutput(testList, PositiveZero, NegativeZero);

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
                        ExponentChoice = 126;
                        break;
                    case 3:
                        ExponentChoice = 127;
                        break;
                    case 4:
                        ExponentChoice = 128;
                        break;
                    case 5:
                        ExponentChoice = 253;
                        break;
                    case 6:
                        ExponentChoice = 254;
                        break;
                }

                GenerateSingleOutput(testList, ExponentChoice);
            }
        }

        private static void GenerateSingleOutput(List<OutputSingle> testList, uint exponentChoice)
        {
            for (int SignificandIndex = 0; SignificandIndex <= 11; SignificandIndex++)
            {
                ulong SignificandChoice;

                switch (SignificandIndex)
                {
                    default:
                    case 0:
                        SignificandChoice = 0x000000;
                        break;
                    case 1:
                        SignificandChoice = 0x000001;
                        break;
                    case 2:
                        SignificandChoice = 0x000002;
                        break;
                    case 3:
                        SignificandChoice = 0x000100;
                        break;
                    case 4:
                        SignificandChoice = 0x00FF00;
                        break;
                    case 5:
                        SignificandChoice = 0x07F800;
                        break;
                    case 6:
                        SignificandChoice = 0x3FC000;
                        break;
                    case 7:
                        SignificandChoice = 0x7F8000;
                        break;
                    case 8:
                        SignificandChoice = 0x400000;
                        break;
                    case 9:
                        SignificandChoice = 0x600000;
                        break;
                    case 10:
                        SignificandChoice = 0x7FFFFE;
                        break;
                    case 11:
                        SignificandChoice = 0x7FFFFF;
                        break;
                }

                GenerateSingleOutput(testList, exponentChoice, SignificandChoice);
            }
        }

        private static void GenerateSingleOutput(List<OutputSingle> testList, uint exponentChoice, ulong significandChoice)
        {
            byte[] Content = new byte[4];

            byte[] ExponentBytes = BitConverter.GetBytes(exponentChoice << 7);

            Content[3] = ExponentBytes[1];
            Content[2] = ExponentBytes[0];

            byte[] SignificandBytes = BitConverter.GetBytes(significandChoice);

            Content[2] |= SignificandBytes[2];
            Content[1] = SignificandBytes[1];
            Content[0] = SignificandBytes[0];

            float PositiveSingle = BitConverter.ToSingle(Content, 0);
            Content[3] |= 0x80;
            float NegativeSingle = BitConverter.ToSingle(Content, 0);

            GenerateSingleOutput(testList, PositiveSingle, NegativeSingle);
        }

        private static void GenerateSingleOutput(List<OutputSingle> testList, float positiveSingle, float negativeSingle)
        {
            GenerateSingleOutput(testList, positiveSingle, negativeSingle, null);

            GenerateSingleOutput(testList, positiveSingle, negativeSingle, string.Empty);
            GenerateSingleOutput(testList, positiveSingle, negativeSingle, "E");
            GenerateSingleOutput(testList, positiveSingle, negativeSingle, "F");
            GenerateSingleOutput(testList, positiveSingle, negativeSingle, "G");

            for (int i = 0; i <= 99; i++)
            {
                GenerateSingleOutput(testList, positiveSingle, negativeSingle, $"E{i}");
                GenerateSingleOutput(testList, positiveSingle, negativeSingle, $"F{i}");
                GenerateSingleOutput(testList, positiveSingle, negativeSingle, $"G{i}");
            }
        }

        private static void GenerateSingleOutput(List<OutputSingle> testList, float positiveSingle, float negativeSingle, string? format)
        {
            string DSP, DSN;

            if (format == null)
            {
                DSP = positiveSingle.ToString();
                DSN = negativeSingle.ToString();
            }
            else
            {
                DSP = positiveSingle.ToString(format);
                DSN = negativeSingle.ToString(format);
            }

            testList.Add(new OutputSingle() { Positive = positiveSingle, Negative = negativeSingle, Format = format, PositiveString = DSP, NegativeString = DSN });
        }
    }
}
