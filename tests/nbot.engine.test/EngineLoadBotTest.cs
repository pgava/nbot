using Xunit;

namespace nbot.engine.test
{
    public class EngineLoadBotTest
    {
        [Fact]
        public void Can_Load_Bot_From_Assembly()
        {
            var task = TestHelper.CreateBot(TestHelper.GetAssembly());

            task.PlayTurn();
        }
    }
}
