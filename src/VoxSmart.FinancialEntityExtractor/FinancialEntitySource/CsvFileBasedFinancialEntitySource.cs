using CsvHelper;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Runtime.CompilerServices;
using VoxSmart.FinancialEntityExtractor.Configuration;
using VoxSmart.FinancialEntityExtractor.FinancialEntitySource.DataSources;

namespace VoxSmart.FinancialEntityExtractor.FinancialEntitySource;

public sealed class CsvFileBasedFinancialEntitySource<T> : IFinancialEntitySource
    where T : FinancialEntityDto
{
    private readonly IOptions<CsvFileSettings> _fileSettings;

    public CsvFileBasedFinancialEntitySource(IOptions<CsvFileSettings> fileSettings) => _fileSettings = fileSettings;

    public async IAsyncEnumerable<FinancialEntity> GetFinancialEntitiesAsync([EnumeratorCancellation] CancellationToken cancellationToken)
    {
        using (var reader = new StreamReader(_fileSettings.Value.FilePath))
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