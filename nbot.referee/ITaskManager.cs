using System.Collections.Generic;

namespace nbot.referee
{
    public interface ITaskManager
    {
        void StartBots(IEnumerable<IBotController> bots);
    }
}
