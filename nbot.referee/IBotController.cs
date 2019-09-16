namespace nbot.referee
{
    public interface IBotController
    {
        bool IsRunning { get; }
        bool IsWaiting { get; }
        bool IsAlive { get; }

        void Turn();
        void Wakeup();
    }
}
