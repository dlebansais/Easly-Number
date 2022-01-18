namespace Interop.Mpfr;

using System;
using System.Runtime.InteropServices;

#pragma warning disable SA1600 // Elements should be documented
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
}
#pragma warning restore SA1600 // Elements should be documented
#pragma warning restore SA1601 // Partial elements should be documented
