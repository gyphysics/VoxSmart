namespace VoxSmart.FinancialEntityExtractor.FinancialEntitySource;

public interface IFinancialEntitySource
{
    public IAsyncEnumerable<FinancialEntity> GetFinancialEntitiesAsync(CancellationToken cancellationToken);
}