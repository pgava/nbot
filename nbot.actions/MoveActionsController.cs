using System;
using nbot.common;

namespace nbot.actions
{
    public class MoveActionsController : IMoveActionsController
    {
        private readonly IPositionProvider positionProvider;
        private readonly IMovementManager speedometer;
        private double currentLinearSpeed = 0;
        private double currentAngularSpeed = 0;
        private double currentDistance = 0;
        private Vector currentPosition;
        private double forward;
        private double steer;

        public Vector Position => currentPosition;

        public MoveActionsController(IPositionProvider positionProvider, IMovementManager speedometer)
        {
            Validation.ThrowIfArgumentIsNull(positionProvider);
            Validation.ThrowIfArgumentIsNull(speedometer);

            this.positionProvider = positionProvider;
            this.speedometer = speedometer;
            this.speedometer.SetLimits(new BotLimits());

            currentPosition = new Vector(positionProvider.RandomPosition(), 0);

        }
        public MoveActionsController(IPositionProvider positionProvider, IMovementManager speedometer, double x, double y)
        {
            Validation.ThrowIfArgumentIsNull(positionProvider);
            Validation.ThrowIfArgumentIsNull(speedometer);

            this.positionProvider = positionProvider;
            this.speedometer = speedometer;

            currentPosition = new Vector(new Point(x, y), 0);
        }

        public MoveActionsController(IPositionProvider positionProvider, IMovementManager speedometer, Point position)
        {
            Validation.ThrowIfArgumentIsNull(positionProvider);
            Validation.ThrowIfArgumentIsNull(speedometer);

            this.positionProvider = positionProvider;
            this.speedometer = speedometer;

            currentPosition = new Vector(position, 0);
        }

        public void MoveAhead(double d)
        {
            forward = d;
        }

        public void MoveBack(double d)
        {
            forward = -d;
        }

        public void TurnRight(double d)
        {
            steer = d;
        }

        public void TurnLeft(double d)
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
            currentPosition = CalculatePosition(currentDistance);
        }

        private Vector CalculatePosition(double distance)
        {
            var direction = CalculateDirection(currentAngularSpeed);

            return new Vector(positionProvider.CalculatePosition(currentPosition.Point(), direction, distance), direction);
        }

        private double CalculateLinearSpeed()
        {
            if (speedometer.HasReachedMaxSpeed(currentLinearSpeed))
            {
                return currentLinearSpeed;
            }

            if (steer != 0 && currentLinearSpeed != 0)
            {
                return currentLinearSpeed;
            }

            return speedometer.CalculateLinearSpeed(currentLinearSpeed);
        }

        private double CalculateAngularSpeed(double r, double linearSpeed)
        {
            return speedometer.CalculateAngularSpeed(r, linearSpeed);
        }

        private double CalculateDistance()
        {
            // If we are turning keep distance fixed.
            if (steer != 0 && currentDistance != 0)
            {
                return currentDistance;
            }

            var distanceDelta = speedometer.CalculateDistance(currentLinearSpeed);

            // Make sure we don't go further than value requested.
            var distanceNew = Math.Min(distanceDelta, Math.Abs(forward));

            if (forward < 0)
            {
                distanceNew *= -1;
            }

            forward -= distanceNew;

            return distanceNew;
        }

        private double CalculateDirection(double angularSpeed)
        {
            if (steer == 0)
            {
                return currentPosition.Direction;
            }

            var directionDelta = speedometer.CalculateDirection(angularSpeed);

            // Make sure we don't go further than value requested.
            directionDelta = Math.Min(directionDelta, Math.Abs(steer));

            if (steer < 0)
            {
                directionDelta *= -1;
            }

            var directionNew = currentPosition.Direction + directionDelta;

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