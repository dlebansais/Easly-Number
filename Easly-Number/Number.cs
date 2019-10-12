namespace EaslyNumber
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Describes and manipulates real numbers with arbitrary precision.
    /// </summary>
    public partial struct Number
    {
        #region Special Values
        /// <summary>
        /// The special value for not-a-number.
        /// </summary>
        public static readonly Number NaN = new Number(true, false, false);

        /// <summary>
        /// The special value for positive infinity.
        /// </summary>
        public static readonly Number PositiveInfinity = new Number(false, true, false);

        /// <summary>
        /// The special value for negative infinity.
        /// </summary>
        public static readonly Number NegativeInfinity = new Number(false, false, true);

        /// <summary>
        /// The special value zero.
        /// </summary>
        public static readonly Number Zero = new Number(false, false, false);
        #endregion

        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from plain text.
        /// </summary>
        /// <param name="text">The number in plain text.</param>
        /// <exception cref="ArgumentException">The text is not a valid number.</exception>
        public Number(string text)
        {
            Parse(text, out TextPartition Partition, out Number SpecialNumber);

            if (Partition == null)
                throw new ArgumentException();

            if (Partition.DiscardedProlog.Length > 0)
                throw new ArgumentException();

            if (Partition.InvalidPart.Length > 0)
                throw new ArgumentException();

            int Radix = Partition.Radix;

            if (Radix == DecimalRadix || Radix == BinaryRadix || Radix == OctalRadix || Radix == HexadecimalRadix)
            {
                IsNaN = false;
                IsPositiveInfinity = false;
                IsNegativeInfinity = false;
                IsZero = false;
                SignificandPrecision = Arithmetic.SignificandPrecision;
                ExponentPrecision = Arithmetic.ExponentPrecision;
                Rounding = Arithmetic.Rounding;
                IsSignificandNegative = false;
                IsExponentNegative = false;

                IntegerField = Partition.IntegerField;
                FractionalField = Partition.FractionalField;
                ExponentField = Partition.ExponentField;
            }
            else
            {
                IsNaN = SpecialNumber.IsNaN;
                IsPositiveInfinity = SpecialNumber.IsPositiveInfinity;
                IsNegativeInfinity = SpecialNumber.IsNegativeInfinity;
                IsZero = SpecialNumber.IsZero;
                SignificandPrecision = Arithmetic.SignificandPrecision;
                ExponentPrecision = Arithmetic.ExponentPrecision;
                Rounding = Arithmetic.Rounding;
                IsSignificandNegative = false;
                IsExponentNegative = false;

                IntegerField = null;
                FractionalField = null;
                ExponentField = null;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from parsed parts.
        /// </summary>
        /// <param name="partition">The parsed parts.</param>
        internal Number(TextPartition partition)
        {
            Debug.Assert(partition != null);
            Debug.Assert(partition.IntegerPart.Length > 0 || partition.FractionalPart.Length > 0);
            Debug.Assert(partition.ExponentSign == OptionalSign.None || partition.ExponentCharacter != OptionalExponent.None);
            Debug.Assert(partition.ExponentPart.Length == 0 || partition.ExponentCharacter != OptionalExponent.None);

            IsNaN = false;
            IsPositiveInfinity = false;
            IsNegativeInfinity = false;
            IsZero = false;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;

            IntegerField = partition.IntegerField;
            FractionalField = partition.FractionalField;
            ExponentField = partition.ExponentField;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a parsed integer.
        /// </summary>
        /// <param name="integerField">The integer data field.</param>
        internal Number(BitField integerField)
        {
            IsNaN = false;
            IsPositiveInfinity = false;
            IsNegativeInfinity = false;
            IsZero = false;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;

            IntegerField = integerField;
            FractionalField = null;
            ExponentField = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# float.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(float value)
        {
            IsNaN = false;
            IsPositiveInfinity = false;
            IsNegativeInfinity = false;
            IsZero = false;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;

            IntegerField = null;
            FractionalField = null;
            ExponentField = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# double.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(double value)
        {
            IsNaN = false;
            IsPositiveInfinity = false;
            IsNegativeInfinity = false;
            IsZero = false;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;

            IntegerField = null;
            FractionalField = null;
            ExponentField = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# decimal.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(decimal value)
        {
            IsNaN = false;
            IsPositiveInfinity = false;
            IsNegativeInfinity = false;
            IsZero = false;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;

            IntegerField = null;
            FractionalField = null;
            ExponentField = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# int.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(int value)
        {
            IsNaN = false;
            IsPositiveInfinity = false;
            IsNegativeInfinity = false;
            IsZero = false;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;

            IntegerField = null;
            FractionalField = null;
            ExponentField = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# unsigned int.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(uint value)
        {
            IsNaN = false;
            IsPositiveInfinity = false;
            IsNegativeInfinity = false;
            IsZero = false;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;

            IntegerField = null;
            FractionalField = null;
            ExponentField = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# long.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(long value)
        {
            IsNaN = false;
            IsPositiveInfinity = false;
            IsNegativeInfinity = false;
            IsZero = false;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;

            IntegerField = null;
            FractionalField = null;
            ExponentField = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# unsigned long.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(ulong value)
        {
            IsNaN = false;
            IsPositiveInfinity = false;
            IsNegativeInfinity = false;
            IsZero = false;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;

            IntegerField = null;
            FractionalField = null;
            ExponentField = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// </summary>
        /// <param name="isNaN">Value of the special NaN flag.</param>
        /// <param name="isPositiveInfinity">Value of the special positive infinity flag.</param>
        /// <param name="isNegativeInfinity">Value of the special negative infinity flag.</param>
        private Number(bool isNaN, bool isPositiveInfinity, bool isNegativeInfinity)
        {
            Debug.Assert((!isPositiveInfinity && !isNegativeInfinity) || (!isNaN && !isNegativeInfinity) || (!isNaN && !isPositiveInfinity));

            IsNaN = isNaN;
            IsPositiveInfinity = isPositiveInfinity;
            IsNegativeInfinity = isNegativeInfinity;
            IsZero = !isNaN && !isPositiveInfinity && !isNegativeInfinity;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;

            IntegerField = null;
            FractionalField = null;
            ExponentField = null;
        }
        #endregion

        #region Properties
        /// <summary>
        /// True if the number is one of the special numbers.
        /// </summary>
        public bool IsSpecial { get { return IsNaN || IsInfinite; } }

        /// <summary>
        /// True if the number is a NaN.
        /// </summary>
        public bool IsNaN { get; private set; }

        /// <summary>
        /// True if the number is one of the infinite values.
        /// </summary>
        public bool IsInfinite { get { return IsPositiveInfinity || IsNegativeInfinity; } }

        /// <summary>
        /// True if the number is the positive infinite value.
        /// </summary>
        public bool IsPositiveInfinity { get; private set; }

        /// <summary>
        /// True if the number is the negative infinite value.
        /// </summary>
        public bool IsNegativeInfinity { get; private set; }

        /// <summary>
        /// True if the number is 0.
        /// </summary>
        public bool IsZero { get; private set; }

        /// <summary>
        /// The number of bits in the significand.
        /// </summary>
        public long SignificandPrecision { get; private set; }

        /// <summary>
        /// The number of bits in the exponent.
        /// </summary>
        public long ExponentPrecision { get; private set; }

        /// <summary>
        /// The rounding mode that was used when the number was created.
        /// </summary>
        public Rounding Rounding { get; private set; }

        /// <summary>
        /// True if the number is negative.
        /// </summary>
        public bool IsSignificandNegative { get; private set; }

        /// <summary>
        /// True if the absolute value of the number is between zero and 1.
        /// </summary>
        public bool IsExponentNegative { get; private set; }

        /// <summary>
        /// True if the number is an integer.
        /// </summary>
        public bool IsInteger { get { return false; } }

        /// <summary>
        /// The binary data corresponding to the integer part.
        /// </summary>
        internal BitField IntegerField { get; }

        /// <summary>
        /// The binary data corresponding to the fractional part.
        /// </summary>
        internal BitField FractionalField { get; }

        /// <summary>
        /// The binary data corresponding to the exponent part.
        /// </summary>
        internal BitField ExponentField { get; }
        #endregion

        #region Basic Operations
        /// <summary>
        /// Returns the sum of two numbers: x + y.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <returns>The arithmetic sum of <paramref name="x"/> and <paramref name="y"/>.</returns>
        public static Number operator +(Number x, Number y)
        {
            return Add(x, y, Arithmetic.SignificandPrecision, Arithmetic.ExponentPrecision, Arithmetic.Rounding);
        }

        /// <summary>
        /// Returns the sum of two numbers: x + y.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <param name="significandPrecision">The precision to use for the significand when creating the result.</param>
        /// <param name="exponentPrecision">The precision to use for the exponent when creating the result.</param>
        /// <param name="rounding">The rounding mode to use when creating the result.</param>
        /// <returns>The arithmetic sum of <paramref name="x"/> and <paramref name="y"/>.</returns>
        public static Number Add(Number x, Number y, long significandPrecision, long exponentPrecision, Rounding rounding)
        {
            return NaN;
        }
        #endregion

        #region Text representation
        /// <summary>
        /// Returns the default text representation of the number.
        /// </summary>
        /// <returns>The default text representation of the number.</returns>
        public override string ToString()
        {
            if (IsNaN)
                return double.NaN.ToString();
            else if (IsPositiveInfinity)
                return double.PositiveInfinity.ToString();
            else if (IsNegativeInfinity)
                return double.NegativeInfinity.ToString();
            else
            {
                return string.Empty;
            }
        }
        #endregion
    }
}
