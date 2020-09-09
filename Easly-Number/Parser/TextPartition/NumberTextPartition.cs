namespace EaslyNumber
{
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    /// The partition of a string into different components of a number.
    /// </summary>
    internal abstract class NumberTextPartition : TextPartition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberTextPartition"/> class.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        public NumberTextPartition(string text)
            : base(text)
        {
        }

        /// <summary>
        /// Index of the last optional zero character, -1 if not parsed.
        /// </summary>
        public int LastLeadingZeroIndex { get; set; } = -1;

        /// <summary>
        /// The beginning of <see cref="TextPartition.Text"/> that can be ignored.
        /// </summary>
        public override string DiscardedProlog
        {
            get
            {
                int LastDiscardedIndex = -1;
                if (LastDiscardedIndex < LastLeadingSpaceIndex)
                    LastDiscardedIndex = LastLeadingSpaceIndex;
                if (LastDiscardedIndex < LastLeadingZeroIndex)
                    LastDiscardedIndex = LastLeadingZeroIndex;

                return LastDiscardedIndex < 0 ? string.Empty : Text.Substring(0, LastDiscardedIndex + 1);
            }
        }

        /// <summary>
        /// Sign of the significand, None if not parsed.
        /// </summary>
        public OptionalSign SignificandSign { get; set; } = OptionalSign.None;

        /// <summary>
        /// Index of the integer part, -1 if not parsed.
        /// </summary>
        public int FirstIntegerPartIndex { get; set; } = -1;

        /// <summary>
        /// Index of the first character after the integer part, -1 if not parsed.
        /// </summary>
        public int LastIntegerPartIndex { get; set; } = -1;

        /// <summary>
        /// The integer part in front of the decimal separator (if any).
        /// </summary>
        public string IntegerPart { get { return FirstIntegerPartIndex < 0 ? string.Empty : Text.Substring(FirstIntegerPartIndex, LastIntegerPartIndex - FirstIntegerPartIndex); } }

        /// <summary>
        /// Delegate type of a method that validates a digit.
        /// </summary>
        /// <param name="digit">The digit to validate.</param>
        /// <param name="value">The digit value, if valid.</param>
        /// <returns>True if valid; Otherwise, false.</returns>
        public delegate bool IsValidDigitHandler(char digit, out int value);

        /// <summary>
        /// Delegate type of a method that validates a digit.
        /// </summary>
        /// <param name="value">The value to turn convert to a digit.</param>
        /// <returns>The digit corresponding to <paramref name="value"/>.</returns>
        public delegate char ToDigitHandler(int value);

        /// <summary>
        /// Returns the input number divided by two.
        /// </summary>
        /// <param name="text">The number to divide.</param>
        /// <param name="radix">The radix to use.</param>
        /// <param name="validityHandler">The handler to use to validate digits.</param>
        /// <param name="digitHandler">The handler to use to convert to digits.</param>
        /// <param name="hasCarry">True upon return if <paramref name="text"/> is odd.</param>
        internal static string DividedByTwo(string text, int radix, IsValidDigitHandler validityHandler, ToDigitHandler digitHandler, out bool hasCarry)
        {
            string Result = string.Empty;
            int Carry = 0;

            for (int i = 0; i < text.Length; i++)
            {
                bool IsValid = validityHandler(text[i], out int Value);
                Debug.Assert(IsValid);

                Value += Carry;
                char Digit = digitHandler(Value / 2);

                if (Digit != '0' || i > 0 || text.Length == 1)
                    Result += Digit;

                Carry = Value % 2 != 0 ? radix : 0;
            }

            hasCarry = Carry != 0;

            return Result;
        }

        /// <summary>
        /// Returns the input number multiplied by two, with an optional carry to add.
        /// </summary>
        /// <param name="text">The number to multiply.</param>
        /// <param name="radix">The radix to use.</param>
        /// <param name="validityHandler">The handler to use to validate digits.</param>
        /// <param name="digitHandler">The handler to use to convert to digits.</param>
        /// <param name="addCarry">True if a carry should be added.</param>
        internal static string MultipliedByTwo(string text, int radix, IsValidDigitHandler validityHandler, ToDigitHandler digitHandler, bool addCarry)
        {
            string Result = string.Empty;
            int Carry = addCarry ? 1 : 0;

            for (int i = 0; i < text.Length; i++)
            {
                bool IsValid = validityHandler(text[text.Length - 1 - i], out int Value);
                Debug.Assert(IsValid);

                Value = (Value * 2) + Carry;
                if (Value >= radix)
                {
                    Value -= radix;
                    Carry = 1;
                }
                else
                    Carry = 0;

                Result = digitHandler(Value) + Result;
            }

            if (Carry > 0)
                Result = digitHandler(Carry) + Result;

            return Result;
        }

        /// <summary>
        /// Returns the input number incremented by one.
        /// </summary>
        /// <param name="text">The number to increment.</param>
        /// <param name="radix">The radix to use.</param>
        /// <param name="validityHandler">The handler to use to validate digits.</param>
        /// <param name="digitHandler">The handler to use to convert to digits.</param>
        internal static string Incremented(string text, int radix, IsValidDigitHandler validityHandler, ToDigitHandler digitHandler)
        {
            bool Carry = false;

            for (int i = 0; i < text.Length; i++)
            {
                bool IsValid = validityHandler(text[text.Length - 1 - i], out int Value);
                Debug.Assert(IsValid);
                Debug.Assert(Value >= 0 && Value < radix);

                Value++;
                Carry = Value >= radix;

                if (Carry)
                    text = text.Substring(0, text.Length - 1 - i) + '0' + text.Substring(0, text.Length - i);
                else
                {
                    char Digit = digitHandler(Value);
                    text = text.Substring(0, text.Length - 1 - i) + Digit + text.Substring(0, text.Length - i);
                    break;
                }
            }

            if (Carry)
                text = "1" + text;

            return text;
        }

        /// <summary>
        /// Returns the input number decremented by one.
        /// </summary>
        /// <param name="text">The number to decrement.</param>
        /// <param name="radix">The radix to use.</param>
        /// <param name="validityHandler">The handler to use to validate digits.</param>
        /// <param name="digitHandler">The handler to use to convert to digits.</param>
        internal static string Decremented(string text, int radix, IsValidDigitHandler validityHandler, ToDigitHandler digitHandler)
        {
            Debug.Assert(text.Length > 0);
            Debug.Assert(text[0] != '0');

            bool Carry = false;

            for (int i = 0; i < text.Length; i++)
            {
                bool IsValid = validityHandler(text[text.Length - 1 - i], out int Value);
                Debug.Assert(IsValid);
                Debug.Assert(Value >= 0 && Value < radix);

                Value--;
                Carry = Value < 0;

                if (Carry)
                    text = text.Substring(0, text.Length - 1 - i) + '9' + text.Substring(0, text.Length - i);
                else
                {
                    char Digit = digitHandler(Value);
                    text = text.Substring(0, text.Length - 1 - i) + Digit + text.Substring(0, text.Length - i);
                    break;
                }
            }

            Debug.Assert(!Carry);

            if (text.Length > 1 && text[0] == '0')
                text = text.Substring(1);

            Debug.Assert(text.Length > 0);
            Debug.Assert(text[0] != '0' || text.Length == 1);

            return text;
        }
    }
}
