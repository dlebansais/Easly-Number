﻿namespace EaslyNumber
{
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
        /// <param name="validityHandler">The handler to use to validate digits.</param>
        /// <param name="fieldHandler">The handler used to update the data field.</param>
        public CustomRadixIntegerTextPartition(string text, int radix, char radixPrefixCharacter, IsValidDigitHandler validityHandler, UpdateFieldHandler fieldHandler)
            : base(text, radix)
        {
            HasRadixPrefixCharacter = true;
            RadixPrefixCharacter = radixPrefixCharacter;
            ValidityHandler = validityHandler;
            FieldHandler = fieldHandler;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRadixIntegerTextPartition"/> class.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="radix">The radix to use.</param>
        /// <param name="validityHandler">The handler to use to validate digits.</param>
        /// <param name="fieldHandler">The handler used to update the data field.</param>
        public CustomRadixIntegerTextPartition(string text, int radix, IsValidDigitHandler validityHandler, UpdateFieldHandler fieldHandler)
            : base(text, radix)
        {
            HasRadixPrefixCharacter = false;
            ValidityHandler = validityHandler;
            FieldHandler = fieldHandler;
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
                        IntegerField.SetZero();

                        if (index + 1 == Text.Length || !HasRadixPrefixCharacter)
                            State = ParsingState.IntegerPart;
                        else
                        {
                            LastIntegerPartIndex = index;
                            State = ParsingState.Radix;
                        }
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
                        FirstIntegerPartIndex = index + 1;
                        IntegerField.SetZero();
                        State = ParsingState.IntegerPart;
                    }
                    else
                    {
                        FirstInvalidCharacterIndex = 0;
                        State = ParsingState.InvalidPart;
                    }
                    break;

                case ParsingState.IntegerPart:
                    if (ValidityHandler(c, out int DigitValue))
                    {
                        FieldHandler(IntegerField, DigitValue);
                    }
                    else
                    {
                        FirstInvalidCharacterIndex = index;
                        LastIntegerPartIndex = index;
                        State = ParsingState.InvalidPart;
                    }
                    break;
            }

            if (index + 1 == Text.Length && FirstIntegerPartIndex >= 0 && LastIntegerPartIndex < 0)
                LastIntegerPartIndex = Text.Length;
        }

        /// <summary>
        /// True of there is a radix character used to prefix the string.
        /// </summary>
        public bool HasRadixPrefixCharacter { get; }

        /// <summary>
        /// The radix character used to prefix the string.
        /// </summary>
        public char RadixPrefixCharacter { get; }

        /// <summary>
        /// The optional separator. This partition cannot have one.
        /// </summary>
        public override OptionalSeparator Separator { get { return OptionalSeparator.None; } }

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
    }
}
