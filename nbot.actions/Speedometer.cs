using System;

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

    public interface ILimits
    {
        double MaxAcceleration { get; }
        double TimeSlot { get; }
        double MaxLinearSpeed { get; }
    }

    public class Speedometer : ISpeedometer
    {
        private ILimits limits;

        public void SetLimits(ILimits limits)
        {
            if (limits is null)
            {
                throw new ArgumentNullException(nameof(limits));
            }

            this.limits = limits;
        }
        public bool HasMaxSpeed(double speed)
        {
            ThrowIfParameterIsNull(limits);

            return speed >= limits.MaxLinearSpeed;
        }

        /// <summary>
        /// v = v0 + at
        /// </summary>
        public double CalculateLinearSpeed(double speed)
        {
            ThrowIfParameterIsNull(limits);

            return speed + limits.MaxAcceleration * limits.TimeSlot;
        }

        /// <summary>
        /// v = v0 / r
        /// </summary>
        public double CalculateAngularSpeed(double r, double linearSpeed)
        {
            return 0.5 * linearSpeed / r;
        }

        /// <summary>
        /// d = v0*t + 1/2*a*t^2
        /// </summary>
        public double CalculateDistance(double speed)
        {
            ThrowIfParameterIsNull(limits);

            return speed * limits.TimeSlot + (limits.MaxAcceleration * limits.TimeSlot * limits.TimeSlot) / 2;
        }

        /// <summary>
        /// @ = w * t
        /// </summary>
        public double CalculateDirection(double angularSpeed)
        {
            ThrowIfParameterIsNull(limits);
            
            // @ = w * t
            return RadianToDegree(angularSpeed * limits.TimeSlot);
        }

        private double RadianToDegree(double radians)
        {
            return radians * 180.0 / Math.PI;
        }

        private void ThrowIfParameterIsNull<T>(T parameter)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException();
            }
        }
    }
}