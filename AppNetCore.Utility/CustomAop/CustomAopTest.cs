/*************************************************************************************
   * CLR版本：        4.0.30319.42000
   * 类 名 称：       CustomAopTest
   * 机器名称：       WIN-AQ2FSGMEQLL
   * 命名空间：       AppNetCore.Utility.CustomAop
   * 文 件 名：       CustomAopTest
   * 创建时间：       2020/10/13 13:50:23
   * 作    者：       LIUFANG
   * 说   明：
   * 类型                    命外规则                     说明
   * 命名空间  namespace     Pascal           以.分隔，其中每一个限定词均为Pascal命名方式 如ExcelQuicker.Work
   * 类 class                Pascal           每一个逻辑断点首字母大写 如public class MyHome
   * 接口 interface          IPascal          每一个逻辑断点首字母大写,总是以I前缀开始，后接Pascal命名 如public interface IBankAccount 
   * 方法 method             Pascal           每一个逻辑断点首字母大写如private void SetMember(string)
   * 枚举类型 enum           Pascal           每一个逻辑断点首字母大写
   * 委托 delegate        Pascal           每一个逻辑断点首字母大写局部变量方法的
   * 参数                    Camel            首字母小写，之后Pascal命名如string myName   
   * 修改时间：
   * 修 改 人：
  *************************************************************************************/
using System;
using System.Collections.Generic;
using System.Text;

using Castle.DynamicProxy;

namespace AppNetCore.Utility.CustomAop
{
    public class CustomAopTest
    {
        public static void show()
        {
            ProxyGenerator generator = new ProxyGenerator();
            CustomInterceptor interceptor = new CustomInterceptor();
            CommonClass test = generator.CreateClassProxy<CommonClass>(interceptor);

            Console.WriteLine($"当前类型{test.GetType()},父类型{test.GetType().BaseType}");
            test.MethodInterceptor();
            Console.WriteLine();
            test.MethodNoInterceptor();
            Console.ReadLine();


        }
    }
    public class CustomInterceptor : StandardInterceptor
    {
        /// <summary>
        /// 拦截的方法返回时调用的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PerformProceed(IInvocation invocation)
        {
            Console.WriteLine($"拦截的方法返回时调用的拦截器，方法名称{invocation.Method.Name}");
        }
        /// <summary>
        /// 调用后拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PostProceed(IInvocation invocation)
        {
            Console.WriteLine($"调用后拦截器，方法名称{invocation.Method.Name}");
        }
        /// <summary>
        /// 调用前拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PreProceed(IInvocation invocation)
        {
            Console.WriteLine($"调用前拦截器，方法名称{invocation.Method.Name}");
        }
    }
}
