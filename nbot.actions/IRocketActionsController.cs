using System.Collections.Generic;

namespace nbot.actions
{
    public interface IRocketActionsController : IRocketActions
    {
        IEnumerable<IRocket> rockets { get; }

        void CalculateTrajectories(Vector startAt);
    }

    public interface IRocket
    {
        Point Position { get; }

        void CalculateTrajectory(Vector startAt);
    }
}