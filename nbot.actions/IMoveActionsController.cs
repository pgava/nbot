namespace nbot.actions
{
    public interface IMoveActionsController : IMoveActions
    {
        Vector Position { get; }
        void CalculateNextPosition();
    }
}