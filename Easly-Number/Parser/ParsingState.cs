﻿namespace EaslyNumber
{
    /// <summary>
    /// States of a number parser.
    /// </summary>
    internal enum ParsingState
    {
        /// <summary>
        /// The initial state.
        /// </summary>
        Init,

        /// <summary>
        /// Parsing optional leading whitespace characters.
        /// </summary>
        LeadingWhitespaces,

        /// <summary>
        /// Parsing optionl leading zeroes.
        /// </summary>
        LeadingZeroes,

        /// <summary>
        /// Parsing the integer base.
        /// </summary>
        IntegerBase,

        /// <summary>
        /// Parsing the integer part.
        /// </summary>
        IntegerPart,

        /// <summary>
        /// Parsing the fractional part.
        /// </summary>
        FractionalPart,

        /// <summary>
        /// Parsing the exponent part.
        /// </summary>
        ExponentPart,

        /// <summary>
        /// Parsing the invalid part at the end.
        /// </summary>
        InvalidPart,
    }
}
