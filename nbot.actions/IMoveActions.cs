namespace nbot.actions
{
    public interface IMoveActions
    {
        void MoveAhead(double a);
        void MoveBack(double b);
        void TurnRight(double r);
        void TurnLeft(double l);
    }
}