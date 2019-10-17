namespace nbot.engine
{
    public enum ItemType
    {
        bot = 0
    }
    public interface IMove
    {
        ItemType ItemType { get; }
        int PosX { get; }
        int PosY { get; }
    }
}