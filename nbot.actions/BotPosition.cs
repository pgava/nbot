using System;
using nbot.actions.screens;

namespace nbot.actions
{
    public class BotPosition : IBotPosition
    {
        private const double MAX_ACCELERATION = 3D;
        private const double TIME_SLOT = 2D;
        private const double MAX_LINEAR_SPEED = 50D;
        private readonly IScreenProperties screenProperties;
        private double currentLinearSpeed = 0;
        private double currentAngularSpeed = 0;
        private Point currentPosition;
        private Point previousPosition;
        private double currentDirection = 0;
        private double currentDistance = 0;
        private double forward;
        private double steer;

        public Point Position => currentPosition;

        public BotPosition(double x, double y, IScreenProperties screenProperties)
        {
            if (screenProperties is null)
            {
                throw new ArgumentNullException(nameof(screenProperties));
            }

            this.screenProperties = screenProperties;

            currentPosition = new Point(x, y);
            
        }

        public BotPosition(Point position, IScreenProperties screenProperties)
        {
            if (screenProperties is null)
            {
                throw new ArgumentNullException(nameof(screenProperties));
            }

            this.screenProperties = screenProperties;

            currentPosition = new Point(position.X, position.Y);
        }

        public void SetMoveAhead(double d)
        {
            forward = d;
        }

        public void SetMoveBack(double d)
        {
            forward = -d;
        }

        public void SetMoveRight(double d)
        {
            steer = d;
        }

        public void SetMoveLeft(double d)
        {
            steer = -d;
        }

        public void CalculateNextPosition()
        {
            if (forward == 0)
            {
                return;
            }

            currentDistance = CalculateDistance();
            currentLinearSpeed = CalculateLinearSpeed();
            currentAngularSpeed = CalculateAngularSpeed(Math.Abs(currentDistance), currentLinearSpeed);
            currentDirection = CalculateDirection(currentDistance, currentAngularSpeed);
            currentPosition = CalculatePosition(currentDistance, currentDirection);
        }

        private Point CalculatePosition(double distance, double direction)
        {
            previousPosition = currentPosition;

            return screenProperties.CheckLimits(currentPosition, 
                new Point(distance * Math.Cos(DegreeToRadian(direction)), distance * Math.Sin(DegreeToRadian(direction))));
        }

        private double DegreeToRadian(double degrees)
        {
            return Math.PI * degrees / 180.0;
        }

        private double RadianToDegree(double radians)
        {
            return radians * 180.0 / Math.PI;
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

            if (steer != 0 && currentLinearSpeed != 0)
            {
                return currentLinearSpeed;
            }

            return currentLinearSpeed + MAX_ACCELERATION * TIME_SLOT;
        }

        /// <summary>
        /// v = v0 + at
        /// </summary>
        private double CalculateAngularSpeed(double r, double linearSpeed)
        {
            return 0.5 * linearSpeed / r;
        }

        /// <summary>
        /// d = v0*t + 1/2*a*t^2
        /// </summary>
        private double CalculateDistance()
        {
            // If we are turning keep distance fixed.
            if (steer != 0 && currentDistance != 0)
            {
                return currentDistance;
            }

            // d = v0*t + 1/2*a*t^2
            var distanceDelta = currentLinearSpeed * TIME_SLOT + (MAX_ACCELERATION * TIME_SLOT * TIME_SLOT) / 2;

            // Make sure we don't go further than value requested.
            var distanceNew = Math.Min(distanceDelta, Math.Abs(forward));

            if (forward < 0)
            {
                distanceNew *= -1;
            }

            forward -= distanceNew;

            return distanceNew;
        }

        /// <summary>
        /// @ = w * t
        /// </summary>
        private double CalculateDirection(double r, double angularSpeed)
        {
            if (steer == 0)
            {
                return currentDirection;
            }

            // @ = w * t
            var directionDelta = RadianToDegree(angularSpeed * TIME_SLOT);

            // Make sure we don't go further than value requested.
            directionDelta = Math.Min(directionDelta, Math.Abs(steer));

            if (steer < 0)
            {
                directionDelta *= -1;
            }

            var directionNew = currentDirection + directionDelta;

            // Direction value is between 0-360. If the value is negative get the complement angle.
            if (directionNew < 0)
            {
                directionNew += 360;
            }

            if (directionNew > 360)
            {
                directionNew -= 360;
            }

            steer -= directionDelta;

            return directionNew;
        }

    }
}