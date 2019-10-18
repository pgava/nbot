using nbot.actions;

namespace nbot.contract
{
    public interface IBotController
    {
        bool IsRunning { get; }
        bool IsWaiting { get; }
        bool IsAlive { get; }

        void Turn();
        void Wakeup();
        Point CalculateNextPosition();
    }
}
