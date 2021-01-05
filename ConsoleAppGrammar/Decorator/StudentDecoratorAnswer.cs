using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.Decorator
{
    public class StudentDecoratorAnswer : AbstractDecorator
    {
        public StudentDecoratorAnswer(AbstractorStudent student) : base(student) { 
        
        }

        public override void Study()
        {
            Console.WriteLine($"获取问题答疑1{this.GetType().Name}");
            base.Study();
            Console.WriteLine($"获取问题答疑2{this.GetType().Name}");
        }
    }
}
