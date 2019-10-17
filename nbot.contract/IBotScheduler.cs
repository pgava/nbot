namespace nbot.contract
{
    public interface IBotScheduler
    {
        void WaitForNextTurn();
        void Wakeup();
    }

}