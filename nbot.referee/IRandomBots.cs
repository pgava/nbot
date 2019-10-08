using System.Collections.Generic;
using nbot.contracts;

namespace nbot.referee
{
    public interface IRandomBots
    {
        IEnumerable<IBotController> RandomizeList(IList<IBotController> items);
    }
}