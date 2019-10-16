namespace nbot.contracts
{
    public interface IBotPosition
    {
        double X { get; }
        double Y { get; }

        void SetMoveAhead(double d);
        void SetMoveBack(double d);
        void SetMoveRight(double a);
        void SetMoveLeft(double a);
        void CalculateNextPosition();
    }
}