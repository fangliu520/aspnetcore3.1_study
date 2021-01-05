using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.CompsitePattern
{
    public class DomainLeaf : AbstractDomain
    {
        public override void Commision(double total)
        {
            double result = total * Percent * 0.01;
            Console.WriteLine($"this{Name}提成{result}");
        }
    }
}
