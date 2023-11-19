using Microsoft.Extensions.Options;
using System.ServiceModel.Syndication;
using System.Xml;
using VoxSmart.FinancialEntityExtractor.Configuration;
using VoxSmart.FinancialEntityExtractor.Exceptions;
using VoxSmart.FinancialEntityExtractor.InformationSource.Mappers;

namespace VoxSmart.FinancialEntityExtractor.InformationSource;

public sealed class RssReader : IRssReader
{
    private readonly IOptions<RssReaderSettings> _readerSettings;

    public RssReader(IOptions<RssReaderSettings> readerSettings) => _readerSettings = readerSettings;

    /// <summary>
    /// Will read data from an RSS Feed and map the information out as an <see cref="InformationSource"/> object
    /// </summary>
    /// <returns>A collection of <see cref="InformationSource"/> objects from the RSS Feed</returns>
    /// <exception cref="ReadInformationSourceException">Thrown if there is an exception detected whilst trying to obtain the RSS data</exception>
    public IEnumerable<InformationSource> GetRssFeedItems()
    {
        try
        {
            using var reader = XmlReader.Create(_readerSettings.Value.Uri);
            return RssSyndicationFeedToInformationSourceMapper.Map(SyndicationFeed.Load(reader).Items);
        }
        catch (Exception ex)
        {
            throw new ReadInformationSourceException("Error occurred attempting to read the RSS feed, see inner exception for further details", ex);
        }
    }
}