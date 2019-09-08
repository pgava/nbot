using System;
using nbot.contracts;

namespace nbot.samples
{
    public class Sample1 : Bot
    {
        public string Name => "Sampe 1";

        public override void Run()
        {
            Console.WriteLine($"This is {Name}");
        }
    }
}
