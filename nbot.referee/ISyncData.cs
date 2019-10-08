using System.Collections.Generic;

namespace nbot.referee
{
    public interface ISyncData
    {
        void SyncMoves(IEnumerable<IMove> moves);
    }
}