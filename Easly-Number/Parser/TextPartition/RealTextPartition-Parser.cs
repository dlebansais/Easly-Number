namespace EaslyNumber
{
    using System.Diagnostics;

    /// <summary>
    /// The partition of a string into different components of a real number.
    /// </summary>
    internal partial class RealTextPartition : NumberTextPartition
    {
        #region Parser
        /// <summary>
        /// Parses a new character.
        /// </summary>
        /// <param name="index">The position of the character to parse in <see cref="TextPartition.Text"/>.</param>
        public override void Parse(int index)
        {
            char c = Text[index];

            switch (State)
            {
                case ParsingState.Init:
                    InitCultureSeparator();
                    State = ParsingState.LeadingWhitespaces;
                    Parse(index);
                    break;

                case ParsingState.LeadingWhitespaces:
                    ParseLeadingWhitespaces(index, c);
                    break;

                case ParsingState.LeadingZeroes:
                    ParseLeadingZeroes(index, c);
                    break;

                case ParsingState.IntegerPart:
                    ParseIntegerPart(index, c);
                    break;

                case ParsingState.FractionalPart:
                    ParseFractionalPart(index, c);
                    break;

                case ParsingState.ExponentPart:
                    ParseExponentPart(index, c);
                    break;
            }

            if (index + 1 == Text.Length)
            {
                if (FirstIntegerPartIndex >= 0 && LastIntegerPartIndex < 0)
                    LastIntegerPartIndex = Text.Length;
                else if (FirstFractionalPartIndex >= 0 && LastFractionalPartIndex < 0)
                    LastFractionalPartIndex = Text.Length;
            }
        }

        /// <summary>
        /// Runs the parser in the <see cref="ParsingState.LeadingWhitespaces"/> state.
        /// </summary>
        /// <param name="index">Index of the parsed character.</param>
        /// <param name="c">The parsed character.</param>
        private void ParseLeadingWhitespaces(int index, char c)
        {
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
            else if (Number.IsValidDecimalDigit(c, out int DigitValue))
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
        }

        /// <summary>
        /// Runs the parser in the <see cref="ParsingState.LeadingZeroes"/> state.
        /// </summary>
        /// <param name="index">Index of the parsed character.</param>
        /// <param name="c">The parsed character.</param>
        private void ParseLeadingZeroes(int index, char c)
        {
            if (Number.IsValidDecimalDigit(c, out int DigitValue))
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
                ExponentIndex = index;

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
        }

        /// <summary>
        /// Runs the parser in the <see cref="ParsingState.IntegerPart"/> state.
        /// </summary>
        /// <param name="index">Index of the parsed character.</param>
        /// <param name="c">The parsed character.</param>
        private void ParseIntegerPart(int index, char c)
        {
            if (Number.IsValidDecimalDigit(c, out int DigitValue))
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
        }

        /// <summary>
        /// Runs the parser in the <see cref="ParsingState.FractionalPart"/> state.
        /// </summary>
        /// <param name="index">Index of the parsed character.</param>
        /// <param name="c">The parsed character.</param>
        private void ParseFractionalPart(int index, char c)
        {
            if (Number.IsValidDecimalDigit(c, out int DigitValue))
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
        }

        /// <summary>
        /// Runs the parser in the <see cref="ParsingState.ExponentPart"/> state.
        /// </summary>
        /// <param name="index">Index of the parsed character.</param>
        /// <param name="c">The parsed character.</param>
        private void ParseExponentPart(int index, char c)
        {
            if (Number.IsValidDecimalDigit(c, out int DigitValue))
            {
                LastExponentPartIndex = index + 1;
            }
            else if (c == '-' || c == '+')
            {
                bool IsExponentIndexRangeValid = ExponentIndex >= 0 && ExponentIndex < index;
                Debug.Assert(IsExponentIndexRangeValid);

                if (index == ExponentIndex + 1)
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
        }
        #endregion
    }
}
