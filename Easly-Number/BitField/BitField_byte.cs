namespace EaslyNumber
{
    using System;

    internal class BitField_byte
    {
        public BitField_byte()
        {
            Content = null;
            SignificantBits = 0;
            FractionalBits = 0;
        }

        public ulong SignificantBits { get; private set; }
        public ulong FractionalBits { get; private set; }

        public void SetZero()
        {
            SetFromDigit(0);
        }

        public void SetFromDigit(int digitValue)
        {
            Content = new byte[sizeof(long) / sizeof(byte)];
            Content[0] = (byte)digitValue;

            SignificantBits = sizeof(sbyte);
            FractionalBits = 0;
        }

        public void MultiplyBy10AndAdd(int addValue)
        {
            long Carry = 0;
            long LastElementIndex = (long)(SignificantBits / sizeof(byte));

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

                SignificantBits += 4;
            }
        }

        public void ShiftLeftAndAdd(int shiftValue, int addValue)
        {
            long Carry = 0;
            long LastElementIndex = (long)(SignificantBits / sizeof(byte));

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

                SignificantBits += 4;
            }
        }

        private byte[] Content;
    }
}
