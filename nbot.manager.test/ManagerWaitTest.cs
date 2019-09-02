using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;
using nbot.contracts;
using Xunit;

namespace nbot.manager.test
{
    public class ManagerWaitTest
    {
        private ManualResetEvent mev = new ManualResetEvent(false);

        [Fact]
        public void Should_Be_Able_To_Wait_While_Bots_Run()
        {
            Stopwatch watch = Stopwatch.StartNew();

            DoWait();

            watch.Stop();

        }

        private void DoWait()
        {
            //Thread.Sleep(10);
            mev.WaitOne(10);
        }
    }
}
