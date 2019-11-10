namespace EaslyNumber
{
    using System.Collections.Generic;

    /// <summary>
    /// The partition of a string into different components of a number.
    /// </summary>
    internal class TextPartitionCollection : List<TextPartition>
    {
        /// <summary>
        /// True if all partitions in the collection are valid.
        /// </summary>
        public bool IsValid { get { return TrueForAll((TextPartition item) => item.IsValid); } }

        /// <summary>
        /// True if all partitions in the collection are invalid.
        /// </summary>
        public bool IsInvalid { get { return TrueForAll((TextPartition item) => !item.IsValid); } }

        /// <summary>
        /// Parses a new character in all partitions of the list.
        /// </summary>
        /// <param name="index">The position of the character to parse.</param>
        public void Parse(int index)
        {
            ForEach((TextPartition item) => item.Parse(index));
        }

        /// <summary>
        /// Gets the preferred partition in the list.
        /// </summary>
        public TextPartition PreferredPartition
        {
            get
            {
                TextPartition Result = null;

                foreach (TextPartition item in this)
                {
                    bool ChooseCandidate = ((Result == null) && item.IsPartiallyValid) || ((Result != null) && (Result.ComparisonIndex < item.ComparisonIndex));

                    Result = ChooseCandidate ? item : Result;
                }

                return Result;
            }
        }
    }
}
