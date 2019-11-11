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
            if (partition.DiscardedProlog.Length > 0)
                throw new ArgumentException();

            if (partition.InvalidPart.Length > 0)
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
            if (partition.DiscardedProlog.Length > 0)
                throw new ArgumentException();

            if (partition.InvalidPart.Length > 0)
                throw new ArgumentException();

            IsNaN = false;
            IsPositiveInfinity = false;
            IsNegativeInfinity = false;
            IsZero = false;
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
        /// Checks if two numbers are equal.
        /// </summary>
        /// <param name="other">The other instance.</param>
        public bool IsEqual(Number other)
        {
            if (IsNaN || other.IsNaN)
                return false;

            if ((IsPositiveInfinity && other.IsPositiveInfinity) || (IsNegativeInfinity && other.IsNegativeInfinity) || (IsZero && other.IsZero))
                return true;

            if (IsSpecial || other.IsSpecial)
                return false;

            if (IsSignificandNegative != other.IsSignificandNegative || IsExponentNegative != other.IsExponentNegative)
                return false;

            Debug.Assert(IntegerField != null);
            Debug.Assert(FractionalField != null);
            Debug.Assert(ExponentField != null);

            bool IsSameInteger = IntegerField.IsEqual(other.IntegerField);
            bool IsSameFractional = FractionalField.IsEqual(other.FractionalField);
            bool IsSameExponent = ExponentField.IsEqual(other.ExponentField);

            return IsSameInteger && IsSameFractional && IsSameExponent;
        }

        /// <summary>
        /// Checks if <paramref name="x"/> is lesser than <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        public static bool operator <(Number x, Number y)
        {
            if (x.IsNaN || y.IsNaN)
                return false;

            if (x.IsNegativeInfinity || y.IsPositiveInfinity)
                return true;

            if (x.IsPositiveInfinity || y.IsNegativeInfinity)
                return false;

            if (x.IsZero)
                return !y.IsZero && !y.IsSignificandNegative;

            if (y.IsZero)
                return !x.IsZero && x.IsSignificandNegative;

            Debug.Assert(!x.IsSpecial && !y.IsSpecial);

            if (x.IsSignificandNegative && !y.IsSignificandNegative)
                return true;

            if (y.IsSignificandNegative && !x.IsSignificandNegative)
                return false;

            Debug.Assert(x.IsSignificandNegative == y.IsSignificandNegative);

            bool IsSameInteger = x.IntegerField.IsEqual(y.IntegerField);
            bool IsSameFractional = x.FractionalField.IsEqual(y.FractionalField);
            bool IsSameExponent = x.ExponentField.IsEqual(y.ExponentField);

            if (!IsSameInteger)
            {
                //TODO handle exponent
                bool IsSignificandLesser = x.IntegerField < y.IntegerField;
                return IsSignificandLesser ^ x.IsSignificandNegative;
            }

            if (!IsSameFractional)
            {
                //TODO handle exponent
                bool IsSignificandLesser = x.FractionalField < y.FractionalField;
                return IsSignificandLesser ^ x.IsSignificandNegative;
            }

            if (!IsSameExponent)
            {
                bool IsSignificandLesser = x.ExponentField < y.ExponentField;
                return IsSignificandLesser ^ x.IsSignificandNegative;
            }

            return false;
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
        #endregion

        #region Arithmetic
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

        /// <summary>
        /// Returns the difference between two numbers: x - y.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        public static Number operator -(Number x, Number y)
        {
            return Subtract(x, y, Arithmetic.SignificandPrecision, Arithmetic.ExponentPrecision, Arithmetic.Rounding);
        }

        /// <summary>
        /// Returns the difference between two numbers: x - y.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <param name="significandPrecision">The precision to use for the significand when creating the result.</param>
        /// <param name="exponentPrecision">The precision to use for the exponent when creating the result.</param>
        /// <param name="rounding">The rounding mode to use when creating the result.</param>
        /// <returns>The arithmetic difference of <paramref name="x"/> and <paramref name="y"/>.</returns>
        public static Number Subtract(Number x, Number y, long significandPrecision, long exponentPrecision, Rounding rounding)
        {
            return NaN;
        }

        /// <summary>
        /// Returns the product of two numbers: x * y.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        public static Number operator *(Number x, Number y)
        {
            return Multiply(x, y, Arithmetic.SignificandPrecision, Arithmetic.ExponentPrecision, Arithmetic.Rounding);
        }

        /// <summary>
        /// Returns the product of two numbers: x * y.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <param name="significandPrecision">The precision to use for the significand when creating the result.</param>
        /// <param name="exponentPrecision">The precision to use for the exponent when creating the result.</param>
        /// <param name="rounding">The rounding mode to use when creating the result.</param>
        /// <returns>The arithmetic product of <paramref name="x"/> and <paramref name="y"/>.</returns>
        public static Number Multiply(Number x, Number y, long significandPrecision, long exponentPrecision, Rounding rounding)
        {
            return NaN;
        }

        /// <summary>
        /// Returns the ratio of two numbers: x / y.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        public static Number operator /(Number x, Number y)
        {
            return Divide(x, y, Arithmetic.SignificandPrecision, Arithmetic.ExponentPrecision, Arithmetic.Rounding);
        }

        /// <summary>
        /// Returns the ratio of two numbers: x / y.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <param name="significandPrecision">The precision to use for the significand when creating the result.</param>
        /// <param name="exponentPrecision">The precision to use for the exponent when creating the result.</param>
        /// <param name="rounding">The rounding mode to use when creating the result.</param>
        /// <returns>The arithmetic ratio of <paramref name="x"/> and <paramref name="y"/>.</returns>
        public static Number Divide(Number x, Number y, long significandPrecision, long exponentPrecision, Rounding rounding)
        {
            return NaN;
        }

        /// <summary>
        /// Returns the negation of a number: -x.
        /// </summary>
        /// <param name="x">The number.</param>
        public static Number operator -(Number x)
        {
            return Negate(x, Arithmetic.SignificandPrecision, Arithmetic.ExponentPrecision, Arithmetic.Rounding);
        }

        /// <summary>
        /// Returns the negation of a number: -x.
        /// </summary>
        /// <param name="x">The number.</param>
        /// <param name="significandPrecision">The precision to use for the significand when creating the result.</param>
        /// <param name="exponentPrecision">The precision to use for the exponent when creating the result.</param>
        /// <param name="rounding">The rounding mode to use when creating the result.</param>
        /// <returns>The arithmetic negation of <paramref name="x"/>.</returns>
        public static Number Negate(Number x, long significandPrecision, long exponentPrecision, Rounding rounding)
        {
            return NaN;
        }

        /// <summary>
        /// Returns the absolute value.
        /// </summary>
        public Number Abs()
        {
            return NaN;
        }

        /// <summary>
        /// Returns e (the base of natural logarithms) raised to the power of this object's value.
        /// </summary>
        public Number Exp()
        {
            return NaN;
        }

        /// <summary>
        /// Returns the natural logarithms of this object's value.
        /// </summary>
        public Number Log()
        {
            return NaN;
        }

        /// <summary>
        /// Returns the base-10 logarithms of this object's value.
        /// </summary>
        public Number Log10()
        {
            return NaN;
        }

        /// <summary>
        /// Returns this object's value raised to the power x.
        /// </summary>
        /// <param name="x">The number.</param>
        public Number Pow(Number x)
        {
            return NaN;
        }

        /// <summary>
        /// Returns the square root of this object's value.
        /// </summary>
        public Number Sqrt()
        {
            return NaN;
        }

        /// <summary>
        /// Returns this object's value multiplied by a specified power of two.
        /// </summary>
        /// <param name="other">The other number.</param>
        public Number ShiftLeft(Number other)
        {
            return NaN;
        }

        /// <summary>
        /// Returns this object's value divided by a specified power of two.
        /// </summary>
        /// <param name="other">The other number.</param>
        public Number ShiftRight(Number other)
        {
            return NaN;
        }

        /// <summary>
        /// Returns the remainder when this object's value is divided by another.
        /// </summary>
        /// <param name="other">The other number.</param>
        public Number Remainder(Number other)
        {
            return NaN;
        }

        /// <summary>
        /// Returns the bitwise AND of this object's value and another.
        /// </summary>
        /// <param name="other">The other number.</param>
        public Number BitwiseAnd(Number other)
        {
            return NaN;
        }

        /// <summary>
        /// Returns the bitwise OR of this object's value and another.
        /// </summary>
        /// <param name="other">The other number.</param>
        public Number BitwiseOr(Number other)
        {
            return NaN;
        }

        /// <summary>
        /// Returns the bitwise OR of this object's value and another.
        /// </summary>
        /// <param name="other">The other number.</param>
        public Number BitwiseXor(Number other)
        {
            return NaN;
        }

        #endregion

        #region Conversion
        /// <summary>
        /// Gets the value if it can be represented with a <see cref="int"/>.
        /// </summary>
        /// <param name="value">The value upon return.</param>
        public bool TryParseInt(out int value)
        {
            value = 0;
            return false;
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

                while (BitIndex <= IntegerField.SignificantBits + IntegerField.ShiftBits)
                {
                    bool Carry = IntegerField.GetBit(IntegerField.SignificantBits + IntegerField.ShiftBits - BitIndex);
                    IntegerString = NumberTextPartition.MultipliedByTwo(IntegerString, DecimalRadix, IsValidDecimalDigit, ToDecimalDigit, Carry);
                    BitIndex++;
                }

                string FractionalString;

                if (!IsInteger)
                {
                    Debug.Assert(FractionalField != null && FractionalField.SignificantBits > 0);

                    FractionalString = "500000000000";
                    BitIndex = 1;

                    while (BitIndex <= FractionalField.SignificantBits + FractionalField.ShiftBits)
                    {
                        bool Carry = FractionalField.GetBit(FractionalField.SignificantBits + FractionalField.ShiftBits - BitIndex);
                        int OldLength = FractionalString.Length;

                        if (Carry)
                            FractionalString = "1" + FractionalString;

                        FractionalString = NumberTextPartition.DividedByTwo(FractionalString, DecimalRadix, IsValidDecimalDigit, ToDecimalDigit, out bool HasDivisionbCarry);

                        if (FractionalString.Length < OldLength)
                            FractionalString = "0" + FractionalString;

                        BitIndex++;
                    }

                    FractionalString = RealTextPartition.RoundedToNearest(FractionalString, DecimalRadix, IsValidDecimalDigit, ToDecimalDigit, false);

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
