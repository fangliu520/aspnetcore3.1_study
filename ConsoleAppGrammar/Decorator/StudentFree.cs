using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.Decorator
{
    public class StudentFree : AbstractorStudent
    {
        public override void Study()
        {
            Console.WriteLine($"{base.Name} is a free Student Study .NET Free");
        }
    }
}
