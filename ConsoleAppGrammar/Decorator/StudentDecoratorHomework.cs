using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.Decorator
{
    public class StudentDecoratorHomework : AbstractDecorator
    {
        public StudentDecoratorHomework(AbstractorStudent student) : base(student) { 
        
        }

        public override void Study()
        {
            Console.WriteLine($"获取作业代码1{this.GetType().Name}");
            base.Study();
            Console.WriteLine($"获取作业代码2{this.GetType().Name}");
        }
    }
}
