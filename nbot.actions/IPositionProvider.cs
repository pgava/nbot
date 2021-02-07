namespace nbot.actions
{
    public interface IPositionProvider
    {
        Point CalculatePosition(Point current, double distance, double direction);
        Point RandomPosition();
    }
}