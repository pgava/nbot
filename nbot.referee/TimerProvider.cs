using System.Threading;

namespace nbot.referee
{

    public class TimerProvider : ITimerProvider
    {
        private readonly ManualResetEvent timerEvent = new ManualResetEvent(false);
        private readonly int maxWaitMs = 20;

        public TimerProvider()
        {

        }
        public TimerProvider(int waitms)
        {
            maxWaitMs = waitms;
        }

        public void WaitForTimer()
        {
            timerEvent.WaitOne(maxWaitMs);
        }

        public void WaitForTimer(int waitms)
        {
            timerEvent.WaitOne(waitms);
        }
    }
}
