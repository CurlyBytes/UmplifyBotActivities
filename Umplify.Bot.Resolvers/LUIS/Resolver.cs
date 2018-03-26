using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Umplify.Bot.Resolvers.LUIS
{
	public sealed class Resolver : IResolver<Settings>
	{
		private List<Settings> _luisSettingsList;
		private ILogger<Resolver> _logger;

		public Resolver(ILoggerFactory loggerFactory)
		{
			_luisSettingsList = new List<Settings>();
			_logger = loggerFactory?.CreateLogger<Resolver>();
		}

		public Settings Get(string customerKey) => _luisSettingsList.FirstOrDefault(settings => settings.CustomerKey == customerKey);

		public bool Load()
		{
			var loaded = true;
			try
			{
				_luisSettingsList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Settings>>("SampleLuisSettings.json");
			}
			catch(Exception ex)
			{
				loaded = false;
				_logger?.LogCritical(ex.ToString());
			}
			return loaded;
		}
	}
}
