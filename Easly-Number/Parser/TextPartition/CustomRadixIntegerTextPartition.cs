namespace EaslyNumber
{
    /// <summary>
    /// The partition of a string into different components of an integer number.
    /// </summary>
    internal abstract class CustomRadixIntegerTextPartition : NumberTextPartition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRadixIntegerTextPartition"/> class.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="radix">The radix to use.</param>
        /// <param name="validityHandler">The handler to use to validate digits.</param>
        /// <param name="digitHandler">The handler to use to convert to digits.</param>
        public CustomRadixIntegerTextPartition(string text, int radix, IsValidDigitHandler validityHandler, ToDigitHandler digitHandler)
            : base(text)
        {
            Radix = radix;
            ValidityHandler = validityHandler;
            DigitHandler = digitHandler;
        }

        /// <summary>
        /// The radix for digits.
        /// </summary>
        public int Radix { get; }

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
        /// True if the parsed number is zero.
        /// </summary>
        public bool IsZero
        {
            get
            {
                if (FirstIntegerPartIndex < 0 || LastIntegerPartIndex < FirstIntegerPartIndex + 1)
                    return false;

                for (int i = FirstIntegerPartIndex; i < LastIntegerPartIndex; i++)
                    if (Text[i] != '0')
                        return false;

                return true;
            }
        }

        /// <summary>
        /// Converts the parsed partition to bit fields.
        /// </summary>
        /// <param name="significandPrecision">The number of bits in the significand.</param>
        /// <param name="integerField">The bit field of the integer part upon return.</param>
        public virtual void ConvertToBitField(long significandPrecision, out BitField integerField)
        {
            long BitIndex;

            string IntegerString = Text.Substring(FirstIntegerPartIndex, LastIntegerPartIndex - FirstIntegerPartIndex);
            integerField = new BitField();
            BitIndex = 0;

            do
            {
                if (BitIndex >= significandPrecision)
                {
                    integerField.ShiftRight();
                    BitIndex--;
                }

                IntegerString = DividedByTwo(IntegerString, Radix, ValidityHandler, DigitHandler, out bool HasCarry);
                integerField.SetBit(BitIndex++, HasCarry);
            }
            while (IntegerString != "0");
        }
    }
}
