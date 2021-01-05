using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.Decorator
{
    public class StudentDecoratorVideo : AbstractDecorator
    {
        public StudentDecoratorVideo(AbstractorStudent student) : base(student) { 
        
        }

        public override void Study()
        {
            Console.WriteLine($"获取视屏代码1{this.GetType().Name}");
            base.Study();
            Console.WriteLine($"获取视屏代码2{this.GetType().Name}");
        }
    }
}
