using System;
using nbot.actions.screens;

namespace nbot.actions
{    
    /// <summary>
    /// Provides all the logic to manage the position of an object in a <see cref="IScreen"/>.
    /// 
    /// </summary>
    public class PositionProvider : IPositionProvider
    {
        private readonly IScreen screen;

        public PositionProvider(IScreen screen)
        {
            if (screen is null)
            {
                throw new ArgumentNullException(nameof(screen));
            }

            this.screen = screen;
        }

        /// <summary>
        /// Calculate the next position on the screen of an object
        /// </summary>
        /// <param name="current">Current position.</param>
        /// <param name="distance">The distance from the current position.</param>
        /// <param name="direction">The angle (in degree) in which the object has moved. 
        /// </param>
        /// <remarks>
        /// If the direction is 90 degrees the object moved only vertically, if the direction is
        /// 0 degree the object moved only horizontaly.
        /// </remarks>
        /// <returns>The <see cref="Point"> of the new position of the object.</returns>
        public Point CalculatePosition(Point current, double distance, double direction)
        {
            var x = distance * Math.Cos(DegreeToRadian(direction));
            var y = distance * Math.Sin(DegreeToRadian(direction));
            
            // To ignore small issues in aproximation consider only 2 decimal points.
            return screen.MapNextPointToScreen(current, new Point(Math.Round(x, 2), Math.Round(y,2)));
        }

        public Point RandomPosition()
        {
            return Point.Rnd(this.screen.Width, this.screen.Height);
        }

        private double DegreeToRadian(double degrees)
        {
            return Math.PI * degrees / 180.0;
        }

    }
}