namespace EaslyNumber2
{
    using System;
    using System.Runtime.InteropServices;

#pragma warning disable SA1601 // Partial elements should be documented
    internal static partial class NativeMethods
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct __mpfr_t
        {
            public long Precision;
            public int Sign;
            public long Exponent;
            public IntPtr Limbs;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct __mpz_t
        {
            public int AllocCount;
            public int LimbCount;
            public IntPtr Limbs;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct __mpq_t
        {
            public __mpz_t Numerator;
            public __mpz_t Denominator;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct __mpf_t
        {
            public int Precision;
            public int Size;
            public long Exponent;
            public IntPtr Limbs;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct __gmp_randstate_t
        {
            public __mpz_t seed;
            public int Algorithm;
            public IntPtr AlgorithmData;
        }
    }
#pragma warning restore SA1601 // Partial elements should be documented
}
