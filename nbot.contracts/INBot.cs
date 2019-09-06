using System;

namespace nbot.contracts
{
    public interface INBot
    {
        string Name { get; }
        void Run();
    }
}
