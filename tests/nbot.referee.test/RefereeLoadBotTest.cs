using Xunit;

namespace nbot.referee.test
{
    public class RefereeLoadBotTest
    {
        [Fact]
        public void Can_Load_Bot_From_Assembly()
        {
            var task = TestHelper.CreateBot(TestHelper.GetAssembly());

            task.PlayTurn();
        }
    }
}
