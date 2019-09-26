using System;

namespace nbot.contracts
{
    public class PositionProvider : IPositionProvider
    {
        private const double MAX_ACCELERATION = 0.4D;
        private const double TIME_SLOT = 5D;
        private readonly IScreenProvider screenProvider;
        private double currentLinearSpeed = 0;
        private double currentAngularSpeed = 0;
        private double currentX;
        private double currentY;
        private double currentDirection = 0;
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
            var distance = CalculateDistance();
            distance = Math.Min(distance, Math.Abs(forward));
            if (forward < 0)
            {
                distance *= -1;
            }

            currentDirection += CalculateDirection(Math.Abs(distance));

            currentX += screenProvider.HorizontalDirection(distance * Math.Cos(DegreeToRadian(currentDirection)), currentDirection);
            currentY += screenProvider.VeriticalDirection(distance * Math.Sin(DegreeToRadian(currentDirection)), currentDirection);

            currentLinearSpeed = CalculateLinearSpeed();
            currentAngularSpeed = CalculateAngularSpeed(Math.Abs(distance));
            forward -= distance;
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
            return currentLinearSpeed + MAX_ACCELERATION * TIME_SLOT;
        }

        /// <summary>
        /// v = v0 + at
        /// </summary>
        private double CalculateAngularSpeed(double r)
        {
            return currentLinearSpeed / r;
        }

        /// <summary>
        /// d = v0t + 1/2at^2
        /// </summary>
        private double CalculateDistance()
        {
            return currentLinearSpeed * TIME_SLOT + (MAX_ACCELERATION * TIME_SLOT * TIME_SLOT) / 2;
        }

        /// <summary>
        /// @ = w * t
        /// </summary>
        private double CalculateDirection(double r)
        {
            return CalculateAngularSpeed(r) * TIME_SLOT;
        }

    }
}