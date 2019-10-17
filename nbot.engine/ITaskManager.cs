using System.Collections.Generic;
using nbot.contract;

namespace nbot.engine
{
    public interface ITaskManager
    {
        void StartBots(IEnumerable<IBotController> bots);
    }
}
