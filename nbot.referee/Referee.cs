using System;
using System.Linq;
using System.Threading.Tasks;

namespace nbot.referee
{
    public class Referee : IReferee
    {
        private readonly IBotControllerCollection bots;
        private readonly ITimerProvider timerProvider;
        private readonly ITaskManagerProvider taskManagerProvider;
        private readonly IMovesProvider actionProvider;
        private readonly ISyncDataProvider syncDataProvider;

        public Referee(IBotControllerCollection bots, ITimerProvider timerProvider, ITaskManagerProvider taskManagerProvider, IMovesProvider actionProvider, ISyncDataProvider syncDataProvider)
        {
            if (bots is null)
            {
                throw new ArgumentNullException(nameof(bots));
            }

            if (timerProvider is null)
            {
                throw new ArgumentNullException(nameof(timerProvider));
            }

            if (taskManagerProvider is null)
            {
                throw new ArgumentNullException(nameof(taskManagerProvider));
            }

            if (actionProvider is null)
            {
                throw new ArgumentNullException(nameof(actionProvider));
            }

            if (syncDataProvider is null)
            {
                throw new ArgumentNullException(nameof(syncDataProvider));
            }

            this.bots = bots;
            this.timerProvider = timerProvider;
            this.taskManagerProvider = taskManagerProvider;
            this.actionProvider = actionProvider;
            this.syncDataProvider = syncDataProvider;
        }

        public void PlayMatch()
        {
            StartBots();

            while (IsMatchActive())
            {
                WakeupBots();

                WaitEndTurn();

                ProcessTurn();
            }
        }

        private void WakeupBots()
        {
            foreach (var b in bots.GetRndBots())
            {
                b.Wakeup();
            }
        }

        private void ProcessTurn()
        {
            var moves = actionProvider.ProcessNextMove(bots.GetRndBots());
            syncDataProvider.SyncMoves(moves);
        }

        private void WaitEndTurn()
        {
            timerProvider.WaitForTimer();
        }

        private void StartBots()
        {
            taskManagerProvider.StartBots(bots.GetRndBots());
        }

        private bool IsMatchActive()
        {
            return bots.GetBots().Count(b => b.IsAlive) > 1;
        }
    }
}
