namespace EaslyNumber2
{
    using System;
    using static EaslyNumber2.NativeMethods;

    /// <summary>
    /// Represents numbers with arbitrary precision.
    /// </summary>
    public partial struct Number : IFormattable
    {
        /// <summary>
        /// Returns the sum of two numbers: x + y.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <param name="precision">The precision to use for the result.</param>
        /// <param name="rounding">The rounding mode to use when creating the result.</param>
        /// <returns>The arithmetic sum of <paramref name="x"/> and <paramref name="y"/>.</returns>
        public static Number Add(Number x, Number y, ulong precision, Rounding rounding)
        {
            x.Consolidate();
            y.Consolidate();

            Number z = new Number(precision, rounding);

            mpfr_add(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, rounding);

            return z;
        }

        /// <summary>
        /// Returns the difference between two numbers: x - y.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <param name="precision">The precision to use for the result.</param>
        /// <param name="rounding">The rounding mode to use when creating the result.</param>
        /// <returns>The arithmetic difference of <paramref name="x"/> and <paramref name="y"/>.</returns>
        public static Number Subtract(Number x, Number y, ulong precision, Rounding rounding)
        {
            x.Consolidate();
            y.Consolidate();

            Number z = new Number(precision, rounding);

            mpfr_sub(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, rounding);

            return z;
        }

        /// <summary>
        /// Returns the product of two numbers: x * y.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <param name="precision">The precision to use for the result.</param>
        /// <param name="rounding">The rounding mode to use when creating the result.</param>
        /// <returns>The arithmetic product of <paramref name="x"/> and <paramref name="y"/>.</returns>
        public static Number Multiply(Number x, Number y, ulong precision, Rounding rounding)
        {
            x.Consolidate();
            y.Consolidate();

            Number z = new Number(precision, rounding);

            mpfr_mul(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, rounding);

            return z;
        }

        /// <summary>
        /// Returns the ratio of two numbers: x / y.
        /// </summary>
        /// <param name="x">The first number.</param>
        /// <param name="y">The second number.</param>
        /// <param name="precision">The precision to use for the result.</param>
        /// <param name="rounding">The rounding mode to use when creating the result.</param>
        /// <returns>The arithmetic ratio of <paramref name="x"/> and <paramref name="y"/>.</returns>
        public static Number Divide(Number x, Number y, ulong precision, Rounding rounding)
        {
            x.Consolidate();
            y.Consolidate();

            Number z = new Number(precision, rounding);

            mpfr_div(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, rounding);

            return z;
        }

        /// <summary>
        /// Returns the negation of a number: -x.
        /// </summary>
        /// <param name="x">The number.</param>
        /// <param name="precision">The precision to use for the result.</param>
        /// <param name="rounding">The rounding mode to use when creating the result.</param>
        /// <returns>The arithmetic negation of <paramref name="x"/>.</returns>
        public static Number Negate(Number x, ulong precision, Rounding rounding)
        {
            x.Consolidate();

            Number z = new Number(precision, rounding);

            mpfr_neg(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, rounding);

            return z;
        }

        /// <summary>
        /// Returns the absolute value.
        /// </summary>
        /// <param name="precision">The precision to use for the result.</param>
        /// <param name="rounding">The rounding mode to use when creating the result.</param>
        public Number Abs(ulong precision = ulong.MaxValue, Rounding rounding = Rounding.Nearest)
        {
            Consolidate();

            Number z = new Number(precision, rounding);

            mpfr_abs(ref z.Proxy.MpfrStruct, ref Proxy.MpfrStruct, rounding);

            return z;
        }

        /// <summary>
        /// Returns e (the base of natural logarithms) raised to the power of this object's value.
        /// </summary>
        /// <param name="precision">The precision to use for the result.</param>
        /// <param name="rounding">The rounding mode to use when creating the result.</param>
        public Number Exp(ulong precision = ulong.MaxValue, Rounding rounding = Rounding.Nearest)
        {
            Consolidate();

            Number z = new Number(precision, rounding);

            mpfr_exp(ref z.Proxy.MpfrStruct, ref Proxy.MpfrStruct, rounding);

            return z;
        }

        /// <summary>
        /// Returns the natural logarithms of this object's value.
        /// </summary>
        /// <param name="precision">The precision to use for the result.</param>
        /// <param name="rounding">The rounding mode to use when creating the result.</param>
        public Number Log(ulong precision = ulong.MaxValue, Rounding rounding = Rounding.Nearest)
        {
            Consolidate();

            Number z = new Number(precision, rounding);

            mpfr_log(ref z.Proxy.MpfrStruct, ref Proxy.MpfrStruct, rounding);

            return z;
        }

        /// <summary>
        /// Returns the base-10 logarithms of this object's value.
        /// </summary>
        /// <param name="precision">The precision to use for the result.</param>
        /// <param name="rounding">The rounding mode to use when creating the result.</param>
        public Number Log10(ulong precision = ulong.MaxValue, Rounding rounding = Rounding.Nearest)
        {
            Consolidate();

            Number z = new Number(precision, rounding);

            mpfr_log10(ref z.Proxy.MpfrStruct, ref Proxy.MpfrStruct, rounding);

            return z;
        }

        /// <summary>
        /// Returns this object's value raised to the power x.
        /// </summary>
        /// <param name="x">The number.</param>
        /// <param name="precision">The precision to use for the result.</param>
        /// <param name="rounding">The rounding mode to use when creating the result.</param>
        public Number Pow(Number x, ulong precision = ulong.MaxValue, Rounding rounding = Rounding.Nearest)
        {
            Consolidate();
            x.Consolidate();

            Number z = new Number(precision, rounding);

            mpfr_pow(ref z.Proxy.MpfrStruct, ref Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, rounding);

            return z;
        }

        /// <summary>
        /// Returns the square root of this object's value.
        /// </summary>
        /// <param name="precision">The precision to use for the result.</param>
        /// <param name="rounding">The rounding mode to use when creating the result.</param>
        public Number Sqrt(ulong precision = ulong.MaxValue, Rounding rounding = Rounding.Nearest)
        {
            Consolidate();

            Number z = new Number(precision, rounding);

            mpfr_sqrt(ref z.Proxy.MpfrStruct, ref Proxy.MpfrStruct, rounding);

            return z;
        }

        /// <summary>
        /// Returns this object's value multiplied by a specified power of two.
        /// </summary>
        /// <param name="shift">The power of two.</param>
        /// <param name="precision">The precision to use for the result.</param>
        /// <param name="rounding">The rounding mode to use when creating the result.</param>
        public Number ShiftLeft(ulong shift, ulong precision = ulong.MaxValue, Rounding rounding = Rounding.Nearest)
        {
            Consolidate();

            Number z = new Number(precision, rounding);

            mpfr_mul_2exp(ref z.Proxy.MpfrStruct, ref Proxy.MpfrStruct, shift, rounding);

            return z;
        }

        /// <summary>
        /// Returns this object's value divided by a specified power of two.
        /// </summary>
        /// <param name="shift">The power of two.</param>
        /// <param name="precision">The precision to use for the result.</param>
        /// <param name="rounding">The rounding mode to use when creating the result.</param>
        public Number ShiftRight(ulong shift, ulong precision = ulong.MaxValue, Rounding rounding = Rounding.Nearest)
        {
            Consolidate();

            Number z = new Number(precision, rounding);

            mpfr_div_2exp(ref z.Proxy.MpfrStruct, ref Proxy.MpfrStruct, shift, rounding);

            return z;
        }

        /// <summary>
        /// Returns the remainder when this object's value is divided by x.
        /// </summary>
        /// <param name="x">The number.</param>
        /// <param name="precision">The precision to use for the result.</param>
        /// <param name="rounding">The rounding mode to use when creating the result.</param>
        public Number Remainder(Number x, ulong precision = ulong.MaxValue, Rounding rounding = Rounding.Nearest)
        {
            Consolidate();
            x.Consolidate();

            Number z = new Number(precision, rounding);

            mpfr_remainder(ref z.Proxy.MpfrStruct, ref Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, rounding);

            return z;
        }

        /// <summary>
        /// Returns the bitwise AND of this object's value and another.
        /// </summary>
        /// <param name="other">The other number.</param>
        public Number BitwiseAnd(Number other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the bitwise OR of this object's value and another.
        /// </summary>
        /// <param name="other">The other number.</param>
        public Number BitwiseOr(Number other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the bitwise OR of this object's value and another.
        /// </summary>
        /// <param name="other">The other number.</param>
        public Number BitwiseXor(Number other)
        {
            throw new NotImplementedException();
        }
    }
}
