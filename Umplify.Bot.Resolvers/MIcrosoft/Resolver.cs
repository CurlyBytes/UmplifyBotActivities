using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Umplify.Bot.Activity.Settings;

namespace Umplify.Bot.Resolvers.Microsoft
{
	public class Resolver : BasicResolver<MicrosoftAppSettings>, IResolver<MicrosoftAppSettings>
	{
		private const string SettingsFileName = "SampleMicrosoftAppSettings.json";
		protected List<MicrosoftAppSettings> _microsoftSettingsList;
		protected readonly MicrosoftAppSettings _microsoftSettings;

		public Resolver(ILoggerFactory loggerFactory,
			IOptions<ApplicationSettings> applicationSettings,
			IOptions<MicrosoftAppSettings> luisSettings)
			: base(loggerFactory, applicationSettings)
		{
			_microsoftSettingsList = new List<MicrosoftAppSettings>();
			_microsoftSettings = luisSettings?.Value;
			_configurationFileName = SettingsFileName;
			_microsoftSettingsList = Load();
		}

		public MicrosoftAppSettings Get() => _microsoftSettingsList.FirstOrDefault(settings => settings.CustomerKey == _applicationSettings.CustomerKey);

		protected override List<MicrosoftAppSettings> Load()
		{
			var microsoftAppSettings = base.Load();

			if (microsoftAppSettings.GroupBy(luisSetting => luisSetting.CustomerKey).Count() != microsoftAppSettings.Count)
			{
				throw new ApplicationException("CustomerKey cannot be redundant in the list of Microsoft app settings definitions.");
			}

			return microsoftAppSettings;
		}
	}
}
