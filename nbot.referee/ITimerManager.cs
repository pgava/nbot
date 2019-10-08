namespace nbot.referee
{
    public interface ITimerManager
    {
        void WaitForTimer();
        void WaitForTimer(int waitms);
    }
}
