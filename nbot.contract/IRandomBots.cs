using System.Collections.Generic;

namespace nbot.contract
{
    public interface IRandomBots
    {
        IEnumerable<IBotController> RandomizeList(IList<IBotController> items);
    }
}