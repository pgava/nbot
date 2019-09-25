namespace nbot.contracts
{
    public interface IActionsProvider
    {
        void Ahead(double d);
        void Back(double d);
        void Right(double d);
        void Left(double d);
    }
}