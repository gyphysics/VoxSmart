namespace VoxSmart.FinancialEntityExtractor.FinancialEntitySource.DataSources;

/// <summary>
/// Represents Currency data stored in a CSV file
/// </summary>
public sealed record CurrencyDto : FinancialEntityDto
{
    public string Entity { get; init; } = default!;
    public string Currency { get; init; } = default!;
    public string AlphabeticCode { get; init; } = default!;
    public string NumericCode { get; init; } = default!;
    public string MinorUnit { get; init; } = default!;
    public string WithdrawalDate { get; init; } = default!;
    
    /// <summary>
    /// Converts this DTO object to a <see cref="FinancialEntity"/>
    /// </summary>
    /// <returns>This object as a <see cref="FinancialEntity"/></returns>
    public override FinancialEntity ToFinancialEntity() => new(AlphabeticCode, Currency);
}