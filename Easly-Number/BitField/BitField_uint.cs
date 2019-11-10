[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute("Test-Easly-Number")]

namespace EaslyNumber
{
    using System;
    using System.Diagnostics;

    internal class BitField_uint
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="BitField_uint"/> class.
        /// </summary>
        public BitField_uint()
        {
            Content = new uint[0];
            SignificantBits = 0;
            ShiftBits = 0;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Number of significant bits in the field.
        /// </summary>
        public long SignificantBits { get; set; }

        /// <summary>
        /// Number of unstored bits to the right of significant bits.
        /// </summary>
        public long ShiftBits { get; set; }
        #endregion

        #region Client Interface
        /// <summary>
        /// Sets the object to represent zero.
        /// </summary>
        public void SetZero()
        {
            Content = new uint[sizeof(long) / sizeof(uint)];
            Content[0] = 0;
            SignificantBits = 1;
            ShiftBits = 0;
        }

        /// <summary>
        /// Shift bits to the right.
        /// </summary>
        public void ShiftRight()
        {
            int shiftValue = 1;

            long Carry = 0;
            long LastElementIndex = SignificantBits / (sizeof(uint) * 8);
            int CarryShift = (sizeof(uint) * 8) - shiftValue;

            for (long i = LastElementIndex + 1; i > 0; i--)
            {
                long ElementValue = Content[i - 1];
                long NextCarry = (uint)(ElementValue << CarryShift);

                ElementValue >>= shiftValue;
                ElementValue += Carry;
                Content[i - 1] = (uint)ElementValue;

                Carry = NextCarry;
            }

            SignificantBits -= shiftValue;
            ShiftBits += shiftValue;

            if (LastElementIndex > SignificantBits / (sizeof(uint) * 8))
            {
                Debug.Assert(Content[LastElementIndex] == 0);

                Array.Resize(ref Content, Content.Length - 1);
            }
        }

        /// <summary>
        /// Gets the value of the bit at position <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Position of the bit to get.</param>
        public bool GetBit(long index)
        {
            Debug.Assert(index >= 0 && index < SignificantBits);

            const int Domain = sizeof(uint) * 8;
            long ElementIndex = index / Domain;
            int ElementBitIndex = (int)(index % Domain);

            uint Mask = (uint)(1UL << ElementBitIndex);
            return (Content[ElementIndex] & Mask) != 0;
        }

        /// <summary>
        /// Sets the bit at position <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Position of the bit to get.</param>
        /// <param name="value">The new value.</param>
        public void SetBit(long index, bool value)
        {
            Debug.Assert(index >= 0 && index <= SignificantBits);

            const int Domain = sizeof(uint) * 8;
            long ElementIndex = index / Domain;
            int ElementBitIndex = (int)(index % Domain);

            if (index >= SignificantBits)
            {
                if (ElementIndex >= Content.Length)
                {
                    Array.Resize(ref Content, (int)(ElementIndex + 1));
                }

                SignificantBits = index + 1;
            }

            if (value)
            {
                uint Mask = (uint)(1UL << ElementBitIndex);
                Content[ElementIndex] |= Mask;
            }
        }
        #endregion

        #region Implementation
        private uint[] Content;
        #endregion
    }
}
