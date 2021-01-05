using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.FlyweightPattern
{
    public class E : BaseWord
    {
        public E() {
            Console.WriteLine("E被构造");
        }
        public override string Get()
        {
           return this.GetType().Name;
        }
    }
}
