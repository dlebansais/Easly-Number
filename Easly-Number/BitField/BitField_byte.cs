[assembly: System.Runtime.CompilerServices.InternalsVisibleToAttribute("Test-Easly-Number")]

namespace EaslyNumber
{
    using System;
    using System.Diagnostics;

    internal class BitField_byte
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="BitField_byte"/> class.
        /// </summary>
        public BitField_byte()
        {
            Content = new byte[0];
            SignificantBits = 0;
            ShiftBits = 0;

            Debug.Assert(LastItemIndex < 0);
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
            Content = new byte[1];
            Content[0] = 0;
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
            int CarryShift = (sizeof(byte) * 8) - shiftValue;

            Debug.Assert(LastIndex >= 0);
            Debug.Assert(LastIndex < Content.Length);

            for (long i = LastIndex + 1; i > 0; i--)
            {
                long ElementValue = Content[i - 1];
                long NextCarry = (byte)(ElementValue << CarryShift);

                ElementValue >>= shiftValue;
                ElementValue += Carry;
                Content[i - 1] = (byte)ElementValue;

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

            byte Mask = (byte)(1UL << Offset);
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
                byte Mask = (byte)(1UL << Offset);
                Content[Index] |= Mask;
            }
        }

        public BitField_byte Clone()
        {
            BitField_byte Result = Create();

            byte[] ContentClone = new byte[Content.Length];
            Array.Copy(Content, ContentClone, Content.Length);

            Result.Content = ContentClone;
            Result.SignificantBits = SignificantBits;
            Result.ShiftBits = ShiftBits;

            return Result;
        }
        #endregion

        #region Comparison Operations
        /// <summary>
        /// Gets a hash code for the object.
        /// </summary>
        public static int GetHashCode(BitField_byte item)
        {
            if (item is BitField_byte AsBitField)
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
        public static bool Equals(BitField_byte x, BitField_byte y)
        {
            if (!(x is BitField_byte) && !(y is BitField_byte))
                return true;
            else if (x is BitField_byte AsBitField1)
            {
                if (y is BitField_byte AsBitField2)
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
        public override bool Equals(object other)
        {
            if (other is BitField_byte AsBitField)
                return this == AsBitField;
            else
                return false;
        }

        /// <summary>
        /// Checks if <paramref name="x"/> is equal to <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first bit field.</param>
        /// <param name="y">The second bit field.</param>
        public static bool operator ==(BitField_byte x, BitField_byte y)
        {
            if (!(x is BitField_byte) && !(y is BitField_byte))
                return true;
            else if (x is BitField_byte AsBitFieldX)
            {
                if (y is BitField_byte AsBitFieldY)
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
        public static bool operator !=(BitField_byte x, BitField_byte y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Checks if <paramref name="x"/> is lesser than <paramref name="y"/>.
        /// </summary>
        /// <param name="x">The first bit field.</param>
        /// <param name="y">The second bit field.</param>
        public static bool operator <(BitField_byte x, BitField_byte y)
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
        private static bool IsContentLesser(BitField_byte x, BitField_byte y, long position)
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
        public static bool operator >(BitField_byte x, BitField_byte y)
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

                            return (i * sizeof(byte) * 8) + j - 1 + ShiftBits;
                        }
                    }
                }

                return -1;
            }
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Creates a new BitField_byte object.
        /// </summary>
        protected virtual BitField_byte Create()
        {
            return new BitField_byte();
        }

        /// <summary>
        /// Index of the last item in <see cref="Content"/>.
        /// Returns -1 if no items.
        /// </summary>
        private long LastItemIndex
        {
            get { return ItemIndex(SignificantBits + (sizeof(byte) * 8) - 1) - 1; }
        }

        /// <summary>
        /// Index of an item specified by its bit position.
        /// </summary>
        /// <param name="position">The bit position.</param>
        /// <returns>The corresponding item index in <see cref="Content"/>.</returns>
        private long ItemIndex(long position)
        {
            return position / (sizeof(byte) * 8);
        }

        /// <summary>
        /// Offset of a bit in the item specified by its position.
        /// </summary>
        /// <param name="position">The bit position.</param>
        /// <returns>The corresponding offset in the <see cref="Content"/> item.</returns>
        private int ItemOffset(long position)
        {
            return (int)(position % (sizeof(byte) * 8));
        }

        /// <summary>
        /// The bit field data.
        /// </summary>
        private byte[] Content;
        #endregion
    }
}
