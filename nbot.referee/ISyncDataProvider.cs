using System.Collections.Generic;

namespace nbot.referee
{
    public interface ISyncDataProvider
    {
        void SyncMoves(IEnumerable<IMove> moves);
    }
}