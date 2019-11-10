﻿namespace EaslyNumber
{
    using System.Diagnostics;

    /// <summary>
    /// The partition of a string into different components of a special number.
    /// </summary>
    internal class SpecialNumberTextPartition : TextPartition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecialNumberTextPartition"/> class.
        /// <param name="text">The string to parse.</param>
        /// </summary>
        public SpecialNumberTextPartition(string text)
            : base(text)
        {
        }

        /// <summary>
        /// The string for NaN in the current culture.
        /// </summary>
        private readonly string NaNString = double.NaN.ToString();

        /// <summary>
        /// The string for +∞ in the current culture.
        /// </summary>
        private readonly string PositiveInfinityString = double.PositiveInfinity.ToString();

        /// <summary>
        /// The string for -∞ in the current culture.
        /// </summary>
        private readonly string NegativeInfinityString = double.NegativeInfinity.ToString();

        /// <summary>
        /// Parses a new character.
        /// </summary>
        /// <param name="index">The position of the character to parse in <see cref="TextPartition.Text"/>.</param>
        public override void Parse(int index)
        {
            char c = Text[index];

            switch (State)
            {
                case ParsingState.Init:
                    State = ParsingState.LeadingWhitespaces;
                    Parse(index);
                    break;

                case ParsingState.LeadingWhitespaces:
                    ParseLeadingWhitespaces(index, c);
                    break;

                case ParsingState.SpecialPart:
                    ParseSpecialPart(index, c);
                    break;

                case ParsingState.InvalidPart:
                    ParseInvalidPart(index, c);
                    break;
            }
        }

        /// <summary>
        /// Runs the parser in the <see cref="ParsingState.LeadingWhitespaces"/> state.
        /// </summary>
        /// <param name="index">Index of the parsed character.</param>
        /// <param name="c">The parsed character.</param>
        private void ParseLeadingWhitespaces(int index, char c)
        {
            if (char.IsWhiteSpace(c))
            {
                LastLeadingSpaceIndex = index;
            }
            else
            {
                FirstSpecialPartIndex = index;
                State = ParsingState.SpecialPart;
                Parse(index);
            }
        }

        /// <summary>
        /// Runs the parser in the <see cref="ParsingState.SpecialPart"/> state.
        /// </summary>
        /// <param name="index">Index of the parsed character.</param>
        /// <param name="c">The parsed character.</param>
        private void ParseSpecialPart(int index, char c)
        {
            string Substring = Text.Substring(FirstSpecialPartIndex, index - FirstSpecialPartIndex);

            if (Substring == NaNString)
            {
                LastSpecialPartIndex = index;
                Value = Number.NaN;
                State = ParsingState.InvalidPart;
            }
            else if (Substring == PositiveInfinityString)
            {
                LastSpecialPartIndex = index;
                Value = Number.PositiveInfinity;
                State = ParsingState.InvalidPart;
            }
            else if (Substring == NegativeInfinityString)
            {
                LastSpecialPartIndex = index;
                Value = Number.NegativeInfinity;
                State = ParsingState.InvalidPart;
            }
            else if (index + 1 == Text.Length)
            {
                FirstInvalidCharacterIndex = 0;
                Value = Number.Uninitialized;
                State = ParsingState.InvalidPart;
            }
        }

        /// <summary>
        /// Runs the parser in the <see cref="ParsingState.InvalidPart"/> state.
        /// </summary>
        /// <param name="index">Index of the parsed character.</param>
        /// <param name="c">The parsed character.</param>
        private void ParseInvalidPart(int index, char c)
        {
            if (FirstInvalidCharacterIndex < 0)
                FirstInvalidCharacterIndex = index;
        }

        /// <summary>
        /// The optional separator. This partition cannot have one.
        /// </summary>
        public Number Value { get; private set; }

        /// <summary>
        /// The beginning of <see cref="TextPartition.Text"/> that can be ignored.
        /// </summary>
        public override string DiscardedProlog
        {
            get
            {
                int LastDiscardedIndex = -1;
                if (LastDiscardedIndex < LastLeadingSpaceIndex)
                    LastDiscardedIndex = LastLeadingSpaceIndex;

                return LastDiscardedIndex < 0 ? string.Empty : Text.Substring(0, LastDiscardedIndex + 1);
            }
        }

        /// <summary>
        /// Index of the special part, -1 if not parsed.
        /// </summary>
        public int FirstSpecialPartIndex { get; set; } = -1;

        /// <summary>
        /// Index of the first character after the special part, -1 if not parsed.
        /// </summary>
        public int LastSpecialPartIndex { get; set; } = -1;

        /// <summary>
        /// The beginning of <see cref="TextPartition.Text"/> that can be ignored.
        /// </summary>
        public string SpecialPart
        {
            get
            {
                if (FirstSpecialPartIndex >= 0 && LastSpecialPartIndex >= FirstSpecialPartIndex)
                    return Text.Substring(FirstSpecialPartIndex, LastSpecialPartIndex - FirstSpecialPartIndex + 1);
                else
                    return string.Empty;
            }
        }

        /// <summary>
        /// Index to use for partition comparison.
        /// </summary>
        public override int ComparisonIndex
        {
            get { return FirstInvalidCharacterIndex < 0 ? Text.Length : FirstInvalidCharacterIndex; }
        }
    }
}