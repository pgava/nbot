using System.Collections.Generic;

namespace nbot.actions
{
    public interface IBotRecord
    {
        string Name { get; }
        Point Position { get; }

        IEnumerable<IRocketRecord> Rockets { get; }
    }
}