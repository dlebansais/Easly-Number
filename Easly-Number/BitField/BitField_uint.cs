namespace EaslyNumber
{
    using System;

    internal class BitField_uint
    {
        public BitField_uint()
        {
            Content = new uint[0];
            SignificantBits = 0;
        }

        public long SignificantBits { get; set; }

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
            long LastElementIndex = SignificantBits / sizeof(uint);

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
            long LastElementIndex = SignificantBits / sizeof(uint);

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

        public void ShiftRight(int shiftValue)
        {
        }

        public bool GetBit(long index)
        {
            const int Domain = sizeof(uint) * 8;
            long ElementIndex = index / Domain;
            int ElementBitIndex = (int)(index % Domain);

            uint Mask = (uint)(1UL << ElementBitIndex);
            return (Content[ElementIndex] & Mask) != 0;
        }

        public void SetBit(long index, bool value)
        {
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

        private uint[] Content;
    }
}
