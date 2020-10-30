/*************************************************************************************
   * CLR版本：        4.0.30319.42000
   * 类 名 称：       ReflectionTest
   * 机器名称：       WIN-AQ2FSGMEQLL
   * 命名空间：       ConsoleAppNetCore3
   * 文 件 名：       ReflectionTest
   * 创建时间：       2020/10/26 14:42:57
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
using System.Reflection;
using System.Linq;

namespace ConsoleAppNetCore3
{
    public class ReflectionCeshi
    {
        public void Test()
        {
            //#region 无参数调用三种方法：  dynamic/强制类型转换/反射方法调用
            //{
            //    Assembly assembly = Assembly.Load("ConsoleAppNetCore3");
            //    Type type = assembly.GetType("ConsoleAppNetCore3.ReflectionTest");
            //    object oHelper = Activator.CreateInstance(type);
            //    dynamic h = oHelper;
            //    h.Show1();

            //    ReflectionTest r = (ReflectionTest)oHelper;
            //    r.Show1();

            //    MethodInfo method = type.GetMethod("Show1");
            //    method.Invoke(oHelper, new object[] { });
            //}

            //#region 无参数
            //{
            //    Assembly assembly = Assembly.Load("ConsoleAppNetCore3");
            //    Type type = assembly.GetType("ConsoleAppNetCore3.ReflectionTest");
            //    object oHelper = Activator.CreateInstance(type);
            //    MethodInfo method = type.GetMethod("Show1");
            //    method.Invoke(oHelper, new object[] { });
            //}
            //#endregion
            //#region 一个参数

            //{
            //    Assembly assembly = Assembly.Load("ConsoleAppNetCore3");
            //    Type type = assembly.GetType("ConsoleAppNetCore3.ReflectionTest");
            //    object oHelper = Activator.CreateInstance(type);
            //    MethodInfo method = type.GetMethod("Show2");
            //    method.Invoke(oHelper, new object[] { 1233 });
            //}
            //#endregion
            //#region 两个个参数

            //{
            //    Assembly assembly = Assembly.Load("ConsoleAppNetCore3");
            //    Type type = assembly.GetType("ConsoleAppNetCore3.ReflectionTest");
            //    object oHelper = Activator.CreateInstance(type);
            //    MethodInfo method = type.GetMethod("Show3",new Type[] { typeof(int),typeof(string)});
            //    method.Invoke(oHelper, new object[] { 1233 ,"我是字符串参数"});
            //}
            //#endregion
            //#region 静态方法

            //{
            //    Assembly assembly = Assembly.Load("ConsoleAppNetCore3");
            //    Type type = assembly.GetType("ConsoleAppNetCore3.ReflectionTest");
            //    object oHelper = Activator.CreateInstance(type);
            //    MethodInfo method = type.GetMethod("Show4");
            //    method.Invoke(oHelper, null);
            //    method.Invoke(null,null);
            //}
            //#endregion

            ///带构造函数的类的创建(参数为非string类型)
            {
                Assembly assembly = Assembly.Load("ConsoleAppNetCore3");
                Type type = assembly.GetType("ConsoleAppNetCore3.ReflectionTest");

                ConstructorInfo constructor = type.GetConstructors().OrderByDescending(c => c.GetParameters().Length).First();

                List<object> paralist = new List<object>();
                //foreach (var par in constructor.GetParameters())
                //{
                //    Console.WriteLine($"{par.Name}————————{par.ParameterType}");
                //    if (par.ParameterType.Name == "String")
                //    {
                //        object[] parameters = new object[1];
                //        // parameters[0] = "lpCh";

                //        paralist.Add(Activator.CreateInstance(new Type[] { typeof(par.ParameterType) })) ;
                //    }
                //    else
                //    {
                //        paralist.Add(Activator.CreateInstance(par.ParameterType));

                //    }

                //}
                paralist.AddRange(new object[] { 123,"122334"});
              object oHelperConstructor = Activator.CreateInstance(type, paralist.ToArray());

                //MethodInfo method = type.GetMethods().Where(m => m.Name.Equals("Show3")).FirstOrDefault();
                //foreach (var par in method.GetParameters())
                //{
                //    Console.WriteLine($"{method.Name}————————{par.ParameterType}");

                //}

            }
            //{
            //    Assembly assembly = Assembly.Load("ConsoleAppNetCore3");
            //    Type type = assembly.GetType("ConsoleAppNetCore3.ReflectionTest");

            //    object oHelper = Activator.CreateInstance(type);

            //    MethodInfo method = type.GetMethods().Where(m => m.Name.Equals("Show3")).FirstOrDefault();
            //    foreach (var par in method.GetParameters())
            //    {
            //        Console.WriteLine($"{method.Name}————————{par.ParameterType}");
                  
            //    }
              
            //}
        }
    }

    public class ReflectionTest {
        public ReflectionTest()
        {
            Console.WriteLine($"我是无参数构造函数{this.GetType()}");
        }
        //public ReflectionTest(int n)
        //{
        //    Console.WriteLine($"我是一个参数构造函数{this.GetType()}");
        //}
        public ReflectionTest(string n)
        {
            Console.WriteLine($"我是一个参数构造函数{this.GetType()}");
        }
        public ReflectionTest(int n, string str)
        {
            Console.WriteLine($"我是两个个参数构造函数{this.GetType()}|{n}|{str}");
        }
        public void Show1() {

            Console.WriteLine("我是无参数方法");
        }

        public void Show2(int n)
        {

            Console.WriteLine($"我是一个参数方法{n}");
        }

        public void Show3(int n,string str)
        {

            Console.WriteLine($"我是两个参数方法int:{n},string{str}");
        }
        public static void Show4()
        {
            Console.WriteLine($"我是静态方法Show4");
        }
    }
}
