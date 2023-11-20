namespace VoxSmart.FinancialEntityExtractor.InformationSource;

/// <summary>
/// Represents a source of information that we may wish to interrogate for financial details
/// </summary>
/// <param name="Id">The ID of this information source</param>
/// <param name="Summary">A title or summary describing the information source</param>
/// <param name="Details">Detailed content of the information source</param>
public sealed record InformationSource(string Id, string Summary, string Details);