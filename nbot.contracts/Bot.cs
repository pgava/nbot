using System;
using System.Threading;

namespace nbot.contracts
{
    public abstract class Bot : IBot, IPlay
    {
        public abstract void Run();
        public void Execute()
        {
        }

    }
}
