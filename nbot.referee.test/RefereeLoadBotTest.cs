using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using nbot.contracts;
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
