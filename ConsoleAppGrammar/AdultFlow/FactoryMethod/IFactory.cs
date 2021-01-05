using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.AdultFlow.FactoryMethod
{
    public interface IFactory
    {
        AbstractAdult CreateInstance();
    }
}
