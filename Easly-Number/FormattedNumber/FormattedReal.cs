﻿namespace EaslyNumber;

using System;
using System.Diagnostics;

/// <summary>
/// A real number and its components.
/// </summary>
public class FormattedReal : FormattedNumber
{
    #region Init
    /// <summary>
    /// Initializes a new instance of the <see cref="FormattedReal"/> class.
    /// </summary>
    /// <param name="sign">The optional sign.</param>
    /// <param name="leadingZeroCount">The number of leading zeroes.</param>
    /// <param name="integerText">The text before the decimal separator character. Can be empty.</param>
    /// <param name="separatorCharacter">The decimal separator character. Can be <see cref="Parser.NoSeparator"/>.</param>
    /// <param name="fractionalText">The text after the decimal separator character and before the exponent. Can be empty.</param>
    /// <param name="exponentCharacter">The exponent character (e or E). Can be <see cref="Parser.NoSeparator"/>.</param>
    /// <param name="exponentSign">The optional exponent sign.</param>
    /// <param name="exponentText">The text after the exponent character. Can be empty.</param>
    /// <param name="invalidText">The trailing invalid text, if any.</param>
    /// <param name="canonical">The canonical form of the number.</param>
    /// <exception cref="NullReferenceException"><paramref name="invalidText"/> or <paramref name="canonical"/> is null.</exception>
    internal FormattedReal(OptionalSign sign, int leadingZeroCount, string integerText, char separatorCharacter, string fractionalText, char exponentCharacter, OptionalSign exponentSign, string exponentText, string invalidText, CanonicalNumber canonical)
        : base(invalidText, canonical)
    {
        Sign = sign;
        Debug.Assert(leadingZeroCount >= 0);
        LeadingZeroCount = leadingZeroCount;
        IntegerText = integerText;
        SeparatorCharacter = separatorCharacter;
        FractionalText = fractionalText;
        ExponentCharacter = exponentCharacter;
        ExponentSign = exponentSign;
        ExponentText = exponentText;

        if (SeparatorCharacter != Parser.NoSeparator)
            Debug.Assert(integerText.Length + fractionalText.Length > 0);
        else
            Debug.Assert(integerText.Length > 0);

        if (exponentCharacter != Parser.NoSeparator)
            Debug.Assert(exponentText.Length > 0);
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets the optional sign.
    /// </summary>
    public OptionalSign Sign { get; }

    /// <summary>
    /// Gets the number of leading zeroes. Can be 0.
    /// </summary>
    public int LeadingZeroCount { get; }

    /// <summary>
    /// Gets the text before the decimal separator character. Can be empty.
    /// </summary>
    public string IntegerText { get; }

    /// <summary>
    /// Gets the decimal separator character. Can be <see cref="Parser.NoSeparator"/>.
    /// </summary>
    public char SeparatorCharacter { get; }

    /// <summary>
    /// Gets the text after the decimal separator character and before the exponent. Can be empty.
    /// </summary>
    public string FractionalText { get; }

    /// <summary>
    /// Gets the exponent character (e or E). Can be <see cref="Parser.NoSeparator"/>.
    /// </summary>
    public char ExponentCharacter { get; }

    /// <summary>
    /// Gets the optional exponent sign.
    /// </summary>
    public OptionalSign ExponentSign { get; }

    /// <summary>
    /// Gets the text after the exponent character. Can be empty.
    /// </summary>
    public string ExponentText { get; }

    /// <summary>
    /// Gets the significand part. Can be empty.
    /// This includes all characters up to and including the exponent character.
    /// </summary>
    public override string SignificandPart
    {
        get
        {
            string SignText = GetSignText(Sign);
            string LeadingZeroesText = GetLeadingZeroesText(LeadingZeroCount);

            string SignificandText;
            if (SeparatorCharacter != Parser.NoSeparator)
                SignificandText = $"{SignText}{LeadingZeroesText}{IntegerText}{SeparatorCharacter}{FractionalText}";
            else
                SignificandText = $"{SignText}{LeadingZeroesText}{IntegerText}";

            if (ExponentCharacter != Parser.NoSeparator)
                return $"{SignificandText}{ExponentCharacter}";
            else
                return SignificandText;
        }
    }

    /// <summary>
    /// Gets the exponent part. Can be empty.
    /// This includes all characters after the exponent character and before the invalid text.
    /// </summary>
    public override string ExponentPart
    {
        get
        {
            string ExponentSignText = GetSignText(ExponentSign);

            return $"{ExponentSignText}{ExponentText}";
        }
    }

    /// <summary>
    /// Gets a value indicating whether the number is valid.
    /// A valid number is finite in the sense of arithmetic (not NaN, not infinite), and has no trailing invalid text.
    /// </summary>
    public override bool IsValid { get { return InvalidText.Length == 0; } }

    /// <summary>
    /// Gets a diagnostic string for debug purpose.
    /// </summary>
    public override string Diagnostic
    {
        get
        {
            string SignText = GetSignText(Sign);
            string LeadingZeroesText = GetLeadingZeroesText(LeadingZeroCount);
            string ExponentSignText = GetSignText(ExponentSign);

            return $"{SignText}/{LeadingZeroesText}/{IntegerText}/{(int)SeparatorCharacter}/{FractionalText}/{(int)ExponentCharacter}/{ExponentSignText}/{ExponentText}/{InvalidText}";
        }
    }
    #endregion

    #region Client Interface
    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return $"{SignificandPart}{ExponentPart}{InvalidText}";
    }
    #endregion
}
