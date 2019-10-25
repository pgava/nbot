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
        private readonly IHelm helm;
        private readonly ISpeedometer speedometer;
        private double currentLinearSpeed;
        private Vector currentPosition;
        private RocketStatus status = RocketStatus.Fired;

        public Point Position => currentPosition.Point();

        public Rocket(IHelm helm, ISpeedometer speedometer)
        {
            if (helm is null)
            {
                throw new ArgumentNullException(nameof(helm));
            }

            if (speedometer is null)
            {
                throw new ArgumentNullException(nameof(speedometer));
            }

            this.helm = helm;
            this.speedometer = speedometer;
            this.speedometer.SetLimits(new RocketLimits());

        }

        public void CalculateTrajectory(Vector startAt)
        {
            if (status == RocketStatus.Fired)
            {
                status = RocketStatus.OnTheWay;
                currentPosition = startAt;
                return;
            }

            var currentDistance = CalculateDistance();
            currentLinearSpeed = CalculateLinearSpeed();
            currentPosition = CalculatePosition(currentDistance);
        }

        private Vector CalculatePosition(double distance)
        {
            return new Vector(helm.CalculatePosition(currentPosition.Point(), currentPosition.Direction, distance), currentPosition.Direction);
        }

        /// <summary>
        /// v = v0 + at
        /// </summary>
        private double CalculateLinearSpeed()
        {
            if (speedometer.HasMaxSpeed(currentLinearSpeed))
            {
                return currentLinearSpeed;
            }

            return speedometer.CalculateLinearSpeed(currentLinearSpeed);
        }

        /// <summary>
        /// d = v0*t + 1/2*a*t^2
        /// </summary>
        private double CalculateDistance()
        {

            // d = v0*t + 1/2*a*t^2
            var distanceNew = speedometer.CalculateDistance(currentLinearSpeed);

            return distanceNew;
        }
    }
}