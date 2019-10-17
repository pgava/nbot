namespace nbot.actions
{
    public interface IActions
    {
        void Ahead(double d);
        void Back(double d);
        void Right(double d);
        void Left(double d);
    }
}