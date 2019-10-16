namespace nbot.contracts.screens
{
    public interface IScreenProperties
    {
        double Width { get; }
        double Height { get; }
        Point CheckLimits(Point current, Point next, bool canBounce = true);
    }
}