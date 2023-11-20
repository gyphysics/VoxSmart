namespace VoxSmart.FinancialEntityExtractor.Configuration;

public sealed record RssReaderSettings
{
    public string Uri { get; init; } = default!;
}