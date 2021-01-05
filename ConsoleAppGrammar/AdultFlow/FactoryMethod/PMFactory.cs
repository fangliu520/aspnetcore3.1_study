using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.AdultFlow.FactoryMethod
{
    public class PMFactory : IFactory
    {
        public virtual AbstractAdult CreateInstance()
        {
            AbstractAdult _adult = new PM()
            {
                Name = "张三组长"
            };

            return _adult;
        }
    }

    public class PMFactoryChild : PMFactory
    {
        public override AbstractAdult CreateInstance()
        {
            Console.WriteLine("12323232");
            AbstractAdult _adult = new PM()
            {
                Name = "张三组长"
            };

            return _adult;
        }
    }
}
