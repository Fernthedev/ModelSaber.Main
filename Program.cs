using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Webhook;

namespace ModelSaber.Main
{
    public class Program
    {
        public static DateTime CompiledTime { get; private set; }
        public static DiscordWebhookClient DiscordClient { get; private set; } = null!;

        public static void Main(string[] args)
        {
            CompiledTime = Assembly.GetExecutingAssembly().GetLinkerTime();
            var webhookUrl = Environment.GetEnvironmentVariable("DISCORD_WEBHOOK_URL", EnvironmentVariableTarget.User);
            if (string.IsNullOrWhiteSpace(webhookUrl)) throw new Exception("DISCORD_WEBHOOK_URL cannot be null and must be set.");
            DiscordClient = new DiscordWebhookClient(webhookUrl);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    public static class Extensions
    {
        public static DateTime GetLinkerTime(this Assembly assembly)
        {
            const string buildVersionMetadataPrefix = "+build";

            var attribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            if (attribute?.InformationalVersion == null) return default;
            var value = attribute.InformationalVersion;
            var index = value.IndexOf(buildVersionMetadataPrefix, StringComparison.Ordinal);
            if (index <= 0) return default;
            value = value[(index + buildVersionMetadataPrefix.Length)..];
            return DateTime.ParseExact(value, "yyyy-MM-ddTHH:mm:ss:fffZ", CultureInfo.InvariantCulture);
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> enumerable)
        {
            foreach (var element in enumerable)
            {
                collection.Add(element);
            }
        }
    }
}
