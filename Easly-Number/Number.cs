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
        /// A psecial value for uninitialized.
        /// </summary>
        internal static readonly Number Uninitialized;

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
            if (!Parse(text, out TextPartition Partition))
                throw new ArgumentException();

            InitFromPartition(Partition);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from plain text.
        /// </summary>
        /// <param name="partition">The partition to copy values from.</param>
        /// <exception cref="ArgumentException">The partition does not represent a valid number.</exception>
        internal Number(TextPartition partition)
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

            InitFromPartition(partition);
        }

        /// <summary>
        /// Initializes the object from a special number value.
        /// </summary>
        /// <param name="partition">The partition to copy values from.</param>
        /// <exception cref="ArgumentException">The partition does not represent a valid number.</exception>
        private void InitFromPartition(TextPartition partition)
        {
            if (partition.DiscardedProlog.Length > 0)
                throw new ArgumentException();

            if (partition.InvalidPart.Length > 0)
                throw new ArgumentException();

            bool IsHandled = false;

            switch (partition)
            {
                case SpecialNumberTextPartition AsSpecialNumberTextPartition:
                    InitFromSpecialNumber(AsSpecialNumberTextPartition.Value);
                    IsHandled = true;
                    break;

                case CustomRadixIntegerTextPartition AsIntegerTextPartition:
                    InitFromIntegerNumber(AsIntegerTextPartition);
                    IsHandled = true;
                    break;

                case RealTextPartition AsRealTextPartition:
                    InitFromRealNumber(AsRealTextPartition);
                    IsHandled = true;
                    break;
            }

            Debug.Assert(IsHandled);
        }

        /// <summary>
        /// Initializes the object from a special number value.
        /// </summary>
        /// <param name="value">The special value to copy from.</param>
        /// <exception cref="ArgumentException">The partition does not represent a valid number.</exception>
        private void InitFromSpecialNumber(Number value)
        {
            IsNaN = value.IsNaN;
            IsPositiveInfinity = value.IsPositiveInfinity;
            IsNegativeInfinity = value.IsNegativeInfinity;
            IsZero = value.IsZero;
            SignificandPrecision = value.SignificandPrecision;
            ExponentPrecision = value.ExponentPrecision;
            Rounding = value.Rounding;
            IsSignificandNegative = value.IsSignificandNegative;
            IsExponentNegative = value.IsExponentNegative;
            IntegerField = value.IntegerField;
            FractionalField = value.FractionalField;
            ExponentField = value.ExponentField;
        }

        /// <summary>
        /// Initializes the object from an integer number value.
        /// </summary>
        /// <param name="partition">The partition to copy values from.</param>
        /// <exception cref="ArgumentException">The source is not a valid number.</exception>
        private void InitFromIntegerNumber(CustomRadixIntegerTextPartition partition)
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

            partition.ConvertToBitField(SignificandPrecision, out BitField InitIntegerField);

            IntegerField = InitIntegerField;
            FractionalField = new BitField();
            ExponentField = new BitField();
            ExponentField.SetZero();
        }

        /// <summary>
        /// Initializes the object from an integer number value.
        /// </summary>
        /// <param name="partition">The partition to copy values from.</param>
        /// <exception cref="ArgumentException">The partition does not represent a valid number.</exception>
        private void InitFromRealNumber(RealTextPartition partition)
        {
            IsNaN = false;
            IsPositiveInfinity = false;
            IsNegativeInfinity = false;
            IsZero = partition.IsZero;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = partition.SignificandSign == OptionalSign.Negative;
            IsExponentNegative = partition.ExponentSign == OptionalSign.Negative;

            partition.ConvertToBitField(SignificandPrecision, ExponentPrecision, out BitField InitIntegerField, out BitField InitFractionalField, out BitField InitExponentField);

            IntegerField = InitIntegerField;
            FractionalField = InitFractionalField;
            ExponentField = InitExponentField;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a parsed integer.
        /// </summary>
        /// <param name="significandPrecision">The precision used to obtain the integer and fractional data fields.</param>
        /// <param name="exponentPrecision">The precision used to obtain the exponent data fields.</param>
        /// <param name="isSignificandNegative">True if the number is negative.</param>
        /// <param name="integerField">The integer data field.</param>
        /// <param name="fractionalField">The fractional data field.</param>
        /// <param name="isExponentNegative">True if the number exponent is negative.</param>
        /// <param name="exponentField">The exponent data field.</param>
        internal Number(long significandPrecision, long exponentPrecision, bool isSignificandNegative, BitField integerField, BitField fractionalField, bool isExponentNegative, BitField exponentField)
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
        /// This contructor creates the number from a parsed integer.
        /// </summary>
        /// <param name="significandPrecision">The precision used to obtain the integer and fractional data fields.</param>
        /// <param name="exponentPrecision">The precision used to obtain the exponent data fields.</param>
        /// <param name="isSignificandNegative">True if the number is negative.</param>
        /// <param name="integerField">The integer data field.</param>
        internal Number(long significandPrecision, long exponentPrecision, bool isSignificandNegative, BitField integerField)
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
            FractionalField = new BitField();
            ExponentField = new BitField();
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
            bool NotInfinity = !isPositiveInfinity && !isNegativeInfinity;
            bool PositiveInfinityOnly = !isNaN && !isNegativeInfinity;
            bool NegativeInfinityOnly = !isNaN && !isPositiveInfinity;

            Debug.Assert(NotInfinity || PositiveInfinityOnly || NegativeInfinityOnly);

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
        public bool IsInteger { get { return IsZero || FractionalField == null || FractionalField.SignificantBits == 0 || FractionalField.IsZero; } }

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

        #region Comparison
        /// <summary>
        /// Gets a hash code for the current object.
        /// </summary>
        public override int GetHashCode()
        {
            int Result = 0;

            Result ^= IsNaN.GetHashCode();
            Result ^= IsPositiveInfinity.GetHashCode();
            Result ^= IsNegativeInfinity.GetHashCode();
            Result ^= IsZero.GetHashCode();
            Result ^= SignificandPrecision.GetHashCode();
            Result ^= ExponentPrecision.GetHashCode();
            Result ^= Rounding.GetHashCode();
            Result ^= IsSignificandNegative.GetHashCode();
            Result ^= IsExponentNegative.GetHashCode();

            Result ^= BitField.GetHashCode(IntegerField);
            Result ^= BitField.GetHashCode(FractionalField);
            Result ^= BitField.GetHashCode(ExponentField);

            return Result;
        }

        /// <summary>
        /// Checks if two numbers are equal.
        /// </summary>
        /// <param name="other">The other instance.</param>
        public override bool Equals(object other)
        {
            if (other is Number AsNumber)
            {
                return IsNaN == AsNumber.IsNaN &&
                       IsPositiveInfinity == AsNumber.IsPositiveInfinity &&
                       IsNegativeInfinity == AsNumber.IsNegativeInfinity &&
                       IsZero == AsNumber.IsZero &&
                       SignificandPrecision == AsNumber.SignificandPrecision &&
                       ExponentPrecision == AsNumber.ExponentPrecision &&
                       Rounding == AsNumber.Rounding &&
                       IsSignificandNegative == AsNumber.IsSignificandNegative &&
                       IsExponentNegative == AsNumber.IsExponentNegative &&
                       Equals(IntegerField, AsNumber.IntegerField) &&
                       Equals(FractionalField, AsNumber.FractionalField) &&
                       Equals(ExponentField, AsNumber.ExponentField);
            }
            else
                return false;
        }

        /// <summary>
        /// Checks if <paramref name="x"/> is lesser than <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        public static bool operator ==(Number x, Number y)
        {
            if (x.IsNaN || y.IsNaN)
                return false;

            if ((x.IsPositiveInfinity && y.IsPositiveInfinity) || (x.IsNegativeInfinity && y.IsNegativeInfinity) || (x.IsZero && y.IsZero))
                return true;

            if (x.IsSpecial || y.IsSpecial)
                return false;

            if (x.IsSignificandNegative != y.IsSignificandNegative || x.IsExponentNegative != y.IsExponentNegative)
                return false;

            Debug.Assert(x.IntegerField != null);
            Debug.Assert(y.IntegerField != null);
            Debug.Assert(x.FractionalField != null);
            Debug.Assert(y.FractionalField != null);
            Debug.Assert(x.ExponentField != null);
            Debug.Assert(y.ExponentField != null);

            bool IsSameInteger = x.IntegerField == y.IntegerField;
            bool IsSameFractional = x.FractionalField == y.FractionalField;
            bool IsSameExponent = x.ExponentField == y.ExponentField;

            return IsSameInteger && IsSameFractional && IsSameExponent;
        }

        /// <summary>
        /// Checks if <paramref name="x"/> is lesser than <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        public static bool operator !=(Number x, Number y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Checks if <paramref name="x"/> is lesser than <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        public static bool operator <(Number x, Number y)
        {
            bool Result;

            if (ComparedAsSpecialNumber(x, y, out Result))
                return Result;

            Debug.Assert(!x.IsSpecial && !y.IsSpecial);

            if (x.IsSignificandNegative && !y.IsSignificandNegative)
                return true;

            if (y.IsSignificandNegative && !x.IsSignificandNegative)
                return false;

            return ComparedSameSign(x, y);
        }

        /// <summary>
        /// Checks if <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        public static bool operator >(Number x, Number y)
        {
            return y < x;
        }

        /// <summary>
        /// Checks if <paramref name="x"/> is lesser than <paramref name="y"/> when x or y is a special number.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <param name="result">The comparison result.</param>
        /// <returns>True if <paramref name="result"/> contains the result upon return; False if <paramref name="x"/> and <paramref name="y"/> are not special numbers.</returns>
        private static bool ComparedAsSpecialNumber(Number x, Number y, out bool result)
        {
            if (x.IsNaN || y.IsNaN)
            {
                result = false;
                return true;
            }

            if (x.IsNegativeInfinity || y.IsPositiveInfinity)
            {
                result = true;
                return true;
            }

            if (x.IsPositiveInfinity || y.IsNegativeInfinity)
            {
                result = false;
                return true;
            }

            if (x.IsZero)
            {
                result = !y.IsZero && !y.IsSignificandNegative;
                return true;
            }

            if (y.IsZero)
            {
                result = !x.IsZero && x.IsSignificandNegative;
                return true;
            }

            result = false;
            return false;
        }

        /// <summary>
        /// Checks if <paramref name="x"/> is lesser than <paramref name="y"/> when x and y have the same sign.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <returns>The comparison result.</returns>
        private static bool ComparedSameSign(Number x, Number y)
        {
            Debug.Assert(!x.IsSpecial && !y.IsSpecial);
            Debug.Assert(x.IsSignificandNegative == y.IsSignificandNegative);

            bool IsSameInteger = x.IntegerField == y.IntegerField;
            bool IsSameFractional = x.FractionalField == y.FractionalField;
            bool IsSameExponent = x.ExponentField == y.ExponentField;

            if (!IsSameInteger)
                return ComparedDifferentIntegerPart(x, y);

            if (!IsSameFractional)
                return ComparedDifferentFractionalPart(x, y);

            if (!IsSameExponent)
                return ComparedDifferentExponentPart(x, y);

            Debug.Assert(IsSameInteger);
            Debug.Assert(IsSameFractional);
            Debug.Assert(IsSameExponent);

            return false;
        }

        /// <summary>
        /// Checks if <paramref name="x"/> is lesser than <paramref name="y"/> when x and y have a different integer part.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <returns>The comparison result.</returns>
        private static bool ComparedDifferentIntegerPart(Number x, Number y)
        {
            Debug.Assert(x.IntegerField != y.IntegerField);

            //TODO: handle exponent
            bool IsSignificandLesser = x.IntegerField < y.IntegerField;
            return IsSignificandLesser ^ x.IsSignificandNegative;
        }

        /// <summary>
        /// Checks if <paramref name="x"/> is lesser than <paramref name="y"/> when x and y have the same integer part and a different fractional part.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <returns>The comparison result.</returns>
        private static bool ComparedDifferentFractionalPart(Number x, Number y)
        {
            Debug.Assert(x.IntegerField == y.IntegerField);
            Debug.Assert(x.FractionalField != y.FractionalField);

            //TODO: handle exponent
            bool IsSignificandLesser = x.FractionalField < y.FractionalField;
            return IsSignificandLesser ^ x.IsSignificandNegative;
        }

        /// <summary>
        /// Checks if <paramref name="x"/> is lesser than <paramref name="y"/> when x and y have the same integer and fractional parts, but a different exponent part.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <returns>The comparison result.</returns>
        private static bool ComparedDifferentExponentPart(Number x, Number y)
        {
            Debug.Assert(x.IntegerField == y.IntegerField);
            Debug.Assert(x.FractionalField == y.FractionalField);
            Debug.Assert(x.ExponentField != y.ExponentField);

            bool IsSignificandLesser = x.ExponentField < y.ExponentField;
            return IsSignificandLesser ^ x.IsSignificandNegative;
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
                string IntegerString = ComposeIntegerString(out long BitIndex);
                string FractionalString = ComposeFractionalString(BitIndex);
                string ExponentString = ComposeExponentString();

                string Result = $"{IntegerString}{FractionalString}{ExponentString}";

                return Result;
            }
        }

        private string ComposeIntegerString(out long bitIndex)
        {
            string IntegerString = "0";
            bitIndex = 1;

            if (IntegerField != null)
            {
                while (bitIndex <= IntegerField.SignificantBits + IntegerField.ShiftBits)
                {
                    bool Carry = IntegerField.GetBit(IntegerField.SignificantBits + IntegerField.ShiftBits - bitIndex);
                    IntegerString = NumberTextPartition.MultipliedByTwo(IntegerString, DecimalRadix, IsValidDecimalDigit, ToDecimalDigit, Carry);
                    bitIndex++;
                }
            }

            return IntegerString;
        }

        private string ComposeFractionalString(long bitIndex)
        {
            string FractionalString;

            if (!IsInteger)
            {
                Debug.Assert(FractionalField != null && FractionalField.SignificantBits > 0);

                FractionalString = "500000000000";
                bitIndex = 1;

                while (bitIndex <= FractionalField.SignificantBits + FractionalField.ShiftBits)
                {
                    bool Carry = FractionalField.GetBit(FractionalField.SignificantBits + FractionalField.ShiftBits - bitIndex);
                    int OldLength = FractionalString.Length;

                    if (Carry)
                        FractionalString = "1" + FractionalString;

                    FractionalString = NumberTextPartition.DividedByTwo(FractionalString, DecimalRadix, IsValidDecimalDigit, ToDecimalDigit, out bool HasDivisionbCarry);

                    if (FractionalString.Length < OldLength)
                        FractionalString = "0" + FractionalString;

                    bitIndex++;
                }

                FractionalString = RealTextPartition.RoundedToNearest(FractionalString, DecimalRadix, IsValidDecimalDigit, ToDecimalDigit, false);

                string Separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

                return $"{Separator}{FractionalString}";
            }
            else
                return string.Empty;
        }

        private string ComposeExponentString()
        {
            if (ExponentField != null && ExponentField.SignificantBits > 0 && !ExponentField.IsZero)
            {
                string ExponentString = "0";
                long BitIndex = 1;

                while (BitIndex <= ExponentField.SignificantBits + ExponentField.ShiftBits)
                {
                    bool Carry = ExponentField.GetBit(ExponentField.SignificantBits + ExponentField.ShiftBits - BitIndex);
                    ExponentString = NumberTextPartition.MultipliedByTwo(ExponentString, DecimalRadix, IsValidDecimalDigit, ToDecimalDigit, Carry);
                    BitIndex++;
                }

                return $"e{ExponentString}";
            }
            else
                return string.Empty;
        }
        #endregion
    }
}
