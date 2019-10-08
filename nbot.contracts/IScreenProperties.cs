namespace nbot.contracts
{
    public interface IScreenProperties
    {
        double Width { get; }
        double Height { get; }
        double HorizontalDirection(double x, double direction);
        double VeriticalDirection(double y, double direction);
    }
}