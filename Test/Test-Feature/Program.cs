namespace TestEaslyNumber
{
    using EaslyNumber;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;

    public static class Program
    {
        private static string NL = Environment.NewLine;
        private static string SP = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

        public static int Main(string[] args)
        {
            //CreateSingleTextFile();
            //CreateDoubleTextFile();

            return 0;
        }

        private static void CreateSingleTextFile()
        {
            List<OutputSingle> TestList = new();
            TestSingle.GenerateSingleOutput(TestList);

            using FileStream Stream = new FileStream("SingleOutput.txt", FileMode.Create);
            using StreamWriter Writer = new StreamWriter(Stream);

            foreach (OutputSingle Item in TestList)
            {
                Writer.WriteLine(Item.PositiveString);
                Writer.WriteLine(Item.NegativeString);
            }
        }

        private static void CreateDoubleTextFile()
        {
            List<OutputDouble> TestList = new();
            TestDouble.GenerateDoubleOutput(TestList);

            using FileStream Stream = new FileStream("DoubleOutput.txt", FileMode.Create);
            using StreamWriter Writer = new StreamWriter(Stream);

            foreach (OutputDouble Item in TestList)
            {
                Writer.WriteLine(Item.PositiveString);
                Writer.WriteLine(Item.NegativeString);
            }
        }
    }
}
