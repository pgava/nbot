using System.Collections.Generic;
using nbot.contracts;

namespace nbot.referee
{
    public interface IRandomBotsProvider
    {
        IEnumerable<IBotController> RandomizeList(IList<IBotController> items);
    }
}