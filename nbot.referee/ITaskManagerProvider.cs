using System.Collections.Generic;

namespace nbot.referee
{
    public interface ITaskManagerProvider
    {
        void StartBots(IEnumerable<IBotController> bots);
    }
}
