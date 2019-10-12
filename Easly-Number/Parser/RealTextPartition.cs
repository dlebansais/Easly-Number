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
                        IntegerField.SetZero();
                        FractionalField.SetZero();
                        ExponentField.SetZero();
                        State = ParsingState.IntegerPart;
                    }
                    else if (Number.IsValidDecimalDigit(c, out DigitValue))
                    {
                        if (DigitValue == 0)
                        {
                            LastLeadingZeroIndex = index;
                            IntegerField.SetZero();
                            FractionalField.SetZero();
                            ExponentField.SetZero();
                            State = ParsingState.LeadingZeroes;
                        }
                        else
                        {
                            FirstIntegerPartIndex = index;
                            IntegerField.SetFromDigit(DigitValue);
                            FractionalField.SetZero();
                            ExponentField.SetZero();
                            State = ParsingState.IntegerPart;
                        }
                    }
                    else if (c == '.' || c == CultureDecimalSeparator)
                    {
                        DecimalSeparatorIndex = index;
                        IntegerField.SetZero();
                        FractionalField.SetZero();
                        ExponentField.SetZero();
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
                            IntegerField.SetFromDigit(DigitValue);
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
                        IntegerField.MultiplyBy10AndAdd(DigitValue);
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
                        FractionalField.MultiplyBy10AndAdd(DigitValue);
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
                        ExponentField.MultiplyBy10AndAdd(DigitValue);
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
    }
}
