namespace EaslyNumber
{
    using System.Diagnostics;

    /// <summary>
    /// Describes a number with discarded and invalid parts.
    /// </summary>
    public struct FormattedNumber
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedNumber"/> struct.
        /// </summary>
        /// <param name="text">The number in plain text.</param>
        public FormattedNumber(string text)
        {
            Number.Parse(text, out string DiscardedProlog, out int IntegerBase, out OptionalSign SignificandSign, out string IntegerPart, out OptionalSeparator Separator, out string FractionalPart, out OptionalExponent ExponentCharacter, out OptionalSign ExponentSign, out string ExponentPart, out string InvalidPart);

            this.DiscardedProlog = DiscardedProlog;

            string BeforeExponentText;
            string ExponentText;

            if (IntegerBase == Number.DecimalIntegerBase)
            {
                Value = new Number(SignificandSign, IntegerPart, Separator, FractionalPart, ExponentCharacter, ExponentSign, ExponentPart);
                GetFormattedTextForReal(SignificandSign, IntegerPart, Separator, FractionalPart, ExponentCharacter, ExponentSign, ExponentPart, out BeforeExponentText, out ExponentText);

                if (Value.IsInteger)
                {
                    GetFormattedTextForInteger(IntegerBase, IntegerPart, out string BeforeExponentTextInteger, out string ExponentTextInteger);

                    Debug.Assert(Separator == OptionalSeparator.None && ExponentCharacter == OptionalExponent.None);
                    Debug.Assert(BeforeExponentTextInteger == BeforeExponentText);
                    Debug.Assert(ExponentTextInteger == ExponentText);
                }
            }
            else
            {
                Value = new Number(IntegerBase, IntegerPart);
                GetFormattedTextForInteger(IntegerBase, IntegerPart, out BeforeExponentText, out ExponentText);
            }

            BeforeExponent = BeforeExponentText;
            Exponent = ExponentText;

            this.InvalidPart = InvalidPart;
        }

        private static void GetFormattedTextForInteger(int integerBase, string integerPart, out string beforeExponentText, out string exponentText)
        {
            string IntegerBaseText = Number.BasePrefixText(integerBase);

            beforeExponentText = $"{IntegerBaseText}{integerPart}";
            exponentText = string.Empty;
        }

        private static void GetFormattedTextForReal(OptionalSign significandSign, string integerPart, OptionalSeparator separator, string fractionalPart, OptionalExponent exponentCharacter, OptionalSign exponentSign, string exponentPart, out string beforeExponentText, out string exponentText)
        {
            string SignificandSignText = Number.SignText(significandSign);
            string SeparatorText = Number.SeparatorText(separator);
            string ExponentCharacterText = Number.ExponentCharacterText(exponentCharacter);
            beforeExponentText = $"{SignificandSignText}{integerPart}{SeparatorText}{fractionalPart}{ExponentCharacterText}";

            string ExponentSignText = Number.SignText(exponentSign);
            exponentText = $"{ExponentSignText}{exponentPart}";
        }
        #endregion

        #region Properties
        /// <summary>
        /// The discarded prolog before the number.
        /// </summary>
        public string DiscardedProlog { get; private set; }

        /// <summary>
        /// The value.
        /// </summary>
        public Number Value { get; private set; }

        /// <summary>
        /// The text in <see cref="Value"/> before the exponent sign (the exponent character is included).
        /// </summary>
        public string BeforeExponent { get; private set; }

        /// <summary>
        /// The exponent text in <see cref="Value"/> (the exponent character is excluded).
        /// </summary>
        public string Exponent { get; private set; }

        /// <summary>
        /// The invalid part after the number.
        /// </summary>
        public string InvalidPart { get; private set; }
        #endregion

        #region Text representation
        /// <summary>
        /// Returns the default text representation of the formatted number.
        /// </summary>
        /// <returns>The default text representation of the formatted number.</returns>
        public override string ToString()
        {
            return $"{DiscardedProlog}{BeforeExponent}{Exponent}{InvalidPart}";
        }
        #endregion
    }
}
