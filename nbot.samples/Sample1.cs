using System;
using nbot.contracts;

namespace nbot.samples
{
    public class Sample1 : Bot
    {
        public string Name => "Sampe 1";

        public override void PlayTurn()
        {
            System.Threading.Thread.Sleep(200);
            Console.WriteLine($"This is {Name}");
        }
    }
}
