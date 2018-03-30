using System.Collections.Generic;

namespace Umplify.Bot.Resolvers
{
    public interface IResolver<T>
    {
        T Get();
    }
}
