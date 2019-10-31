namespace EaslyNumber
{
    using System.Diagnostics;

    /// <summary>
    /// The partition of a string into different components of a real number.
    /// </summary>
    internal class RealTextPartition : TextPartition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RealTextPartition"/> class.
        /// <param name="text">The string to parse.</param>
        /// </summary>
        public RealTextPartition(string text)
            : base(text, Number.DecimalRadix)
        {
        }

        /// <summary>
        /// The optional separator. This partition cannot have one.
        /// </summary>
        public override OptionalSeparator Separator
        {
            get
            {
                if (DecimalSeparatorIndex < 0)
                    return OptionalSeparator.None;
                else if (Text[DecimalSeparatorIndex] != CultureDecimalSeparator)
                    return OptionalSeparator.Normalized;
                else
                    return OptionalSeparator.CultureSpecific;
            }
        }

        /// <summary>
        /// Parses a new character.
        /// </summary>
        /// <param name="index">The position of the character to parse in <see cref="TextPartition.Text"/>.</param>
        public override void Parse(int index)
        {
            int DigitValue;

            char c = Text[index];

            switch (State)
            {
                case ParsingState.Init:
                    InitCultureSeparator();
                    State = ParsingState.LeadingWhitespaces;
                    Parse(index);
                    break;

                case ParsingState.LeadingWhitespaces:
                    if (char.IsWhiteSpace(c))
                    {
                        LastLeadingSpaceIndex = index;
                    }
                    else if (c == '-' || c == '+')
                    {
                        SignificandSign = c == '-' ? OptionalSign.Negative : OptionalSign.Positive;
                        FirstIntegerPartIndex = index + 1;
                        State = ParsingState.IntegerPart;
                    }
                    else if (Number.IsValidDecimalDigit(c, out DigitValue))
                    {
                        if (DigitValue == 0)
                        {
                            if (index + 1 == Text.Length)
                            {
                                FirstIntegerPartIndex = index;
                                State = ParsingState.IntegerPart;
                            }
                            else
                            {
                                LastLeadingZeroIndex = index;
                                State = ParsingState.LeadingZeroes;
                            }
                        }
                        else
                        {
                            FirstIntegerPartIndex = index;
                            State = ParsingState.IntegerPart;
                        }
                    }
                    else if (c == '.' || c == CultureDecimalSeparator)
                    {
                        DecimalSeparatorIndex = index;
                        State = ParsingState.FractionalPart;
                    }
                    else
                    {
                        FirstInvalidCharacterIndex = index;
                        State = ParsingState.InvalidPart;
                    }
                    break;

                case ParsingState.LeadingZeroes:
                    if (Number.IsValidDecimalDigit(c, out DigitValue))
                    {
                        if (DigitValue == 0)
                        {
                            LastLeadingZeroIndex = index;
                        }
                        else
                        {
                            FirstIntegerPartIndex = index;
                            State = ParsingState.IntegerPart;
                        }
                    }
                    else if (c == '.' || c == CultureDecimalSeparator)
                    {
                        Debug.Assert(LastLeadingZeroIndex >= 0);
                        FirstIntegerPartIndex = LastLeadingZeroIndex;
                        LastLeadingZeroIndex--;

                        DecimalSeparatorIndex = index;
                        LastIntegerPartIndex = index;
                        State = ParsingState.FractionalPart;
                    }
                    else if (c == 'E' || c == 'e')
                    {
                        ExponentCharacter = c == 'E' ? OptionalExponent.UpperCaseE : OptionalExponent.LowerCaseE;

                        Debug.Assert(LastLeadingZeroIndex >= 0);
                        FirstIntegerPartIndex = LastLeadingZeroIndex;
                        LastLeadingZeroIndex--;
                        LastIntegerPartIndex = index;
                        FirstExponentPartIndex = index + 1;
                        LastExponentPartIndex = FirstExponentPartIndex;

                        State = ParsingState.ExponentPart;
                    }
                    else
                    {
                        FirstInvalidCharacterIndex = index;
                        LastIntegerPartIndex = index;
                        State = ParsingState.InvalidPart;
                    }
                    break;

                case ParsingState.IntegerPart:
                    if (Number.IsValidDecimalDigit(c, out DigitValue))
                    {
                    }
                    else if (index == FirstIntegerPartIndex)
                    {
                        Debug.Assert(SignificandSign != OptionalSign.None);
                        Debug.Assert(index > 0);
                        Debug.Assert(LastLeadingZeroIndex == -1);

                        FirstInvalidCharacterIndex = 0;
                        LastIntegerPartIndex = FirstIntegerPartIndex;
                        State = ParsingState.InvalidPart;
                    }
                    else if (c == '.' || c == CultureDecimalSeparator)
                    {
                        DecimalSeparatorIndex = index;
                        LastIntegerPartIndex = index;
                        State = ParsingState.FractionalPart;
                    }
                    else if (c == 'E' || c == 'e')
                    {
                        ExponentCharacter = c == 'E' ? OptionalExponent.UpperCaseE : OptionalExponent.LowerCaseE;
                        ExponentIndex = index;
                        LastIntegerPartIndex = index;
                        FirstExponentPartIndex = index + 1;
                        LastExponentPartIndex = FirstExponentPartIndex;
                        State = ParsingState.ExponentPart;
                    }
                    else
                    {
                        FirstInvalidCharacterIndex = index;
                        LastIntegerPartIndex = index;
                        State = ParsingState.InvalidPart;
                    }
                    break;

                case ParsingState.FractionalPart:
                    if (Number.IsValidDecimalDigit(c, out DigitValue))
                    {
                    }
                    else if (c == 'E' || c == 'e')
                    {
                        ExponentCharacter = c == 'E' ? OptionalExponent.UpperCaseE : OptionalExponent.LowerCaseE;
                        ExponentIndex = index;
                        LastFractionalPartIndex = index;
                        FirstExponentPartIndex = index + 1;
                        LastExponentPartIndex = FirstExponentPartIndex;
                        State = ParsingState.ExponentPart;
                    }
                    else
                    {
                        FirstInvalidCharacterIndex = index;
                        LastFractionalPartIndex = index;
                        State = ParsingState.InvalidPart;
                    }
                    break;

                case ParsingState.ExponentPart:
                    if (Number.IsValidDecimalDigit(c, out DigitValue))
                    {
                        LastExponentPartIndex = index + 1;
                    }
                    else if (c == '-' || c == '+')
                    {
                        if (index == FirstExponentPartIndex)
                        {
                            ExponentSign = c == '-' ? OptionalSign.Negative : OptionalSign.Positive;
                            FirstExponentPartIndex++;
                            LastExponentPartIndex = FirstExponentPartIndex;
                        }
                        else
                        {
                            FirstInvalidCharacterIndex = index;
                            State = ParsingState.InvalidPart;
                        }
                    }
                    else
                    {
                        FirstInvalidCharacterIndex = index;
                        State = ParsingState.InvalidPart;
                    }
                    break;
            }

            if (index + 1 == Text.Length)
            {
                if (FirstIntegerPartIndex >= 0 && LastIntegerPartIndex < 0)
                    LastIntegerPartIndex = Text.Length;
                else if (FirstFractionalPartIndex >= 0 && LastFractionalPartIndex < 0)
                    LastFractionalPartIndex = Text.Length;
                else if (FirstExponentPartIndex >= 0 && LastExponentPartIndex < 0)
                    LastExponentPartIndex = Text.Length;
            }
        }

        /// <summary>
        /// Index to use for partition comparison.
        /// </summary>
        public override int ComparisonIndex
        {
            get { return FirstInvalidCharacterIndex < 0 ? Text.Length : FirstInvalidCharacterIndex; }
        }

        public override void ConvertToBitField(long significandPrecision, long exponentPrecision, out BitField integerField, out BitField fractionalField, out BitField exponentField)
        {
            long BitIndex;

            string IntegerString = Text.Substring(FirstIntegerPartIndex, LastIntegerPartIndex - FirstIntegerPartIndex);
            integerField = new BitField();
            BitIndex = 0;

            do
            {
                if (BitIndex >= significandPrecision)
                {
                    integerField.ShiftRight(1);
                    BitIndex--;
                }

                IntegerString = DividedByTwo(IntegerString, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit, out bool HasCarry);
                integerField.SetBit(BitIndex++, HasCarry);
            }
            while (IntegerString != "0");

            long IntegerBitIndex = integerField.SignificantBits;

            fractionalField = new BitField();

            if (HasFractionalPart)
            {
                string FractionalString = Text.Substring(FirstFractionalPartIndex, LastFractionalPartIndex - FirstFractionalPartIndex);
                BitIndex = 0;
                int StartingLength = FractionalString.Length;
                bool DebugString = false; // FractionalString == "47856";

                if (DebugString)
                {
                    Debug.Assert(false);
                    Debug.WriteLine(FractionalString);
                }

                do
                {
                    if (IntegerBitIndex + BitIndex >= significandPrecision)
                        break;

                    FractionalString = MultipliedByTwo(FractionalString, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit, false);

                    bool HasCarry = FractionalString.Length > StartingLength;
                    fractionalField.SetBit(BitIndex++, HasCarry);

                    if (HasCarry)
                        FractionalString = FractionalString.Substring(1);

                    if (DebugString)
                        Debug.WriteLine((HasCarry ? "(1) " : "(0) ") + FractionalString);
                }
                while (FractionalString != "0");
            }

            exponentField = new BitField();

            if (HasExponentPart)
            {
                string ExponentString = Text.Substring(FirstExponentPartIndex, LastExponentPartIndex - FirstExponentPartIndex);
                BitIndex = 0;

                do
                {
                    if (BitIndex >= exponentPrecision)
                    {
                        exponentField.ShiftRight(1);
                        BitIndex--;
                    }

                    ExponentString = DividedByTwo(ExponentString, Number.DecimalRadix, Number.IsValidDecimalDigit, Number.ToDecimalDigit, out bool HasCarry);
                    exponentField.SetBit(BitIndex++, HasCarry);
                }
                while (ExponentString != "0");
            }
        }
    }
}
