using System.Collections.Generic;
using System.Linq;
using nbot.contracts;
using Xunit;

namespace nbot.referee.test
{
    public class RefereeGetBotsTest
    {
        [Fact]
        public void Can_Get_Bots_In_Random_Order()
        {
            var referee = new Referee(new RandomBotsProvider());
            var bot1 = new TesBot1();
            var bot2 = new TesBot2();

            referee.AddBots(new List<IBot> { bot1, bot1, bot2, bot2 });

            var bots = referee.GetRndBots();

            Assert.True(bots.Count() == 4);
            Assert.False(bots.First() == bot1 &&
                bots.Skip(1).First() == bot1 &&
                bots.Skip(2).First() == bot2 &&
                bots.Skip(3).First() == bot2
            );
            Assert.True(bots.Count(b => b == bot1) == 2);
            Assert.True(bots.Count(b => b == bot2) == 2);
        }
    }

}
