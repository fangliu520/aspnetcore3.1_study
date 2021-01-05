
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.ProxyPattern
{
    public class RealSubject : ISubject
    {
        public void DoSomethingLong()
        {
            Console.WriteLine($"{this.GetType().Name} DoSomethingLong");
        }

        public string GetSomethingLong()
        {
          
            return "GetSomethingLong";
        }
    }
}
