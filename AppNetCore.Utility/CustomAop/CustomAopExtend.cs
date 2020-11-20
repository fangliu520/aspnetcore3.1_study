/*************************************************************************************
   * CLR版本：        4.0.30319.42000
   * 类 名 称：       CustomAopExtend
   * 机器名称：       WIN-AQ2FSGMEQLL
   * 命名空间：       AppNetCore.Utility.CustomAop
   * 文 件 名：       CustomAopExtend
   * 创建时间：       2020/10/13 14:23:01
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
using AppNetCore.Utility.Validate;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AppNetCore.Utility.CustomAop
{
    public static class CustomAopExtend
    {
        public static object AOP(this object t, Type interfaceType)
        {
            ProxyGenerator generator = new ProxyGenerator();
            IOCInterceptor interceptor = new IOCInterceptor();
            t = generator.CreateInterfaceProxyWithTarget(interfaceType, t, interceptor);
            return t;
        }
    }

    #region  attribute Interceptor
    public class LogBeforeAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                //去执行真实逻辑
                Console.WriteLine($"This is LogBeforeAttribute,MethodName:{invocation.Method.Name},1");
               // invocation.ReturnValue = "失败！";
                action.Invoke();
                Console.WriteLine($"This is LogBeforeAttribute,MethodName:{invocation.Method.Name},2");

            };

        }
    }
    public class LogAfterAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                //去执行真实逻辑
                Console.WriteLine($"This is LogAfterAttribute,MethodName:{invocation.Method.Name},1");
                action.Invoke();
                Console.WriteLine($"This is LogAfterAttribute,MethodName:{invocation.Method.Name},2");

            };

        }
    }
    public class MonitorAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine($"This is MonitorAttribute,MethodName:{invocation.Method.Name},1");
                //invocation.ReturnValue = null; 返回值
                action.Invoke();
                Console.WriteLine($"This is MonitorAttribute,MethodName:{invocation.Method.Name},2");

            };

        }
    }
    public abstract class BaseInterceptorAttribute : Attribute
    {
        public abstract Action Do(IInvocation invocation, Action action);
    }

    #endregion
    public class IOCInterceptor : StandardInterceptor
    {
        /// <summary>
        /// 调用前拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PreProceed(IInvocation invocation)
        {
            Console.WriteLine($"调用前拦截器，方法名称{invocation.Method.Name}");           
        }
        /// <summary>
        /// 拦截的方法返回时调用的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PerformProceed(IInvocation invocation)
        {
            Console.WriteLine($"拦截的方法返回时调用的拦截器，方法名称{invocation.Method.Name}");
            var method = invocation.Method;

            Action action = () => base.PerformProceed(invocation);
            if (method.IsDefined(typeof(BaseInterceptorAttribute), true))
            {
                var attributeArray = method.GetCustomAttributes<BaseInterceptorAttribute>().ToArray().Reverse();
                foreach (var item in attributeArray)
                {
                    action = item.Do(invocation, action);
                }

            }
            action.Invoke();


        }
        /// <summary>
        /// 调用后拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PostProceed(IInvocation invocation)
        {
            Console.WriteLine($"调用后拦截器，方法名称{invocation.Method.Name}");
        }


    }
}
