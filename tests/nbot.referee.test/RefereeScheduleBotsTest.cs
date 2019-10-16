using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Xunit;
using FluentAssertions;

namespace nbot.referee.test
{
    public class RefereeScheduleBotsTest
    {
        [Fact]
        public void Should_Be_Able_To_Stop_Bot_After_Turn()
        {
            BotController botController = StartBot();

            botController.IsWaiting.Should().BeTrue();
        }

        [Fact]
        public void Should_Be_Able_To_Start_Bot_For_Next_Turn()
        {
            BotController botController = StartBot();

            botController.Wakeup();
            
            botController.IsRunning.Should().BeTrue();
        }

        private static BotController StartBot()
        {
            var botScheduler = new BotScheduler();
            var bots = TestHelper.LoadBots(TestHelper.GetAssembly());

            var botController = new BotController(botScheduler, bots.FirstOrDefault());

            Task doBot = new Task(() => botController.Turn());
            doBot.Start();

            Thread.Sleep(500);

            return botController;
        }

    }

}
