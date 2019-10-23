using System;
using nbot.actions.screens;

namespace nbot.actions
{
    enum RocketStatus
    {
        Fired,
        OnTheWay
    }

    public class Rocket : IRocket
    {
        private const double MAX_ACCELERATION = 10D;
        private const double TIME_SLOT = 2D;
        private const double MAX_LINEAR_SPEED = 100D;
        private readonly IScreenProperties screenProperties;
        private double currentLinearSpeed;
        private Vector currentPosition;
        private RocketStatus status = RocketStatus.Fired;

        public Point Position => currentPosition.Point();

        public Rocket(IScreenProperties screenProperties)
        {
            if (screenProperties is null)
            {
                throw new ArgumentNullException(nameof(screenProperties));
            }

            this.screenProperties = screenProperties;
        }

        public void CalculateNextPosition(Vector currentBotPosition)
        {
            if (status == RocketStatus.Fired)
            {
                status = RocketStatus.OnTheWay;
                currentPosition = currentBotPosition;
                return; 
            }

            var currentDistance = CalculateDistance();
            currentLinearSpeed = CalculateLinearSpeed();
            currentPosition = CalculatePosition(currentDistance);
        }

        private Vector CalculatePosition(double distance)
        {
            return new Vector(screenProperties.CheckLimits(currentPosition.Point(),
                                new Point(distance * Math.Cos(DegreeToRadian(currentPosition.Direction)), distance * Math.Sin(DegreeToRadian(currentPosition.Direction))), false),
                              currentPosition.Direction);
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