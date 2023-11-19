using CsvHelper;
using System.Globalization;
using System.Runtime.CompilerServices;
using VoxSmart.FinancialEntityExtractor.FinancialEntitySource.DataSources;

namespace VoxSmart.FinancialEntityExtractor.FinancialEntitySource;

public sealed class CsvFileBasedFinancialEntitySource<T> : IFinancialEntitySource
    where T : FinancialEntityDto
{
    private readonly string _fileName;

    public CsvFileBasedFinancialEntitySource(string fileName) => _fileName = fileName;

    public async IAsyncEnumerable<FinancialEntity> GetFinancialEntitiesAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        using (var reader = new StreamReader(_fileName))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecordsAsync<T>(cancellationToken);

            await foreach (var record in records.WithCancellation(cancellationToken))
            {
                yield return record.ToFinancialEntity();
            }
        }
    }
}