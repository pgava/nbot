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
            var botCollection = new BotControllerCollection(randomProvider);
            var bot = new BaseBot("b1");

            botCollection.AddBot(bot);

            var bots = botCollection.GetBots();

            Assert.True(botCollection.GetBots().Count() == 1);
        }

        [Fact]
        public void Can_Add_List_Of_Bots()
        {
            var botCollection = new BotControllerCollection(randomProvider);
            var bot = new BaseBot("b1");

            botCollection.AddBots(new List<IBotController> { bot, bot, bot });

            var bots = botCollection.GetBots();

            Assert.True(botCollection.GetBots().Count() == 3);
        }
    }
}
