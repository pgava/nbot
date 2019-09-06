using System;
using nbot.contracts;

namespace nbot.samples
{
    public class Sample1 : INBot
    {
        public string Name => "Sampe 1";

        public void Run() 
        {
            Console.WriteLine("This is Sample1");
        }
    }
}
