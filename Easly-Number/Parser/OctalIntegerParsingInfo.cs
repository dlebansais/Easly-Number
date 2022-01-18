namespace EaslyNumber;

/// <summary>
/// Hold information during parsing of an integer in octal base.
/// </summary>
internal class OctalIntegerParsingInfo : IntegerWithBaseParsingInfo
{
    /// <summary>
    /// Gets the base to use when parsing.
    /// </summary>
    protected override IntegerBase Base { get { return IntegerBase.Octal; } }
}
