using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace NetcupPromotions;

public class SlashCommands : ApplicationCommandModule
{
    [SlashCommand("promotions", "See netcup's current promotions")]
    public async Task PromotionsCommand(InteractionContext ctx)
    {
        await ctx.DeferAsync();
        var promotions = new Promotions("https://www.netcup-sonderangebote.de/feed/");
        await ctx.EditResponseAsync(new DiscordWebhookBuilder().AddEmbed(new DiscordEmbedBuilder()
        {
            Title = promotions.Feed.Title,
            Description = promotions.Feed.Items.DescriptionString(10)
        }));
    }
}