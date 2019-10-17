using System.Collections.Generic;

namespace nbot.engine
{
    public interface ISyncData
    {
        void SyncMoves(IEnumerable<IMove> moves);
    }
}