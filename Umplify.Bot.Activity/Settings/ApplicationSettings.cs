namespace Umplify.Bot.Activity.Settings
{
    public sealed class ApplicationSettings
    {
        public bool IsMultiTenant { get; set; }
        public string AzureWebJobsStorage { get; set; }
    }
}
