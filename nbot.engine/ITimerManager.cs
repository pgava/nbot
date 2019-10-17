namespace nbot.engine
{
    public interface ITimerManager
    {
        void WaitForTimer();
        void WaitForTimer(int waitms);
    }
}
