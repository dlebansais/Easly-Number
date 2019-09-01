namespace EaslyNumber
{
    /// <summary>
    /// The partition of a string into different components of an integer number.
    /// </summary>
    internal class CustomBaseIntegerTextPartition : TextPartition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomBaseIntegerTextPartition"/> class.
        /// </summary>
        /// <param name="integerBase">The integer base to use.</param>
        /// <param name="basePrefixCharacter">The prefix character to use.</param>
        /// <param name="validityHandler">The handler to use to validate digits.</param>
        public CustomBaseIntegerTextPartition(int integerBase, char basePrefixCharacter, IsValidDigitHandler validityHandler)
        {
            IntegerBase = integerBase;
            HasBasePrefixCharacter = true;
            BasePrefixCharacter = basePrefixCharacter;
            ValidityHandler = validityHandler;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomBaseIntegerTextPartition"/> class.
        /// </summary>
        /// <param name="integerBase">The integer base to use.</param>
        /// <param name="validityHandler">The handler to use to validate digits.</param>
        public CustomBaseIntegerTextPartition(int integerBase, IsValidDigitHandler validityHandler)
        {
            IntegerBase = integerBase;
            HasBasePrefixCharacter = false;
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
                        if (index + 1 == text.Length || !HasBasePrefixCharacter)
                            State = ParsingState.IntegerPart;
                        else
                            State = ParsingState.IntegerBase;
                    }
                    else
                    {
                        FirstInvalidCharacterIndex = 0;
                        State = ParsingState.InvalidPart;
                    }
                    break;

                case ParsingState.IntegerBase:
                    if (c == BasePrefixCharacter && index + 1 < text.Length)
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
        /// The integer base for digits.
        /// </summary>
        public int IntegerBase { get; }

        /// <summary>
        /// True of there is a base character used to prefix the string.
        /// </summary>
        public bool HasBasePrefixCharacter { get; }

        /// <summary>
        /// The base character used to prefix the string.
        /// </summary>
        public char BasePrefixCharacter { get; }

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
