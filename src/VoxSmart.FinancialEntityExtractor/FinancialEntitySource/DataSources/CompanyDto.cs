namespace VoxSmart.FinancialEntityExtractor.FinancialEntitySource.DataSources;

public sealed record CompanyDto : FinancialEntityDto
{
    public string AlphabeticCode { get; init; } = default!;
    public string Entity { get; init; } = default!;
    public override FinancialEntity ToFinancialEntity() => new(AlphabeticCode, Entity);
}