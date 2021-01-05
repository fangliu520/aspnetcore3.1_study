using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.AdultFlow.FactoryMethod
{
    public class ManagerFactory : IFactory
    {
        public  AbstractAdult CreateInstance()
        {
            AbstractAdult _adult = new Manager()
            {
                Name = "王五经理"
            };

            return _adult;
        }
    }
}
