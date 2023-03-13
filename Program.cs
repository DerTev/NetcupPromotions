using dotenv.net;
using DSharpPlus.SlashCommands;
using NetcupPromotions;

DotEnv.Load();
var env = DotEnv.Read();

if (env.ContainsKey("TOKEN"))
{
    var bot = new Bot(env["TOKEN"]);

    var slash = bot.Client.UseSlashCommands();
    slash.RegisterCommands<SlashCommands>(env.ContainsKey("GUILD") ? ulong.Parse(env["GUILD"]) : null);

    if (env.ContainsKey("PROMOTIONS_CHANNEL"))
    {
        new ScanJob(env.ContainsKey("CRON_EXPRESSION") ? env["CRON_EXPRESSION"] : "*/5 * * * *", bot.Client, 
                "https://www.netcup-sonderangebote.de/feed/", ulong.Parse(env["PROMOTIONS_CHANNEL"]));
    }
    else
    {
        Console.WriteLine("Can't set promotions cron job up, because PROMOTIONS_CHANNEL isn't provided.");
    }
    
    bot.StartBot();
}
else
{
    Console.WriteLine("Please provide the token using env variables!");
}