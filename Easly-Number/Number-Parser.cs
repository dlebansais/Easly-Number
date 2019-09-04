namespace EaslyNumber
{
    using System;
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    /// Describes and manipulates real numbers with arbitrary precision.
    /// </summary>
    public partial struct Number
    {
        #region Integer Constants
        /// <summary>
        /// The base of binary digits.
        /// </summary>
        public const int BinaryRadix = 2;

        /// <summary>
        /// The base of octal digits.
        /// </summary>
        public const int OctalRadix = 8;

        /// <summary>
        /// The base of decimal digits.
        /// </summary>
        public const int DecimalRadix = 10;

        /// <summary>
        /// The base of hexadecimal digits.
        /// </summary>
        public const int HexadecimalRadix = 16;

        /// <summary>
        /// The prefix character for binary integer string format.
        /// </summary>
        public const char BinaryPrefixCharacter = 'b';

        /// <summary>
        /// The prefix character for hexadecimal integer string format.
        /// </summary>
        public const char HexadecimalPrefixCharacter = 'x';
        #endregion

        #region Client Interface
        /// <summary>
        /// Converts the string representation of a number to a <see cref="Number"/> object. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="text">A string containing a number to convert.</param>
        /// <param name="value">The converted number if successful.</param>
        /// <returns>True if the conversion succeeded; Otherwise, false.</returns>
        public bool TryParse(string text, out Number value)
        {
            value = NaN;

            Parse(text, out string DiscardedProlog, out Number SpecialNumber, out int Radix, out OptionalSign SignificandSign, out string IntegerPart, out OptionalSeparator Separator, out string FractionalPart, out OptionalExponent ExponentCharacter, out OptionalSign ExponentSign, out string ExponentPart, out string InvalidPart);

            if (DiscardedProlog.Length > 0)
                return false;

            if (InvalidPart.Length > 0)
                return false;

            if (Radix == DecimalRadix)
                value = new Number(SignificandSign, IntegerPart, Separator, FractionalPart, ExponentCharacter, ExponentSign, ExponentPart);
            else if (Radix == BinaryRadix || Radix == OctalRadix || Radix == HexadecimalRadix)
                value = new Number(Radix, IntegerPart);
            else
                value = SpecialNumber;

            return true;
        }
        #endregion

        #region Parser
        /// <summary>
        /// Parses a string to extract relevant parts for a number.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="discardedProlog">The beginning of the string that can be ignored.</param>
        /// <param name="specialNumber">The special number if NaN or infinity.</param>
        /// <param name="radix">The radix (10 for decimal integers and reals).</param>
        /// <param name="significandSign">The optional sign of the significand.</param>
        /// <param name="integerPart">The integer part in front of the separator (if any).</param>
        /// <param name="separator">The optional separator.</param>
        /// <param name="fractionalPart">The fractional part after the separator (if any).</param>
        /// <param name="exponentCharacter">The optional exponent character.</param>
        /// <param name="exponentSign">The optional exponent sign.</param>
        /// <param name="exponentPart">The exponent part (if any).</param>
        /// <param name="invalidPart">The remaining part of the string that is not parsed in the number.</param>
        internal static void Parse(string text, out string discardedProlog, out Number specialNumber, out int radix, out OptionalSign significandSign, out string integerPart, out OptionalSeparator separator, out string fractionalPart, out OptionalExponent exponentCharacter, out OptionalSign exponentSign, out string exponentPart, out string invalidPart)
        {
            discardedProlog = null;
            specialNumber = NaN;
            radix = -1;
            significandSign = OptionalSign.None;
            integerPart = null;
            separator = OptionalSeparator.None;
            fractionalPart = null;
            exponentCharacter = OptionalExponent.None;
            exponentSign = OptionalSign.None;
            exponentPart = null;
            invalidPart = null;

            TextPartition BinaryIntegerPartition = new CustomRadixIntegerTextPartition(BinaryRadix, BinaryPrefixCharacter, IsValidBinaryDigit);
            TextPartition OctalIntegerPartition = new CustomRadixIntegerTextPartition(OctalRadix, IsValidOctalDigit);
            TextPartition HexadecimalIntegerPartition = new CustomRadixIntegerTextPartition(HexadecimalRadix, HexadecimalPrefixCharacter, IsValidHexadecimalDigit);
            TextPartition RealPartition = new RealTextPartition();

            for (int Index = 0; Index < text.Length; Index++)
            {
                BinaryIntegerPartition.Parse(text, Index);
                OctalIntegerPartition.Parse(text, Index);
                HexadecimalIntegerPartition.Parse(text, Index);
                RealPartition.Parse(text, Index);
            }

            Debug.Assert(integerPart.Length > 0 || fractionalPart.Length > 0);
            Debug.Assert(exponentSign == OptionalSign.None || exponentCharacter != OptionalExponent.None);
            Debug.Assert(exponentPart.Length == 0 || exponentCharacter != OptionalExponent.None);
        }

        /// <summary>
        /// Checks if a binary digit is valid.
        /// </summary>
        /// <param name="digit">The digit to validate.</param>
        /// <param name="value">The digit value, if valid.</param>
        /// <returns>True if valid; Otherwise, false.</returns>
        public static bool IsValidBinaryDigit(char digit, out int value)
        {
            if (digit >= '0' && digit < '2')
            {
                value = digit - '0';
                return true;
            }
            else
            {
                value = -1;
                return false;
            }
        }

        /// <summary>
        /// Gets the binary digit of a value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The digit.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Parameter <paramref name="value"/> is not in the valid range for the binary digit.</exception>
        public static char ToBinaryDigit(int value)
        {
            if (value >= 0 && value < 2)
                return (char)('0' + value);
            else
                throw new ArgumentOutOfRangeException(nameof(value));
        }

        /// <summary>
        /// Checks if a string is a number in binary format.
        /// </summary>
        /// <param name="text">The string to check.</param>
        /// <returns>True if valid; Otherwise, false.</returns>
        public static bool IsValidBinaryNumber(string text)
        {
            TextPartition BinaryIntegerPartition = new CustomRadixIntegerTextPartition(BinaryRadix, BinaryPrefixCharacter, IsValidBinaryDigit);

            for (int Index = 0; Index < text.Length; Index++)
                BinaryIntegerPartition.Parse(text, Index);

            return BinaryIntegerPartition.FirstInvalidCharacterIndex < 0;
        }

        /// <summary>
        /// Checks if an octal digit is valid.
        /// </summary>
        /// <param name="digit">The digit to validate.</param>
        /// <param name="value">The digit value, if valid.</param>
        /// <returns>True if valid; Otherwise, false.</returns>
        public static bool IsValidOctalDigit(char digit, out int value)
        {
            if (digit >= '0' && digit < '8')
            {
                value = digit - '0';
                return true;
            }
            else
            {
                value = -1;
                return false;
            }
        }

        /// <summary>
        /// Gets the octal digit of a value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The digit.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Parameter <paramref name="value"/> is not in the valid range for the octal digit.</exception>
        public static char ToOctalDigit(int value)
        {
            if (value >= 0 && value < 8)
                return (char)('0' + value);
            else
                throw new ArgumentOutOfRangeException(nameof(value));
        }

        /// <summary>
        /// Checks if a string is a number in octal format.
        /// </summary>
        /// <param name="text">The string to check.</param>
        /// <returns>True if valid; Otherwise, false.</returns>
        public static bool IsValidOctalNumber(string text)
        {
            TextPartition OctalIntegerPartition = new CustomRadixIntegerTextPartition(OctalRadix, IsValidOctalDigit);

            for (int Index = 0; Index < text.Length; Index++)
                OctalIntegerPartition.Parse(text, Index);

            return OctalIntegerPartition.FirstInvalidCharacterIndex < 0;
        }

        /// <summary>
        /// Checks if a decimal digit is valid.
        /// </summary>
        /// <param name="digit">The digit to validate.</param>
        /// <param name="value">The digit value, if valid.</param>
        /// <returns>True if valid; Otherwise, false.</returns>
        public static bool IsValidDecimalDigit(char digit, out int value)
        {
            if (digit >= '0' && digit <= '9')
            {
                value = digit - '0';
                return true;
            }
            else
            {
                value = -1;
                return false;
            }
        }

        /// <summary>
        /// Gets the decimal digit of a value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The digit.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Parameter <paramref name="value"/> is not in the valid range for the decimal digit.</exception>
        public static char ToDecimalDigit(int value)
        {
            if (value >= 0 && value < 10)
                return (char)('0' + value);
            else
                throw new ArgumentOutOfRangeException(nameof(value));
        }

        /// <summary>
        /// Checks if an hexadecimal digit is valid.
        /// </summary>
        /// <param name="digit">The digit to validate.</param>
        /// <param name="value">The digit value, if valid.</param>
        /// <returns>True if valid; Otherwise, false.</returns>
        public static bool IsValidHexadecimalDigit(char digit, out int value)
        {
            if (digit >= '0' && digit <= '9')
            {
                value = digit - '0';
                return true;
            }
            else if (digit >= 'a' && digit <= 'f')
            {
                value = digit - 'a' + 10;
                return true;
            }
            else if (digit >= 'A' && digit <= 'F')
            {
                value = digit - 'A' + 10;
                return true;
            }
            else
            {
                value = -1;
                return false;
            }
        }

        /// <summary>
        /// Gets the hexadecimal digit of a value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="lowerCase">True to return a lower case digit; False for an upper case digit.</param>
        /// <returns>The digit.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Parameter <paramref name="value"/> is not in the valid range for the hexadecimal digit.</exception>
        public static char ToHexadecimalDigit(int value, bool lowerCase)
        {
            if (value >= 0 && value < 10)
                return (char)('0' + value);
            else if (value > 10 && value < 16)
                if (lowerCase)
                    return (char)('a' + value - 10);
                else
                    return (char)('A' + value - 10);
            else
                throw new ArgumentOutOfRangeException(nameof(value));
        }

        /// <summary>
        /// Checks if a string is a number in hexadecimal format.
        /// </summary>
        /// <param name="text">The string to check.</param>
        /// <returns>True if valid; Otherwise, false.</returns>
        public static bool IsValidHexadecimalNumber(string text)
        {
            TextPartition HexadecimalIntegerPartition = new CustomRadixIntegerTextPartition(HexadecimalRadix, HexadecimalPrefixCharacter, IsValidHexadecimalDigit);

            for (int Index = 0; Index < text.Length; Index++)
                HexadecimalIntegerPartition.Parse(text, Index);

            return HexadecimalIntegerPartition.FirstInvalidCharacterIndex < 0;
        }
        #endregion

        #region Tools
        /// <summary>
        /// Returns the text of an optional sign.
        /// </summary>
        /// <param name="sign">The optional sign.</param>
        internal static string SignText(OptionalSign sign)
        {
            if (sign == OptionalSign.Positive)
                return "+";
            else if (sign == OptionalSign.Negative)
                return "-";
            else
                return string.Empty;
        }

        /// <summary>
        /// Returns the text of an optional separator.
        /// </summary>
        /// <param name="separator">The optional separator.</param>
        internal static string SeparatorText(OptionalSeparator separator)
        {
            if (separator == OptionalSeparator.Normalized)
                return ".";
            else if (separator == OptionalSeparator.CultureSpecific)
                return CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            else
                return string.Empty;
        }

        /// <summary>
        /// Returns the text of an optional exponent character.
        /// </summary>
        /// <param name="exponentCharacter">The optional exponent character.</param>
        internal static string ExponentCharacterText(OptionalExponent exponentCharacter)
        {
            if (exponentCharacter == OptionalExponent.UpperCaseE)
                return "E";
            else if (exponentCharacter == OptionalExponent.LowerCaseE)
                return "e";
            else
                return string.Empty;
        }

        /// <summary>
        /// Returns the text of the radix prefix.
        /// </summary>
        /// <param name="radix">The radix.</param>
        internal static string RadixPrefixText(int radix)
        {
            Debug.Assert(radix == BinaryRadix || radix == OctalRadix || radix == DecimalRadix || radix == HexadecimalRadix);

            if (radix == 2)
                return "0b";
            else if (radix == 8)
                return "0";
            else if (radix == 16)
                return "0x";
            else
                return string.Empty;
        }
        #endregion
    }
}
