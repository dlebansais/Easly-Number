namespace EaslyNumber
{
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    /// The partition of a string into different components of a number.
    /// </summary>
    internal abstract class TextPartition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextPartition"/> class.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        public TextPartition(string text)
        {
            Text = text;
        }

        /// <summary>
        /// The string to parse.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Index of the last optional space character, -1 if not parsed.
        /// </summary>
        public int LastLeadingSpaceIndex { get; set; } = -1;

        /// <summary>
        /// The beginning of <see cref="Text"/> that can be ignored.
        /// </summary>
        public abstract string DiscardedProlog { get; }

        /// <summary>
        /// Index of the first invalid character, -1 if not parsed.
        /// </summary>
        public int FirstInvalidCharacterIndex { get; set; } = -1;

        /// <summary>
        /// True if the partition represents a valid number.
        /// </summary>
        public bool IsValid { get { return Text.Length > (LastLeadingSpaceIndex + 1) && FirstInvalidCharacterIndex < 0; } }

        /// <summary>
        /// True if the partition represents a valid number, possibly followed by an invalid part.
        /// </summary>
        public bool IsPartiallyValid { get { return Text.Length > 0 && FirstInvalidCharacterIndex != 0; } }

        /// <summary>
        /// The remaining part of the string that is not parsed in the number.
        /// </summary>
        public string InvalidPart { get { return FirstInvalidCharacterIndex < 0 ? string.Empty : Text.Substring(FirstInvalidCharacterIndex); } }

        /// <summary>
        /// The end of <see cref="Text"/> that can be ignored.
        /// </summary>
#pragma warning disable CA1822 // Mark members as static
        public string DiscardedEpilog { get { return string.Empty; } }
#pragma warning restore CA1822 // Mark members as static

        /// <summary>
        /// The parser current state.
        /// </summary>
        protected ParsingState State { get; set; } = ParsingState.Init;

        /// <summary>
        /// Parses a new character.
        /// </summary>
        /// <param name="index">The position of the character to parse in <see cref="Text"/>.</param>
        public abstract void Parse(int index);

        /// <summary>
        /// Index to use for partition comparison.
        /// </summary>
        public abstract int ComparisonIndex { get; }
    }
}
