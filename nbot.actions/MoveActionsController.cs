using System;

namespace nbot.actions
{
    public class MoveActionsController : IMoveActionsController
    {
        private readonly IHelm helm;
        private readonly ISpeedometer speedometer;
        private double currentLinearSpeed = 0;
        private double currentAngularSpeed = 0;
        private double currentDistance = 0;
        private Vector currentPosition;
        private double forward;
        private double steer;

        public Point Position => currentPosition.Point();

        public MoveActionsController(IHelm helm, ISpeedometer speedometer)
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
            this.speedometer.SetLimits(new BotLimits());

            currentPosition = new Vector(helm.RandomPosition(), 0);

        }
        public MoveActionsController(IHelm helm, ISpeedometer speedometer, double x, double y)
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

            currentPosition = new Vector(new Point(x, y), 0);
        }

        public MoveActionsController(IHelm helm, ISpeedometer speedometer, Point position)
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

            currentPosition = new Vector(position, 0);
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
            currentPosition = CalculatePosition(currentDistance);
        }

        private Vector CalculatePosition(double distance)
        {
            var direction = CalculateDirection(currentAngularSpeed);

            return new Vector(helm.CalculatePosition(currentPosition.Point(), direction, distance), direction);
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

            if (steer != 0 && currentLinearSpeed != 0)
            {
                return currentLinearSpeed;
            }

            return speedometer.CalculateLinearSpeed(currentLinearSpeed);
        }

        /// <summary>
        /// v = v0 + at
        /// </summary>
        private double CalculateAngularSpeed(double r, double linearSpeed)
        {
            return speedometer.CalculateAngularSpeed(r, linearSpeed);
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

        /// <summary>
        /// @ = w * t
        /// </summary>
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