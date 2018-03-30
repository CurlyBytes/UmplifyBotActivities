using Microsoft.AspNetCore.Mvc;
using Microsoft.Bot.Connector;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Umplify.Bot.Resolvers;
using Umplify.Bot.Resolvers.LUIS;
using Umplify.Bot.Resolvers.Microsoft;

namespace Umplify.Bot.Activity.Controllers
{
	[Route("api/[controller]")]
	public class MessagesController : Controller
    {
        private readonly ILogger<MessagesController> _logger;
        private readonly IResolver<MicrosoftAppSettings> _microsoftAppSettingsResolver;
        private readonly IResolver<LUISSettings> _luisResolver;

		public MessagesController(
			IResolver<LUISSettings> luisResolver,
			IResolver<MicrosoftAppSettings> microsoftAppSettingsResolver,
			ILoggerFactory loggerFactory)
		{
			_logger = loggerFactory?.CreateLogger<MessagesController>();
			_microsoftAppSettingsResolver = microsoftAppSettingsResolver;
			_luisResolver = luisResolver;
		}

		//[ResponseType(typeof(void))]
		//[Authorize(Roles = "Bot")]
		[HttpPost]
        public async Task<OkResult> Post([FromBody] Microsoft.Bot.Connector.Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                //MicrosoftAppCredentials.TrustServiceUrl(activity.ServiceUrl);
                var appCredentials = new MicrosoftAppCredentials(_microsoftAppSettingsResolver.Get(activity.From.Id).Id, _microsoftAppSettingsResolver.Get(activity.From.Id).Password,_logger);
                var connector = new ConnectorClient(new Uri(activity.ServiceUrl), appCredentials);

                // return our reply to the user
                var reply = activity.CreateReply("HelloWorld");
                await connector.Conversations.ReplyToActivityAsync(reply);
            }
            else
            {
                //HandleSystemMessage(activity);
            }
            return Ok();
        }
    }
}
