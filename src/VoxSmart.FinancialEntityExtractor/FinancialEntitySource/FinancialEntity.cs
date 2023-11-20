namespace VoxSmart.FinancialEntityExtractor.FinancialEntitySource;

/// <summary>
/// Represents a financial entity, e.g. a stock, commodity or currency
/// </summary>
/// <param name="EntitySymbol">A symbol representing the entity, e.g. ISO currency code or stock ticker</param>
/// <param name="EntityName">The name of the entity</param>
public sealed record FinancialEntity(string EntitySymbol, string EntityName);