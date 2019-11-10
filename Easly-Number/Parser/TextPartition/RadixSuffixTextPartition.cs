namespace EaslyNumber
{
    using System.Diagnostics;

    /// <summary>
    /// The partition of a string into different components of an integer number.
    /// </summary>
    internal class RadixSuffixTextPartition : CustomRadixIntegerTextPartition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RadixSuffixTextPartition"/> class.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="radix">The radix to use.</param>
        /// <param name="radixSuffixCharacter">The suffix character to use.</param>
        /// <param name="validityHandler">The handler to use to validate digits.</param>
        /// <param name="digitHandler">The handler to use to convert to digits.</param>
        public RadixSuffixTextPartition(string text, int radix, char radixSuffixCharacter, IsValidDigitHandler validityHandler, ToDigitHandler digitHandler)
            : base(text, radix, validityHandler, digitHandler)
        {
            RadixSuffixCharacter = radixSuffixCharacter;
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

                case ParsingState.IntegerPart:
                    ParseIntegerPart(index, c);
                    break;

                case ParsingState.SuffixPart:
                    ParseSuffixPart(index, c);
                    break;
            }

            if (index + 1 == Text.Length && FirstIntegerPartIndex >= 0 && LastIntegerPartIndex < 0 && State != ParsingState.InvalidPart)
            {
                Debug.Assert(RadixSuffix < 0);

                FirstIntegerPartIndex = -1;
                FirstInvalidCharacterIndex = 0;
                State = ParsingState.InvalidPart;
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
            else if (ValidityHandler(c, out int DigitValue))
            {
                FirstIntegerPartIndex = index;
                State = ParsingState.IntegerPart;
            }
            else
            {
                FirstInvalidCharacterIndex = 0;
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
            if (ValidityHandler(c, out int DigitValue))
            {
            }
            else
            {
                LastIntegerPartIndex = index;

                if (c == ':' && index + 1 < Text.Length)
                    State = ParsingState.SuffixPart;
                else
                {
                    FirstInvalidCharacterIndex = index;
                    State = ParsingState.InvalidPart;
                }
            }
        }

        /// <summary>
        /// Runs the parser in the <see cref="ParsingState.SuffixPart"/> state.
        /// </summary>
        /// <param name="index">Index of the parsed character.</param>
        /// <param name="c">The parsed character.</param>
        private void ParseSuffixPart(int index, char c)
        {
            if (c == RadixSuffixCharacter)
            {
                if (index + 1 < Text.Length)
                    FirstInvalidCharacterIndex = index + 1;

                RadixSuffix = index;
            }
            else
                FirstInvalidCharacterIndex = index;

            State = ParsingState.InvalidPart;
        }

        /// <summary>
        /// The radix character used as suffix to the string.
        /// </summary>
        public char RadixSuffixCharacter { get; }

        /// <summary>
        /// True if the partition includes a suffix part.
        /// </summary>
        public bool HasRadixSuffix { get { return RadixSuffix >= 0; } }

        /// <summary>
        /// Index of the suffix indicating a radix, -1 if not parsed.
        /// </summary>
        public int RadixSuffix { get; set; } = -1;

        /// <summary>
        /// Index to use for partition comparison.
        /// </summary>
        public override int ComparisonIndex
        {
            get
            {
                if (RadixSuffix >= 0)
                    return FirstInvalidCharacterIndex < 0 ? Text.Length : FirstInvalidCharacterIndex;
                else
                    return 0;
            }
        }
    }
}
