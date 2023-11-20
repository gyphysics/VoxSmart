namespace VoxSmart.FinancialEntityExtractor.Configuration;

public sealed record CsvFileSettings
{
    public string FilePath { get; init; }
}