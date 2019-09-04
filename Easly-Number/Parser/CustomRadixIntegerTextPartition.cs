namespace EaslyNumber
{
    /// <summary>
    /// The partition of a string into different components of an integer number.
    /// </summary>
    internal class CustomRadixIntegerTextPartition : TextPartition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRadixIntegerTextPartition"/> class.
        /// </summary>
        /// <param name="radix">The radix to use.</param>
        /// <param name="radixPrefixCharacter">The prefix character to use.</param>
        /// <param name="validityHandler">The handler to use to validate digits.</param>
        public CustomRadixIntegerTextPartition(int radix, char radixPrefixCharacter, IsValidDigitHandler validityHandler)
        {
            Radix = radix;
            HasRadixPrefixCharacter = true;
            RadixPrefixCharacter = radixPrefixCharacter;
            ValidityHandler = validityHandler;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRadixIntegerTextPartition"/> class.
        /// </summary>
        /// <param name="radix">The radix to use.</param>
        /// <param name="validityHandler">The handler to use to validate digits.</param>
        public CustomRadixIntegerTextPartition(int radix, IsValidDigitHandler validityHandler)
        {
            Radix = radix;
            HasRadixPrefixCharacter = false;
            ValidityHandler = validityHandler;
        }

        /// <summary>
        /// Parses a new character.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="index">The position of the character to parse in <paramref name="text"/>.</param>
        public override void Parse(string text, int index)
        {
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
                        if (index + 1 == text.Length || !HasRadixPrefixCharacter)
                            State = ParsingState.IntegerPart;
                        else
                            State = ParsingState.Radix;
                    }
                    else
                    {
                        FirstInvalidCharacterIndex = 0;
                        State = ParsingState.InvalidPart;
                    }
                    break;

                case ParsingState.Radix:
                    if (c == RadixPrefixCharacter && index + 1 < text.Length)
                    {
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
                    if (ValidityHandler(c, out int Value))
                    {
                    }
                    else
                    {
                        FirstInvalidCharacterIndex = index;
                        State = ParsingState.InvalidPart;
                    }
                    break;
            }
        }

        /// <summary>
        /// The radix for digits.
        /// </summary>
        public int Radix { get; }

        /// <summary>
        /// True of there is a radix character used to prefix the string.
        /// </summary>
        public bool HasRadixPrefixCharacter { get; }

        /// <summary>
        /// The radix character used to prefix the string.
        /// </summary>
        public char RadixPrefixCharacter { get; }

        /// <summary>
        /// Delegate type of a method that validates a digit.
        /// </summary>
        /// <param name="digit">The digit to validate.</param>
        /// <param name="value">The digit value, if valid.</param>
        /// <returns>True if valid; Otherwise, false.</returns>
        public delegate bool IsValidDigitHandler(char digit, out int value);

        /// <summary>
        /// The handler used to validate digits.
        /// </summary>
        public IsValidDigitHandler ValidityHandler { get; }
    }
}
