using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.ObservePattern.Observe
{
    public class Mouse : IObserve
    {
        public void Action()
        {
            Run();
        }

        public void Run()
        {
            Console.WriteLine($"{this.GetType().Name} Run");
        }
    }
}
