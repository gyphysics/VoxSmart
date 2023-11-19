namespace VoxSmart.FinancialEntityExtractor.FinancialEntitySource.DataSources;

public abstract record FinancialEntityDto
{
    public abstract FinancialEntity ToFinancialEntity();
}