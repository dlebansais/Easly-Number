namespace EaslyNumber;

using System;
using Interop.Mpfr;
using static Interop.Mpfr.NativeMethods;

/// <summary>
/// Represents numbers with arbitrary precision.
/// </summary>
public partial struct Number : IFormattable
{
    #region Add
    /// <summary>
    /// Return x + y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator +(Number x, Number y)
    {
        x.Consolidate();
        y.Consolidate();

        Number z = new Number(x.Precision, x.Rounding);

        mpfr_add(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, (mpfr_rnd_t)x.Rounding);

        return z;
    }

    /// <summary>
    /// Return x + y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator +(Number x, ulong y)
    {
        x.Consolidate();

        Number z = new Number(x.Precision, x.Rounding);

        mpfr_add_ui(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, (mpfr_rnd_t)x.Rounding);

        return z;
    }

    /// <summary>
    /// Return x + y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator +(ulong x, Number y)
    {
        y.Consolidate();

        Number z = new Number(y.Precision, y.Rounding);

        mpfr_add_ui(ref z.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, x, (mpfr_rnd_t)y.Rounding);

        return z;
    }

    /// <summary>
    /// Return x + y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator +(Number x, long y)
    {
        x.Consolidate();

        Number z = new Number(x.Precision, x.Rounding);

        mpfr_add_si(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, (mpfr_rnd_t)x.Rounding);

        return z;
    }

    /// <summary>
    /// Return x + y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator +(long x, Number y)
    {
        y.Consolidate();

        Number z = new Number(y.Precision, y.Rounding);

        mpfr_add_si(ref z.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, x, (mpfr_rnd_t)y.Rounding);

        return z;
    }

    /// <summary>
    /// Return x + y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator +(Number x, double y)
    {
        x.Consolidate();

        Number z = new Number(x.Precision, x.Rounding);

        mpfr_add_d(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, (mpfr_rnd_t)x.Rounding);

        return z;
    }

    /// <summary>
    /// Return x + y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator +(double x, Number y)
    {
        y.Consolidate();

        Number z = new Number(y.Precision, y.Rounding);

        mpfr_add_d(ref z.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, x, (mpfr_rnd_t)y.Rounding);

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
        x.Consolidate();
        y.Consolidate();

        Number z = new Number(x.Precision, x.Rounding);

        mpfr_sub(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, (mpfr_rnd_t)x.Rounding);

        return z;
    }

    /// <summary>
    /// Return x - y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator -(Number x, ulong y)
    {
        x.Consolidate();

        Number z = new Number(x.Precision, x.Rounding);

        mpfr_sub_ui(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, (mpfr_rnd_t)x.Rounding);

        return z;
    }

    /// <summary>
    /// Return x - y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator -(ulong x, Number y)
    {
        y.Consolidate();

        Number z = new Number(y.Precision, y.Rounding);

        mpfr_ui_sub(ref z.Proxy.MpfrStruct, x, ref y.Proxy.MpfrStruct, (mpfr_rnd_t)y.Rounding);

        return z;
    }

    /// <summary>
    /// Return x - y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator -(Number x, long y)
    {
        x.Consolidate();

        Number z = new Number(x.Precision, x.Rounding);

        mpfr_sub_si(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, (mpfr_rnd_t)x.Rounding);

        return z;
    }

    /// <summary>
    /// Return x - y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator -(long x, Number y)
    {
        y.Consolidate();

        Number z = new Number(y.Precision, y.Rounding);

        mpfr_si_sub(ref z.Proxy.MpfrStruct, x, ref y.Proxy.MpfrStruct, (mpfr_rnd_t)y.Rounding);

        return z;
    }

    /// <summary>
    /// Return x - y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator -(Number x, double y)
    {
        x.Consolidate();

        Number z = new Number(x.Precision, x.Rounding);

        mpfr_sub_d(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, (mpfr_rnd_t)x.Rounding);

        return z;
    }

    /// <summary>
    /// Return x - y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator -(double x, Number y)
    {
        y.Consolidate();

        Number z = new Number(y.Precision, y.Rounding);

        mpfr_d_sub(ref z.Proxy.MpfrStruct, x, ref y.Proxy.MpfrStruct, (mpfr_rnd_t)y.Rounding);

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
        x.Consolidate();
        y.Consolidate();

        Number z = new Number(x.Precision, x.Rounding);

        mpfr_mul(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, (mpfr_rnd_t)x.Rounding);

        return z;
    }

    /// <summary>
    /// Return x * y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator *(Number x, ulong y)
    {
        x.Consolidate();

        Number z = new Number(x.Precision, x.Rounding);

        mpfr_mul_ui(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, (mpfr_rnd_t)x.Rounding);

        return z;
    }

    /// <summary>
    /// Return x * y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator *(ulong x, Number y)
    {
        y.Consolidate();

        Number z = new Number(y.Precision, y.Rounding);

        mpfr_mul_ui(ref z.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, x, (mpfr_rnd_t)y.Rounding);

        return z;
    }

    /// <summary>
    /// Return x * y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator *(Number x, long y)
    {
        x.Consolidate();

        Number z = new Number(x.Precision, x.Rounding);

        mpfr_mul_si(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, (mpfr_rnd_t)x.Rounding);

        return z;
    }

    /// <summary>
    /// Return x * y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator *(long x, Number y)
    {
        y.Consolidate();

        Number z = new Number(y.Precision, y.Rounding);

        mpfr_mul_si(ref z.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, x, (mpfr_rnd_t)y.Rounding);

        return z;
    }

    /// <summary>
    /// Return x * y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator *(Number x, double y)
    {
        x.Consolidate();

        Number z = new Number(x.Precision, x.Rounding);

        mpfr_mul_d(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, (mpfr_rnd_t)x.Rounding);

        return z;
    }

    /// <summary>
    /// Return x * y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator *(double x, Number y)
    {
        y.Consolidate();

        Number z = new Number(y.Precision, y.Rounding);

        mpfr_mul_d(ref z.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, x, (mpfr_rnd_t)y.Rounding);

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
        x.Consolidate();
        y.Consolidate();

        Number z = new Number(x.Precision, x.Rounding);

        mpfr_div(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct, (mpfr_rnd_t)x.Rounding);

        return z;
    }

    /// <summary>
    /// Return x / y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator /(Number x, ulong y)
    {
        x.Consolidate();

        Number z = new Number(x.Precision, x.Rounding);

        mpfr_div_ui(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, (mpfr_rnd_t)x.Rounding);

        return z;
    }

    /// <summary>
    /// Return x / y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator /(ulong x, Number y)
    {
        y.Consolidate();

        Number z = new Number(y.Precision, y.Rounding);

        mpfr_ui_div(ref z.Proxy.MpfrStruct, x, ref y.Proxy.MpfrStruct, (mpfr_rnd_t)y.Rounding);

        return z;
    }

    /// <summary>
    /// Return x / y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator /(Number x, long y)
    {
        x.Consolidate();

        Number z = new Number(x.Precision, x.Rounding);

        mpfr_div_si(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, (mpfr_rnd_t)x.Rounding);

        return z;
    }

    /// <summary>
    /// Return x / y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator /(long x, Number y)
    {
        y.Consolidate();

        Number z = new Number(y.Precision, y.Rounding);

        mpfr_si_div(ref z.Proxy.MpfrStruct, x, ref y.Proxy.MpfrStruct, (mpfr_rnd_t)y.Rounding);

        return z;
    }

    /// <summary>
    /// Return x / y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator /(Number x, double y)
    {
        x.Consolidate();

        Number z = new Number(x.Precision, x.Rounding);

        mpfr_div_d(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, y, (mpfr_rnd_t)x.Rounding);

        return z;
    }

    /// <summary>
    /// Return x / y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator /(double x, Number y)
    {
        y.Consolidate();

        Number z = new Number(y.Precision, y.Rounding);

        mpfr_d_div(ref z.Proxy.MpfrStruct, x, ref y.Proxy.MpfrStruct, (mpfr_rnd_t)y.Rounding);

        return z;
    }
    #endregion

    #region Shift
    /// <summary>
    /// Return x &lt;&lt; y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator <<(Number x, int y)
    {
        x.Consolidate();

        Number z = new Number(x.Precision, x.Rounding);

        mpfr_mul_2exp(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, (ulong)y, (mpfr_rnd_t)z.Rounding);

        return z;
    }

    /// <summary>
    /// Return x &gt;&gt; y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator >>(Number x, int y)
    {
        x.Consolidate();

        Number z = new Number(x.Precision, x.Rounding);

        mpfr_div_2exp(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, (ulong)y, (mpfr_rnd_t)z.Rounding);

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
        x.Consolidate();

        Number z = new Number(x.Precision, x.Rounding);

        mpfr_neg(ref z.Proxy.MpfrStruct, ref x.Proxy.MpfrStruct, (mpfr_rnd_t)x.Rounding);

        return z;
    }
    #endregion

    #region Bitwise
    /// <summary>
    /// Return x &amp; y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator &(Number x, Number y)
    {
        return x.BitwiseAnd(y, x.Precision, x.Rounding);
    }

    /// <summary>
    /// Return x | y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator |(Number x, Number y)
    {
        return x.BitwiseOr(y, x.Precision, x.Rounding);
    }

    /// <summary>
    /// Return x ^ y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static Number operator ^(Number x, Number y)
    {
        return x.BitwiseXor(y, x.Precision, x.Rounding);
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
        x.Consolidate();
        y.Consolidate();

        return mpfr_greater_p(ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct) != 0;
    }

    /// <summary>
    /// Return whether x > y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator >(Number x, ulong y)
    {
        x.Consolidate();

        return x.CompareTo(y) > 0;
    }

    /// <summary>
    /// Return whether x > y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator >(ulong x, Number y)
    {
        y.Consolidate();

        return y.CompareTo(x) < 0;
    }

    /// <summary>
    /// Return whether x > y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator >(Number x, long y)
    {
        x.Consolidate();

        return x.CompareTo(y) > 0;
    }

    /// <summary>
    /// Return whether x > y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator >(long x, Number y)
    {
        y.Consolidate();

        return y.CompareTo(x) < 0;
    }

    /// <summary>
    /// Return whether x > y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator >(Number x, double y)
    {
        x.Consolidate();

        return x.CompareTo(y) > 0;
    }

    /// <summary>
    /// Return whether x > y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator >(double x, Number y)
    {
        y.Consolidate();

        return y.CompareTo(x) < 0;
    }

    /// <summary>
    /// Return whether x >= y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator >=(Number x, Number y)
    {
        x.Consolidate();
        y.Consolidate();

        return mpfr_greaterequal_p(ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct) != 0;
    }

    /// <summary>
    /// Return whether x >= y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator >=(Number x, ulong y)
    {
        x.Consolidate();

        return x.CompareTo(y) >= 0;
    }

    /// <summary>
    /// Return whether x >= y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator >=(ulong x, Number y)
    {
        y.Consolidate();

        return y.CompareTo(x) <= 0;
    }

    /// <summary>
    /// Return whether x >= y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator >=(Number x, long y)
    {
        x.Consolidate();

        return x.CompareTo(y) >= 0;
    }

    /// <summary>
    /// Return whether x >= y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator >=(long x, Number y)
    {
        y.Consolidate();

        return y.CompareTo(x) <= 0;
    }

    /// <summary>
    /// Return whether x >= y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator >=(Number x, double y)
    {
        x.Consolidate();

        return x.CompareTo(y) >= 0;
    }

    /// <summary>
    /// Return whether x >= y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator >=(double x, Number y)
    {
        y.Consolidate();

        return y.CompareTo(x) <= 0;
    }

    /// <summary>
    /// Return whether x &lt; y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator <(Number x, Number y)
    {
        x.Consolidate();
        y.Consolidate();

        return mpfr_less_p(ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct) != 0;
    }

    /// <summary>
    /// Return whether x &lt; y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator <(Number x, ulong y)
    {
        x.Consolidate();

        return x.CompareTo(y) < 0;
    }

    /// <summary>
    /// Return whether x &lt; y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator <(ulong x, Number y)
    {
        y.Consolidate();

        return y.CompareTo(x) > 0;
    }

    /// <summary>
    /// Return whether x &lt; y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator <(Number x, long y)
    {
        x.Consolidate();

        return x.CompareTo(y) < 0;
    }

    /// <summary>
    /// Return whether x &lt; y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator <(long x, Number y)
    {
        y.Consolidate();

        return y.CompareTo(x) > 0;
    }

    /// <summary>
    /// Return whether x &lt; y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator <(Number x, double y)
    {
        x.Consolidate();

        return x.CompareTo(y) < 0;
    }

    /// <summary>
    /// Return whether x &lt; y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator <(double x, Number y)
    {
        y.Consolidate();

        return y.CompareTo(x) > 0;
    }

    /// <summary>
    /// Return whether x &lt;= y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator <=(Number x, Number y)
    {
        x.Consolidate();
        y.Consolidate();

        return mpfr_lessequal_p(ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct) != 0;
    }

    /// <summary>
    /// Return whether x &lt;= y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator <=(Number x, ulong y)
    {
        x.Consolidate();

        return x.CompareTo(y) <= 0;
    }

    /// <summary>
    /// Return whether x &lt;= y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator <=(ulong x, Number y)
    {
        y.Consolidate();

        return y.CompareTo(x) >= 0;
    }

    /// <summary>
    /// Return whether x &lt;= y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator <=(Number x, long y)
    {
        x.Consolidate();

        return x.CompareTo(y) <= 0;
    }

    /// <summary>
    /// Return whether x &lt;= y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator <=(long x, Number y)
    {
        y.Consolidate();

        return y.CompareTo(x) >= 0;
    }

    /// <summary>
    /// Return whether x &lt;= y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator <=(Number x, double y)
    {
        x.Consolidate();

        return x.CompareTo(y) <= 0;
    }

    /// <summary>
    /// Return whether x &lt;= y.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator <=(double x, Number y)
    {
        y.Consolidate();

        return y.CompareTo(x) >= 0;
    }

    /// <summary>
    /// Return whether x and y are equal.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator ==(Number x, Number y)
    {
        x.Consolidate();
        y.Consolidate();

        return mpfr_equal_p(ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct) != 0;
    }

    /// <summary>
    /// Return whether x and y are equal.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator ==(Number x, ulong y)
    {
        x.Consolidate();

        return x.CompareTo(y) == 0;
    }

    /// <summary>
    /// Return whether x and y are equal.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator ==(ulong x, Number y)
    {
        y.Consolidate();

        return y.CompareTo(x) == 0;
    }

    /// <summary>
    /// Return whether x and y are equal.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator ==(Number x, long y)
    {
        x.Consolidate();

        return x.CompareTo(y) == 0;
    }

    /// <summary>
    /// Return whether x and y are equal.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator ==(long x, Number y)
    {
        y.Consolidate();

        return y.CompareTo(x) == 0;
    }

    /// <summary>
    /// Return whether x and y are equal.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator ==(Number x, double y)
    {
        x.Consolidate();

        return x.CompareTo(y) == 0;
    }

    /// <summary>
    /// Return whether x and y are equal.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator ==(double x, Number y)
    {
        y.Consolidate();

        return y.CompareTo(x) == 0;
    }

    /// <summary>
    /// Return whether x and y are different.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator !=(Number x, Number y)
    {
        x.Consolidate();
        y.Consolidate();

        return mpfr_equal_p(ref x.Proxy.MpfrStruct, ref y.Proxy.MpfrStruct) == 0;
    }

    /// <summary>
    /// Return whether x and y are different.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator !=(Number x, ulong y)
    {
        x.Consolidate();

        return x.CompareTo(y) != 0;
    }

    /// <summary>
    /// Return whether x and y are different.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator !=(ulong x, Number y)
    {
        y.Consolidate();

        return y.CompareTo(x) != 0;
    }

    /// <summary>
    /// Return whether x and y are different.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator !=(Number x, long y)
    {
        x.Consolidate();

        return x.CompareTo(y) != 0;
    }

    /// <summary>
    /// Return whether x and y are different.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator !=(long x, Number y)
    {
        y.Consolidate();

        return y.CompareTo(x) != 0;
    }

    /// <summary>
    /// Return whether x and y are different.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator !=(Number x, double y)
    {
        x.Consolidate();

        return x.CompareTo(y) != 0;
    }

    /// <summary>
    /// Return whether x and y are different.
    /// </summary>
    /// <param name="x">The first operand.</param>
    /// <param name="y">The second operand.</param>
    public static bool operator !=(double x, Number y)
    {
        y.Consolidate();

        return y.CompareTo(x) != 0;
    }
    #endregion
}
