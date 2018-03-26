namespace Umplify.Bot.Resolvers
{
    public interface IResolver<out T>
    {
        T Get(string customerKey);
        bool Load();
    }
}
