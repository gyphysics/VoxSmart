using System.Reflection;
using VoxSmart.FinancialEntityExtractor.FinancialEntitySource;
using VoxSmart.FinancialEntityExtractor.FinancialEntitySource.DataSources;

namespace VoxSmart.FinancialEntityExtractor.Tests;

[TestFixture]
public sealed class CsvFileBasedFinancialEntitySourceTests
{
    private CsvFileBasedFinancialEntitySource<CurrencyDto> _sut;

    [SetUp]
    public void SetUp() => _sut = new CsvFileBasedFinancialEntitySource<CurrencyDto>(Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!, "DataSources", "Currencies.csv"));

    [Test]
    public void CanParseCurrencyCsvData()
    {
        var results = _sut.GetFinancialEntitiesAsync(CancellationToken.None).ToEnumerable();

        Assert.That(results.Count(), Is.EqualTo(441));
        Assert.That(results.First().EntityName, Is.EqualTo("Afghani"));
        Assert.That(results.First().EntitySymbol, Is.EqualTo("AFN"));
        Assert.That(results.Last().EntityName, Is.EqualTo("UIC-Franc"));
        Assert.That(results.Last().EntitySymbol, Is.EqualTo("XFU"));
    }
}