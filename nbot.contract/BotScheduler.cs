using System.Threading;

namespace nbot.contract
{
    public class BotScheduler : IBotScheduler
    {
        private readonly SemaphoreSlim sem = new SemaphoreSlim(0, 1);

        public void WaitForNextTurn()
        {
            sem.Wait();
        }

        public void Wakeup()
        {
            sem.Release();
        }
    }
}
