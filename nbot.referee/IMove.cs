namespace nbot.referee
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