using System;
using nbot.actions.screens;
using nbot.common;

namespace nbot.actions
{    
    public class PositionProvider : IPositionProvider
    {
        private readonly IScreen screen;

        public PositionProvider(IScreen screen)
        {
            Validation.ThrowIfArgumentIsNull(screen);

            this.screen = screen;
        }
        
        public Point CalculatePosition(Point current, double distance, double direction)
        {
            var x = distance * Math.Cos(NBotMath.DegreeToRadian(direction));
            var y = distance * Math.Sin(NBotMath.DegreeToRadian(direction));

            // To ignore small issues in aproximation consider only 2 decimal points.
            x = Math.Round(x, 2);
            y = Math.Round(y, 2);

            return screen.MapNextPointToScreen(current, new Point(x, y));
        }

        public Point RandomPosition()
        {
            return Point.Rnd(this.screen.Width, this.screen.Height);
        }

    }
}