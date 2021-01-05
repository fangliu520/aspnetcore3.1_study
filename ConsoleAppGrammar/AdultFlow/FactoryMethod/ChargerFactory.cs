using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.AdultFlow.FactoryMethod
{
    class ChargerFactory : IFactory
    {
        public  AbstractAdult CreateInstance()
        {
            AbstractAdult _adult = new Charger()
            {
                Name = "李四主管"
            };

            return _adult;
        }
    }
}
