using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Database;
using System;

namespace Services
{
    public static class ServiceProviderRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection sc)
        {
            sc.AddTransient<IMessageHandler, MessageHandler>();
            //sc.AddSingleton<IMushroomsDatabaseSettings, MushroomsDatabaseSettings>();
            sc.AddSingleton(sp => sp.GetRequiredService<IConfiguration>().Get<AppSettingsConfiguration>());
            return sc;
        }
    }
}
