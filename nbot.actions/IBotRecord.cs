using System.Collections.Generic;

namespace nbot.actions
{
    public interface IBotRecord
    {
        string Name { get; }
        Point Position { get; }

        IEnumerable<IRocketrecord> Rockets { get; }
    }

    public interface IRocketrecord
    {
        string Name { get; }
        Point Position { get; }
    }
}