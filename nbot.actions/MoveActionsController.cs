using System;
using nbot.common;

namespace nbot.actions
{
    public class MoveActionsController : IMoveActionsController
    {
        private readonly IPositionProvider positionProvider;
        private readonly IMovementController movementController;
        private double forward;
        private double steer;
        private double currentLinearSpeed = 0;
        private double currentAngularSpeed = 0;
        private double currentDistance = 0;
        private Vector currentPosition;

        public Vector Position => currentPosition;

        public MoveActionsController(IPositionProvider positionProvider, IMovementController movementController)
        {
            Validation.ThrowIfArgumentIsNull(positionProvider);
            Validation.ThrowIfArgumentIsNull(movementController);

            this.positionProvider = positionProvider;
            this.movementController = movementController;
            this.movementController.SetLimits(new BotLimits());

            currentPosition = new Vector(positionProvider.RandomPosition(), 0);

        }
        public MoveActionsController(IPositionProvider positionProvider, IMovementController movementController, double x, double y)
        {
            Validation.ThrowIfArgumentIsNull(positionProvider);
            Validation.ThrowIfArgumentIsNull(movementController);

            this.positionProvider = positionProvider;
            this.movementController = movementController;

            currentPosition = new Vector(new Point(x, y), 0);
        }

        public MoveActionsController(IPositionProvider positionProvider, IMovementController movementController, Point position)
        {
            Validation.ThrowIfArgumentIsNull(positionProvider);
            Validation.ThrowIfArgumentIsNull(movementController);

            this.positionProvider = positionProvider;
            this.movementController = movementController;

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
            // Check if ok to move
            if (forward == 0)
            {
                return;
            }

            var nextLinearSpeed = CalculateLinearSpeed(currentLinearSpeed);
            var nextDistance = CalculateDistance(currentDistance, nextLinearSpeed);
            var nextAngularSpeed = CalculateAngularSpeed(nextDistance, nextLinearSpeed);
            var nextPosition = CalculatePosition(currentPosition, nextAngularSpeed, nextDistance);

            // Set the new current values
            currentLinearSpeed = nextLinearSpeed;
            currentDistance = nextDistance;
            currentAngularSpeed = nextAngularSpeed;
            currentPosition = nextPosition;

        }
        
        private double CalculateLinearSpeed(double initialSpeed)
        {
            // Cannot go faster than the max speed
            if (movementController.HasReachedMaxSpeed(initialSpeed))
            {
                return initialSpeed;
            }

            // If the object is steering linear speed is fixed
            if (steer != 0 && initialSpeed != 0)
            {
                return initialSpeed;
            }

            return movementController.CalculateLinearSpeed(initialSpeed);
        }

        private double CalculateDistance(double distance, double initialSpeed)
        {
            // If the object is turning keep distance fixed.
            if (steer != 0 && distance != 0)
            {
                return distance;
            }

            var distanceDelta = movementController.CalculateDistance(initialSpeed);

            // Make sure we don't go further than value requested.
            var distanceNew = Math.Min(distanceDelta, Math.Abs(forward));

            // Update how far the object should still move
            if (forward < 0)
            {
                distanceNew *= -1;
            }

            forward -= distanceNew;

            return distanceNew;
        }

        private double CalculateAngularSpeed(double distance, double initialSpeed)
        {
            return movementController.CalculateAngularSpeed(Math.Abs(distance), initialSpeed);
        }

        private Vector CalculatePosition(Vector position, double angularSpeed, double distance)
        {
            var direction = CalculateDirection(position, angularSpeed);

            return new Vector(positionProvider.CalculatePosition(position.Point(), direction, distance), direction);
        }
        
        private double CalculateDirection(Vector position, double angularSpeed)
        {
            // If not turning the direction is the same.
            if (steer == 0)
            {
                return position.Direction;
            }

            var directionDelta = movementController.CalculateDirection(angularSpeed);

            // Make sure we don't go further than the value requested.
            directionDelta = Math.Min(directionDelta, Math.Abs(steer));

            // Update how far the object should still turn
            if (steer < 0)
            {
                directionDelta *= -1;
            }

            var directionNew = position.Direction + directionDelta;

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