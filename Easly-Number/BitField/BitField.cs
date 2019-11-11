namespace EaslyNumber
{
    internal class BitField : BitField_byte
    {
        /// <summary>
        /// Creates a new BitField object.
        /// </summary>
        protected override BitField_byte Create()
        {
            return new BitField();
        }
    }
}
