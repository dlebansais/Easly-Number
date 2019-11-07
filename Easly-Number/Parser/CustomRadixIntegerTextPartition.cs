namespace EaslyNumber
{
    /// <summary>
    /// The partition of a string into different components of an integer number.
    /// </summary>
    internal abstract class CustomRadixIntegerTextPartition : TextPartition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRadixIntegerTextPartition"/> class.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="radix">The radix to use.</param>
        /// <param name="validityHandler">The handler to use to validate digits.</param>
        /// <param name="digitHandler">The handler to use to convert to digits.</param>
        /// <param name="fieldHandler">The handler used to update the data field.</param>
        public CustomRadixIntegerTextPartition(string text, int radix, IsValidDigitHandler validityHandler, ToDigitHandler digitHandler, UpdateFieldHandler fieldHandler)
            : base(text, radix)
        {
            ValidityHandler = validityHandler;
            DigitHandler = digitHandler;
            FieldHandler = fieldHandler;
        }

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
