namespace EaslyNumber
{
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
            Number.Parse(text, out string DiscardedProlog, out OptionalSign SignificandSign, out string IntegerPart, out OptionalSeparator Separator, out string FractionalPart, out OptionalExponent ExponentCharacter, out OptionalSign ExponentSign, out string ExponentPart, out string InvalidPart);

            this.DiscardedProlog = DiscardedProlog;
            Value = new Number(SignificandSign, IntegerPart, Separator, FractionalPart, ExponentCharacter, ExponentSign, ExponentPart);
            this.InvalidPart = InvalidPart;

            string SignificandSignText = Number.SignText(SignificandSign);
            string SeparatorText = Number.SeparatorText(Separator);
            string ExponentCharacterText = Number.ExponentCharacterText(ExponentCharacter);
            BeforeExponent = $"{SignificandSignText}{IntegerPart}{SeparatorText}{FractionalPart}{ExponentCharacterText}";

            string ExponentSignText = Number.SignText(ExponentSign);
            Exponent = $"{ExponentSignText}{ExponentPart}";
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
