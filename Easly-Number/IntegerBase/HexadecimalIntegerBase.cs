﻿namespace EaslyNumber;

using System;
using System.Diagnostics;

/// <summary>
/// Class describing an hexadecimal (base 16) integer.
/// </summary>
public class HexadecimalIntegerBase : IntegerBase
{
    #region Init
    /// <summary>
    /// Initializes a new instance of the <see cref="HexadecimalIntegerBase"/> class.
    /// </summary>
    internal HexadecimalIntegerBase()
    {
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the suffix for hexadecimal integers.
    /// </summary>
    public override string Suffix { get { return HexadecimalSuffix; } }

    /// <summary>
    /// Gets the number of digits for hexadecimal integers.
    /// </summary>
    public override int Radix { get { return HexadecimalRadix; } }
    #endregion

    #region Client Interface
    /// <summary>
    /// Checks if a character is an hex digit, and return the corresponding value.
    /// </summary>
    /// <param name="digit">The character to check.</param>
    /// <param name="value">The digit's value.</param>
    /// <returns>True if <paramref name="digit"/> is a valid digit; Otherwise, false.</returns>
    public override bool IsValidDigit(char digit, out int value)
    {
        value = 0;
        bool IsParsed = false;

        if (digit >= '0' && digit <= '9')
        {
            value = digit - '0';
            IsParsed = true;
        }
        else if (digit >= 'a' && digit <= 'f')
        {
            value = digit - 'a' + 10;
            IsParsed = true;
        }
        else if (digit >= 'A' && digit <= 'F')
        {
            value = digit - 'A' + 10;
            IsParsed = true;
        }

        return IsParsed;
    }

    /// <summary>
    /// Returns the digit corresponding to a value.
    /// </summary>
    /// <param name="value">The value.</param>
    public override char ToDigit(int value)
    {
        if (value < 0 || value >= Radix)
            throw new ArgumentException(nameof(value));

        return value < 10 ? (char)('0' + value) : (char)('A' + value - 10);
    }

    /// <summary>
    /// Returns the value corresponding to a digit.
    /// </summary>
    /// <param name="digit">The digit.</param>
    public override int ToValue(char digit)
    {
        bool IsDecimalDigit = digit >= '0' && digit <= '9';
        bool IsHexadecimalDigitLower = digit >= 'a' && digit <= 'f';
        bool IsHexadecimalDigitUpper = digit >= 'A' && digit <= 'F';

        if (!IsDecimalDigit && !IsHexadecimalDigitLower && !IsHexadecimalDigitUpper)
            throw new ArgumentException(nameof(digit));

        return (digit >= '0' && digit <= '9') ? (digit - '0') : ((digit >= 'a' && digit <= 'f') ? (digit - 'a' + 10) : (digit - 'A' + 10));
    }
    #endregion
}
