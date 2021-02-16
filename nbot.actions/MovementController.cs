using System;
using nbot.common;

namespace nbot.actions
{
    public class BotLimits : ILimits
    {
        public double MaxAcceleration => 3D;
        public double TimeSlot => 2D;
        public double MaxLinearSpeed => 50D;
    }

    public class RocketLimits : ILimits
    {
        public double MaxAcceleration => 10D;
        public double TimeSlot => 2D;
        public double MaxLinearSpeed => 100D;
    }

    public class MovementController : IMovementController
    {
        private ILimits limits;

        public void SetLimits(ILimits limits)
        {
            Validation.ThrowIfArgumentIsNull(limits);

            this.limits = limits;
        }
        
        public bool HasReachedMaxSpeed(double speed)
        {
            Validation.ThrowIfArgumentIsNull(limits);

            return speed >= limits.MaxLinearSpeed;
        }

        /// <summary>
        /// v = v0 + at
        /// </summary>
        public double CalculateLinearSpeed(double speed)
        {
            Validation.ThrowIfArgumentIsNull(limits);

            return speed + limits.MaxAcceleration * limits.TimeSlot;
        }

        /// <summary>
        /// v = v0 / r
        /// </summary>
        public double CalculateAngularSpeed(double r, double linearSpeed)
        {
            return linearSpeed / r;
        }

        /// <summary>
        /// d = v0*t + 1/2*a*t^2
        /// </summary>
        public double CalculateDistance(double speed)
        {
            Validation.ThrowIfArgumentIsNull(limits);

            return speed * limits.TimeSlot + (limits.MaxAcceleration * Math.Pow(limits.TimeSlot, 2)) / 2;
        }

        /// <summary>
        /// @ = w * t
        /// </summary>
        public double CalculateDirection(double angularSpeed)
        {
            Validation.ThrowIfArgumentIsNull(limits);
            
            // @ = w * t
            return NBotMath.RadianToDegree(angularSpeed * limits.TimeSlot);
        }
       
    }
}