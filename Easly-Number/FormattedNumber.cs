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
            Number.Parse(text, out TextPartition Partition, out Number SpecialNumber);

            DiscardedProlog = Partition != null ? Partition.DiscardedProlog : string.Empty;

            string BeforeExponentText;
            string ExponentText;

            if (Partition != null)
            {
                int Radix = Partition.Radix;

                if (Radix == Number.DecimalRadix)
                {
                    Value = new Number(Partition);
                    Debug.Assert(!Value.IsNaN);

                    GetFormattedTextForReal(Partition, out BeforeExponentText, out ExponentText);

                    if (Value.IsInteger)
                    {
                        GetFormattedTextForInteger(Partition, out string BeforeExponentTextInteger, out string ExponentTextInteger);

                        Debug.Assert(Partition.Separator == OptionalSeparator.None && Partition.ExponentCharacter == OptionalExponent.None);
                        Debug.Assert(BeforeExponentTextInteger == BeforeExponentText);
                        Debug.Assert(ExponentTextInteger == ExponentText);
                    }
                }
                else
                {
                    Debug.Assert(Radix == Number.BinaryRadix || Radix == Number.OctalRadix || Radix == Number.HexadecimalRadix);

                    Value = new Number(Partition.IntegerField);
                    Debug.Assert(!Value.IsNaN);

                    GetFormattedTextForInteger(Partition, out BeforeExponentText, out ExponentText);
                }
            }
            else
            {
                Value = SpecialNumber;
                BeforeExponentText = string.Empty;
                ExponentText = string.Empty;
            }

            BeforeExponent = BeforeExponentText;
            Exponent = ExponentText;

            InvalidPart = Partition != null ? Partition.InvalidPart : text;
        }

        private static void GetFormattedTextForInteger(TextPartition partition, out string beforeExponentText, out string exponentText)
        {
            string RadixText = Number.RadixPrefixText(partition.Radix);

            beforeExponentText = $"{RadixText}{partition.IntegerPart}";
            exponentText = string.Empty;
        }

        private static void GetFormattedTextForReal(TextPartition partition, out string beforeExponentText, out string exponentText)
        {
            string SignificandSignText = Number.SignText(partition.SignificandSign);
            string SeparatorText = Number.SeparatorText(partition.Separator);
            string ExponentCharacterText = Number.ExponentCharacterText(partition.ExponentCharacter);
            beforeExponentText = $"{SignificandSignText}{partition.IntegerPart}{SeparatorText}{partition.FractionalPart}{ExponentCharacterText}";

            string ExponentSignText = Number.SignText(partition.ExponentSign);
            exponentText = $"{ExponentSignText}{partition.ExponentPart}";
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

        /// <summary>
        /// True if the number is invalid.
        /// </summary>
        public bool IsValid { get { return InvalidPart.Length == 0; } }
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
