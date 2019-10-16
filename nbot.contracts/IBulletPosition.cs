namespace nbot.contracts
{
    public interface IBulletPosition
    {
        double X { get; }
        double Y { get; }

        void CalculateNextPosition();
    }
}