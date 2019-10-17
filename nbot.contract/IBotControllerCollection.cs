using System.Collections.Generic;

namespace nbot.contract
{
    public interface IBotControllerCollection
    {
        void AddBot(IBotController b);
        void AddBots(List<IBotController> bots);
        IEnumerable<IBotController> GetBots();
        IEnumerable<IBotController> GetRndBots();
    }
}