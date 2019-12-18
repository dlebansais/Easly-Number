namespace EaslyNumber
{
    using System;

    internal class BitField : BitField_byte
    {
        #region Init
        /// <summary>
        /// Initializes a new instance of the <see cref="BitField"/> class.
        /// </summary>
        public BitField()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitField"/> class.
        /// </summary>
        public BitField(ulong value)
            : base(value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BitField"/> class.
        /// </summary>
        /// <param name="source">The source bits.</param>
        /// <param name="offset">The offset in <paramref name="source"/>.</param>
        /// <param name="length">The number of bits in <paramref name="source"/>.</param>
        protected BitField(byte[] source, int offset, int length)
            : base(source, offset, length)
        {
        }

        /// <summary>
        /// Creates a bit field representing the fraction in <paramref name="d"/>.
        /// </summary>
        /// <param name="d">The number.</param>
        public static BitField CreateFractionBitField(double d)
        {
            byte[] SourceBits = BitConverter.GetBytes(d);

            byte[] Source = new byte[SourceBits.Length / sizeof(byte)];
            for (int i = 0; i < Source.Length; i++)
            {
                long Value = 0;
                for (int j = 0; j < sizeof(byte); j++)
                {
                    Value <<= 8;
                    Value += SourceBits[(i * sizeof(byte)) + j];
                }

                Source[i] = (byte)Value;
            }

            return new BitField(Source, 0, 52);
        }

        /// <summary>
        /// Creates a bit field representing the exponent in <paramref name="d"/>.
        /// </summary>
        /// <param name="d">The number.</param>
        public static BitField CreateExponentBitField(double d)
        {
            byte[] SourceBits = BitConverter.GetBytes(d);
            int Exponent = (SourceBits[6] >> 4) + ((SourceBits[7] & 0x7F) * 16);
            Exponent -= 1023;

            if (Exponent < 0)
                Exponent = -Exponent;

            byte[] Source = BitConverter.GetBytes((ushort)Exponent);
            return new BitField(Source, 0, 11);
        }
        #endregion

        #region Overrides
        /// <summary>
        /// Creates a new BitField object.
        /// </summary>
        protected override BitField_byte Create()
        {
            return new BitField();
        }
        #endregion

        #region Tools
        /// <summary>
        /// Converts a ulong value to an array of bytes.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The converted array.</returns>
        public static byte[] LongToArray_byte(ulong value)
        {
            return BitConverter.GetBytes(value);
        }

        /// <summary>
        /// Converts a ulong value to an array of uint.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The converted array.</returns>
        public static uint[] LongToArray_uint(ulong value)
        {
            byte[] Bytes = BitConverter.GetBytes(value);

            uint[] Result = new uint[Bytes.Length / sizeof(uint)];
            for (int i = 0; i < Result.Length; i++)
                Result[i] = BitConverter.ToUInt32(Bytes, sizeof(uint) * 8 * i);

            return Result;
        }
        #endregion
    }
}
