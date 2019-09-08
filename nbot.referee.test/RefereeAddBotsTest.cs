using System.Collections.Generic;
using System.Linq;
using nbot.contracts;
using Xunit;

namespace nbot.referee.test
{
    public class RefereeAddBotsTest
    {
        private IRandomBotsProvider randomProvider = new RandomBotsProviderMock();

        [Fact]
        public void Can_Add_Bot()
        {
            var referee = new Referee(randomProvider);
            var bot = new TesBot1();

            referee.AddBot(bot);

            var bots = referee.GetBots();

            Assert.True(bots.Count() == 1);
        }

        [Fact]
        public void Can_Add_List_Of_Bots()
        {
            var referee = new Referee(randomProvider);
            var bot = new TesBot1();

            referee.AddBots(new List<IBot> { bot, bot, bot });

            var bots = referee.GetBots();

            Assert.True(bots.Count() == 3);
        }
    }

    public class TesBot1 : IBot
    {
        public string Name => "TestBot 1";

        public void Run()
        {
            throw new System.NotImplementedException();
        }
    }

    public class TesBot2 : IBot
    {
        public string Name => "TestBot 2";

        public void Run()
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
