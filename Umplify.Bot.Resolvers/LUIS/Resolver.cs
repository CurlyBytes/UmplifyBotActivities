using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using Umplify.Bot.Activity.Settings;

namespace Umplify.Bot.Resolvers.LUIS
{
	public class Resolver : BasicResolver<LUISSettings>, IResolver<LUISSettings>
	{
		private const string SettingsFileName = "SampleLuisSettings.json";
		protected List<LUISSettings> _luisSettingsList;
		protected readonly LUISSettings _luisSettings;

		public Resolver(ILoggerFactory loggerFactory,
			IOptions<ApplicationSettings> applicationSettings,
			IOptions<LUISSettings> luisSettings)
			: base(loggerFactory, applicationSettings)
		{
			_luisSettingsList = new List<LUISSettings>();
			_luisSettings = luisSettings?.Value;
			_configurationFileName = SettingsFileName;
			_luisSettingsList = Load();
		}

		public LUISSettings Get() => _luisSettingsList.FirstOrDefault(settings => settings.CustomerKey == _applicationSettings.CustomerKey);

		protected override List<LUISSettings> Load()
		{
			var luisSettings = base.Load();

			if(luisSettings.GroupBy(luisSetting => luisSetting.CustomerKey).Count() != luisSettings.Count)
			{
				throw new ApplicationException("CustomerKey cannot be redundant in the list of LUIS settings definitions.");
			}

			return luisSettings;
		}
	}
}
