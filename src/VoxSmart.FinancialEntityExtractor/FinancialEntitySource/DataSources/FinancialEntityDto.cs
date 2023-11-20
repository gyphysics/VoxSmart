namespace VoxSmart.FinancialEntityExtractor.FinancialEntitySource.DataSources;

/// <summary>
/// Represents a Financial Entity - e.g. a currency, commodity or company stock
/// </summary>
public abstract record FinancialEntityDto
{
    /// <summary>
    /// Converts this DTO object to a <see cref="FinancialEntity"/>
    /// </summary>
    /// <returns>This object as a <see cref="FinancialEntity"/></returns>
    public abstract FinancialEntity ToFinancialEntity();
}