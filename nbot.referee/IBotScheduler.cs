namespace nbot.referee
{
    public interface IBotScheduler
    {
        void WaitForNextTurn();
        void Wakeup();
    }

}