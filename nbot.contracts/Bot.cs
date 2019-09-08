using System;

namespace nbot.contracts
{
    public abstract class Bot : IBot, IPlay
    {
        public abstract void Run();

        public void Round()
        {
            while (true)
            {
                Run();

                // wait here
            }
        }
    }
}
