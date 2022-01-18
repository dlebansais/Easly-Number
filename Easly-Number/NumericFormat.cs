namespace EaslyNumber;

/// <summary>
/// Format used to create a string representation of an instance of <see cref="Number"/>.
/// </summary>
internal enum NumericFormat
{
    /// <summary>
    /// The default "G" format.
    /// </summary>
    Default,

    /// <summary>
    /// The scientific "E" format.
    /// </summary>
    Exponential,

    /// <summary>
    /// The fixed point "F" format.
    /// </summary>
    FixedPoint,
}
