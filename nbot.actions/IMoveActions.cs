namespace nbot.actions
{
    public interface IMoveActions
    {
        void Ahead(double d);
        void Back(double d);
        void Right(double d);
        void Left(double d);

        void SetPosition(IBotPosition position);
    }
}