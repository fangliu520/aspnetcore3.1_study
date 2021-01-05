using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.FlyweightPattern
{
    public class L : BaseWord
    {
        public L()
        {
            Console.WriteLine("L被构造");
        }
        public override string Get()
        {
            return this.GetType().Name;
        }
    }
}
