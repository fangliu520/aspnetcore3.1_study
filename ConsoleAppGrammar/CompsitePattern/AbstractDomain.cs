using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.CompsitePattern
{
    public abstract class AbstractDomain
    {
        public string Name { get; set; }

        public double Percent { get; set; }

       
        public abstract void Commision(double total);
        
    }
}
