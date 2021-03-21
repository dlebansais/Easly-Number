namespace EaslyNumber
{
    using System;
    using System.Runtime.InteropServices;

#pragma warning disable SA1601 // Partial elements should be documented
    internal static partial class NativeMethods
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpfr_init2(ref __mpfr_t x, ulong prec);
        public static __mpfr_init2 mpfr_init2 { get; } = Marshal.GetDelegateForFunctionPointer<__mpfr_init2>(GetMpfrPointer(nameof(mpfr_init2)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpfr_clear(ref __mpfr_t x);
        public static __mpfr_clear mpfr_clear { get; } = Marshal.GetDelegateForFunctionPointer<__mpfr_clear>(GetMpfrPointer(nameof(mpfr_clear)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpfr_set_default_prec(ulong prec);
        public static __mpfr_set_default_prec mpfr_set_default_prec { get; } = Marshal.GetDelegateForFunctionPointer<__mpfr_set_default_prec>(GetMpfrPointer(nameof(mpfr_set_default_prec)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpfr_set_prec(ref __mpfr_t x, ulong prec);
        public static __mpfr_set_prec mpfr_set_prec { get; } = Marshal.GetDelegateForFunctionPointer<__mpfr_set_prec>(GetMpfrPointer(nameof(mpfr_set_prec)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate ulong __mpfr_get_prec(ref __mpfr_t x);
        public static __mpfr_get_prec mpfr_get_prec { get; } = Marshal.GetDelegateForFunctionPointer<__mpfr_get_prec>(GetMpfrPointer(nameof(mpfr_get_prec)));
    }
#pragma warning restore SA1601 // Partial elements should be documented
}
