using Microsoft.Extensions.Options;
using System.Reflection;
using VoxSmart.FinancialEntityExtractor.Configuration;
using VoxSmart.FinancialEntityExtractor.FinancialEntitySource;
using VoxSmart.FinancialEntityExtractor.FinancialEntitySource.DataSources;

namespace VoxSmart.FinancialEntityExtractor.Tests;

[TestFixture]
public sealed class CompanyCsvFileBasedFinancialEntitySourceTests
{
    private CsvFileBasedFinancialEntitySource<CompanyDto> _sut;

    [SetUp]
    public void SetUp() => _sut = new CsvFileBasedFinancialEntitySource<CompanyDto>(Options.Create(new CsvFileSettings
    {
        FilePath = Path.Combine(Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().Location).LocalPath)!,
            "DataSources", "Companies.csv")
    }));

    [Test]
    public void CanParseCompanyCsvData()
    {
        var results = _sut.GetFinancialEntitiesAsync(CancellationToken.None).ToEnumerable();

        Assert.That(results.Count(), Is.EqualTo(4));
        Assert.That(results.First().EntityName, Is.EqualTo("Agilent"));
        Assert.That(results.First().EntitySymbol, Is.EqualTo("A"));
        Assert.That(results.Last().EntityName, Is.EqualTo("Target"));
        Assert.That(results.Last().EntitySymbol, Is.EqualTo("TGT"));
    }
}