using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.CommandPattern
{
    public class Document
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void Read()
        {
            Console.WriteLine($"{this.GetType().Name} Read");
        }

        public void Write()
        {
            Console.WriteLine($"{this.GetType().Name} Write");
        }
    }
}
