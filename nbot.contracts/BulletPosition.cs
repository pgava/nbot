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
        private double currentX;
        private double currentY;
        private double currentDirection;
        private double currentDistance;
        public double X => currentX;
        public double Y => currentY;

        public BulletPosition(double x, double y, double direction, IScreenProperties screenProperties)
        {
            if (screenProperties is null)
            {
                throw new ArgumentNullException(nameof(screenProperties));
            }

            this.screenProperties = screenProperties;
            currentX = x;
            currentY = y;
            currentDirection = direction;
        }

        public void CalculateNextPosition()
        {
            currentDistance = CalculateDistance();
            currentLinearSpeed = CalculateLinearSpeed();
            currentX = CalculateHorizontalPosition(currentDistance, currentDirection);
            currentY = CalculateVerticalPosition(currentDistance, currentDirection);
        }

        private double CalculateHorizontalPosition(double distance, double direction)
        {
            return screenProperties.HorizontalDirection(currentX, distance * Math.Cos(DegreeToRadian(direction)), direction);
        }

        private double CalculateVerticalPosition(double distance, double direction)
        {
            return screenProperties.VeriticalDirection(currentY, distance * Math.Sin(DegreeToRadian(direction)), direction);
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