using System.Collections.Generic;

namespace nbot.actions
{
    public interface IRocketActionsController : IRocketActions
    {
        IEnumerable<IRocket> rockets { get; }

        void CalculateNextPosition(Point currentBotPosition);
    }

    public interface IRocket
    {
        Point Position { get; }
    }
}