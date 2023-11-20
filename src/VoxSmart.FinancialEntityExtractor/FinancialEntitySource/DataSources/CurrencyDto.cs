namespace VoxSmart.FinancialEntityExtractor.FinancialEntitySource.DataSources;

public sealed record CurrencyDto : FinancialEntityDto
{
    public string Entity { get; init; } = default!;
    public string Currency { get; init; } = default!;
    public string AlphabeticCode { get; init; } = default!;
    public string NumericCode { get; init; } = default!;
    public string MinorUnit { get; init; } = default!;
    public string WithdrawalDate { get; init; } = default!;

    public override FinancialEntity ToFinancialEntity() => new(AlphabeticCode, Currency);
}