using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Umplify.Bot.Activity.Settings;
using Umplify.Bot.Resolvers;
using Umplify.Bot.Resolvers.LUIS;
using Umplify.Bot.Resolvers.Microsoft;

namespace Umplify.Bot.Activity
{
	public static class Extenders
	{
		public static IServiceCollection AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<ApplicationSettings>(configuration.GetSection("ApplicationSettings"));
			services.Configure<MicrosoftAppSettings>(configuration.GetSection("MicrosoftAppSettings"));
			services.Configure<LUISSettings>(configuration.GetSection("LUISSettings"));
			return services;
		}

		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			//Note: add your services here if any.
			services.AddSingleton<IResolver<LUISSettings>, Resolvers.LUIS.Resolver>();
			services.AddSingleton<IResolver<MicrosoftAppSettings>, Resolvers.Microsoft.Resolver>();
			return services;
		}
	}
}
