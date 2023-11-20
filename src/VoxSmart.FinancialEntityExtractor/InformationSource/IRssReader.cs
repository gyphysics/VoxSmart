namespace VoxSmart.FinancialEntityExtractor.InformationSource;

public interface IRssReader
{
    /// <summary>
    /// Will read data from an RSS Feed and map the information out as an <see cref="InformationSource"/> object
    /// </summary>
    /// <returns>A collection of <see cref="InformationSource"/> objects from the RSS Feed</returns>
    /// <exception cref="ReadInformationSourceException">Thrown if there is an exception detected whilst trying to obtain the RSS data</exception>
    IEnumerable<InformationSource> GetRssFeedItems();
}