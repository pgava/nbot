using System;
using System.Diagnostics;
using System.Threading;
using Xunit;

namespace nbot.manager.test
{
    /// <summary>
    /// The task that manage all bots have to wait for a predefined amount of time,
    /// while bots do their moves, and then re-start the execution and perform all 
    /// necessary computations.
    /// The "Wait for a predefined amount of time" is not easy to achieve in a system 
    /// like Windows or Linux. Context swith is expensive and can happen anytime.
    /// On the other end for the purpose of this work, time doesn't have to be super
    /// accurate, we would have implement this with an RTOS if it was.
    /// Here as an example I used 3 different ways to wait.
    /// Te first is Thread.Sleep very simple but has the worst performance. In 1000
    /// run it goes out of scale average 60 times. Where for out of scale I mean the 
    /// actual wait is longer or shorter more than 10% of the predefined timeout.
    /// The second is using ManualResetEvent this works better and goes out of scale 
    /// average 30 times.
    /// The third solution just do a spin, which is very expensive on the CPU but
    /// never goes out of scale.
    /// </summary>
    public class ManagerWaitTest
    {
        private ManualResetEvent mev = new ManualResetEvent(false);
        private const int maxwaitms = 10;

        [Fact]
        public void Should_Be_Able_To_Wait_While_Bots_Run_Sleep()
        {
            DoTest(DoWaitSleep);
        }

        [Fact]
        public void Should_Be_Able_To_Wait_While_Bots_Run_ManualResetEvent()
        {
            DoTest(DoWaitManualResetEvent);
        }

        [Fact]
        public void Should_Be_Able_To_Wait_While_Bots_Run_Spin()
        {
            DoTest(DoWaitSpin);
        }

        private void DoTest(Action<int> doSleep)
        {
            var maxDiff = 0M;
            var outOfScaleCounter = 0;
            for (int i = 0; i < 1000; i++)
            {
                Stopwatch watch = Stopwatch.StartNew();

                doSleep(maxwaitms);

                watch.Stop();

                var durationMs = ((decimal)watch.ElapsedTicks / Stopwatch.Frequency) * 1000;
                var diffPercent = 100 * Math.Abs(durationMs - maxwaitms) / durationMs;

                if (diffPercent > 10)
                {
                    if (diffPercent > maxDiff) maxDiff = diffPercent;
                    outOfScaleCounter++;
                }
            }

            Console.WriteLine("*************************************");
            Console.WriteLine($"Number of time out of scale: {outOfScaleCounter} and the max error was: {maxDiff}");
            Console.WriteLine("*************************************");
        }

        private void DoWaitSleep(int timer)
        {
            Thread.Sleep(timer);
        }

        private void DoWaitManualResetEvent(int timer)
        {
            mev.WaitOne(timer);
        }

        private void DoWaitSpin(int timer)
        {
            var watch = Stopwatch.StartNew();
            var exit = false;
            while (!exit)
            {
                long timeout = timer - watch.ElapsedMilliseconds;
                if (timeout > 0)
                {
                    Thread.MemoryBarrier();
                    for (int i = 0; i < 10000; i++) ;
                }
                else
                {
                    exit = true;
                }
            }
        }
    }
}
