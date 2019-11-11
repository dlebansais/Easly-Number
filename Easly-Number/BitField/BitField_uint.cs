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

        #region Comparison Operations
        /// <summary>
        /// Checks if two bit fields are equal.
        /// </summary>
        /// <param name="other">The other instance.</param>
        public bool IsEqual(BitField_uint other)
        {
            if (SignificantBits != other.SignificantBits || ShiftBits != other.ShiftBits)
                return false;

            long LastElementIndex = SignificantBits / (sizeof(uint) * 8);

            for (long i = 0; i < LastElementIndex; i++)
                if (Content[i] != other.Content[i])
                    return false;

            return true;
        }

        /// <summary>
        /// Checks if <paramref name="x"/> is lesser than <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first bit field.</param>
        /// <param name="y">The second bit field.</param>
        public static bool operator <(BitField_uint x, BitField_uint y)
        {
            long PositionX = x.HighestBitSet;
            long PositionY = y.HighestBitSet;

            if (PositionX >= 0 && PositionY >= 0)
            {
                if (PositionX != PositionY)
                    return PositionX < PositionY;

                //TODO compare content.
                return false;
            }
            else if (PositionX < 0)
                return true;
            else if (PositionY < 0)
                return false;
            else
                return false;
        }

        /// <summary>
        /// Checks if <paramref name="x"/> is greater than <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first bit field.</param>
        /// <param name="y">The second bit field.</param>
        public static bool operator >(BitField_uint x, BitField_uint y)
        {
            return y < x;
        }

        /// <summary>
        /// Return the position of the highest bit set, -1 if none.
        /// </summary>
        private long HighestBitSet
        {
            get
            {
                long LastElementIndex = SignificantBits / (sizeof(uint) * 8);

                for (long i = LastElementIndex; i > 0; i--)
                {
                    long ElementValue = Content[i];

                    if (ElementValue != 0)
                    {
                        int j = 0;
                        do
                        {
                            ElementValue >>= 1;
                            j++;
                        }
                        while (ElementValue != 0);

                        return (i * sizeof(uint) * 8) + j + ShiftBits;
                    }
                }

                return -1;
            }
        }
        #endregion

        #region Implementation
        private uint[] Content;
        #endregion
    }
}
