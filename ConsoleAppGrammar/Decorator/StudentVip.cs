using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.Decorator
{
    public class StudentVip : AbstractorStudent
    {
        public override void Study()
        {
            Console.WriteLine($"{base.Name} is vip study .NET");
        }
    }

    public class StudentVipStudy : StudentVip
    {
        public override void Study()
        {
            Console.WriteLine($"获取视屏代码课件1{this.GetType().Name}");
            base.Study();
            Console.WriteLine($"获取视屏代码课件2{this.GetType().Name}");
        }
    }


    /// <summary>
    /// 组合，构造函数注入
    /// </summary>
    public class StudentCombination
    {
        private AbstractorStudent _abstractorStudent = null;
        public StudentCombination(AbstractorStudent abstractorStudent)
        {
            this._abstractorStudent = abstractorStudent;            
        }
        public void Study()
        {
            this._abstractorStudent.Study();
            Console.WriteLine($"获取视屏代码课件");
        }
    }
}
