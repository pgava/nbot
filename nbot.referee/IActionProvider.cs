using System.Collections.Generic;

namespace nbot.referee
{
    public interface IActionProvider
    {
        IEnumerable<IMove> ProcessNextMove(IEnumerable<IBotController> bots);
    }
}