using System;
using nbot.contracts.screens;

namespace nbot.contracts
{
    public class BulletPosition : IBulletPosition
    {
        private const double MAX_ACCELERATION = 10D;
        private const double TIME_SLOT = 2D;
        private const double MAX_LINEAR_SPEED = 100D;
        private readonly IScreenProperties screenProperties;
        private double currentLinearSpeed;
        private Point currentPosition;
        private double currentDirection;
        private double currentDistance;
        public Point Position => currentPosition;

        public BulletPosition(double x, double y, double direction, IScreenProperties screenProperties)
        {
            if (screenProperties is null)
            {
                throw new ArgumentNullException(nameof(screenProperties));
            }

            this.screenProperties = screenProperties;
            currentPosition = new Point(x, y);
            currentDirection = direction;
        }

        public BulletPosition(Point position, double direction, IScreenProperties screenProperties)
        {
            if (screenProperties is null)
            {
                throw new ArgumentNullException(nameof(screenProperties));
            }

            this.screenProperties = screenProperties;
            currentPosition = new Point(position.X, position.Y);
            currentDirection = direction;
        }

        public void CalculateNextPosition()
        {
            currentDistance = CalculateDistance();
            currentLinearSpeed = CalculateLinearSpeed();
            currentPosition = CalculatePosition(currentDistance, currentDirection);
        }

        private Point CalculatePosition(double distance, double direction)
        {
            return screenProperties.CheckLimits(currentPosition,
                new Point(distance * Math.Cos(DegreeToRadian(direction)), distance * Math.Sin(DegreeToRadian(direction))), false);
        }

        private double DegreeToRadian(double degrees)
        {
            return Math.PI * degrees / 180.0;
        }

        /// <summary>
        /// v = v0 + at
        /// </summary>
        private double CalculateLinearSpeed()
        {
            if (currentLinearSpeed >= MAX_LINEAR_SPEED)
            {
                return currentLinearSpeed;
            }

            return currentLinearSpeed + MAX_ACCELERATION * TIME_SLOT;
        }

        /// <summary>
        /// d = v0*t + 1/2*a*t^2
        /// </summary>
        private double CalculateDistance()
        {

            // d = v0*t + 1/2*a*t^2
            var distanceNew = currentLinearSpeed * TIME_SLOT + (MAX_ACCELERATION * TIME_SLOT * TIME_SLOT) / 2;

            return distanceNew;
        }

    }
}