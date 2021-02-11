namespace nbot.actions
{
    public interface IMovementManager
    {
        bool HasReachedMaxSpeed(double speed);
        void SetLimits(ILimits limits);
        double CalculateDirection(double angularSpeed);
        double CalculateDistance(double speed);
        double CalculateLinearSpeed(double speed);
        double CalculateAngularSpeed(double r, double linearSpeed);
    }
}