namespace nbot.actions
{
    public interface IHelm
    {
        Point CalculatePosition(Point current, double distance, double direction);
        Point RandomPosition();
    }
}