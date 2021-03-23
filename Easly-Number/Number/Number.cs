namespace EaslyNumber
{
    using System;
    using System.Globalization;
    using Interop.Mpfr;
    using static Interop.Mpfr.NativeMethods;

    /// <summary>
    /// Represents numbers with arbitrary precision.
    /// </summary>
    public partial struct Number : IFormattable
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

        /// <summary>
        /// The special min value for long.
        /// </summary>
        public static readonly Number LongMinValue = new Number("-9223372036854775808", 10);

        /// <summary>
        /// The special max value for long.
        /// </summary>
        public static readonly Number LongMaxValue = new Number("7FFFFFFFFFFFFFFF", 16);

        /// <summary>
        /// The special max value for ulong.
        /// </summary>
        public static readonly Number ULongMaxValue = new Number("FFFFFFFFFFFFFFFF", 16);
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
            Proxy = new mpfr_t();
            Rounding = DefaultRounding;

            if (text == CultureInfo.CurrentCulture.NumberFormat.NaNSymbol)
                mpfr_set_nan(ref Proxy.MpfrStruct);
            else if (text == CultureInfo.CurrentCulture.NumberFormat.PositiveInfinitySymbol)
                mpfr_set_inf(ref Proxy.MpfrStruct, +1);
            else if (text == CultureInfo.CurrentCulture.NumberFormat.NegativeInfinitySymbol)
                mpfr_set_inf(ref Proxy.MpfrStruct, -1);
            else
            {
                text = text.Replace(" ", string.Empty);
                text = text.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".");
                text = text.Replace(CultureInfo.CurrentCulture.NumberFormat.NumberGroupSeparator, string.Empty);

                if (text.Length > 0 && text[0] == '.')
                    text = "0" + text;

                int Success = mpfr_set_str(ref Proxy.MpfrStruct, text, 10, (mpfr_rnd_t)Rounding);
                if (Success != 0)
                    throw new ArgumentException(nameof(text));
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# float.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(float value)
        {
            Proxy = new mpfr_t(24);
            Rounding = DefaultRounding;

            mpfr_set_flt(ref Proxy.MpfrStruct, value, (mpfr_rnd_t)Rounding);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# double.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(double value)
        {
            Proxy = new mpfr_t(53);
            Rounding = DefaultRounding;

            mpfr_set_d(ref Proxy.MpfrStruct, value, (mpfr_rnd_t)Rounding);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# int.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(int value)
        {
            Proxy = new mpfr_t(32);
            Rounding = DefaultRounding;

            mpfr_set_si(ref Proxy.MpfrStruct, value, (mpfr_rnd_t)Rounding);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# unsigned int.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(uint value)
        {
            Proxy = new mpfr_t(32);
            Rounding = DefaultRounding;

            mpfr_set_ui(ref Proxy.MpfrStruct, value, (mpfr_rnd_t)Rounding);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# long.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(long value)
        {
            Proxy = new mpfr_t(64);
            Rounding = DefaultRounding;

            mpfr_set_str(ref Proxy.MpfrStruct, value.ToString(), 10, (mpfr_rnd_t)Rounding);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// This contructor creates the number from a C# unsigned long.
        /// </summary>
        /// <param name="value">The number value.</param>
        public Number(ulong value)
        {
            Proxy = new mpfr_t(64);
            Rounding = DefaultRounding;

            mpfr_set_str(ref Proxy.MpfrStruct, value.ToString(), 10, (mpfr_rnd_t)Rounding);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// </summary>
        /// <param name="text">The number in plain text.</param>
        /// <param name="textBase">The digit base for <paramref name="text"/>.</param>
        private Number(string text, uint textBase)
        {
            Proxy = new mpfr_t();
            Rounding = DefaultRounding;

            mpfr_set_str(ref Proxy.MpfrStruct, text, textBase, (mpfr_rnd_t)Rounding);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// </summary>
        /// <param name="precision">The precision.</param>
        /// <param name="rounding">The rounding.</param>
        private Number(ulong precision, Rounding rounding)
        {
            if (precision == ulong.MaxValue)
                Proxy = new mpfr_t();
            else
                Proxy = new mpfr_t(precision);

            if (rounding == Rounding.Default)
                Rounding = DefaultRounding;
            else
                Rounding = rounding;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Number"/> struct.
        /// </summary>
        /// <param name="isNaN">Value of the special NaN flag.</param>
        /// <param name="isPositiveInfinity">Value of the special positive infinity flag.</param>
        /// <param name="isNegativeInfinity">Value of the special negative infinity flag.</param>
        private Number(bool isNaN, bool isPositiveInfinity, bool isNegativeInfinity)
        {
            Proxy = new mpfr_t();
            Rounding = DefaultRounding;

            if (isNaN)
                mpfr_set_nan(ref Proxy.MpfrStruct);
            else if (isPositiveInfinity)
                mpfr_set_inf(ref Proxy.MpfrStruct, +1);
            else if (isNegativeInfinity)
                mpfr_set_inf(ref Proxy.MpfrStruct, -1);
            else
                mpfr_set_zero(ref Proxy.MpfrStruct, +1);
        }

        /// <summary>
        /// Resets the default precision to its initial value.
        /// </summary>
        public static void ResetDefaultPrecision()
        {
            DefaultPrecision = NativeDefaultPrecision;
        }

        private void Consolidate()
        {
            if (ReferenceEquals(Proxy, null))
                Proxy = new mpfr_t();
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the default precision, used when creating new numbers.
        /// </summary>
        public static ulong DefaultPrecision
        {
            get { return mpfr_get_default_prec(); }
            set { mpfr_set_default_prec(value); }
        }

        /// <summary>
        /// Gets or sets the precision.
        /// </summary>
        public ulong Precision
        {
            get
            {
                Consolidate();
                return mpfr_get_prec(ref Proxy.MpfrStruct);
            }
            set
            {
                Consolidate();
                mpfr_set_prec(ref Proxy.MpfrStruct, value);
            }
        }

        /// <summary>
        /// Gets or sets the precision.
        /// </summary>
        public Rounding Rounding { get; set; }

        /// <summary>
        /// Gets a value indicating whether the number is one of the special numbers.
        /// </summary>
        public bool IsSpecial { get { return IsNaN || IsInfinite; } }

        /// <summary>
        /// Gets a value indicating whether the number is a NaN.
        /// </summary>
        public bool IsNaN
        {
            get
            {
                Consolidate();
                return mpfr_nan_p(ref Proxy.MpfrStruct) != 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the number is one of the infinite values.
        /// </summary>
        public bool IsInfinite
        {
            get
            {
                Consolidate();
                return mpfr_inf_p(ref Proxy.MpfrStruct) != 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the number is the positive infinite value.
        /// </summary>
        public bool IsPositiveInfinity
        {
            get
            {
                Consolidate();
                return mpfr_inf_p(ref Proxy.MpfrStruct) != 0 && mpfr_sgn(ref Proxy.MpfrStruct) > 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the number is the negative infinite value.
        /// </summary>
        public bool IsNegativeInfinity
        {
            get
            {
                Consolidate();
                return mpfr_inf_p(ref Proxy.MpfrStruct) != 0 && mpfr_sgn(ref Proxy.MpfrStruct) < 0;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the number is 0.
        /// </summary>
        public bool IsZero
        {
            get
            {
                Consolidate();
                return mpfr_zero_p(ref Proxy.MpfrStruct) != 0;
            }
        }

        /// <summary>
        /// Gets the number sign.
        /// </summary>
        public int Sign
        {
            get
            {
                Consolidate();
                return mpfr_sgn(ref Proxy.MpfrStruct);
            }
        }

        /// <summary>
        /// Gets a value indicating whether the number is an integer.
        /// </summary>
        public bool IsInteger
        {
            get
            {
                Consolidate();
                return mpfr_integer_p(ref Proxy.MpfrStruct) != 0;
            }
        }
        #endregion

        #region Implementation
        private static Rounding DefaultRounding = Rounding.Nearest;
        private mpfr_t Proxy;
        #endregion
    }
}
