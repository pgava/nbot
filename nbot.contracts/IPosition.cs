namespace nbot.contracts
{
    public interface IPosition
    {
        double X { get; }
        double Y { get; }

        void SetMoveAhead(double d);
        void SetMoveBack(double d);
        void SetMoveRight(double d);
        void SetMoveLeft(double d);
        void CalculateNextPosition();
    }
}