﻿namespace EaslyNumber
{
    using System.Runtime.InteropServices;

#pragma warning disable SA1601 // Partial elements should be documented
    internal static partial class NativeMethods
    {
        #region Number Theoretic Functions
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int __mpz_probable_prime_p(ref __mpz_t n, ref __gmp_randstate_t state, int prob, uint div);
        public static __mpz_probable_prime_p mpz_probable_prime_p { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_probable_prime_p>(GetMpirPointer(nameof(mpz_probable_prime_p)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int __mpz_likely_prime_p(ref __mpz_t n, ref __gmp_randstate_t state, uint div);
        public static __mpz_likely_prime_p mpz_likely_prime_p { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_likely_prime_p>(GetMpirPointer(nameof(mpz_likely_prime_p)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_next_prime_candidate(ref __mpz_t rop, ref __mpz_t op, ref __gmp_randstate_t state);
        public static __mpz_next_prime_candidate mpz_next_prime_candidate { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_next_prime_candidate>(GetMpirPointer(nameof(mpz_next_prime_candidate)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_gcd(ref __mpz_t rop, ref __mpz_t op1, ref __mpz_t op2);
        public static __mpz_gcd mpz_gcd { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_gcd>(GetMpirPointer(nameof(mpz_gcd)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate uint __mpz_gcd_ui(ref __mpz_t rop, ref __mpz_t op1, uint op2);
        public static __mpz_gcd_ui mpz_gcd_ui { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_gcd_ui>(GetMpirPointer(nameof(mpz_gcd_ui)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_gcdext(ref __mpz_t g, ref __mpz_t s, ref __mpz_t t, ref __mpz_t a, ref __mpz_t b);
        public static __mpz_gcdext mpz_gcdext { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_gcdext>(GetMpirPointer(nameof(mpz_gcdext)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_lcm(ref __mpz_t rop, ref __mpz_t op1, ref __mpz_t op2);
        public static __mpz_lcm mpz_lcm { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_lcm>(GetMpirPointer(nameof(mpz_lcm)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_lcm_ui(ref __mpz_t rop, ref __mpz_t op1, uint op2);
        public static __mpz_lcm_ui mpz_lcm_ui { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_lcm_ui>(GetMpirPointer(nameof(mpz_lcm_ui)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int __mpz_invert(ref __mpz_t rop, ref __mpz_t op1, ref __mpz_t op2);
        public static __mpz_invert mpz_invert { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_invert>(GetMpirPointer(nameof(mpz_invert)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int __mpz_jacobi(ref __mpz_t a, ref __mpz_t b);
        public static __mpz_jacobi mpz_jacobi { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_jacobi>(GetMpirPointer(nameof(mpz_jacobi)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int __mpz_kronecker_si(ref __mpz_t a, int b);
        public static __mpz_kronecker_si mpz_kronecker_si { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_kronecker_si>(GetMpirPointer(nameof(mpz_kronecker_si)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int __mpz_kronecker_ui(ref __mpz_t a, uint b);
        public static __mpz_kronecker_ui mpz_kronecker_ui { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_kronecker_ui>(GetMpirPointer(nameof(mpz_kronecker_ui)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int __mpz_si_kronecker(int a, ref __mpz_t b);
        public static __mpz_si_kronecker mpz_si_kronecker { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_si_kronecker>(GetMpirPointer(nameof(mpz_si_kronecker)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int __mpz_ui_kronecker(uint a, ref __mpz_t b);
        public static __mpz_ui_kronecker mpz_ui_kronecker { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_ui_kronecker>(GetMpirPointer(nameof(mpz_ui_kronecker)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate ulong __mpz_remove(ref __mpz_t rop, ref __mpz_t op, ref __mpz_t f);
        public static __mpz_remove mpz_remove { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_remove>(GetMpirPointer(nameof(mpz_remove)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_fac_ui(ref __mpz_t rop, ulong n);
        public static __mpz_fac_ui mpz_fac_ui { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_fac_ui>(GetMpirPointer(nameof(mpz_fac_ui)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_2fac_ui(ref __mpz_t rop, ulong n);
        public static __mpz_2fac_ui mpz_2fac_ui { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_2fac_ui>(GetMpirPointer(nameof(mpz_2fac_ui)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_mfac_uiui(ref __mpz_t rop, ulong n, ulong m);
        public static __mpz_mfac_uiui mpz_mfac_uiui { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_mfac_uiui>(GetMpirPointer(nameof(mpz_mfac_uiui)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_primorial_ui(ref __mpz_t rop, ulong n);
        public static __mpz_primorial_ui mpz_primorial_ui { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_primorial_ui>(GetMpirPointer(nameof(mpz_primorial_ui)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_bin_ui(ref __mpz_t rop, ref __mpz_t n, uint k);
        public static __mpz_bin_ui mpz_bin_ui { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_bin_ui>(GetMpirPointer(nameof(mpz_bin_ui)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_bin_uiui(ref __mpz_t rop, uint n, uint k);
        public static __mpz_bin_uiui mpz_bin_uiui { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_bin_uiui>(GetMpirPointer(nameof(mpz_bin_uiui)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_fib_ui(ref __mpz_t fn, uint n);
        public static __mpz_fib_ui mpz_fib_ui { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_fib_ui>(GetMpirPointer(nameof(mpz_fib_ui)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_fib2_ui(ref __mpz_t fn, ref __mpz_t fnsub1, uint n);
        public static __mpz_fib2_ui mpz_fib2_ui { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_fib2_ui>(GetMpirPointer(nameof(mpz_fib2_ui)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_lucnum_ui(ref __mpz_t ln, uint n);
        public static __mpz_lucnum_ui mpz_lucnum_ui { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_lucnum_ui>(GetMpirPointer(nameof(mpz_lucnum_ui)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_lucnum2_ui(ref __mpz_t ln, ref __mpz_t lnsub1, uint n);
        public static __mpz_lucnum2_ui mpz_lucnum2_ui { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_lucnum2_ui>(GetMpirPointer(nameof(mpz_lucnum2_ui)));
        #endregion

        #region Logical and Bit Manipulation Functions
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_and(ref __mpz_t rop, ref __mpz_t op1, ref __mpz_t op2);
        public static __mpz_and mpz_and { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_and>(GetMpirPointer(nameof(mpz_and)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_ior(ref __mpz_t rop, ref __mpz_t op1, ref __mpz_t op2);
        public static __mpz_ior mpz_ior { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_ior>(GetMpirPointer(nameof(mpz_ior)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_xor(ref __mpz_t rop, ref __mpz_t op1, ref __mpz_t op2);
        public static __mpz_xor mpz_xor { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_xor>(GetMpirPointer(nameof(mpz_xor)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_com(ref __mpz_t rop, ref __mpz_t op);
        public static __mpz_com mpz_com { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_com>(GetMpirPointer(nameof(mpz_com)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate ulong __mpz_popcount(ref __mpz_t op);
        public static __mpz_popcount mpz_popcount { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_popcount>(GetMpirPointer(nameof(mpz_popcount)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate ulong __mpz_hamdist(ref __mpz_t op1, ref __mpz_t op2);
        public static __mpz_hamdist mpz_hamdist { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_hamdist>(GetMpirPointer(nameof(mpz_hamdist)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate ulong __mpz_scan0(ref __mpz_t op, ulong starting_bit);
        public static __mpz_scan0 mpz_scan0 { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_scan0>(GetMpirPointer(nameof(mpz_scan0)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate ulong __mpz_scan1(ref __mpz_t op, ulong starting_bit);
        public static __mpz_scan1 mpz_scan1 { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_scan1>(GetMpirPointer(nameof(mpz_scan1)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_setbit(ref __mpz_t rop, ulong bit_index);
        public static __mpz_setbit mpz_setbit { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_setbit>(GetMpirPointer(nameof(mpz_setbit)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_clrbit(ref __mpz_t rop, ulong bit_index);
        public static __mpz_clrbit mpz_clrbit { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_clrbit>(GetMpirPointer(nameof(mpz_clrbit)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_combit(ref __mpz_t rop, ulong bit_index);
        public static __mpz_combit mpz_combit { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_combit>(GetMpirPointer(nameof(mpz_combit)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int __mpz_tstbit(ref __mpz_t op, ulong bit_index);
        public static __mpz_tstbit mpz_tstbit { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_tstbit>(GetMpirPointer(nameof(mpz_tstbit)));
        #endregion

        #region Random Number Functions
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int __mpz_urandomb(ref __mpz_t rop, ref __gmp_randstate_t state, ulong n);
        public static __mpz_urandomb mpz_urandomb { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_urandomb>(GetMpirPointer(nameof(mpz_urandomb)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int __mpz_urandomm(ref __mpz_t rop, ref __gmp_randstate_t state, ref __mpz_t n);
        public static __mpz_urandomm mpz_urandomm { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_urandomm>(GetMpirPointer(nameof(mpz_urandomm)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int __mpz_rrandomb(ref __mpz_t rop, ref __gmp_randstate_t state, ulong n);
        public static __mpz_rrandomb mpz_rrandomb { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_rrandomb>(GetMpirPointer(nameof(mpz_rrandomb)));
        #endregion

        #region Integer Import and Export
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_import(ref __mpz_t rop, long count, int order, long size, int endian, long nails, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] op);
        public static __mpz_import mpz_import { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_import>(GetMpirPointer(nameof(mpz_import)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_export([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 3)] byte[] rop, out long countp, int order, long size, int endian, long nails, ref __mpz_t op);
        public static __mpz_export mpz_export { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_export>(GetMpirPointer(nameof(mpz_export)));
        #endregion

        #region Miscellaneous Functions
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int __mpz_fits_ulong_p(ref __mpz_t op);
        public static __mpz_fits_ulong_p mpz_fits_ulong_p { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_fits_ulong_p>(GetMpirPointer(nameof(mpz_fits_ulong_p)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int __mpz_fits_slong_p(ref __mpz_t op);
        public static __mpz_fits_slong_p mpz_fits_slong_p { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_fits_slong_p>(GetMpirPointer(nameof(mpz_fits_slong_p)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int __mpz_fits_uint_p(ref __mpz_t op);
        public static __mpz_fits_uint_p mpz_fits_uint_p { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_fits_uint_p>(GetMpirPointer(nameof(mpz_fits_uint_p)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int __mpz_fits_sint_p(ref __mpz_t op);
        public static __mpz_fits_sint_p mpz_fits_sint_p { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_fits_sint_p>(GetMpirPointer(nameof(mpz_fits_sint_p)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int __mpz_fits_ushort_p(ref __mpz_t op);
        public static __mpz_fits_ushort_p mpz_fits_ushort_p { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_fits_ushort_p>(GetMpirPointer(nameof(mpz_fits_ushort_p)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int __mpz_fits_sshort_p(ref __mpz_t op);
        public static __mpz_fits_sshort_p mpz_fits_sshort_p { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_fits_sshort_p>(GetMpirPointer(nameof(mpz_fits_sshort_p)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate long __mpz_sizeinbase(ref __mpz_t op, uint resultbase);
        public static __mpz_sizeinbase mpz_sizeinbase { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_sizeinbase>(GetMpirPointer(nameof(mpz_sizeinbase)));

        public static int mpz_sgn(ref __mpz_t op)
        {
            return (op.LimbCount < 0) ? -1 : ((op.LimbCount > 0) ? 1 : 0);
        }
        #endregion
    }
#pragma warning restore SA1601 // Partial elements should be documented
}
