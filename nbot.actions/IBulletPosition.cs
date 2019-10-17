namespace nbot.actions
{
    public interface IBulletPosition
    {
        Point Position { get; }

        void CalculateNextPosition();
    }
}