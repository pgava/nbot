using nbot.actions;

namespace nbot.engine
{
    public enum ItemType
    {
        bot = 0
    }
    public interface IMove
    {
        ItemType ItemType { get; }
        Point Position { get; }
    }

    public class Move : IMove
    {
        public Move(Point position, ItemType itemType)
        {
            Position = position;
            ItemType = ItemType;
        }

        public Point Position { get; }
        public ItemType ItemType { get; }
    }
}