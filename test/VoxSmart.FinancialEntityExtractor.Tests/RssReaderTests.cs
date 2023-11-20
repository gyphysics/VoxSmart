using Microsoft.Extensions.Options;
using VoxSmart.FinancialEntityExtractor.Configuration;
using VoxSmart.FinancialEntityExtractor.Exceptions;
using VoxSmart.FinancialEntityExtractor.InformationSource;

namespace VoxSmart.FinancialEntityExtractor.Tests;

[TestFixture]
public sealed class RssReaderTests
{
    private IRssReader _sut;

    [SetUp]
    public void Setup() => _sut = new RssReader(CreateOptions("https://feeds.a.dj.com/rss/RSSMarketsMain.xml"));

    [Test]
    public void CanReadRssFeed()
    {
        var data = _sut.GetRssFeedItems();

        Assert.That(data, Is.Not.Null);
        Assert.That(data.Count(), Is.GreaterThan(0));
    }

    [Test]
    public void ThrowsRssInformationSourceExceptionIfSuppliedWithInvalidUri()
    {
        _sut = new RssReader(CreateOptions("asdfjaklfjasdijfasdkjf;l"));

        Assert.Throws<ReadInformationSourceException>(() => _sut.GetRssFeedItems());
    }

    private static IOptions<RssReaderSettings> CreateOptions(string uri) =>
        Options.Create(new RssReaderSettings { Uri = uri });
}