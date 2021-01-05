using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.Decorator
{
    public class StudentDecoratorPriview : AbstractDecorator
    {
        public StudentDecoratorPriview(AbstractorStudent student) : base(student) { }
        public override void Study()
        {
            Console.WriteLine($"提前预习1{this.GetType().Name}");
            base.Study();
            Console.WriteLine($"提前预习2{this.GetType().Name}");
        }
    }
}
