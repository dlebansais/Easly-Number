namespace EaslyNumber
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    /// Describes and manipulates real numbers with arbitrary precision.
    /// </summary>
    public partial struct Number : IFormattable
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
            IntegerField = BitField.Empty;
            FractionalField = BitField.Empty;
            ExponentField = BitField.Empty;
            CheatSingle = float.NaN;
            CheatDouble = double.NaN;

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
                throw new ArgumentException($"{nameof(text)} is not a valid number.");

            InitFromPartition(Partition);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a text partition.
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
            IntegerField = BitField.Empty;
            FractionalField = BitField.Empty;
            ExponentField = BitField.Empty;
            CheatSingle = float.NaN;
            CheatDouble = double.NaN;

            InitFromPartition(partition);
        }

        /// <summary>
        /// Initializes the object from a text partition.
        /// </summary>
        /// <param name="partition">The partition to copy values from.</param>
        /// <exception cref="ArgumentException">The partition does not represent a valid number.</exception>
        private void InitFromPartition(TextPartition partition)
        {
            if (partition.DiscardedProlog.Length > 0)
                throw new ArgumentException($"{nameof(partition)} does not represent a valid number.");

            if (partition.InvalidPart.Length > 0)
                throw new ArgumentException($"{nameof(partition)} does not represent a valid number.");

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

            SetCheatFromSpecialNumber();
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

            SetCheatFromText();
        }

        /// <summary>
        /// Initializes the object from a real number value.
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

            SetCheatFromText();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a parsed real text.
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
            IsSignificandNegative = isSignificandNegative;
            IsExponentNegative = isExponentNegative;
            CheatSingle = float.NaN;
            CheatDouble = double.NaN;

            IntegerField = integerField;
            FractionalField = fractionalField;
            ExponentField = exponentField;

            SetCheatFromText();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a parsed integer text.
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
            IsSignificandNegative = isSignificandNegative;
            IsExponentNegative = false;
            CheatSingle = float.NaN;
            CheatDouble = double.NaN;

            IntegerField = integerField;
            FractionalField = new BitField();
            ExponentField = new BitField();

            SetCheatFromText();
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
            IntegerField = BitField.Empty;
            FractionalField = BitField.Empty;
            ExponentField = BitField.Empty;
            CheatSingle = float.NaN;
            CheatDouble = double.NaN;

            InitFromText(value.ToString(CultureInfo.CurrentCulture));

            CheatSingle = value;
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

            bool ValueIsNaN = double.IsNaN(value);
            bool ValueIsPositiveInfinity = double.IsPositiveInfinity(value);
            bool ValueIsNegativeInfinity = double.IsNegativeInfinity(value);
            bool ValueIsZero = value == 0;

            if (ValueIsNaN || ValueIsPositiveInfinity || ValueIsNegativeInfinity || ValueIsZero)
            {
                SignificandPrecision = 0;
                ExponentPrecision = 0;
                Rounding = Arithmetic.Rounding;
                IsSignificandNegative = false;
                IsExponentNegative = false;
                IntegerField = BitField.Empty;
                FractionalField = BitField.Empty;
                ExponentField = BitField.Empty;
                CheatSingle = float.NaN;
                CheatDouble = double.NaN;

                InitAsSpecial(ValueIsNaN, ValueIsPositiveInfinity, ValueIsNegativeInfinity);
            }
            else
            {
                SignificandPrecision = 0;
                ExponentPrecision = 0;
                Rounding = Arithmetic.Rounding;
                IsSignificandNegative = value < 0;
                IsExponentNegative = value < 1;
                IntegerField = new BitField();
                IntegerField.SetOne();
                FractionalField = BitField.CreateFractionBitField(value);
                ExponentField = BitField.CreateExponentBitField(value);
                CheatSingle = float.NaN;
                CheatDouble = value;
            }
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
            IntegerField = BitField.Empty;
            FractionalField = BitField.Empty;
            ExponentField = BitField.Empty;
            CheatSingle = float.NaN;
            CheatDouble = double.NaN;

            InitFromText(value.ToString(CultureInfo.CurrentCulture));

            CheatDouble = (double)value;
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
            IntegerField = BitField.Empty;
            FractionalField = BitField.Empty;
            ExponentField = BitField.Empty;
            CheatSingle = float.NaN;
            CheatDouble = double.NaN;

            InitFromText(value.ToString(CultureInfo.CurrentCulture));

            CheatDouble = value;
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
            IntegerField = BitField.Empty;
            FractionalField = BitField.Empty;
            ExponentField = BitField.Empty;
            CheatSingle = float.NaN;
            CheatDouble = double.NaN;

            InitFromText(value.ToString(CultureInfo.CurrentCulture));

            CheatDouble = value;
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
            IntegerField = BitField.Empty;
            FractionalField = BitField.Empty;
            ExponentField = BitField.Empty;
            CheatSingle = float.NaN;
            CheatDouble = double.NaN;

            InitFromText(value.ToString(CultureInfo.CurrentCulture));

            CheatDouble = (double)value;
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
            IntegerField = BitField.Empty;
            FractionalField = BitField.Empty;
            ExponentField = BitField.Empty;
            CheatSingle = float.NaN;
            CheatDouble = double.NaN;

            InitFromText(value.ToString(CultureInfo.CurrentCulture));

            CheatDouble = (double)value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// </summary>
        /// <param name="isNaN">Value of the special NaN flag.</param>
        /// <param name="isPositiveInfinity">Value of the special positive infinity flag.</param>
        /// <param name="isNegativeInfinity">Value of the special negative infinity flag.</param>
        private Number(bool isNaN, bool isPositiveInfinity, bool isNegativeInfinity)
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
            IntegerField = BitField.Empty;
            FractionalField = BitField.Empty;
            ExponentField = BitField.Empty;
            CheatSingle = float.NaN;
            CheatDouble = double.NaN;

            InitAsSpecial(isNaN, isPositiveInfinity, isNegativeInfinity);
        }

        /// <summary>
        /// Initializes a special number.
        /// </summary>
        /// <param name="isNaN">Value of the special NaN flag.</param>
        /// <param name="isPositiveInfinity">Value of the special positive infinity flag.</param>
        /// <param name="isNegativeInfinity">Value of the special negative infinity flag.</param>
        private void InitAsSpecial(bool isNaN, bool isPositiveInfinity, bool isNegativeInfinity)
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
            CheatSingle = float.NaN;
            CheatDouble = double.NaN;

            if (IsZero)
            {
                IntegerField = BitField.Empty;
                FractionalField = BitField.Empty;
                ExponentField = BitField.Empty;
            }
            else
            {
                IntegerField = new BitField();
                IntegerField.SetZero();
                FractionalField = new BitField();
                ExponentField = new BitField();
            }

            SetCheatFromSpecialNumber();
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
        public bool IsInteger { get { return IsZero || FractionalField == BitField.Empty || FractionalField.SignificantBits == 0 || FractionalField.IsZero; } }

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
        public override bool Equals(object? other)
        {
            if (other is Number AsNumber)
                return CompareWithNumber(AsNumber);
            else
                return false;
        }

        /// <summary>
        /// Checks if two numbers are equal.
        /// </summary>
        /// <param name="other">The other instance.</param>
        public bool Equals(Number other)
        {
            return CompareWithNumber(other);
        }

        /// <summary>
        /// Checks if two numbers are equal.
        /// </summary>
        /// <param name="other">The other instance.</param>
        private bool CompareWithNumber(Number other)
        {
            return IsNaN == other.IsNaN &&
                    IsPositiveInfinity == other.IsPositiveInfinity &&
                    IsNegativeInfinity == other.IsNegativeInfinity &&
                    IsZero == other.IsZero &&
                    SignificandPrecision == other.SignificandPrecision &&
                    ExponentPrecision == other.ExponentPrecision &&
                    Rounding == other.Rounding &&
                    IsSignificandNegative == other.IsSignificandNegative &&
                    IsExponentNegative == other.IsExponentNegative &&
                    Equals(IntegerField, other.IntegerField) &&
                    Equals(FractionalField, other.FractionalField) &&
                    Equals(ExponentField, other.ExponentField);
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

            Debug.Assert(x.IntegerField != BitField.Empty);
            Debug.Assert(y.IntegerField != BitField.Empty);
            Debug.Assert(x.FractionalField != BitField.Empty);
            Debug.Assert(y.FractionalField != BitField.Empty);
            Debug.Assert(x.ExponentField != BitField.Empty);
            Debug.Assert(y.ExponentField != BitField.Empty);

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

            Debug.Assert(!x.IsSpecial);
            Debug.Assert(!y.IsSpecial);

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
        /// Compares two instances of <see cref="Number"/> and returns an integer that indicates whether the first instance is earlier than, the same as, or later than the second instance.
        /// Neither <paramref name="x"/> nor <paramref name="y"/> is allowed to be NaN.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        public static int Compare(Number x, Number y)
        {
            if (x.IsNaN)
                throw new ArgumentException($"{nameof(x)} is not allowed to be NaN");
            if (y.IsNaN)
                throw new ArgumentException($"{nameof(y)} is not allowed to be NaN");

            if (x.IsNegativeInfinity && !y.IsNegativeInfinity)
                return -1;

            if (x.IsPositiveInfinity && !y.IsPositiveInfinity)
                return +1;

            if ((x.IsNegativeInfinity && y.IsNegativeInfinity) || (x.IsPositiveInfinity && y.IsPositiveInfinity))
                return 0;

            if (x.IsZero && y.IsZero)
                return 0;

            if (x.IsZero)
                return y.IsSignificandNegative ? +1 : -1;

            if (y.IsZero)
                return x.IsSignificandNegative ? -1 : +1;

            Debug.Assert(!x.IsSpecial);
            Debug.Assert(!y.IsSpecial);

            if (x.IsSignificandNegative && !y.IsSignificandNegative)
                return -1;

            if (y.IsSignificandNegative && !x.IsSignificandNegative)
                return +1;

            Debug.Assert(x.IntegerField != BitField.Empty);
            Debug.Assert(y.IntegerField != BitField.Empty);
            Debug.Assert(x.FractionalField != BitField.Empty);
            Debug.Assert(y.FractionalField != BitField.Empty);
            Debug.Assert(x.ExponentField != BitField.Empty);
            Debug.Assert(y.ExponentField != BitField.Empty);

            bool IsSameInteger = x.IntegerField == y.IntegerField;
            bool IsSameFractional = x.FractionalField == y.FractionalField;
            bool IsSameExponent = x.ExponentField == y.ExponentField;

            if (IsSameInteger && IsSameFractional && IsSameExponent)
                return 0;

            return x < y ? -1 : +1;
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
            Debug.Assert(!x.IsSpecial);
            Debug.Assert(!y.IsSpecial);
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

            // TODO: handle exponent
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

            // TODO: handle exponent
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
        /// Converts the numeric value of this instance to its equivalent string representation.
        /// </summary>
        /// <returns>The string representation of the value of this instance.</returns>
        public override string ToString()
        {
            return ToString("G", CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation using the specified culture-specific format information.
        /// </summary>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the value of this instance as specified by provider.</returns>
        public string ToString(IFormatProvider provider)
        {
            return ToString("G", provider);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation, using the specified format.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <returns>The string representation of the value of this instance as specified by format.</returns>
        public string ToString(string format)
        {
            return ToString(format, CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent string representation using the specified format and culture-specific format information.
        /// </summary>
        /// <param name="format">A numeric format string.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The string representation of the value of this instance as specified by format and provider.</returns>
        public string ToString(string? format, IFormatProvider? provider)
        {
            if (!ParseNumericFormat(format, provider, out DisplayFormat DisplayFormat))
                throw new FormatException("Parameter format is invalid");

            string? Result = null;

            if (IsNaN)
                Result = double.NaN.ToString(CultureInfo.CurrentCulture);
            else if (IsPositiveInfinity)
                Result = double.PositiveInfinity.ToString(CultureInfo.CurrentCulture);
            else if (IsNegativeInfinity)
                Result = double.NegativeInfinity.ToString(CultureInfo.CurrentCulture);
            else
            {
                switch (DisplayFormat.NumericFormat)
                {
                    case NumericFormat.Default:
                        Result = ToStringDefaultFormat(DisplayFormat);
                        break;
                    case NumericFormat.Exponential:
                        Result = ToStringExponentialFormat(DisplayFormat);
                        break;
                    case NumericFormat.FixedPoint:
                        Result = ToStringFixedPointFormat(DisplayFormat);
                        break;
                }
            }

            Debug.Assert(Result != null);

            return Result !;
        }

        private static bool ParseNumericFormat(string? format, IFormatProvider? provider, out DisplayFormat displayFormat)
        {
            char FormatCharacter = (format == null || format.Length == 0) ? 'G' : format[0];
            bool IsExponentUpperCase = char.IsUpper(FormatCharacter);
            NumberFormatInfo NumberFormatInfo = (provider != null && provider.GetFormat(typeof(NumberFormatInfo)) is NumberFormatInfo AsNumberFormatInfo) ? AsNumberFormatInfo : NumberFormatInfo.CurrentInfo;

            NumericFormat NumericFormat;
            int PrecisionSpecifier;

            switch (FormatCharacter)
            {
                case 'G':
                case 'g':
                    NumericFormat = NumericFormat.Default;
                    PrecisionSpecifier = 15;
                    break;

                case 'E':
                case 'e':
                    NumericFormat = NumericFormat.Exponential;
                    PrecisionSpecifier = 6;
                    break;

                case 'F':
                case 'f':
                    NumericFormat = NumericFormat.FixedPoint;
                    PrecisionSpecifier = NumberFormatInfo.NumberDecimalDigits;
                    break;

                default:
                    displayFormat = DisplayFormat.Empty;
                    return false;
            }

            if (format != null && format.Length > 1)
            {
                if (!int.TryParse(format.Substring(1), out PrecisionSpecifier))
                {
                    displayFormat = DisplayFormat.Empty;
                    return false;
                }

                if (PrecisionSpecifier < 0 || PrecisionSpecifier > 99)
                {
                    displayFormat = DisplayFormat.Empty;
                    return false;
                }
            }

            Debug.Assert(NumericFormat == NumericFormat.Default || NumericFormat == NumericFormat.Exponential || NumericFormat == NumericFormat.FixedPoint);
            Debug.Assert(PrecisionSpecifier >= 0 && PrecisionSpecifier <= 99);

            displayFormat = new DisplayFormat(NumericFormat, IsExponentUpperCase, PrecisionSpecifier, NumberFormatInfo);
            return true;
        }

        private string ToStringExponentialFormat(DisplayFormat displayFormat)
        {
            string IntegerString = ComposeIntegerString(0, out long BitIndex);
            string Separator = displayFormat.NumberFormatInfo.NumberDecimalSeparator;

            string FractionalString = IsInteger ? (displayFormat.PrecisionSpecifier > 0 ? Separator : string.Empty) : ComposeFractionalString(displayFormat.PrecisionSpecifier, BitIndex);

            int i = FractionalString.Length > 0 ? FractionalString.Length - 1 : FractionalString.Length;
            for (; i < displayFormat.PrecisionSpecifier; i++)
                FractionalString += "0";

            string ExponentString;
            string ExponentCharacter = displayFormat.IsExponentUpperCase ? "E" : "e";

            if (ExponentField != BitField.Empty && ExponentField.SignificantBits > 0 && !ExponentField.IsZero)
            {
                ExponentString = "0";
                BitIndex = 1;

                while (BitIndex <= ExponentField.SignificantBits + ExponentField.ShiftBits)
                {
                    bool Carry = ExponentField.GetBit(ExponentField.SignificantBits + ExponentField.ShiftBits - BitIndex);
                    ExponentString = NumberTextPartition.MultipliedByTwo(ExponentString, DecimalRadix, IsValidDecimalDigit, ToDecimalDigit, Carry);
                    BitIndex++;
                }

                while (ExponentString.Length < 3)
                    ExponentString = "0" + ExponentString;

                ExponentString = ExponentString.Substring(0, 3);

                if (IsExponentNegative)
                    ExponentString = $"-{ExponentString}";
                else
                    ExponentString = $"+{ExponentString}";

                ExponentString = $"{ExponentCharacter}{ExponentString}";
            }
            else
                ExponentString = $"{ExponentCharacter}+000";

            string Result = $"{IntegerString}{FractionalString}{ExponentString}";

            return Result;
        }

        private string ToStringFixedPointFormat(DisplayFormat displayFormat)
        {
            string Result;

            if (IsInteger)
            {
                string IntegerString = ComposeIntegerString(0, out long _);
                string Separator = displayFormat.NumberFormatInfo.NumberDecimalSeparator;
                string FractionalString = string.Empty;

                for (int i = 0; i < displayFormat.PrecisionSpecifier; i++)
                    FractionalString += "0";

                if (displayFormat.PrecisionSpecifier > 0)
                    FractionalString = $"{Separator}{FractionalString}";

                Result = $"{IntegerString}{FractionalString}";
            }
            else
            {
                string IntegerString = ComposeIntegerString(0, out long BitIndex);
                string FractionalString = ComposeFractionalString(displayFormat.PrecisionSpecifier, BitIndex);
                string ExponentString = ComposeExponentString(displayFormat.IsExponentUpperCase);

                Result = $"{IntegerString}{FractionalString}{ExponentString}";
            }

            return Result;
        }

        private string ToStringDefaultFormat(DisplayFormat displayFormat)
        {
            if (IsInteger)
            {
                bool IsSmall = ExponentField.ToUInt64(out ulong ExponentValue);
                if (IsSmall)
                    return ComposeIntegerString(ExponentValue, out _);
            }

            string IntegerString = ComposeIntegerString(0, out long BitIndex);
            string FractionalString = ComposeFractionalString(displayFormat.PrecisionSpecifier, BitIndex);
            string ExponentString = ComposeExponentString(displayFormat.IsExponentUpperCase);

            string Result = $"{IntegerString}{FractionalString}{ExponentString}";

            return Result;
        }

        private string ComposeIntegerString(ulong exponentValue, out long bitIndex)
        {
            string IntegerString = "0";
            bitIndex = 1;

            if (IntegerField != BitField.Empty)
            {
                while (bitIndex <= IntegerField.SignificantBits + IntegerField.ShiftBits)
                {
                    bool Carry = IntegerField.GetBit(IntegerField.SignificantBits + IntegerField.ShiftBits - bitIndex);
                    IntegerString = NumberTextPartition.MultipliedByTwo(IntegerString, DecimalRadix, IsValidDecimalDigit, ToDecimalDigit, Carry);
                    bitIndex++;
                }

                while (exponentValue > 0)
                {
                    IntegerString = NumberTextPartition.MultipliedByTwo(IntegerString, DecimalRadix, IsValidDecimalDigit, ToDecimalDigit, false);
                    exponentValue--;
                }
            }

            if (IsSignificandNegative)
                IntegerString = $"-{IntegerString}";

            return IntegerString;
        }

        private string ComposeFractionalString(int precisionSpecifier, long bitIndex)
        {
            string FractionalString;

            Debug.Assert(FractionalField is BitField);
            Debug.Assert(FractionalField.SignificantBits > 0);

            FractionalString = "5000000000000000";
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

            if (precisionSpecifier + 1 < FractionalString.Length)
                FractionalString = FractionalString.Substring(0, precisionSpecifier + 1);

            FractionalString = RealTextPartition.RoundedToNearest(FractionalString, DecimalRadix, IsValidDecimalDigit, ToDecimalDigit, false);

            if (precisionSpecifier < FractionalString.Length)
                FractionalString = FractionalString.Substring(0, precisionSpecifier);

            string Separator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

            return $"{Separator}{FractionalString}";
        }

        private string ComposeExponentString(bool isExponentUpperCase)
        {
            if (ExponentField != BitField.Empty && ExponentField.SignificantBits > 0 && !ExponentField.IsZero)
            {
                string ExponentString = "0";
                long BitIndex = 1;

                while (BitIndex <= ExponentField.SignificantBits + ExponentField.ShiftBits)
                {
                    bool Carry = ExponentField.GetBit(ExponentField.SignificantBits + ExponentField.ShiftBits - BitIndex);
                    ExponentString = NumberTextPartition.MultipliedByTwo(ExponentString, DecimalRadix, IsValidDecimalDigit, ToDecimalDigit, Carry);
                    BitIndex++;
                }

                if (IsExponentNegative)
                    ExponentString = $"-{ExponentString}";

                string ExponentLetter = isExponentUpperCase ? "E" : "e";
                return $"{ExponentLetter}{ExponentString}";
            }
            else
                return string.Empty;
        }
        #endregion

        #region Cheat
        private void SetCheatFromSpecialNumber()
        {
            if (IsNaN)
            {
                CheatSingle = float.NaN;
                CheatDouble = double.NaN;
            }
            else if (IsPositiveInfinity)
            {
                CheatSingle = float.PositiveInfinity;
                CheatDouble = double.PositiveInfinity;
            }
            else if (IsNegativeInfinity)
            {
                CheatSingle = float.NegativeInfinity;
                CheatDouble = double.NegativeInfinity;
            }
            else
            {
                CheatSingle = 0;
                CheatDouble = 0;
            }
        }

        private void SetCheatFromText()
        {
            string AsText = ToString(CultureInfo.CurrentCulture);
            double AsDouble;

            if (double.TryParse(AsText, out AsDouble))
                CheatDouble = AsDouble;
        }

        /// <summary>
        /// The cheat value, as float.
        /// </summary>
        public float CheatSingle { get; private set; }

        /// <summary>
        /// The cheat value, as double.
        /// </summary>
        public double CheatDouble { get; private set; }
        #endregion
    }
}
