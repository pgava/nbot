using System.Collections.Generic;
using nbot.contracts;

namespace nbot.referee
{
    public interface IRandomBotsProvider
    {
        IEnumerable<IBot> RandomizeList(IList<IBot> items);
    }
}