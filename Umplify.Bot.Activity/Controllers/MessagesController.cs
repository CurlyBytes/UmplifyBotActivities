using Microsoft.Bot.Connector;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Umplify.Bot.Activity.Settings;
using Umplify.Bot.Resolvers;

namespace Umplify.Bot.Activity.Controllers
{
    public class MessagesController : ApiController
    {
        private readonly MicrosoftAppSettings _microsoftAppSettings;
        private readonly ILogger<MessagesController> _logger;
        private readonly IResolver<Resolvers.LUIS.Settings> _luisSettings;
        private readonly IResolver<Resolvers.LUIS.Settings> _luisResolver;

        public MessagesController(
			IResolver<Resolvers.LUIS.Settings> luisResolver,
			ILoggerFactory loggerFactory,
			IResolver<Resolvers.LUIS.Settings> luisSettings,
            IOptions<MicrosoftAppSettings> microsoftAppSettingsOptions)
        {
            _microsoftAppSettings = microsoftAppSettingsOptions.Value;
            _logger = loggerFactory?.CreateLogger<MessagesController>();
            _luisSettings = luisSettings;
            _luisResolver = luisResolver;
        }


        [Authorize(Roles = "Bot")]
        [HttpPost]
        public async Task<OkResult> Post([FromBody] Microsoft.Bot.Connector.Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                //MicrosoftAppCredentials.TrustServiceUrl(activity.ServiceUrl);
                var appCredentials = new MicrosoftAppCredentials(_microsoftAppSettings.Id,_microsoftAppSettings.Password,_logger);
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
