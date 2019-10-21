namespace nbot.actions
{
    public interface IMoveActionsController : IMoveActions
    {
        Point Position { get; }
        void CalculateNextPosition();
    }
}