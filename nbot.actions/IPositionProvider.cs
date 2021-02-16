namespace nbot.actions
{
    /// <summary>
    /// Provides all the logic to manage the position of an object in a <see cref="IScreen"/>.
    /// </summary>
    public interface IPositionProvider
    {
        /// <summary>
        /// Calculates the position of an object on the screen.
        /// </summary>
        /// <param name="current">Current position.</param>
        /// <param name="distance">The distance from the current position.</param>
        /// <param name="direction">The angle (in degree) in which the object is moving. 
        /// </param>
        /// <remarks>
        /// If the direction is 90 degrees the object moves only vertically, if the direction is
        /// 0 degree the object moves only horizontaly.
        /// </remarks>
        /// <returns>The <see cref="Point"> of the new position of the object.</returns>
        Point CalculatePosition(Point current, double distance, double direction);
        Point RandomPosition();
    }
}