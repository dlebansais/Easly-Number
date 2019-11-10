namespace EaslyNumber
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

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
            IsNaN = false;
            IsPositiveInfinity = false;
            IsNegativeInfinity = false;
            IsZero = false;
            SignificandPrecision = 0;
            ExponentPrecision = 0;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;
            IntegerField = null;
            FractionalField = null;
            ExponentField = null;

            InitFromText(text);
        }

        /// <summary>
        /// Initializes the object from plain text.
        /// </summary>
        /// <param name="text">The number in plain text.</param>
        /// <exception cref="ArgumentException">The text is not a valid number.</exception>
        private void InitFromText(string text)
        {
            if (!Parse(text, out TextPartition Partition, out Number SpecialNumber))
                throw new ArgumentException();

            if (Partition == null)
            {
                IsNaN = SpecialNumber.IsNaN;
                IsPositiveInfinity = SpecialNumber.IsPositiveInfinity;
                IsNegativeInfinity = SpecialNumber.IsNegativeInfinity;
                IsZero = SpecialNumber.IsZero;
                SignificandPrecision = SpecialNumber.SignificandPrecision;
                ExponentPrecision = SpecialNumber.ExponentPrecision;
                Rounding = SpecialNumber.Rounding;
                IsSignificandNegative = SpecialNumber.IsSignificandNegative;
                IsExponentNegative = SpecialNumber.IsExponentNegative;
                IntegerField = SpecialNumber.IntegerField;
                FractionalField = SpecialNumber.FractionalField;
                ExponentField = SpecialNumber.ExponentField;
            }
            else
            {
                if (Partition.DiscardedProlog.Length > 0)
                    throw new ArgumentException();

                if (Partition.InvalidPart.Length > 0)
                    throw new ArgumentException();

                IsNaN = false;
                IsPositiveInfinity = false;
                IsNegativeInfinity = false;
                IsZero = false;
                SignificandPrecision = Arithmetic.SignificandPrecision;
                ExponentPrecision = Arithmetic.ExponentPrecision;
                Rounding = Arithmetic.Rounding;
                IsSignificandNegative = false;
                IsExponentNegative = false;

                Partition.ConvertToBitField(SignificandPrecision, ExponentPrecision, out BitField InitIntegerField, out BitField InitFractionalField, out BitField InitExponentField);

                IntegerField = InitIntegerField;
                FractionalField = InitFractionalField;
                ExponentField = InitExponentField;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a parsed integer.
        /// </summary>
        /// <param name="significandPrecision">The precision used to obtain the integer and fractional data fields.</param>
        /// <param name="exponentPrecision">The precision used to obtain the exponent data fields.</param>
        /// <param name="integerField">The integer data field.</param>
        /// <param name="fractionalField">The fractional data field.</param>
        /// <param name="exponentField">The exponent data field.</param>
        internal Number(long significandPrecision, long exponentPrecision, BitField integerField, BitField fractionalField, BitField exponentField)
        {
            IsNaN = false;
            IsPositiveInfinity = false;
            IsNegativeInfinity = false;
            IsZero = false;
            SignificandPrecision = significandPrecision;
            ExponentPrecision = exponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;

            IntegerField = integerField;
            FractionalField = fractionalField;
            ExponentField = exponentField;
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
            SignificandPrecision = 0;
            ExponentPrecision = 0;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;
            IntegerField = null;
            FractionalField = null;
            ExponentField = null;

            InitFromText(value.ToString());
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
            SignificandPrecision = 0;
            ExponentPrecision = 0;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;
            IntegerField = null;
            FractionalField = null;
            ExponentField = null;

            InitFromText(value.ToString());
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
            SignificandPrecision = 0;
            ExponentPrecision = 0;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;
            IntegerField = null;
            FractionalField = null;
            ExponentField = null;

            InitFromText(value.ToString());
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
            SignificandPrecision = 0;
            ExponentPrecision = 0;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;
            IntegerField = null;
            FractionalField = null;
            ExponentField = null;

            InitFromText(value.ToString());
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
            SignificandPrecision = 0;
            ExponentPrecision = 0;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;
            IntegerField = null;
            FractionalField = null;
            ExponentField = null;

            InitFromText(value.ToString());
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
            SignificandPrecision = 0;
            ExponentPrecision = 0;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;
            IntegerField = null;
            FractionalField = null;
            ExponentField = null;

            InitFromText(value.ToString());
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
            SignificandPrecision = 0;
            ExponentPrecision = 0;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;
            IntegerField = null;
            FractionalField = null;
            ExponentField = null;

            InitFromText(value.ToString());
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
            SignificandPrecision = 0;
            ExponentPrecision = 0;
            Rounding = Rounding.ToNearest;
            IsSignificandNegative = false;
            IsExponentNegative = false;

            if (IsZero)
            {
                IntegerField = null;
                FractionalField = null;
                ExponentField = null;
            }
            else
            {
                IntegerField = new BitField();
                IntegerField.SetZero();
                FractionalField = new BitField();
                ExponentField = new BitField();
            }
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
        public bool IsInteger { get { return IsZero || FractionalField == null || FractionalField.SignificantBits == 0; } }

        /// <summary>
        /// The binary data corresponding to the integer part.
        /// </summary>
        internal BitField IntegerField { get; private set; }

        /// <summary>
        /// The binary data corresponding to the fractional part.
        /// </summary>
        internal BitField FractionalField { get; private set; }

        /// <summary>
        /// The binary data corresponding to the exponent part.
        /// </summary>
        internal BitField ExponentField { get; private set; }
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
                long BitIndex;

                string IntegerString = "0";
                BitIndex = 1;

                while (BitIndex <= IntegerField.SignificantBits)
                {
                    bool Carry = IntegerField.GetBit(IntegerField.SignificantBits - BitIndex);
                    IntegerString = TextPartition.MultipliedByTwo(IntegerString, DecimalRadix, IsValidDecimalDigit, ToDecimalDigit, Carry);
                    BitIndex++;
                }

                string FractionalString;

                if (!IsInteger)
                {
                    Debug.Assert(FractionalField != null && FractionalField.SignificantBits > 0);

                    FractionalString = "500000000000";
                    BitIndex = 1;

                    while (BitIndex <= FractionalField.SignificantBits)
                    {
                        bool Carry = FractionalField.GetBit(FractionalField.SignificantBits - BitIndex);
                        int OldLength = FractionalString.Length;

                        if (Carry)
                            FractionalString = "1" + FractionalString;

                        FractionalString = TextPartition.DividedByTwo(FractionalString, DecimalRadix, IsValidDecimalDigit, ToDecimalDigit, out bool HasDivisionbCarry);

                        if (FractionalString.Length < OldLength)
                            FractionalString = "0" + FractionalString;

                        BitIndex++;
                    }

                    FractionalString = TextPartition.RoundedToNearest(FractionalString, DecimalRadix, IsValidDecimalDigit, ToDecimalDigit, false);

                    string Separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

                    return $"{IntegerString}{Separator}{FractionalString}";
                }
                else
                    return $"{IntegerString}";
            }
        }
        #endregion
    }
}
