using DSharpPlus;

namespace NetcupPromotions;

public class Bot
{
    public readonly DiscordClient Client;

    private async Task StartBotAsync()
    {
        await Client.ConnectAsync();
        await Task.Delay(-1);
    }

    public void StartBot()
    {
        StartBotAsync().GetAwaiter().GetResult();
    }

    public Bot(string token)
    {
        Client = new DiscordClient(new DiscordConfiguration()
        {
            Token = token,
            TokenType = TokenType.Bot,
            Intents = DiscordIntents.AllUnprivileged
        });
    }
}