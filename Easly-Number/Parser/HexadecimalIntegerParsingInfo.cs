namespace EaslyNumber;

/// <summary>
/// Hold information during parsing of an integer in hexadecimal base.
/// </summary>
internal class HexadecimalIntegerParsingInfo : IntegerWithBaseParsingInfo
{
    /// <summary>
    /// Gets the base to use when parsing.
    /// </summary>
    protected override IntegerBase Base { get { return IntegerBase.Hexadecimal; } }
}
