using System;
using System.Threading;

namespace nbot.contracts
{
    public enum BotStatus
    {

        Waiting,
        Running
    }

    public class BotController 
    {
        private SemaphoreSlim _sem = new SemaphoreSlim(1);
        private BotStatus _status;

        public bool IsRunning => _status == BotStatus.Running;        
        public bool IsWaiting => _status == BotStatus.Waiting;
        public void Execute()
        {
            WaitForNextTurn();
        }

        private void WaitForNextTurn()
        {
            _status = BotStatus.Waiting;
            _sem.Wait();
        }

        private void Wakeup()
        {
            _sem.Release();
            _status = BotStatus.Running;
        }

    }
}
