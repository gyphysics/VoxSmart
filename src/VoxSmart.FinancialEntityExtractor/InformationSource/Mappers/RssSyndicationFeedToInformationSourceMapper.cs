using System.ServiceModel.Syndication;

namespace VoxSmart.FinancialEntityExtractor.InformationSource.Mappers;

internal static class RssSyndicationFeedToInformationSourceMapper
{
    internal static IEnumerable<InformationSource> Map(IEnumerable<SyndicationItem> items)
    {
        foreach (var item in items) yield return new(item.Id, item.Title.Text, item.Summary.Text);
    }
}