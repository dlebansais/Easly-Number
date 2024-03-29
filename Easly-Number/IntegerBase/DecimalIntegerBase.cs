﻿namespace EaslyNumber;

using System;
using System.Diagnostics;

/// <summary>
/// Class describing a decimal (base 10) integer.
/// </summary>
public class DecimalIntegerBase : IntegerBase
{
    #region Init
    /// <summary>
    /// Initializes a new instance of the <see cref="DecimalIntegerBase"/> class.
    /// </summary>
    internal DecimalIntegerBase()
    {
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the suffix for decimal integers.
    /// </summary>
    public override string Suffix { get { return string.Empty; } }

    /// <summary>
    /// Gets the number of digits for decimal integers.
    /// </summary>
    public override int Radix { get { return DecimalRadix; } }
    #endregion

    #region Client Interface
    /// <summary>
    /// Checks if a character is a decimal digit, and return the corresponding value.
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

        return (char)('0' + value);
    }

    /// <summary>
    /// Returns the value corresponding to a digit.
    /// </summary>
    /// <param name="digit">The digit.</param>
    public override int ToValue(char digit)
    {
        if (digit < '0' || digit > '9')
            throw new ArgumentException(nameof(digit));

        return digit - '0';
    }
    #endregion
}
