namespace Interop.Mpfr
{
    /// <summary>
    /// Rounding mode for operations on numbers.
    /// </summary>
#pragma warning disable SA1300 // Element should begin with upper-case letter
    public enum mpfr_rnd_t
#pragma warning restore SA1300 // Element should begin with upper-case letter
    {
        /// <summary>
        /// Round to nearest, with ties away from zero (mpfr_round).
        /// </summary>
        MPFR_RNDNA = -1,

        /// <summary>
        /// Round to nearest, with ties to even.
        /// </summary>
        MPFR_RNDN = 0,

        /// <summary>
        /// Round toward zero.
        /// </summary>
        MPFR_RNDZ,

        /// <summary>
        /// Round toward +Inf.
        /// </summary>
        MPFR_RNDU,

        /// <summary>
        /// Round toward -Inf.
        /// </summary>
        MPFR_RNDD,

        /// <summary>
        /// Round away from zero.
        /// </summary>
        MPFR_RNDA,

        /// <summary>
        /// Faithful rounding.
        /// </summary>
        MPFR_RNDF,
    }
}
