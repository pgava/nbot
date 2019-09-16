namespace nbot.referee
{
    public interface IBotController
    {
        bool IsRunning { get; }
        bool IsWaiting { get; }

        void Turn();
        void Wakeup();
    }
}
