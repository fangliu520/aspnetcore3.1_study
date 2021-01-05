using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.ObservePattern.Observe
{
    public class Baby:IObserve
    {
        public void Action()
        {
            this.Cry();
        }

        public void Cry()
        {
            Console.WriteLine($"{this.GetType().Name} Cry");
        }
    }
}
