using VoxSmart.FinancialEntityExtractor.FinancialEntitySource;

namespace VoxSmart.FinancialEntityExtractor;

/// <summary>
/// Extracts information sources that contain financial entities
/// </summary>
public interface IInformationSourceFinancialEntityExtractor
{
    /// <summary>
    /// Iterates over a set of information sources and returns any that contain financial data from the supplied collection of financial data sources
    /// </summary>
    /// <param name="informationSources">The list of information sources to be interrogated</param>
    /// <param name="financialEntitySources">A collection of sources that list financial entities of interest</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>A Dictionary of any information sources that were deemed to contain financial data, with the value containing a collection of financial entities detected in the information source</returns>
    Task<IDictionary<InformationSource.InformationSource, List<FinancialEntity>>> GetInformationSourcesContainingFinancialEntityData(IEnumerable<InformationSource.InformationSource> informationSources, IEnumerable<IFinancialEntitySource> financialEntitySources, CancellationToken cancellationToken);
}

/// <summary>
/// Extracts information sources that contain financial entities
/// </summary>
public sealed class InformationSourceFinancialEntityExtractor : IInformationSourceFinancialEntityExtractor
{
    /// <summary>
    /// Iterates over a set of information sources and returns any that contain financial data from the supplied collection of financial data sources
    /// </summary>
    /// <param name="informationSources">The list of information sources to be interrogated</param>
    /// <param name="financialEntitySources">A collection of sources that list financial entities of interest</param>
    /// <param name="cancellationToken">A cancellation token</param>
    /// <returns>A Dictionary of any information sources that were deemed to contain financial data, with the value containing a collection of financial entities detected in the information source</returns>
    public async Task<IDictionary<InformationSource.InformationSource, List<FinancialEntity>>> GetInformationSourcesContainingFinancialEntityData(IEnumerable<InformationSource.InformationSource> informationSources, IEnumerable<IFinancialEntitySource> financialEntitySources, CancellationToken cancellationToken)
    {
        Dictionary<InformationSource.InformationSource, List<FinancialEntity>> results = new();

        foreach (var informationSource in informationSources)
        {
            await DetermineIfInformationSourceContainsFinancialData(financialEntitySources, informationSource, results, cancellationToken);
        }

        return results;
    }

    private static async Task DetermineIfInformationSourceContainsFinancialData(IEnumerable<IFinancialEntitySource> financialEntitySources, InformationSource.InformationSource informationSource, Dictionary<InformationSource.InformationSource, List<FinancialEntity>> results, CancellationToken cancellationToken)
    {
        foreach (var financialEntitySource in financialEntitySources)
        {
            await ReadAllFinancialEntitiesFromSourceAndAddToResultsIfFoundInInformationSource(informationSource, results, cancellationToken, financialEntitySource);
        }
    }

    private static async Task ReadAllFinancialEntitiesFromSourceAndAddToResultsIfFoundInInformationSource(InformationSource.InformationSource informationSource, Dictionary<InformationSource.InformationSource, List<FinancialEntity>> results, CancellationToken cancellationToken, IFinancialEntitySource financialEntitySource)
    {
        await foreach (var financialEntity in financialEntitySource.GetFinancialEntitiesAsync(cancellationToken))
        {
            if (informationSource.Summary.Contains(financialEntity.EntityName) || informationSource.Details.Contains(financialEntity.EntityName))
            {
                CreateNewResultOrAddToExisting(results, informationSource, financialEntity);
            }
        }
    }

    private static void CreateNewResultOrAddToExisting(Dictionary<InformationSource.InformationSource, List<FinancialEntity>> results, InformationSource.InformationSource informationSource, FinancialEntity financialEntity)
    {
        if (results.TryGetValue(informationSource, out var result))
        {
            result.Add(financialEntity);
        }
        else
        {
            results.Add(informationSource, new List<FinancialEntity> { financialEntity });
        }
    }
}