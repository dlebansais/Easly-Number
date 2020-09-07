namespace EaslyNumber
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Describes a number with discarded and invalid parts.
    /// </summary>
    public struct FormattedNumber : IEquatable<FormattedNumber>
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="FormattedNumber"/> struct.
        /// </summary>
        /// <param name="text">The number in plain text.</param>
        public FormattedNumber(string text)
        {
            Value = Number.Uninitialized;
            DiscardedProlog = string.Empty;
            Prefix = string.Empty;
            BeforeExponent = string.Empty;
            Exponent = string.Empty;
            Suffix = string.Empty;
            InvalidPart = string.Empty;
            DiscardedEpilog = string.Empty;

            if (Number.Parse(text, out TextPartition Partition))
            {
                Debug.Assert(Partition != null);
                CreateFromPartition(Partition);
            }
            else
            {
                Value = Number.NaN;
                InvalidPart = text;
            }
        }

        private void CreateFromPartition(TextPartition partition)
        {
            DiscardedProlog = partition.DiscardedProlog;
            InvalidPart = partition.InvalidPart;
            DiscardedEpilog = partition.DiscardedEpilog;

            bool IsHandled = false;
            switch (partition)
            {
                case SpecialNumberTextPartition AsSpecialNumberTextPartition:
                    CreateFromSpecialNumberTextPartition(AsSpecialNumberTextPartition);
                    IsHandled = true;
                    break;
                case RadixPrefixTextPartition AsRadixPrefixTextPartition:
                    CreateFromRadixPrefixTextPartition(AsRadixPrefixTextPartition);
                    IsHandled = true;
                    break;
                case RadixSuffixTextPartition AsRadixSuffixTextPartition:
                    CreateFromRadixSuffixTextPartition(AsRadixSuffixTextPartition);
                    IsHandled = true;
                    break;
                case RealTextPartition AsRealTextPartition:
                    CreateFromRealTextPartition(AsRealTextPartition);
                    IsHandled = true;
                    break;
            }

            Debug.Assert(IsHandled);
        }

        private void CreateFromSpecialNumberTextPartition(SpecialNumberTextPartition partition)
        {
            Value = partition.Value;
            Prefix = string.Empty;
            BeforeExponent = partition.SpecialPart;
            Exponent = string.Empty;
            Suffix = string.Empty;
        }

        private void CreateFromRadixPrefixTextPartition(RadixPrefixTextPartition partition)
        {
            GetIntegerNumberValue(partition);

            Prefix = Number.RadixPrefixText(partition.Radix);

            Debug.Assert(partition.SignificandSign == OptionalSign.None);
            BeforeExponent = partition.IntegerPart;

            Exponent = string.Empty;
            Suffix = string.Empty;
        }

        private void CreateFromRadixSuffixTextPartition(RadixSuffixTextPartition partition)
        {
            GetIntegerNumberValue(partition);

            Prefix = string.Empty;

            switch (partition.SignificandSign)
            {
                default:
                case OptionalSign.None:
                    BeforeExponent = partition.IntegerPart;
                    break;
                case OptionalSign.Positive:
                    BeforeExponent = "+" + partition.IntegerPart;
                    break;
                case OptionalSign.Negative:
                    BeforeExponent = "-" + partition.IntegerPart;
                    break;
            }

            Exponent = string.Empty;
            Suffix = Number.RadixSuffixText(partition.Radix);
        }

        private void CreateFromRealTextPartition(RealTextPartition partition)
        {
            if (partition.IsZero)
                Value = Number.Zero;
            else
            {
                long SignificandPrecision = Arithmetic.SignificandPrecision;
                long ExponentPrecision = Arithmetic.ExponentPrecision;
                partition.ConvertToBitField(SignificandPrecision, ExponentPrecision, out BitField IntegerField, out BitField FractionalField, out BitField ExponentField);

                Value = new Number(SignificandPrecision, ExponentPrecision, partition.SignificandSign == OptionalSign.Negative, IntegerField, FractionalField, partition.ExponentSign == OptionalSign.Negative, ExponentField);
                Debug.Assert(!Value.IsSpecial);
            }

            string SignificandSignText = Number.SignText(partition.SignificandSign);
            string SeparatorText = Number.SeparatorText(partition.Separator);
            string ExponentCharacterText = Number.ExponentCharacterText(partition.ExponentCharacter);
            BeforeExponent = $"{SignificandSignText}{partition.IntegerPart}{SeparatorText}{partition.FractionalPart}{ExponentCharacterText}";

            string ExponentSignText = Number.SignText(partition.ExponentSign);
            Exponent = $"{ExponentSignText}{partition.ExponentPart}";
        }

        private void GetIntegerNumberValue(CustomRadixIntegerTextPartition partition)
        {
            if (partition.IsZero)
                Value = Number.Zero;
            else
            {
                long SignificandPrecision = Arithmetic.SignificandPrecision;
                long ExponentPrecision = Arithmetic.ExponentPrecision;
                partition.ConvertToBitField(SignificandPrecision, out BitField IntegerField);

                Value = new Number(SignificandPrecision, ExponentPrecision, false, IntegerField);
                Debug.Assert(!Value.IsSpecial);
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// The value.
        /// </summary>
        public Number Value { get; private set; }

        /// <summary>
        /// The discarded prolog before the number.
        /// </summary>
        public string DiscardedProlog { get; private set; }

        /// <summary>
        /// The prefix part before the number.
        /// </summary>
        public string Prefix { get; private set; }

        /// <summary>
        /// The text in <see cref="Value"/> before the exponent sign (the exponent character is included).
        /// </summary>
        public string BeforeExponent { get; private set; }

        /// <summary>
        /// The exponent text in <see cref="Value"/> (the exponent character is excluded).
        /// </summary>
        public string Exponent { get; private set; }

        /// <summary>
        /// The suffix part after the number.
        /// </summary>
        public string Suffix { get; private set; }

        /// <summary>
        /// The invalid part after the number.
        /// </summary>
        public string InvalidPart { get; private set; }

        /// <summary>
        /// The discarded epilog after the number.
        /// </summary>
        public string DiscardedEpilog { get; private set; }

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
            return $"{DiscardedProlog}{BeforeExponent}{Exponent}{Suffix}{InvalidPart}";
        }
        #endregion

        #region Overrides
        public static bool operator ==(FormattedNumber obj1, FormattedNumber obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator !=(FormattedNumber obj1, FormattedNumber obj2)
        {
            return !obj1.Equals(obj2);
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public bool Equals(FormattedNumber obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}
