using System.Collections.Generic;
using System.Linq;
using Xunit;
using FluentAssertions;

namespace nbot.referee.test
{
    public class RefereeGetBotsTest
    {
        [Fact]
        public void Can_Get_Bots_In_Random_Order()
        {
            var botCollection = new BotControllerCollection(new RandomBotsProvider());
            var bot1 = new BaseBot("b1");
            var bot2 = new BaseBot("b2");
            var bot3 = new BaseBot("b3");
            var bot4 = new BaseBot("b4");

            botCollection.AddBots(new List<IBotController> { bot1, bot1, bot3, bot2, bot2, bot4, bot4 });

            var bots = botCollection.GetRndBots();

            bots.Should().HaveCount(7);
            (bots.First() == bot1 &&
                bots.Skip(1).First() == bot1 &&
                bots.Skip(2).First() == bot3 &&
                bots.Skip(3).First() == bot2 &&
                bots.Skip(4).First() == bot2 &&
                bots.Skip(5).First() == bot4 &&
                bots.Skip(6).First() == bot4
            ).Should().BeFalse();
            bots.Count(b => b == bot1).Should().Be(2);
            bots.Count(b => b == bot2).Should().Be(2);
            bots.Count(b => b == bot3).Should().Be(1);
            bots.Count(b => b == bot4).Should().Be(2);
        }
    }

}
