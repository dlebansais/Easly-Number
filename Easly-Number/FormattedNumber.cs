﻿namespace EaslyNumber
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
            DiscardedProlog = string.Empty;
            Value = Number.NaN;
            BeforeExponent = string.Empty;
            Exponent = string.Empty;
            InvalidPart = string.Empty;

            if (Number.Parse(text, out TextPartition Partition, out Number SpecialNumber))
            {
                if (Partition != null)
                    CreateFromPartition(Partition);
                else
                    Value = SpecialNumber;
            }
            else
                InvalidPart = text;
        }

        private void CreateFromPartition(TextPartition partition)
        {
            DiscardedProlog = partition.DiscardedProlog;
            InvalidPart = partition.InvalidPart;

            long SignificandPrecision = Arithmetic.SignificandPrecision;
            long ExponentPrecision = Arithmetic.ExponentPrecision;
            partition.ConvertToBitField(SignificandPrecision, ExponentPrecision, out BitField IntegerField, out BitField FractionalField, out BitField ExponentField);

            Value = new Number(SignificandPrecision, ExponentPrecision, IntegerField, FractionalField, ExponentField);
            Debug.Assert(!Value.IsNaN);

            if (partition.HasFractionalPart)
            {
                Debug.Assert(partition.Radix == Number.DecimalRadix);

                GetFormattedTextForReal(partition, out string BeforeExponentText, out string ExponentText);

                BeforeExponent = BeforeExponentText;
                Exponent = ExponentText;
            }
            else
            {
                GetFormattedTextForInteger(partition, out string BeforeExponentText, out string ExponentText);

                BeforeExponent = BeforeExponentText;
                Exponent = ExponentText;
            }
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

        private static void GetFormattedTextForInteger(TextPartition partition, out string beforeExponentText, out string exponentText)
        {
            string RadixText = Number.RadixPrefixText(partition.Radix);

            beforeExponentText = $"{RadixText}{partition.IntegerPart}";
            exponentText = string.Empty;
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
