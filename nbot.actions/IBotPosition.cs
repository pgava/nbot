namespace nbot.actions
{
    public interface IBotPosition
    {
        Point Position { get; }
        void SetMoveAhead(double d);
        void SetMoveBack(double d);
        void SetMoveRight(double a);
        void SetMoveLeft(double a);
        void CalculateNextPosition();
    }
}