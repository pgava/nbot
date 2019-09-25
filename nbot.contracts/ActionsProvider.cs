using System;

namespace nbot.contracts
{
    public class ActionsProvider : IActionsProvider
    {
        private const double MAX_ACCELERATION = 5;
        private const double TIME_SLOT = 10;
        private readonly IScreenProvider screenProvider;
        private double currentLinearSpeed = 0;
        private double currentAngularSpeed = 0;
        private double currentX;
        private double currentY;
        private double currentDirection = 0;
        private double forward;
        private double steer;

        public ActionsProvider(IScreenProvider screenProvider)
        {
            if (screenProvider is null)
            {
                throw new ArgumentNullException(nameof(screenProvider));
            }

            this.screenProvider = screenProvider;
        }
        public void Ahead(double d)
        {
            forward = d;
        }
        public void Back(double d)
        {
            forward = -d;
        }
        public void Right(double d)
        {
            steer = d;
        }
        public void Left(double d)
        {
            steer = -d;
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

        /// <summary>
        /// s = v0t + 1/2at^2
        /// </summary>
        private void CalculatePosition()
        {
            var distance = CalculateDistance();
            distance = Math.Min(distance, Math.Abs(forward));
            if (forward < 0)
            {
                distance *= -1;
            }

            currentX += screenProvider.HorizontalDirection(distance * Math.Cos(DegreeToRadian(currentDirection)), currentDirection);
            currentY += screenProvider.VeriticalDirection(distance * Math.Sin(DegreeToRadian(currentDirection)), currentDirection);

            currentLinearSpeed = CalculateLinearSpeed();
            currentAngularSpeed = CalculateAngularSpeed(Math.Abs(distance));
            forward -= distance;
        }
    }
}