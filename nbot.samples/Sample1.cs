using System;
using nbot.contracts;

namespace nbot.samples
{
    public class Sample1 : Bot
    {
        public string Name => "Sampe 1";

        public override void Run()
        {
            // run forever 
            //while (true)
            //{
                Console.WriteLine($"This is {Name}");
                // ...
                // do your logic here,
                // then when done end the turn
                Execute();
            //}
        }
    }
}
