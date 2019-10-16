namespace nbot.contracts
{
    public interface IBulletPosition
    {
        Point Position { get; }

        void CalculateNextPosition();
    }
}