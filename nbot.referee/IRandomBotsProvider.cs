using System.Collections.Generic;
using nbot.contracts;

namespace nbot.referee
{
    public interface IRandomBotsProvider
    {
        IEnumerable<INBot> RandomizeList(IList<INBot> items);
    }
}