namespace EaslyNumber
{
    using System;

    /// <summary>
    /// Describes and manipulates real numbers with arbitrary precision.
    /// </summary>
    public partial struct Number
    {
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
            return new Number(x.Cheat + y.Cheat);
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
            return new Number(x.Cheat - y.Cheat);
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
            return new Number(x.Cheat * y.Cheat);
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
            return new Number(x.Cheat / y.Cheat);
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
            return new Number(-x.Cheat);
        }

        /// <summary>
        /// Returns the absolute value.
        /// </summary>
        public Number Abs()
        {
            return new Number(Math.Abs(Cheat));
        }

        /// <summary>
        /// Returns e (the base of natural logarithms) raised to the power of this object's value.
        /// </summary>
        public Number Exp()
        {
            return new Number(Math.Exp(Cheat));
        }

        /// <summary>
        /// Returns the natural logarithms of this object's value.
        /// </summary>
        public Number Log()
        {
            return new Number(Math.Log(Cheat));
        }

        /// <summary>
        /// Returns the base-10 logarithms of this object's value.
        /// </summary>
        public Number Log10()
        {
            return new Number(Math.Log10(Cheat));
        }

        /// <summary>
        /// Returns this object's value raised to the power x.
        /// </summary>
        /// <param name="x">The number.</param>
        public Number Pow(Number x)
        {
            return new Number(Math.Pow(Cheat, x.Cheat));
        }

        /// <summary>
        /// Returns the square root of this object's value.
        /// </summary>
        public Number Sqrt()
        {
            return new Number(Math.Sqrt(Cheat));
        }

        /// <summary>
        /// Returns this object's value multiplied by a specified power of two.
        /// </summary>
        /// <param name="other">The other number.</param>
        public Number ShiftLeft(Number other)
        {
            return new Number(((long)Cheat) << ((int)other.Cheat));
        }

        /// <summary>
        /// Returns this object's value divided by a specified power of two.
        /// </summary>
        /// <param name="other">The other number.</param>
        public Number ShiftRight(Number other)
        {
            return new Number(((long)Cheat) >> ((int)other.Cheat));
        }

        /// <summary>
        /// Returns the remainder when this object's value is divided by x.
        /// </summary>
        /// <param name="x">The number.</param>
        public Number Remainder(Number x)
        {
            return new Number(Math.IEEERemainder(Cheat, x.Cheat));
        }

        /// <summary>
        /// Returns the bitwise AND of this object's value and another.
        /// </summary>
        /// <param name="other">The other number.</param>
        public Number BitwiseAnd(Number other)
        {
            return new Number(((long)Cheat) & ((long)other.Cheat));
        }

        /// <summary>
        /// Returns the bitwise OR of this object's value and another.
        /// </summary>
        /// <param name="other">The other number.</param>
        public Number BitwiseOr(Number other)
        {
            return new Number(((long)Cheat) | ((long)other.Cheat));
        }

        /// <summary>
        /// Returns the bitwise OR of this object's value and another.
        /// </summary>
        /// <param name="other">The other number.</param>
        public Number BitwiseXor(Number other)
        {
            return new Number(((long)Cheat) ^ ((long)other.Cheat));
        }

        #endregion

        #region Conversion
        /// <summary>
        /// Gets the value if it can be represented with a <see cref="int"/>.
        /// </summary>
        /// <param name="value">The value upon return.</param>
        public bool TryParseInt(out int value)
        {
            value = (int)Cheat;

            if (value == Cheat)
                return true;
            else
                return false;
        }
        #endregion
    }
}
