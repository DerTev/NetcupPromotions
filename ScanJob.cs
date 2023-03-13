using DSharpPlus;
using CronNET.Impl;
using DSharpPlus.Entities;

namespace NetcupPromotions;

public class ScanJob
{
    private readonly DiscordClient _client;
    private readonly CronDaemon _daemon;
    private readonly string _promotionsFeedUrl;
    private readonly PromotionsFile _promotionsFile;
    private readonly DiscordChannel _promotionChannel;
    
    private async Task ScanTask()
    {
        try
        {
            if (_promotionsFile.RegisteredPromotions != null)
            {
                var registeredPromotions = _promotionsFile.RegisteredPromotions;
                foreach (var promotion in new Promotions(_promotionsFeedUrl).Feed.Items)
                {
                    if (!_promotionsFile.RegisteredPromotions.Contains(promotion.Link))
                    {
                        Console.WriteLine("Register promotion with link " + promotion.Link + "...");
                        registeredPromotions.Add(promotion.Link);
                        await _client.SendMessageAsync(_promotionChannel, new DiscordEmbedBuilder()
                        {
                            Title = "A new promotion appeared!",
                            Color = DiscordColor.Green,
                            Description = promotion.Description,
                            Url = promotion.Link
                        });
                    }
                }
                _promotionsFile.RegisteredPromotions = registeredPromotions;
            }
            else
            {
                Console.WriteLine("RegisteredPromotions is null.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error while running ScanTask: " + e.Message);
        }
    }

    public ScanJob(string cronExpression, DiscordClient client, string promotionsFeedUrl, ulong promotionsChannel)
    {
        _client = client;
        _promotionsFeedUrl = promotionsFeedUrl;
        _promotionsFile = new PromotionsFile("promotions.json");
        _promotionChannel = client.GetChannelAsync(promotionsChannel).GetAwaiter().GetResult();
        
        _daemon = new CronDaemon();
        _daemon.Add(new CronNET.Impl.CronJob(ScanTask, "ScanTask", cronExpression));
    }
}