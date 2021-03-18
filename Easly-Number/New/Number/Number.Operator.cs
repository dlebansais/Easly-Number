namespace EaslyNumber2
{
    using static EaslyNumber2.NativeMethods;

    /// <summary>
    /// Represents numbers with arbitrary precision.
    /// </summary>
    public partial struct Number
    {
        #region Add
        /// <summary>
        /// Return x + y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator +(Number x, Number y)
        {
            Number z = Uninitialized;

            mpfr_add(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, x.Rounding);

            return z;
        }

        /// <summary>
        /// Return x + y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator +(Number x, ulong y)
        {
            Number z = Uninitialized;

            mpfr_add_ui(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, x.Rounding);

            return z;
        }

        /// <summary>
        /// Return x + y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator +(ulong x, Number y)
        {
            Number z = Uninitialized;

            mpfr_add_ui(ref z.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, x, y.Rounding);

            return z;
        }

        /// <summary>
        /// Return x + y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator +(Number x, long y)
        {
            Number z = Uninitialized;

            mpfr_add_si(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, x.Rounding);

            return z;
        }

        /// <summary>
        /// Return x + y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator +(long x, Number y)
        {
            Number z = Uninitialized;

            mpfr_add_si(ref z.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, x, y.Rounding);

            return z;
        }

        /// <summary>
        /// Return x + y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator +(Number x, double y)
        {
            Number z = Uninitialized;

            mpfr_add_d(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, x.Rounding);

            return z;
        }

        /// <summary>
        /// Return x + y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator +(double x, Number y)
        {
            Number z = Uninitialized;

            mpfr_add_d(ref z.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, x, y.Rounding);

            return z;
        }
        #endregion

        #region Sub
        /// <summary>
        /// Return x - y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator -(Number x, Number y)
        {
            Number z = Uninitialized;

            mpfr_sub(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, x.Rounding);

            return z;
        }

        /// <summary>
        /// Return x - y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator -(Number x, ulong y)
        {
            Number z = Uninitialized;

            mpfr_sub_ui(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, x.Rounding);

            return z;
        }

        /// <summary>
        /// Return x - y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator -(ulong x, Number y)
        {
            Number z = Uninitialized;

            mpfr_ui_sub(ref z.Proxy.MpfrStruct, x, ref y.Proxy.MpfrStruct, y.Rounding);

            return z;
        }

        /// <summary>
        /// Return x - y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator -(Number x, long y)
        {
            Number z = Uninitialized;

            mpfr_sub_si(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, x.Rounding);

            return z;
        }

        /// <summary>
        /// Return x - y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator -(long x, Number y)
        {
            Number z = Uninitialized;

            mpfr_si_sub(ref z.Proxy.MpfrStruct, x, ref y.Proxy.MpfrStruct, y.Rounding);

            return z;
        }

        /// <summary>
        /// Return x - y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator -(Number x, double y)
        {
            Number z = Uninitialized;

            mpfr_sub_d(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, x.Rounding);

            return z;
        }

        /// <summary>
        /// Return x - y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator -(double x, Number y)
        {
            Number z = Uninitialized;

            mpfr_d_sub(ref z.Proxy.MpfrStruct, x, ref y.Proxy.MpfrStruct, y.Rounding);

            return z;
        }
        #endregion

        #region Mul
        /// <summary>
        /// Return x * y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator *(Number x, Number y)
        {
            Number z = Uninitialized;

            mpfr_mul(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, x.Rounding);

            return z;
        }

        /// <summary>
        /// Return x * y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator *(Number x, ulong y)
        {
            Number z = Uninitialized;

            mpfr_mul_ui(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, x.Rounding);

            return z;
        }

        /// <summary>
        /// Return x * y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator *(ulong x, Number y)
        {
            Number z = Uninitialized;

            mpfr_mul_ui(ref z.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, x, y.Rounding);

            return z;
        }

        /// <summary>
        /// Return x * y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator *(Number x, long y)
        {
            Number z = Uninitialized;

            mpfr_mul_si(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, x.Rounding);

            return z;
        }

        /// <summary>
        /// Return x * y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator *(long x, Number y)
        {
            Number z = Uninitialized;

            mpfr_mul_si(ref z.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, x, y.Rounding);

            return z;
        }

        /// <summary>
        /// Return x * y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator *(Number x, double y)
        {
            Number z = Uninitialized;

            mpfr_mul_d(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, x.Rounding);

            return z;
        }

        /// <summary>
        /// Return x * y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator *(double x, Number y)
        {
            Number z = Uninitialized;

            mpfr_mul_d(ref z.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, x, y.Rounding);

            return z;
        }
        #endregion

        #region Div
        /// <summary>
        /// Return x / y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator /(Number x, Number y)
        {
            Number z = Uninitialized;

            mpfr_div(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, x.Rounding);

            return z;
        }

        /// <summary>
        /// Return x / y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator /(Number x, ulong y)
        {
            Number z = Uninitialized;

            mpfr_div_ui(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, x.Rounding);

            return z;
        }

        /// <summary>
        /// Return x / y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator /(ulong x, Number y)
        {
            Number z = Uninitialized;

            mpfr_ui_div(ref z.Proxy.MpfrStruct, x, ref y.Proxy.MpfrStruct, y.Rounding);

            return z;
        }

        /// <summary>
        /// Return x / y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator /(Number x, long y)
        {
            Number z = Uninitialized;

            mpfr_div_si(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, x.Rounding);

            return z;
        }

        /// <summary>
        /// Return x / y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator /(long x, Number y)
        {
            Number z = Uninitialized;

            mpfr_si_div(ref z.Proxy.MpfrStruct, x, ref y.Proxy.MpfrStruct, y.Rounding);

            return z;
        }

        /// <summary>
        /// Return x / y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator /(Number x, double y)
        {
            Number z = Uninitialized;

            mpfr_div_d(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, x.Rounding);

            return z;
        }

        /// <summary>
        /// Return x / y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static Number operator /(double x, Number y)
        {
            Number z = Uninitialized;

            mpfr_d_div(ref z.Proxy.MpfrStruct, x, ref y.Proxy.MpfrStruct, y.Rounding);

            return z;
        }
        #endregion

        #region Misc
        /// <summary>
        /// Return -1.
        /// </summary>
        /// <param name="x">The operand.</param>
        public static Number operator -(Number x)
        {
            Number z = Uninitialized;

            mpfr_neg(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, x.Rounding);

            return z;
        }
        #endregion

        #region Comparison
        /// <summary>
        /// Return whether x > y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator >(Number x, Number y)
        {
            return mpfr_greater_p(ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct) != 0;
        }

        /// <summary>
        /// Return whether x > y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator >(Number x, ulong y)
        {
            return x.CompareTo(y) > 0;
        }

        /// <summary>
        /// Return whether x > y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator >(ulong x, Number y)
        {
            return y.CompareTo(x) < 0;
        }

        /// <summary>
        /// Return whether x > y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator >(Number x, long y)
        {
            return x.CompareTo(y) > 0;
        }

        /// <summary>
        /// Return whether x > y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator >(long x, Number y)
        {
            return y.CompareTo(x) < 0;
        }

        /// <summary>
        /// Return whether x > y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator >(Number x, double y)
        {
            return x.CompareTo(y) > 0;
        }

        /// <summary>
        /// Return whether x > y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator >(double x, Number y)
        {
            return y.CompareTo(x) < 0;
        }

        /// <summary>
        /// Return whether x >= y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator >=(Number x, Number y)
        {
            return mpfr_greaterequal_p(ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct) != 0;
        }

        /// <summary>
        /// Return whether x >= y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator >=(Number x, ulong y)
        {
            return x.CompareTo(y) >= 0;
        }

        /// <summary>
        /// Return whether x >= y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator >=(ulong x, Number y)
        {
            return y.CompareTo(x) <= 0;
        }

        /// <summary>
        /// Return whether x >= y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator >=(Number x, long y)
        {
            return x.CompareTo(y) >= 0;
        }

        /// <summary>
        /// Return whether x >= y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator >=(long x, Number y)
        {
            return y.CompareTo(x) <= 0;
        }

        /// <summary>
        /// Return whether x >= y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator >=(Number x, double y)
        {
            return x.CompareTo(y) >= 0;
        }

        /// <summary>
        /// Return whether x >= y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator >=(double x, Number y)
        {
            return y.CompareTo(x) <= 0;
        }

        /// <summary>
        /// Return whether x &lt; y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator <(Number x, Number y)
        {
            return mpfr_less_p(ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct) != 0;
        }

        /// <summary>
        /// Return whether x &lt; y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator <(Number x, ulong y)
        {
            return x.CompareTo(y) < 0;
        }

        /// <summary>
        /// Return whether x &lt; y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator <(ulong x, Number y)
        {
            return y.CompareTo(x) > 0;
        }

        /// <summary>
        /// Return whether x &lt; y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator <(Number x, long y)
        {
            return x.CompareTo(y) < 0;
        }

        /// <summary>
        /// Return whether x &lt; y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator <(long x, Number y)
        {
            return y.CompareTo(x) > 0;
        }

        /// <summary>
        /// Return whether x &lt; y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator <(Number x, double y)
        {
            return x.CompareTo(y) < 0;
        }

        /// <summary>
        /// Return whether x &lt; y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator <(double x, Number y)
        {
            return y.CompareTo(x) > 0;
        }

        /// <summary>
        /// Return whether x &lt;= y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator <=(Number x, Number y)
        {
            return mpfr_lessequal_p(ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct) != 0;
        }

        /// <summary>
        /// Return whether x &lt;= y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator <=(Number x, ulong y)
        {
            return x.CompareTo(y) <= 0;
        }

        /// <summary>
        /// Return whether x &lt;= y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator <=(ulong x, Number y)
        {
            return y.CompareTo(x) >= 0;
        }

        /// <summary>
        /// Return whether x &lt;= y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator <=(Number x, long y)
        {
            return x.CompareTo(y) <= 0;
        }

        /// <summary>
        /// Return whether x &lt;= y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator <=(long x, Number y)
        {
            return y.CompareTo(x) >= 0;
        }

        /// <summary>
        /// Return whether x &lt;= y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator <=(Number x, double y)
        {
            return x.CompareTo(y) <= 0;
        }

        /// <summary>
        /// Return whether x &lt;= y.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator <=(double x, Number y)
        {
            return y.CompareTo(x) >= 0;
        }

        /// <summary>
        /// Return whether x and y are equal.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator ==(Number x, Number y)
        {
            return mpfr_equal_p(ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct) != 0;
        }

        /// <summary>
        /// Return whether x and y are equal.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator ==(Number x, ulong y)
        {
            return x.CompareTo(y) == 0;
        }

        /// <summary>
        /// Return whether x and y are equal.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator ==(ulong x, Number y)
        {
            return y.CompareTo(x) == 0;
        }

        /// <summary>
        /// Return whether x and y are equal.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator ==(Number x, long y)
        {
            return x.CompareTo(y) == 0;
        }

        /// <summary>
        /// Return whether x and y are equal.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator ==(long x, Number y)
        {
            return y.CompareTo(x) == 0;
        }

        /// <summary>
        /// Return whether x and y are equal.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator ==(Number x, double y)
        {
            return x.CompareTo(y) == 0;
        }

        /// <summary>
        /// Return whether x and y are equal.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator ==(double x, Number y)
        {
            return y.CompareTo(x) == 0;
        }

        /// <summary>
        /// Return whether x and y are different.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator !=(Number x, Number y)
        {
            return mpfr_equal_p(ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct) == 0;
        }

        /// <summary>
        /// Return whether x and y are different.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator !=(Number x, ulong y)
        {
            return x.CompareTo(y) != 0;
        }

        /// <summary>
        /// Return whether x and y are different.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator !=(ulong x, Number y)
        {
            return y.CompareTo(x) != 0;
        }

        /// <summary>
        /// Return whether x and y are different.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator !=(Number x, long y)
        {
            return x.CompareTo(y) != 0;
        }

        /// <summary>
        /// Return whether x and y are different.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator !=(long x, Number y)
        {
            return y.CompareTo(x) != 0;
        }

        /// <summary>
        /// Return whether x and y are different.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator !=(Number x, double y)
        {
            return x.CompareTo(y) != 0;
        }

        /// <summary>
        /// Return whether x and y are different.
        /// </summary>
        /// <param name="x">The first operand.</param>
        /// <param name="y">The second operand.</param>
        public static bool operator !=(double x, Number y)
        {
            return y.CompareTo(x) != 0;
        }
        #endregion
    }
}
