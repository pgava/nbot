using System.Collections.Generic;

namespace nbot.referee
{
    public interface IMovesProcess
    {
        IEnumerable<IMove> ProcessNextMove(IEnumerable<IBotController> bots);
    }
}