namespace Interop.Mpir
{
    using System.Runtime.InteropServices;

#pragma warning disable SA1601 // Partial elements should be documented
    internal static partial class NativeMethods
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_set(ref __mpz_t rop, ref __mpz_t op);
        public static __mpz_set mpz_set { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_set>(GetMpirPointer(nameof(mpz_set)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_set_ui(ref __mpz_t rop, uint op);
        public static __mpz_set_ui mpz_set_ui { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_set_ui>(GetMpirPointer(nameof(mpz_set_ui)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_set_si(ref __mpz_t rop, int op);
        public static __mpz_set_si mpz_set_si { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_set_si>(GetMpirPointer(nameof(mpz_set_si)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_set_ux(ref __mpz_t rop, ulong op);
        public static __mpz_set_ux mpz_set_ux { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_set_ux>(GetMpirPointer(nameof(mpz_set_ux)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_set_sx(ref __mpz_t rop, long op);
        public static __mpz_set_sx mpz_set_sx { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_set_sx>(GetMpirPointer(nameof(mpz_set_sx)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_set_d(ref __mpz_t rop, double op);
        public static __mpz_set_d mpz_set_d { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_set_d>(GetMpirPointer(nameof(mpz_set_d)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_set_q(ref __mpz_t rop, ref __mpq_t op);
        public static __mpz_set_q mpz_set_q { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_set_q>(GetMpirPointer(nameof(mpz_set_q)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_set_f(ref __mpz_t rop, ref __mpf_t op);
        public static __mpz_set_f mpz_set_f { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_set_f>(GetMpirPointer(nameof(mpz_set_f)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int __mpz_set_str(ref __mpz_t rop, string str, uint strbase);
        public static __mpz_set_str mpz_set_str { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_set_str>(GetMpirPointer(nameof(mpz_set_str)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_swap(ref __mpz_t rop1, ref __mpz_t rop2);
        public static __mpz_swap mpz_swap { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_swap>(GetMpirPointer(nameof(mpz_swap)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_init_set(ref __mpz_t rop, ref __mpz_t op);
        public static __mpz_init_set mpz_init_set { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_init_set>(GetMpirPointer(nameof(mpz_init_set)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_init_set_ui(ref __mpz_t rop, uint op);
        public static __mpz_init_set_ui mpz_init_set_ui { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_init_set_ui>(GetMpirPointer(nameof(mpz_init_set_ui)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_init_set_si(ref __mpz_t rop, int op);
        public static __mpz_init_set_si mpz_init_set_si { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_init_set_si>(GetMpirPointer(nameof(mpz_init_set_si)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_init_set_ux(ref __mpz_t rop, ulong op);
        public static __mpz_init_set_ux mpz_init_set_ux { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_init_set_ux>(GetMpirPointer(nameof(mpz_init_set_ux)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_init_set_sx(ref __mpz_t rop, long op);
        public static __mpz_init_set_sx mpz_init_set_sx { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_init_set_sx>(GetMpirPointer(nameof(mpz_init_set_sx)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void __mpz_init_set_d(ref __mpz_t rop, double op);
        public static __mpz_init_set_d mpz_init_set_d { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_init_set_d>(GetMpirPointer(nameof(mpz_init_set_d)));

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public delegate int __mpz_init_set_str(ref __mpz_t rop, string str, uint strbase);
        public static __mpz_init_set_str mpz_init_set_str { get; } = Marshal.GetDelegateForFunctionPointer<__mpz_init_set_str>(GetMpirPointer(nameof(mpz_init_set_str)));
    }
#pragma warning restore SA1601 // Partial elements should be documented
}
