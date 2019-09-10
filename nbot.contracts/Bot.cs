using System;
using System.Threading;

namespace nbot.contracts
{
    public abstract class Bot : IBot, IActions
    {
        public abstract void PlayTurn();

        public void EndTurn()
        {
            throw new NotImplementedException();
        }

        public void MoveAhead(int d)
        {
            throw new NotImplementedException();
        }
    }
}
