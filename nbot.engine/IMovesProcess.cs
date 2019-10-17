using System.Collections.Generic;
using nbot.contract;

namespace nbot.engine
{
    public interface IMovesProcess
    {
        IEnumerable<IMove> ProcessNextMove(IEnumerable<IBotController> bots);
    }
}