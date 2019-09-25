using System.Collections.Generic;

namespace nbot.referee
{
    public interface IMovesProvider
    {
        IEnumerable<IMove> ProcessNextMove(IEnumerable<IBotController> bots);
    }
}