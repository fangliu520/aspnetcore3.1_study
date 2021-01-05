using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.Decorator
{
    /// <summary>
    /// 组合+继承 装饰器的基类实现了AOP
    /// 组合
    /// 继承
    /// 完成了功能的扩展，没有修改原有的代码，没有破坏原有的封装
    /// 功能扩展--> 动态可定制的功能扩展 ---> 组合+继承 的装饰器实现了
    /// 在不破坏封装的前提下，给对象扩展新的功能---AOP面向切面编程的核心思想
    /// </summary>
    public class AbstractDecorator : AbstractorStudent
    {
        private AbstractorStudent _Student = null;
        public AbstractDecorator(AbstractorStudent student) : base()
        {
            this._Student = student;
        }

        public override void Study()
        {
            Console.WriteLine($"*******************");
            this._Student.Study();
            Console.WriteLine($"-------------------");
        }

    }
}
