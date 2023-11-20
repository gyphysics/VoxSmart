using Microsoft.Extensions.Options;
using System.Reflection;
using VoxSmart.FinancialEntityExtractor.Configuration;
using VoxSmart.FinancialEntityExtractor.FinancialEntitySource;
using VoxSmart.FinancialEntityExtractor.FinancialEntitySource.DataSources;
using VoxSmart.FinancialEntityExtractor.InformationSource;

namespace VoxSmart.FinancialEntityExtractor.Tests;

[TestFixture]
public sealed class InformationSourceFinancialEntityExtractorTests
{
    private InformationSourceFinancialEntityExtractor _sut;

    [SetUp]
    public void SetUp() => _sut = new();

    [Test]
    public async Task ExtractsFinancialEntitiesFromInformationSource()
    {
        var reader = new RssReader(Options.Create(new RssReaderSettings { Uri = Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!, "DataSources", "RssFeed.xml") }));

        var currencySource = new CsvFileBasedFinancialEntitySource<CurrencyDto>(Options.Create(new CsvFileSettings
        {
            FilePath = Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
                "DataSources", "Currencies.csv")
        }));

        var companySource = new CsvFileBasedFinancialEntitySource<CompanyDto>(Options.Create(new CsvFileSettings
        {
            FilePath = Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
                "DataSources", "Companies.csv")
        }));

        List<IFinancialEntitySource> entitySources = new();
        entitySources.Add(currencySource);
        entitySources.Add(companySource);

        var result = await _sut.GetInformationSourcesContainingFinancialEntityData(reader.GetRssFeedItems(), entitySources, CancellationToken.None);

        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result.Single().Value[0].EntitySymbol, Is.EqualTo("BA"));
        Assert.That(result.Single().Value[1].EntitySymbol, Is.EqualTo("M"));
        Assert.That(result.Single().Value[2].EntitySymbol, Is.EqualTo("TGT"));
        Assert.That(result.Single().Value[0].EntityName, Is.EqualTo("Boeing"));
        Assert.That(result.Single().Value[1].EntityName, Is.EqualTo("Macy's"));
        Assert.That(result.Single().Value[2].EntityName, Is.EqualTo("Target"));
    }
}