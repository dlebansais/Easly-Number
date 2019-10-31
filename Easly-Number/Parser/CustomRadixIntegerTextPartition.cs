namespace EaslyNumber
{
    using System.Diagnostics;

    /// <summary>
    /// The partition of a string into different components of an integer number.
    /// </summary>
    internal class CustomRadixIntegerTextPartition : TextPartition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRadixIntegerTextPartition"/> class.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="radix">The radix to use.</param>
        /// <param name="radixPrefixCharacter">The prefix character to use.</param>
        /// <param name="radixSuffixCharacter">The suffix character to use.</param>
        /// <param name="validityHandler">The handler to use to validate digits.</param>
        /// <param name="digitHandler">The handler to use to convert to digits.</param>
        /// <param name="fieldHandler">The handler used to update the data field.</param>
        public CustomRadixIntegerTextPartition(string text, int radix, char radixPrefixCharacter, char radixSuffixCharacter, IsValidDigitHandler validityHandler, ToDigitHandler digitHandler, UpdateFieldHandler fieldHandler)
            : base(text, radix)
        {
            HasRadixPrefixCharacter = true;
            RadixPrefixCharacter = radixPrefixCharacter;
            RadixSuffixCharacter = radixSuffixCharacter;
            ValidityHandler = validityHandler;
            DigitHandler = digitHandler;
            FieldHandler = fieldHandler;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRadixIntegerTextPartition"/> class.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="radix">The radix to use.</param>
        /// <param name="radixSuffixCharacter">The suffix character to use.</param>
        /// <param name="validityHandler">The handler to use to validate digits.</param>
        /// <param name="digitHandler">The handler to use to convert to digits.</param>
        /// <param name="fieldHandler">The handler used to update the data field.</param>
        public CustomRadixIntegerTextPartition(string text, int radix, char radixSuffixCharacter, IsValidDigitHandler validityHandler, ToDigitHandler digitHandler, UpdateFieldHandler fieldHandler)
            : base(text, radix)
        {
            HasRadixPrefixCharacter = false;
            RadixSuffixCharacter = radixSuffixCharacter;
            ValidityHandler = validityHandler;
            DigitHandler = digitHandler;
            FieldHandler = fieldHandler;
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
                        break;
                    }
                    else if (c == '0')
                    {
                        if (index + 1 == Text.Length || !HasRadixPrefixCharacter)
                        {
                            RadixPrefix = index;
                            FirstIntegerPartIndex = index + 1;
                            State = ParsingState.IntegerPart;
                        }
                        else
                        {
                            LastIntegerPartIndex = index;
                            State = ParsingState.Radix;
                        }
                    }
                    else if (ValidityHandler(c, out DigitValue))
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
                    break;

                case ParsingState.SuffixPart:
                    if (c == RadixSuffixCharacter)
                    {
                        FirstInvalidCharacterIndex = index + 1;
                        RadixSuffix = index;
                    }
                    else
                        FirstInvalidCharacterIndex = index;

                    State = ParsingState.InvalidPart;
                    break;
            }

            if (index + 1 == Text.Length && FirstIntegerPartIndex >= 0 && LastIntegerPartIndex < 0 && State != ParsingState.InvalidPart)
                if (RadixPrefix >= 0)
                    LastIntegerPartIndex = Text.Length;
                else
                {
                    FirstIntegerPartIndex = -1;
                    FirstInvalidCharacterIndex = 0;
                    State = ParsingState.InvalidPart;
                }
        }

        /// <summary>
        /// True if there is a radix character used to prefix the string.
        /// </summary>
        public bool HasRadixPrefixCharacter { get; }

        /// <summary>
        /// The radix character used to prefix the string.
        /// </summary>
        public char RadixPrefixCharacter { get; }

        /// <summary>
        /// The radix character used as suffix to the string.
        /// </summary>
        public char RadixSuffixCharacter { get; }

        /// <summary>
        /// The optional separator. This partition cannot have one.
        /// </summary>
        public override OptionalSeparator Separator { get { return OptionalSeparator.None; } }

        /// <summary>
        /// The handler used to validate digits.
        /// </summary>
        public IsValidDigitHandler ValidityHandler { get; }

        /// <summary>
        /// The handler to use to convert to digits.
        /// </summary>
        public ToDigitHandler DigitHandler { get; }

        /// <summary>
        /// Delegate type of a method that update the data field with a digit.
        /// </summary>
        /// <param name="field">The data field to update.</param>
        /// <param name="value">The digit value.</param>
        public delegate void UpdateFieldHandler(BitField field, int value);

        /// <summary>
        /// The handler used to update the data field.
        /// </summary>
        public UpdateFieldHandler FieldHandler { get; }

        /// <summary>
        /// Index to use for partition comparison.
        /// </summary>
        public override int ComparisonIndex
        {
            get
            {
                if (RadixPrefix >= 0 || RadixSuffix >= 0)
                    return FirstInvalidCharacterIndex < 0 ? Text.Length : FirstInvalidCharacterIndex;
                else
                    return 0;
            }
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

                IntegerString = DividedByTwo(IntegerString, Radix, ValidityHandler, DigitHandler, out bool HasCarry);
                integerField.SetBit(BitIndex++, HasCarry);
            }
            while (IntegerString != "0");

            fractionalField = new BitField();

            exponentField = new BitField();
            exponentField.SetBit(0, false);
        }
    }
}
