namespace nbot.actions.screens
{
    /// <summary>
    /// Defines screen properties.
    /// </summary>
    public interface IScreen
    {
        /// <summary>
        /// Max width of the screen.
        /// </summary>
        double Width { get; }

        /// <summary>
        /// Max height of the screen.
        /// </summary>
        double Height { get; }

        /// <summary>
        /// Calculates the new <see cref="Point"> of the object which moves from current to next.
        /// Next coordinates are relative to (0,0).
        /// </summary>
        /// <param name="current">Current position.</param>
        /// <param name="next">Relative position.</param>
        /// <returns>The new object position on the screen.</returns>
        Point MapNextPointToScreen(Point current, Point next);
    }
}