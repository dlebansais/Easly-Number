namespace EaslyNumber
{
    using System;

    internal class BitField_byte
    {
        public BitField_byte()
        {
            Content = new byte[0];
            SignificantBits = 0;
        }

        public long SignificantBits { get; set; }

        public void SetZero()
        {
            SetFromDigit(0);
        }

        public void SetFromDigit(int digitValue)
        {
            Content = new byte[sizeof(long) / sizeof(byte)];
            Content[0] = (byte)digitValue;
        }

        public void MultiplyBy10AndAdd(int addValue)
        {
            long Carry = 0;
            long LastElementIndex = SignificantBits / sizeof(byte);

            for (long i = 0; i + 1 < LastElementIndex; i++)
            {
                long ElementValue = Content[i];
                ElementValue *= 10;
                ElementValue += Carry;
                Content[i] = (byte)ElementValue;

                Carry = ElementValue >> (sizeof(byte) * 8);
            }

            if (Carry != 0 && LastElementIndex == Content.LongLength)
            {
                Array.Resize(ref Content, Content.Length + 1);
                Content[LastElementIndex] = (byte)Carry;
            }
        }

        public void ShiftLeftAndAdd(int shiftValue, int addValue)
        {
            long Carry = 0;
            long LastElementIndex = SignificantBits / sizeof(byte);

            for (long i = 0; i + 1 < LastElementIndex; i++)
            {
                long ElementValue = Content[i];
                ElementValue <<= shiftValue;
                ElementValue += Carry;
                Content[i] = (byte)ElementValue;

                Carry = ElementValue >> (sizeof(byte) * 8);
            }

            if (Carry != 0 && LastElementIndex == Content.LongLength)
            {
                Array.Resize(ref Content, Content.Length + 1);
                Content[LastElementIndex] = (byte)Carry;
            }
        }

        public void ShiftRight(int shiftValue)
        {
        }

        public bool GetBit(long index)
        {
            const int Domain = sizeof(byte) * 8;
            long ElementIndex = index / Domain;
            int ElementBitIndex = (int)(index % Domain);

            byte Mask = (byte)(1UL << ElementBitIndex);
            return (Content[ElementIndex] & Mask) != 0;
        }

        public void SetBit(long index, bool value)
        {
            const int Domain = sizeof(byte) * 8;
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
                byte Mask = (byte)(1UL << ElementBitIndex);
                Content[ElementIndex] |= Mask;
            }
        }

        private byte[] Content;
    }
}
