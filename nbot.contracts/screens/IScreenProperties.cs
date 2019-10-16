namespace nbot.contracts.screens
{
    public interface IScreenProperties
    {
        double Width { get; }
        double Height { get; }
        double HorizontalDirection(double curX, double x, double direction);
        double VeriticalDirection(double curY, double y, double direction);
    }
}