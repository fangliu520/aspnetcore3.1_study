using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.Decorator
{
    public abstract class AbstractorStudent
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public abstract void Study();

    }
}
