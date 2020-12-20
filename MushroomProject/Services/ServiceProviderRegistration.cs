using Microsoft.Extensions.DependencyInjection;
using System;

namespace Services
{
    public static class ServiceProviderRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection sc)
        {
            sc.AddTransient<IMessageHandler, MessageHandler>();
            return sc;
        }
    }
}
