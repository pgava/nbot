namespace nbot.actions
{
    public interface ISpeedometer
    {
        double CalculateAngularSpeed(double r, double linearSpeed);
        double CalculateDirection(double angularSpeed);
        double CalculateDistance(double speed);
        double CalculateLinearSpeed(double speed);
        bool HasMaxSpeed(double speed);
        void SetLimits(ILimits limits);
    }
}