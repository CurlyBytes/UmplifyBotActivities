using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using Umplify.Bot.Activity.Settings;

namespace Umplify.Bot.Resolvers
{
	public abstract class BasicResolver<T>
	{
		protected readonly ILogger _logger;
		protected readonly ApplicationSettings _applicationSettings;
		protected string _configurationFileName;

		protected BasicResolver(ILoggerFactory loggerFactory, IOptions<ApplicationSettings> applicationSettings)
		{
			_logger = loggerFactory?.CreateLogger(GetType().Name);
			_applicationSettings = applicationSettings?.Value;
		}

		protected virtual List<T> Load()
		{
			var loadedList = new List<T>();

			try
			{
				var path = Path.Combine(Directory.GetCurrentDirectory(), _configurationFileName);

				if (File.Exists(path))
				{
					loadedList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(path);
				}

				throw new FileNotFoundException("File not found.", path);
			}
			catch (Exception ex)
			{
				_logger?.LogCritical(ex.ToString());
				loadedList.Clear();
				loadedList = null;
			}
			return loadedList;
		}
	}
}
