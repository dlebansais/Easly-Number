﻿[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Test-Easly-Number")]

namespace EaslyNumber
{
    using System;
    using System.Diagnostics;
    using System.Text;

    internal class BitField_uint
    {
        #region Constant
        /// <summary>
        /// Neutral value of the <see cref="BitField_uint"/> class.
        /// </summary>
        public static BitField_uint Empty { get; } = new BitField_uint();
        #endregion

        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="BitField_uint"/> class.
        /// </summary>
        public BitField_uint()
        {
            Content = Array.Empty<uint>();
            SignificantBits = 0;
            ShiftBits = 0;

            Debug.Assert(LastItemIndex < 0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitField_uint"/> class.
        /// </summary>
        public BitField_uint(ulong value)
        {
            Content = BitField.LongToArray_uint(value);
            SignificantBits = Content.Length * sizeof(uint) * 8;
            ShiftBits = 0;

            Debug.Assert(LastItemIndex >= 0);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitField_uint"/> class.
        /// </summary>
        /// <param name="source">The source bits.</param>
        /// <param name="offset">The offset in <paramref name="source"/>.</param>
        /// <param name="length">The number of bits in <paramref name="source"/>.</param>
        protected BitField_uint(uint[] source, int offset, int length)
        {
            Content = new uint[(length + (sizeof(uint) * 8) - 1) / (sizeof(uint) * 8)];

            InitFillContent(source, offset);
            InitCleanup(length);

            ShiftBits = 0;

            Debug.Assert(LastItemIndex >= 0);
        }

        private void InitFillContent(uint[] source, int offset)
        {
            int ElementOffset = offset / (sizeof(uint) * 8);
            int BitOffset = offset - (ElementOffset * sizeof(uint) * 8);

            long Carry = 0;
            long CarryMask = (1L << BitOffset) - 1;

            for (int i = 0; i < Content.Length; i++)
            {
                long ElementValue = source[ElementOffset + i];
                long NextElementValue = ElementValue & CarryMask;

                ElementValue >>= BitOffset;
                ElementValue |= Carry;

                Content[i] = (uint)ElementValue;

                Carry = (uint)(NextElementValue << BitOffset);
            }
        }

        private void InitCleanup(int length)
        {
            int FinalLength = length;
            while (Content.Length > 1 && Content[Content.Length - 1] == 0)
            {
                Array.Resize(ref Content, Content.Length - 1);
                FinalLength = Content.Length * sizeof(uint) * 8;
            }

            uint LastItem = Content[Content.Length - 1];

            if (LastItem == 0)
            {
                Debug.Assert(Content.Length == 1);
                SignificantBits = 1;
            }
            else
            {
                Debug.Assert(FinalLength >= sizeof(uint) * 8);

                int Offset = ItemOffset(FinalLength - 1);
                uint Mask = (uint)(1UL << Offset);

                while (FinalLength > 1 && (LastItem & Mask) == 0)
                {
                    FinalLength--;
                    Mask >>= 1;
                }

                SignificantBits = FinalLength;
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Number of significant bits in the field.
        /// </summary>
        public long SignificantBits { get; protected set; }

        /// <summary>
        /// Number of unstored bits to the right of significant bits.
        /// </summary>
        public long ShiftBits { get; protected set; }

        /// <summary>
        /// True if the bit field represents value zero.
        /// </summary>
        public bool IsZero
        {
            get { return SignificantBits == 1 && Content[0] == 0; }
        }
        #endregion

        #region Client Interface
        /// <summary>
        /// Sets the object to represent zero.
        /// </summary>
        public void SetZero()
        {
            Content = new uint[1];
            Content[0] = 0;
            SignificantBits = 1;
            ShiftBits = 0;

            Debug.Assert(LastItemIndex == 0);
        }

        /// <summary>
        /// Sets the object to represent 1.
        /// </summary>
        public void SetOne()
        {
            Content = new uint[1];
            Content[0] = 1;
            SignificantBits = 1;
            ShiftBits = 0;

            Debug.Assert(LastItemIndex == 0);
        }

        /// <summary>
        /// Decreases the precision of the bit field by shifting bits to the right.
        /// </summary>
        public void DecreasePrecision()
        {
            int shiftValue = 1;

            Debug.Assert(SignificantBits >= shiftValue);

            long Carry = 0;
            long LastIndex = LastItemIndex;
            int CarryShift = (sizeof(uint) * 8) - shiftValue;

            Debug.Assert(LastIndex >= 0);
            Debug.Assert(LastIndex < Content.Length);

            for (long i = LastIndex + 1; i > 0; i--)
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

            // The value of LastItemIndex is recalculated with the updated value of SignificantBits.
            if (LastIndex > LastItemIndex)
            {
                Debug.Assert(Content[LastIndex] == 0);

                Array.Resize(ref Content, Content.Length - 1);
            }
        }

        /// <summary>
        /// Gets the value of the bit at position <paramref name="position"/>.
        /// </summary>
        /// <param name="position">Position of the bit to get.</param>
        public bool GetBit(long position)
        {
            position -= ShiftBits;

            Debug.Assert(position >= -ShiftBits);
            Debug.Assert(position < SignificantBits);

            if (position < 0)
                return false;

            Debug.Assert(position >= 0);

            long Index = ItemIndex(position);
            int Offset = ItemOffset(position);

            uint Mask = (uint)(1UL << Offset);
            return (Content[Index] & Mask) != 0;
        }

        /// <summary>
        /// Sets the bit at position <paramref name="position"/>.
        /// </summary>
        /// <param name="position">Position of the bit to get.</param>
        /// <param name="value">The new value.</param>
        public void SetBit(long position, bool value)
        {
            position -= ShiftBits;

            Debug.Assert(position >= 0);
            Debug.Assert(position <= SignificantBits);

            long Index = ItemIndex(position);
            int Offset = ItemOffset(position);

            if (position >= SignificantBits)
            {
                if (Index >= Content.Length)
                {
                    Array.Resize(ref Content, (int)(Index + 1));
                }

                SignificantBits = position + 1;
            }

            if (value)
            {
                uint Mask = (uint)(1UL << Offset);
                Content[Index] |= Mask;
            }
        }

        /// <summary>
        /// Gets a clone of the object.
        /// </summary>
        public BitField_uint Clone()
        {
            BitField_uint Result = Create();

            uint[] ContentClone = new uint[Content.Length];
            Array.Copy(Content, ContentClone, Content.Length);

            Result.Content = ContentClone;
            Result.SignificantBits = SignificantBits;
            Result.ShiftBits = ShiftBits;

            return Result;
        }

        /// <summary>
        /// Gets the bit field content as a 64-bits integer if possible.
        /// </summary>
        /// <param name="value">The 64-bits integer value upon return.</param>
        /// <returns>True if the value can be reprensented as a 64-bits integer; otherwise, false.</returns>
        public bool ToUInt64(out ulong value)
        {
            value = 0;

            if (ShiftBits + SignificantBits >= 64)
                return false;

            byte[] Result = new byte[8];
            Debug.Assert(Content.Length <= 8 / sizeof(uint));

            for (int i = 0; i < Content.Length; i++)
            {
                uint Item = Content[i];
                byte[] ItemBytes = BitConverter.GetBytes(Item);

                Array.Copy(ItemBytes, 0, Result, i * sizeof(uint), sizeof(uint));
            }

            value = BitConverter.ToUInt64(Result, 0);
            return true;
        }
        #endregion

        #region Comparison Operations
        /// <summary>
        /// Gets a hash code for the object.
        /// </summary>
        public static int GetHashCode(BitField_uint item)
        {
            if (item is BitField_uint AsBitField)
                return AsBitField.GetHashCode();
            else
                return 0;
        }

        /// <summary>
        /// Gets a hash code for the current object.
        /// </summary>
        public override int GetHashCode()
        {
            long Result = 0;
            long LastIndex = LastItemIndex;

            for (long i = 0; i < LastIndex; i++)
            {
                long ElementValue = Content[i];
                Result ^= ElementValue;
            }

            return (int)Result;
        }

        /// <summary>
        /// Checks if two bit fields are equal.
        /// </summary>
        /// <param name="x">The first bit field.</param>
        /// <param name="y">The second bit field.</param>
        public static bool Equals(BitField_uint x, BitField_uint y)
        {
            if (!(x is BitField_uint) && !(y is BitField_uint))
                return true;
            else if (x is BitField_uint AsBitField1)
            {
                if (y is BitField_uint AsBitField2)
                    return AsBitField1.Equals(AsBitField2);
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Checks if two bit fields are equal.
        /// </summary>
        /// <param name="other">The other instance.</param>
        public override bool Equals(object? other)
        {
            if (other is BitField_uint AsBitField)
                return this == AsBitField;
            else
                return false;
        }

        /// <summary>
        /// Checks if <paramref name="x"/> is equal to <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first bit field.</param>
        /// <param name="y">The second bit field.</param>
        public static bool operator ==(BitField_uint x, BitField_uint y)
        {
            if (!(x is BitField_uint) && !(y is BitField_uint))
                return true;
            else if (x is BitField_uint AsBitFieldX)
            {
                if (y is BitField_uint AsBitFieldY)
                {
                    if (x.SignificantBits != y.SignificantBits || x.ShiftBits != y.ShiftBits)
                        return false;
                    if (x.SignificantBits == 0)
                        return true;

                    Debug.Assert(x.SignificantBits > 0);

                    long LastIndex = x.LastItemIndex;

                    Debug.Assert(LastIndex >= 0);
                    Debug.Assert(LastIndex < x.Content.Length);

                    for (long i = 0; i <= LastIndex; i++)
                        if (x.Content[i] != y.Content[i])
                            return false;

                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Checks if <paramref name="x"/> is different than <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first bit field.</param>
        /// <param name="y">The second bit field.</param>
        public static bool operator !=(BitField_uint x, BitField_uint y)
        {
            return !(x == y);
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

                long Position = PositionX;

                Debug.Assert(Position == PositionY);

                return IsContentLesser(x, y, Position);
            }
            else if (PositionX < 0 && PositionY >= 0)
                return true;
            else if (PositionX >= 0 && PositionY < 0)
                return false;
            else
                return false;
        }

        /// <summary>
        /// Checks if the content of <paramref name="x"/> is lesser than the content of <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first bit field.</param>
        /// <param name="y">The second bit field.</param>
        /// <param name="position">The bit position where to start comparing.</param>
        private static bool IsContentLesser(BitField_uint x, BitField_uint y, long position)
        {
            Debug.Assert(position >= x.ShiftBits);
            Debug.Assert(position < x.ShiftBits + x.SignificantBits);
            Debug.Assert(position >= y.ShiftBits);
            Debug.Assert(position < y.ShiftBits + y.SignificantBits);
            Debug.Assert(x.GetBit(position));
            Debug.Assert(y.GetBit(position));

            long LowestPosition = x.ShiftBits <= y.ShiftBits ? x.ShiftBits : y.ShiftBits;

            bool BitX, BitY;
            do
            {
                position--;
                BitX = x.GetBit(position);
                BitY = y.GetBit(position);
            }
            while (BitX == BitY && position > LowestPosition);

            if (!BitX && BitY)
                return true;
            else if (BitX && !BitY)
                return false;
            else
            {
                Debug.Assert(position <= LowestPosition);
                return false;
            }
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
                if (SignificantBits > 0)
                {
                    long LastIndex = LastItemIndex;

                    Debug.Assert(LastIndex >= 0);
                    Debug.Assert(LastIndex < Content.Length);

                    for (long i = LastIndex; i >= 0; i--)
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

                            return (i * sizeof(uint) * 8) + j - 1 + ShiftBits;
                        }
                    }
                }

                return -1;
            }
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Creates a new BitField_uint object.
        /// </summary>
        protected virtual BitField_uint Create()
        {
            return new BitField_uint();
        }

        /// <summary>
        /// Index of the last item in <see cref="Content"/>.
        /// Returns -1 if no items.
        /// </summary>
        private long LastItemIndex
        {
            get { return ItemIndex(SignificantBits + (sizeof(uint) * 8) - 1) - 1; }
        }

        /// <summary>
        /// Index of an item specified by its bit position.
        /// </summary>
        /// <param name="position">The bit position.</param>
        /// <returns>The corresponding item index in <see cref="Content"/>.</returns>
        private static long ItemIndex(long position)
        {
            return position / (sizeof(uint) * 8);
        }

        /// <summary>
        /// Offset of a bit in the item specified by its position.
        /// </summary>
        /// <param name="position">The bit position.</param>
        /// <returns>The corresponding offset in the <see cref="Content"/> item.</returns>
        private static int ItemOffset(long position)
        {
            return (int)(position % (sizeof(uint) * 8));
        }

        /// <summary>
        /// The bit field data.
        /// </summary>
        private uint[] Content;
        #endregion

        #region Debugging
        public override string ToString()
        {
            if (IsZero)
                return "0";

            int ResultLength = (int)SignificantBits;
            StringBuilder ResultBuilder = new StringBuilder(ResultLength);
            ResultBuilder.Length = ResultLength;

            int Offset = 0;
            uint b = 0;

            for (int i = 0; i < ResultLength; i++)
            {
                if (i % (sizeof(uint) * 8) == 0)
                    b = Content[Offset++];

                ResultBuilder[ResultLength - i - 1] = (b & 1) == 0 ? '0' : '1';
                b >>= 1;
            }

            string Result = ResultBuilder.ToString();

            if (ShiftBits != 0)
                Result += $" << {ShiftBits}";

            return Result;
        }
        #endregion
    }
}
