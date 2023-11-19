using VoxSmart.FinancialEntityExtractor.FinancialEntitySource;

namespace VoxSmart.FinancialEntityExtractor;

public interface IInformationSourceFinancialEntityExtractor
{
    IEnumerable<(InformationSource.InformationSource InformationSource, IEnumerable<FinancialEntity> FinancialEntities)> GetInformationSourcesContainingFinancialEntityData(InformationSource.InformationSource informationSource, IEnumerable<IFinancialEntitySource> financialEntitySources);
}

public sealed class InformationSourceFinancialEntityExtractor : IInformationSourceFinancialEntityExtractor
{
    public IEnumerable<(InformationSource.InformationSource InformationSource, IEnumerable<FinancialEntity> FinancialEntities)> GetInformationSourcesContainingFinancialEntityData(InformationSource.InformationSource informationSource, IEnumerable<IFinancialEntitySource> financialEntitySources)
    {
        return null;
    }
}