namespace VoxSmart.FinancialEntityExtractor.FinancialEntitySource.DataSources;

public sealed record CurrencyDto : FinancialEntityDto
{
    public string Entity { get; init; }
    public string Currency { get; init; }
    public string AlphabeticCode { get; init; }
    public string NumericCode { get; init; }
    public string MinorUnit { get; init; }
    public string WithdrawalDate { get; init; }

    public override FinancialEntity ToFinancialEntity() => new(AlphabeticCode, Currency);
}