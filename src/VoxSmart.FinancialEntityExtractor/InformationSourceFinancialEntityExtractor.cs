using VoxSmart.FinancialEntityExtractor.FinancialEntitySource;

namespace VoxSmart.FinancialEntityExtractor;

public interface IInformationSourceFinancialEntityExtractor
{
    Task<IDictionary<InformationSource.InformationSource, List<FinancialEntity>>> GetInformationSourcesContainingFinancialEntityData(IEnumerable<InformationSource.InformationSource> informationSources, IEnumerable<IFinancialEntitySource> financialEntitySources, CancellationToken cancellationToken);
}

public sealed class InformationSourceFinancialEntityExtractor : IInformationSourceFinancialEntityExtractor
{
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