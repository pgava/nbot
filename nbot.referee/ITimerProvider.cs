namespace nbot.referee
{
    public interface ITimerProvider
    {
        void WaitForTimer();
        void WaitForTimer(int waitms);
    }
}
