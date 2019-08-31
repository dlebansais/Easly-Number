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
        /// The special value for positive infinity.
        /// </summary>
        public static readonly Number PositiveInfinity = new Number(true);

        /// <summary>
        /// The special value for negative infinity.
        /// </summary>
        public static readonly Number NegativeInfinity = new Number(true);

        /// <summary>
        /// The special value for not-a-number.
        /// </summary>
        public static readonly Number NaN = new Number(true);

        /// <summary>
        /// The special value zero.
        /// </summary>
        public static readonly Number Zero = new Number(false);
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
            Parse(text, out string DiscardedProlog, out OptionalSign SignificandSign, out string IntegerPart, out OptionalSeparator Separator, out string FractionalPart, out OptionalExponent Exponent, out OptionalSign ExponentSign, out string ExponentPart, out string InvalidPart);

            if (DiscardedProlog.Length > 0)
                throw new ArgumentException();

            if (InvalidPart.Length > 0)
                throw new ArgumentException();

            IsSpecial = false;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from parsed parts.
        /// </summary>
        /// <param name="significandSign">The optional sign of the significand.</param>
        /// <param name="integerPart">The integer part in front of the separator (if any).</param>
        /// <param name="separator">The optional separator.</param>
        /// <param name="fractionalPart">The fractional part after the separator (if any).</param>
        /// <param name="exponent">The optional exponent character.</param>
        /// <param name="exponentSign">The optional exponent sign.</param>
        /// <param name="exponentPart">The exponent part (if any).</param>
        internal Number(OptionalSign significandSign, string integerPart, OptionalSeparator separator, string fractionalPart, OptionalExponent exponent, OptionalSign exponentSign, string exponentPart)
        {
            Debug.Assert(integerPart.Length > 0 || fractionalPart.Length > 0);
            Debug.Assert(exponentSign == OptionalSign.None || exponent != OptionalExponent.None);
            Debug.Assert(exponentPart.Length == 0 || exponent != OptionalExponent.None);

            IsSpecial = false;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# float.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(float value)
        {
            IsSpecial = false;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# double.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(double value)
        {
            IsSpecial = false;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# decimal.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(decimal value)
        {
            IsSpecial = false;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# int.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(int value)
        {
            IsSpecial = false;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# unsigned int.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(uint value)
        {
            IsSpecial = false;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# long.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(long value)
        {
            IsSpecial = false;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# unsigned long.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(ulong value)
        {
            IsSpecial = false;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// </summary>
        /// <param name="isSpecial">The special flag. If false, the number is just zero.</param>
        private Number(bool isSpecial)
        {
            IsSpecial = isSpecial;
            SignificandPrecision = Arithmetic.SignificandPrecision;
            ExponentPrecision = Arithmetic.ExponentPrecision;
            Rounding = Arithmetic.Rounding;
            IsSignificandNegative = false;
            IsExponentNegative = false;
        }
        #endregion

        #region Properties
        /// <summary>
        /// True if the number is one of the special numbers.
        /// </summary>
        public bool IsSpecial { get; private set; }

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
            return string.Empty;
        }
        #endregion
    }
}
