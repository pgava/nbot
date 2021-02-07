namespace nbot.actions.screens
{
    public interface IScreen
    {
        double Width { get; }
        double Height { get; }
        Point MapNextPointToScreen(Point current, Point next, bool canBounce = true);
    }
}