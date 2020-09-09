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
        /// The suffix character for binary integer string format.
        /// </summary>
        public const char BinarySuffixCharacter = 'B';

        /// <summary>
        /// The suffix character for octal integer string format.
        /// </summary>
        public const char OctalSuffixCharacter = 'O';

        /// <summary>
        /// The prefix character for hexadecimal integer string format.
        /// </summary>
        public const char HexadecimalPrefixCharacter = 'x';

        /// <summary>
        /// The suffix character for hexadecimal integer string format.
        /// </summary>
        public const char HexadecimalSuffixCharacter = 'H';
        #endregion

        #region Client Interface
        /// <summary>
        /// Converts the string representation of a number to a <see cref="Number"/> object. A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="text">A string containing a number to convert.</param>
        /// <param name="value">The converted number if successful.</param>
        /// <returns>True if the conversion succeeded; Otherwise, false.</returns>
        public static bool TryParse(string text, out Number value)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));

            if (!Parse(text, out TextPartition Partition))
            {
                value = Uninitialized;
                return false;
            }

            value = new Number(Partition);

            return true;
        }
        #endregion

        #region Parser
        /// <summary>
        /// Parses a string to extract relevant parts for a number.
        /// </summary>
        /// <param name="text">The string to parse.</param>
        /// <param name="partition">The text partition of <paramref name="text"/> if parsed successfully.</param>
        internal static bool Parse(string text, out TextPartition partition)
        {
            TextPartitionCollection PartitionList = new TextPartitionCollection()
            {
                new SpecialNumberTextPartition(text),
                new RealTextPartition(text),
                new RadixPrefixTextPartition(text, BinaryRadix, BinaryPrefixCharacter, IsValidBinaryDigit, ToBinaryDigit),
                new RadixPrefixTextPartition(text, HexadecimalRadix, HexadecimalPrefixCharacter, IsValidHexadecimalDigit, ToUpperCaseHexadecimalDigit),
                new RadixSuffixTextPartition(text, BinaryRadix, BinarySuffixCharacter, IsValidBinaryDigit, ToBinaryDigit),
                new RadixSuffixTextPartition(text, OctalRadix, OctalSuffixCharacter, IsValidOctalDigit, ToOctalDigit),
                new RadixSuffixTextPartition(text, HexadecimalRadix, HexadecimalSuffixCharacter, IsValidHexadecimalDigit, ToUpperCaseHexadecimalDigit),
            };

            for (int Index = 0; Index < text.Length && PartitionList.IsValid; Index++)
                PartitionList.Parse(Index);

            partition = PartitionList.PreferredPartition;

            return partition != null;
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
            if (text == null) throw new ArgumentNullException(nameof(text));

            TextPartitionCollection PartitionList = new TextPartitionCollection()
            {
                new RadixPrefixTextPartition(text, BinaryRadix, BinaryPrefixCharacter, IsValidBinaryDigit, ToBinaryDigit),
                new RadixSuffixTextPartition(text, BinaryRadix, BinarySuffixCharacter, IsValidBinaryDigit, ToBinaryDigit),
            };

            for (int Index = 0; Index < text.Length && PartitionList.IsValid; Index++)
                PartitionList.Parse(Index);

            return PartitionList.IsValid;
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
            if (text == null) throw new ArgumentNullException(nameof(text));

            TextPartition OctalSuffixIntegerPartition = new RadixSuffixTextPartition(text, OctalRadix, OctalSuffixCharacter, IsValidOctalDigit, ToOctalDigit);

            for (int Index = 0; Index < text.Length && OctalSuffixIntegerPartition.IsValid; Index++)
                OctalSuffixIntegerPartition.Parse(Index);

            return OctalSuffixIntegerPartition.IsValid;
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
            else if (value >= 10 && value < 16)
                if (lowerCase)
                    return (char)('a' + value - 10);
                else
                    return (char)('A' + value - 10);
            else
                throw new ArgumentOutOfRangeException(nameof(value));
        }

        /// <summary>
        /// Gets the hexadecimal digit of a value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The digit.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Parameter <paramref name="value"/> is not in the valid range for the hexadecimal digit.</exception>
        public static char ToUpperCaseHexadecimalDigit(int value)
        {
            return ToHexadecimalDigit(value, false);
        }

        /// <summary>
        /// Checks if a string is a number in hexadecimal format.
        /// </summary>
        /// <param name="text">The string to check.</param>
        /// <returns>True if valid; Otherwise, false.</returns>
        public static bool IsValidHexadecimalNumber(string text)
        {
            if (text == null) throw new ArgumentNullException(nameof(text));

            TextPartitionCollection PartitionList = new TextPartitionCollection()
            {
                new RadixPrefixTextPartition(text, HexadecimalRadix, HexadecimalPrefixCharacter, IsValidHexadecimalDigit, ToUpperCaseHexadecimalDigit),
                new RadixSuffixTextPartition(text, HexadecimalRadix, HexadecimalSuffixCharacter, IsValidHexadecimalDigit, ToUpperCaseHexadecimalDigit),
            };

            for (int Index = 0; Index < text.Length && PartitionList.IsValid; Index++)
                PartitionList.Parse(Index);

            return PartitionList.IsValid;
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
            string? Result = null;

            switch (radix)
            {
                case BinaryRadix:
                    Result = "0" + BinaryPrefixCharacter;
                    break;
                case HexadecimalRadix:
                    Result = "0" + HexadecimalPrefixCharacter;
                    break;
            }

            Debug.Assert(Result != null);

            return Result !;
        }

        /// <summary>
        /// Returns the text of the radix suffix.
        /// </summary>
        /// <param name="radix">The radix.</param>
        internal static string RadixSuffixText(int radix)
        {
            string? Result = null;

            switch (radix)
            {
                case BinaryRadix:
                    Result = ":" + BinarySuffixCharacter;
                    break;
                case OctalRadix:
                    Result = ":" + OctalSuffixCharacter;
                    break;
                case HexadecimalRadix:
                    Result = ":" + HexadecimalSuffixCharacter;
                    break;
            }

            Debug.Assert(Result != null);

            return Result !;
        }
        #endregion
    }
}
