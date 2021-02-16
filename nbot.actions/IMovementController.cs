namespace nbot.actions
{
    /// <summary>
    /// Controls the movements of an object based on speed and direction.
    /// </summary>
    public interface IMovementController
    {
        bool HasReachedMaxSpeed(double speed);
        void SetLimits(ILimits limits);
        double CalculateDirection(double angularSpeed);

        /// <summary>
        /// Calculates the distance that an object travel based on its speed.
        /// </summary>
        /// <param name="speed">The current speed.</param>
        /// <returns>The distance.</returns>
        double CalculateDistance(double speed);

        /// <summary>
        /// Calculates the speed of an object based on its current speed.
        /// </summary>
        /// <param name="speed">The current speed</param>
        /// <returns>The speed.</returns>
        double CalculateLinearSpeed(double speed);

        /// <summary>
        /// Calculates the angular speed of an object based on the ray and linear speed.
        /// </summary>
        /// <param name="r">The ray.</param>
        /// <param name="linearSpeed">The linear spped.</param>
        /// <returns>The angular speed.</returns>
        double CalculateAngularSpeed(double r, double linearSpeed);
    }
}