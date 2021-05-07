namespace EaslyNumber
{
#pragma warning disable SA1600 // Elements should be documented
    internal class OutputDouble
    {
        public double Positive { get; set; }
        public double Negative { get; set; }
        public string? Format { get; set; }
        public string PositiveString { get; set; } = string.Empty;
        public string NegativeString { get; set; } = string.Empty;
    }
#pragma warning restore SA1600 // Elements should be documented
}
