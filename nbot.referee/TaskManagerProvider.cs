﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace nbot.referee
{

    public class TaskManagerProvider : ITaskManagerProvider
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
