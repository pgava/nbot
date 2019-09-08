using System.Collections.Generic;
using nbot.contracts;

namespace nbot.referee
{
    public interface IReferee
    {
        void AddBot(IBot b);
        void AddBots(List<IBot> bots);
        IEnumerable<IBot> GetBots();
        IEnumerable<IBot> GetRndBots();
    }
}