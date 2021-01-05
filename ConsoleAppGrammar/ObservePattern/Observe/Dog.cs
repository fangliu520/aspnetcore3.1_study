using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.ObservePattern.Observe
{
    public class Dog : IObserve
    {
        public void Action()
        {
            this.Wang();
        }

        public void Wang()
        {
            Console.WriteLine($"{this.GetType().Name} Wang");
        }
    }
}
