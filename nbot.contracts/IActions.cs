namespace nbot.contracts
{
    public interface IActions
    {
        void Ahead(decimal d);
        void Back(decimal d);
        void Right(decimal d);
        void Left(decimal d);
    }
}