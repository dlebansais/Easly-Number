namespace EaslyNumber
{
    using System.Diagnostics;

    /// <summary>
    /// The partition of a string into different components of a real number.
    /// </summary>
    internal class RealTextPartition : TextPartition
    {
        /// <summary>
        /// Parses a new character.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="index">The position of the character to parse in <paramref name="text"/>.</param>
        public override void Parse(string text, int index)
        {
            int DigitValue;

            char c = text[index];

            switch (State)
            {
                case ParsingState.Init:
                    InitCultureSeparator();
                    State = ParsingState.LeadingWhitespaces;
                    Parse(text, index);
                    break;

                case ParsingState.LeadingWhitespaces:
                    if (char.IsWhiteSpace(c))
                    {
                        LastLeadingSpaceIndex = index;
                        break;
                    }
                    else if (c == '0')
                    {
                        LastLeadingZeroIndex = index;
                        State = ParsingState.LeadingZeroes;
                    }
                    else if (c == '-' || c == '+')
                    {
                        SignificandSign = c == '-' ? OptionalSign.Negative : OptionalSign.Positive;
                        FirstIntegerPartIndex = index + 1;
                        State = ParsingState.IntegerPart;
                    }
                    else if (c > '0' && c <= '9')
                    {
                        FirstIntegerPartIndex = index;
                        State = ParsingState.IntegerPart;
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
                    if (c == '0')
                    {
                        LastLeadingZeroIndex = index;
                    }
                    else if (c > '0' && c <= '9')
                    {
                        FirstIntegerPartIndex = index;
                        State = ParsingState.IntegerPart;
                    }
                    else if (c == '.' || c == CultureDecimalSeparator)
                    {
                        Debug.Assert(LastLeadingZeroIndex >= 0);
                        FirstIntegerPartIndex = LastLeadingZeroIndex;
                        LastLeadingZeroIndex--;

                        DecimalSeparatorIndex = index;
                        State = ParsingState.FractionalPart;
                    }
                    else if (c == 'E' || c == 'e')
                    {
                        Exponent = c == 'E' ? OptionalExponent.UpperCaseE : OptionalExponent.LowerCaseE;

                        Debug.Assert(LastLeadingZeroIndex >= 0);
                        FirstIntegerPartIndex = LastLeadingZeroIndex;
                        LastLeadingZeroIndex--;

                        State = ParsingState.ExponentPart;
                    }
                    else
                    {
                        FirstInvalidCharacterIndex = index;
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
                        State = ParsingState.InvalidPart;
                    }
                    else if (c == '.' || c == CultureDecimalSeparator)
                    {
                        DecimalSeparatorIndex = index;
                        State = ParsingState.FractionalPart;
                    }
                    else if (c == 'E' || c == 'e')
                    {
                        Exponent = c == 'E' ? OptionalExponent.UpperCaseE : OptionalExponent.LowerCaseE;
                        ExponentIndex = index;
                        State = ParsingState.ExponentPart;
                    }
                    else
                    {
                        FirstInvalidCharacterIndex = index;
                        State = ParsingState.InvalidPart;
                    }
                    break;

                case ParsingState.FractionalPart:
                    if (Number.IsValidDecimalDigit(c, out DigitValue))
                    {
                    }
                    else if (c == 'E' || c == 'e')
                    {
                        Exponent = c == 'E' ? OptionalExponent.UpperCaseE : OptionalExponent.LowerCaseE;
                        ExponentIndex = index;
                        State = ParsingState.ExponentPart;
                    }
                    else
                    {
                        FirstInvalidCharacterIndex = index;
                        State = ParsingState.InvalidPart;
                    }
                    break;

                case ParsingState.ExponentPart:
                    if (Number.IsValidDecimalDigit(c, out DigitValue))
                    {
                    }
                    else if (c == '-' || c == '+')
                    {
                        if (index == ExponentIndex)
                        {
                            ExponentSign = c == '-' ? OptionalSign.Negative : OptionalSign.Positive;
                            ExponentIndex = index + 1;
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
        }
    }
}
