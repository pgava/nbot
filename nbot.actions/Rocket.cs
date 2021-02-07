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
        private readonly IPositionProvider positionProvider;
        private readonly ISpeedometer speedometer;
        private double currentLinearSpeed;
        private Vector currentPosition;
        private RocketStatus status = RocketStatus.Fired;

        public Point Position => currentPosition.Point();

        public Rocket(IPositionProvider positionProvider, ISpeedometer speedometer)
        {
            if (positionProvider is null)
            {
                throw new ArgumentNullException(nameof(positionProvider));
            }

            if (speedometer is null)
            {
                throw new ArgumentNullException(nameof(speedometer));
            }

            this.positionProvider = positionProvider;
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
            return new Vector(positionProvider.CalculatePosition(currentPosition.Point(), currentPosition.Direction, distance), currentPosition.Direction);
        }

        private double CalculateLinearSpeed()
        {
            if (speedometer.HasMaxSpeed(currentLinearSpeed))
            {
                return currentLinearSpeed;
            }

            return speedometer.CalculateLinearSpeed(currentLinearSpeed);
        }

        private double CalculateDistance()
        {

            var distanceNew = speedometer.CalculateDistance(currentLinearSpeed);

            return distanceNew;
        }
    }
}