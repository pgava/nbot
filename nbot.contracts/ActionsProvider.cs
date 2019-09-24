using System;

namespace nbot.contracts
{
    public class ActionProvider : IActions
    {
        private const double MAX_ACCELERATION = 5;
        private const double TIME_SLOT = 10;
        private double currentLinearSpeed = 0;
        private double currentAngularSpeed = 0;
        private double currentX;
        private double currentY;
        private double currentDirection = 0;
        private double forward;

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

        }
        public void Left(double d)
        {

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
        /// d = v0t + 1/2at^2
        /// </summary>
        private double CalculateDistance()
        {
            return currentLinearSpeed * TIME_SLOT + (MAX_ACCELERATION * TIME_SLOT * TIME_SLOT) / 2;
        }

        /// <summary>
        /// s = v0t + 1/2at^2
        /// </summary>
        private void CalculatePosition()
        {
            var distance = CalculateDistance();

            currentX = distance * Math.Cos(DegreeToRadian(currentDirection));
            currentY = distance * Math.Sin(DegreeToRadian(currentDirection));

            currentLinearSpeed = CalculateLinearSpeed();
        }
    }
}