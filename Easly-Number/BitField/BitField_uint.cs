namespace EaslyNumber
{
    using System;

    internal class BitField_uint
    {
        public BitField_uint()
        {
            Content = null;
            SignificantBits = 0;
        }

        public ulong SignificantBits { get; set; }

        public void SetZero()
        {
            SetFromDigit(0);
        }

        public void SetFromDigit(int digitValue)
        {
            Content = new uint[sizeof(long) / sizeof(uint)];
            Content[0] = (uint)digitValue;
        }

        public void MultiplyBy10AndAdd(int addValue)
        {
            long Carry = 0;
            long LastElementIndex = (long)(SignificantBits / sizeof(uint));

            for (long i = 0; i + 1 < LastElementIndex; i++)
            {
                long ElementValue = Content[i];
                ElementValue *= 10;
                ElementValue += Carry;
                Content[i] = (uint)ElementValue;

                Carry = ElementValue >> (sizeof(uint) * 8);
            }

            if (Carry != 0 && LastElementIndex == Content.LongLength)
            {
                Array.Resize(ref Content, Content.Length + 1);
                Content[LastElementIndex] = (uint)Carry;
            }
        }

        public void ShiftLeftAndAdd(int shiftValue, int addValue)
        {
            long Carry = 0;
            long LastElementIndex = (long)(SignificantBits / sizeof(uint));

            for (long i = 0; i + 1 < LastElementIndex; i++)
            {
                long ElementValue = Content[i];
                ElementValue <<= shiftValue;
                ElementValue += Carry;
                Content[i] = (uint)ElementValue;

                Carry = ElementValue >> (sizeof(uint) * 8);
            }

            if (Carry != 0 && LastElementIndex == Content.LongLength)
            {
                Array.Resize(ref Content, Content.Length + 1);
                Content[LastElementIndex] = (uint)Carry;
            }
        }

        public void SetBit(ulong index, bool value)
        {
            long LastElementIndex = (long)(SignificantBits / sizeof(uint));
            long ElementIndex = (long)(index / sizeof(uint));

            if (index > SignificantBits)
            {
                if (ElementIndex > LastElementIndex)
                {
                    Array.Resize(ref Content, Content.Length + 1);
                }

                SignificantBits = index;
            }

            if (value)
            {
                int ElementBitIndex = (int)(index % sizeof(uint));
                uint Mask = (uint)(1UL << ElementBitIndex);
                Content[ElementBitIndex] |= Mask;
            }
        }

        private uint[] Content;
    }
}
