using CodeHollow.FeedReader;

namespace NetcupPromotions;

public class Promotions
{
    public readonly Feed Feed;
    
    public Promotions(string url) => Feed = FeedReader.ReadAsync(url).GetAwaiter().GetResult();
}