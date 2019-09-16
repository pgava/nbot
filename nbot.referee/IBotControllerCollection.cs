using System.Collections.Generic;

namespace nbot.referee
{
    public interface IBotControllerCollection
    {
        void AddBot(IBotController b);
        void AddBots(List<IBotController> bots);
        IEnumerable<IBotController> GetBots();
        IEnumerable<IBotController> GetRndBots();
    }
}