using CodeHollow.FeedReader;

namespace NetcupPromotions;

public static class Extensions
{
    public static string DescriptionString(this IList<FeedItem> feedItems, int range)
    {
        var returnValue = "";
        Array.ForEach(feedItems.Take(range).ToArray(), 
            item => returnValue += $"- [{item.Title}]({item.Link})\n");
        return returnValue;
    }
}