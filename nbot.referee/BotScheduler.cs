using System.Threading;

namespace nbot.referee
{
    public class BotScheduler : IBotScheduler
    {
        private readonly SemaphoreSlim sem = new SemaphoreSlim(1, 1);

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
