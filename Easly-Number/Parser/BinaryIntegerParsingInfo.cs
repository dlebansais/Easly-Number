namespace EaslyNumber;

/// <summary>
/// Hold information during parsing of an integer in binary base.
/// </summary>
internal class BinaryIntegerParsingInfo : IntegerWithBaseParsingInfo
{
    /// <summary>
    /// Gets the base to use when parsing.
    /// </summary>
    protected override IntegerBase Base { get { return IntegerBase.Binary; } }
}
