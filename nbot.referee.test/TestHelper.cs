using System.Collections.Generic;
using nbot.contracts;

namespace nbot.referee.test
{
    public class TesBot1 : Bot
    {
        public string Name => "TestBot 1";

        override public void PlayTurn()
        {
            throw new System.NotImplementedException();
        }
    }

    public class TesBot2 : Bot
    {
        public string Name => "TestBot 2";

        override public void PlayTurn()
        {
            throw new System.NotImplementedException();
        }
    }

    public class RandomBotsProviderMock : IRandomBotsProvider
    {
        public IEnumerable<IBot> RandomizeList(IList<IBot> items)
        {
            throw new System.NotImplementedException();
        }
    }
}
