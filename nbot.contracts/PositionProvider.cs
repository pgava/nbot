using System;

namespace nbot.contracts
{
    public class PositionProvider : IPositionProvider
    {
        private const double MAX_ACCELERATION = 4D;
        private const double TIME_SLOT = 2D;
        private const double MAX_LINEAR_SPEED = 100;
        private readonly IScreenProvider screenProvider;
        private double currentLinearSpeed = 0;
        private double currentAngularSpeed = 0;
        private double currentX;
        private double currentY;
        private double previousX;
        private double previousY;
        private double currentDirection = 0;
        private double currentDistance = 0;
        private double forward;
        private double steer;

        public double X => currentX;
        public double Y => currentY;

        public PositionProvider(IScreenProvider screenProvider, double x, double y)
        {
            if (screenProvider is null)
            {
                throw new ArgumentNullException(nameof(screenProvider));
            }

            this.screenProvider = screenProvider;
            currentX = x;
            currentY = y;
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
            currentX = CalculateHorizontalPosition(currentDistance, currentDirection);
            currentY = CalculateVerticalPosition(currentDistance, currentDirection); ;

        }

        private double CalculateHorizontalPosition(double distance, double direction)
        {
            if (steer != 0 && previousX == 0)
            {
                previousX = currentX;
            }
            else
            {
                previousX = currentX;
            }

            return previousX + screenProvider.HorizontalDirection(distance * Math.Cos(DegreeToRadian(direction)), direction);
        }

        private double CalculateVerticalPosition(double distance, double direction)
        {
            if (steer != 0 && previousY == 0)
            {
                previousY = currentY;
            }
            else
            {
                previousY = currentY;
            }

            return previousY + screenProvider.VeriticalDirection(distance * Math.Sin(DegreeToRadian(direction)), direction);
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
            return linearSpeed / r;
        }

        /// <summary>
        /// d = v0t + 1/2at^2
        /// </summary>
        private double CalculateDistance()
        {
            if (steer != 0 && currentDistance != 0)
            {
                return currentDistance;
            }

            var distanceDelta = currentLinearSpeed * TIME_SLOT + (MAX_ACCELERATION * TIME_SLOT * TIME_SLOT) / 2;

            var distance = Math.Min(distanceDelta, Math.Abs(forward));

            forward -= distance;

            return distance;
        }

        /// <summary>
        /// @ = w * t
        /// </summary>
        private double CalculateDirection(double r, double angularSpeed)
        {
            var directionDelta = RadianToDegree(angularSpeed * TIME_SLOT);
            var directionNew = currentDirection + directionDelta;

            if (directionNew < 0 || directionNew > 360) directionNew = 0;

            steer -= directionDelta;

            return directionNew;
        }

    }
}