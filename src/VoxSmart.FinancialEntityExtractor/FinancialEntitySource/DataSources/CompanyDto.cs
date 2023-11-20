namespace VoxSmart.FinancialEntityExtractor.FinancialEntitySource.DataSources;

/// <summary>
/// Represents Company data stored in a CSV file
/// </summary>
public sealed record CompanyDto : FinancialEntityDto
{
    public string AlphabeticCode { get; init; } = default!;
    public string Entity { get; init; } = default!;
    
    /// <summary>
    /// Converts this DTO object to a <see cref="FinancialEntity"/>
    /// </summary>
    /// <returns>This object as a <see cref="FinancialEntity"/></returns>
    public override FinancialEntity ToFinancialEntity() => new(AlphabeticCode, Entity);
}