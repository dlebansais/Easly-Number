namespace EaslyNumber
{
    using System.Diagnostics;

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

            switch (State)
            {
                case ParsingState.Init:
                    State = ParsingState.LeadingWhitespaces;
                    Parse(index);
                    break;

                case ParsingState.LeadingWhitespaces:
                    ParseLeadingWhitespaces(index, c);
                    break;

                case ParsingState.Radix:
                    ParseRadix(index, c);
                    break;

                case ParsingState.IntegerPart:
                    ParseIntegerPart(index, c);
                    break;
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
            else if (c == '0')
            {
                State = ParsingState.Radix;
            }
            else
            {
                FirstInvalidCharacterIndex = 0;
                State = ParsingState.InvalidPart;
            }
        }

        /// <summary>
        /// Runs the parser in the <see cref="ParsingState.Radix"/> state.
        /// </summary>
        /// <param name="index">Index of the parsed character.</param>
        /// <param name="c">The parsed character.</param>
        private void ParseRadix(int index, char c)
        {
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
        }

        /// <summary>
        /// Runs the parser in the <see cref="ParsingState.IntegerPart"/> state.
        /// </summary>
        /// <param name="index">Index of the parsed character.</param>
        /// <param name="c">The parsed character.</param>
        private void ParseIntegerPart(int index, char c)
        {
            if (ValidityHandler(c, out int DigitValue))
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
        }

        /// <summary>
        /// The radix character used to prefix the string.
        /// </summary>
        public char RadixPrefixCharacter { get; }

        /// <summary>
        /// True if the partition includes a suffix part.
        /// </summary>
        public bool HasRadixPrefix { get { return RadixPrefix >= 0; } }

        /// <summary>
        /// Index of the prefix indicating a radix, -1 if not parsed.
        /// </summary>
        public int RadixPrefix { get; set; } = -1;

        /// <summary>
        /// Index to use for partition comparison.
        /// </summary>
        public override int ComparisonIndex
        {
            get
            {
                if (RadixPrefix >= 0)
                    return FirstInvalidCharacterIndex < 0 ? Text.Length : FirstInvalidCharacterIndex;
                else
                    return 0;
            }
        }
    }
}
