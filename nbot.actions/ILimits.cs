namespace nbot.actions
{
    public interface ILimits
    {
        double MaxAcceleration { get; }
        double TimeSlot { get; }
        double MaxLinearSpeed { get; }
    }
}