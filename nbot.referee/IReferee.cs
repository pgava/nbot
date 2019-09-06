using System.Collections.Generic;
using nbot.contracts;

namespace nbot.referee
{
    public interface IReferee
    {
        void AddBot(INBot b);
        void AddBots(List<INBot> bots);
        IEnumerable<INBot> GetBots();
        IEnumerable<INBot> GetRndBots();
    }
}