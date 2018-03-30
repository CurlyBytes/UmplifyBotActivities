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
			Test();

			var luisSettings = base.Load();

			if(luisSettings.GroupBy(luisSetting => luisSetting.CustomerKey).Count() != luisSettings.Count)
			{
				throw new ApplicationException("CustomerKey cannot be redundant in the list of LUIS settings definitions.");
			}

			return luisSettings;
		}

		private void Test()
		{
			var uluisSettings1 = new LUISSettings
			{
				ApiHostName = "apihostname",
				ApiKey = "apikey",
				AppId = "apiId",
				CustomerKey = "233"
			};

			var uluisSettings2 = new LUISSettings
			{
				ApiHostName = "apihostname2",
				ApiKey = "apikey2",
				AppId = "apiId2",
				CustomerKey = "233222"
			};

			var list = new List<LUISSettings> { uluisSettings1, uluisSettings2 };
			var kookoo = Newtonsoft.Json.JsonConvert.SerializeObject(list);
		}
	}
}
