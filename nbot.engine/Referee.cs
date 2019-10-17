using System;
using System.Linq;
using System.Threading.Tasks;
using nbot.contract;

namespace nbot.engine
{
    public class Referee : IReferee
    {
        private readonly IBotControllerCollection bots;
        private readonly ITimerManager timerManager;
        private readonly ITaskManager taskManager;
        private readonly IMovesProcess movesProcess;
        private readonly ISyncData syncData;

        public Referee(IBotControllerCollection bots, ITimerManager timerManager, ITaskManager taskManager, IMovesProcess movesProcess, ISyncData syncData)
        {
            if (bots is null)
            {
                throw new ArgumentNullException(nameof(bots));
            }

            if (timerManager is null)
            {
                throw new ArgumentNullException(nameof(timerManager));
            }

            if (taskManager is null)
            {
                throw new ArgumentNullException(nameof(taskManager));
            }

            if (movesProcess is null)
            {
                throw new ArgumentNullException(nameof(movesProcess));
            }

            if (syncData is null)
            {
                throw new ArgumentNullException(nameof(syncData));
            }

            this.bots = bots;
            this.timerManager = timerManager;
            this.taskManager = taskManager;
            this.movesProcess = movesProcess;
            this.syncData = syncData;
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
            var moves = movesProcess.ProcessNextMove(bots.GetRndBots());
            syncData.SyncMoves(moves);
        }

        private void WaitEndTurn()
        {
            timerManager.WaitForTimer();
        }

        private void StartBots()
        {
            taskManager.StartBots(bots.GetRndBots());
        }

        private bool IsMatchActive()
        {
            return bots.GetBots().Count(b => b.IsAlive) > 1;
        }
    }
}
