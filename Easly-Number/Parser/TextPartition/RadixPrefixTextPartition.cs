namespace EaslyNumber
{
    /// <summary>
    /// The partition of a string into different components of an integer number.
    /// </summary>
    internal class RadixPrefixTextPartition : CustomRadixIntegerTextPartition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RadixPrefixTextPartition"/> class.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="radix">The radix to use.</param>
        /// <param name="radixPrefixCharacter">The prefix character to use.</param>
        /// <param name="validityHandler">The handler to use to validate digits.</param>
        /// <param name="digitHandler">The handler to use to convert to digits.</param>
        public RadixPrefixTextPartition(string text, int radix, char radixPrefixCharacter, IsValidDigitHandler validityHandler, ToDigitHandler digitHandler)
            : base(text, radix, validityHandler, digitHandler)
        {
            RadixPrefixCharacter = radixPrefixCharacter;
        }

        /// <summary>
        /// Parses a new character.
        /// </summary>
        /// <param name="index">The position of the character to parse in <see cref="TextPartition.Text"/>.</param>
        public override void Parse(int index)
        {
            char c = Text[index];
            int DigitValue;

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
                    else if (c == '0')
                    {
                        State = ParsingState.Radix;
                    }
                    else
                    {
                        FirstInvalidCharacterIndex = 0;
                        State = ParsingState.InvalidPart;
                    }
                    break;

                case ParsingState.Radix:
                    if (c == RadixPrefixCharacter && index + 1 < Text.Length)
                    {
                        RadixPrefix = index;
                        FirstIntegerPartIndex = index + 1;
                        State = ParsingState.IntegerPart;
                    }
                    else
                    {
                        FirstInvalidCharacterIndex = 0;
                        State = ParsingState.InvalidPart;
                    }
                    break;

                case ParsingState.IntegerPart:
                    if (ValidityHandler(c, out DigitValue))
                    {
                        if (index + 1 == Text.Length)
                            LastIntegerPartIndex = Text.Length;
                    }
                    else
                    {
                        LastIntegerPartIndex = index;

                        FirstInvalidCharacterIndex = index;
                        State = ParsingState.InvalidPart;
                    }
                    break;
            }
        }

        /// <summary>
        /// The radix character used to prefix the string.
        /// </summary>
        public char RadixPrefixCharacter { get; }
    }
}
