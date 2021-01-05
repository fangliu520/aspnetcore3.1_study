using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.FlyweightPattern
{
    public class V : BaseWord
    {
        public V()
        {
            Console.WriteLine("V被构造");
        }
        public override string Get()
        {
            return this.GetType().Name;
        }
    }
}
