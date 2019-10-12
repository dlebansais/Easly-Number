namespace EaslyNumber
{
    using System;

    internal class BitField_uint
    {
        public BitField_uint()
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
            Content = new uint[sizeof(long) / sizeof(uint)];
            Content[0] = (uint)digitValue;

            SignificantBits = sizeof(sbyte);
            FractionalBits = 0;
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

                SignificantBits += 4;
            }
        }

        private uint[] Content;
    }
}
