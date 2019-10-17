using System.Collections.Generic;
using System.Threading.Tasks;
using nbot.contract;

namespace nbot.engine
{

    public class TaskManager : ITaskManager
    {
        public void StartBots(IEnumerable<IBotController> bots)
        {
            foreach (var b in bots)
            {
                var t = new Task(() => b.Turn());
            }
        }
    }
}
