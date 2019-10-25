using System;
using nbot.actions.screens;

namespace nbot.actions
{
    public class Helm : IHelm
    {
        private readonly IScreenProperties screenProperties;

        public Helm(IScreenProperties screenProperties)
        {
            if (screenProperties is null)
            {
                throw new ArgumentNullException(nameof(screenProperties));
            }

            this.screenProperties = screenProperties;
        }

        public Point CalculatePosition(Point current, double distance, double direction)
        {
            return screenProperties.CheckLimits(current,
                new Point(distance * Math.Cos(DegreeToRadian(direction)), distance * Math.Sin(DegreeToRadian(direction))));
        }

        public Point RandomPosition()
        {
            return Point.Rnd(this.screenProperties.Width, this.screenProperties.Height);
        }

        private double DegreeToRadian(double degrees)
        {
            return Math.PI * degrees / 180.0;
        }

    }
}