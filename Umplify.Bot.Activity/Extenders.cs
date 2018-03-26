using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Umplify.Bot.Activity.Settings;

namespace Umplify.Bot.Activity
{
    public static class Extenders
    {
        public static IServiceCollection AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApplicationSettings>(configuration.GetSection("ApplicationSettings"));
            services.Configure<MicrosoftAppSettings>(configuration.GetSection("MicrosoftAppSettings"));
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            //Note: add your services here if any.
            return services;
        }
    }
}
