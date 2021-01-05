using ConsoleAppGrammar.Decorator;
using ConsoleAppGrammar.SingleTon;
using ConsoleAppGrammar.SingleTonSecond;
using ConsoleAppGrammar.SingleTonThrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static ConsoleAppGrammar.StaticClass;
using ConsoleAppGrammar.AdultFlow;
using ConsoleAppGrammar.DesignerPatter.BridgePattern;
using ConsoleAppGrammar.CompsitePattern;
using ConsoleAppGrammar.FlyweightPattern;
using ConsoleAppGrammar.ProxyPattern;
using ConsoleAppGrammar.TemplateMethodPattern;
using ConsoleAppGrammar.CommandPattern;
using ConsoleAppGrammar.MediatorPattern;
using ConsoleAppGrammar.ObservePattern;
using ConsoleAppGrammar.StatePattern;

namespace ConsoleAppGrammar
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 行为型设计模式
            //享元模式
            /// FlyweightPatternServer.Show();

            // new CompsitePatternServer().Show();
            //代理模式
            //ProxyPatternServer.Show();

            //模板方法设计模式
            //TemplatePatternServer.Show();

            //命令模式
            //  CommandPatternServer.Show();

            //中介者模式
            //MediatorPatternServer.Show();

            //观察者模式
            // ObservePatternServer.Show();

            //状态模式
            StatePatternServer.Show();

            //审批流程的一步步封装
            //行为型设计模式的巅峰之作

            //  ResponsibilityChainPattern.AdultShow();

            //BridgePatternServer bridge = new BridgePatternServer();
            //bridge.Show();


            #region 单例模式
            //{
            //    //懒汉模式
            //    //Singleton single = SingleTon.Singleton.CreateInstance();
            //    //single.Show();

            //    for (int i = 0; i < 5; i++)
            //    {
            //        Task.Run(() =>
            //        {
            //            Singleton single = SingleTon.Singleton.CreateInstance();
            //            single.Show();
            //        });
            //    }
            //}
            //{
            //    //饿汉模式


            //    for (int i = 0; i < 5; i++)
            //    {
            //        Task.Run(() =>
            //        {
            //            SingletonSecond single = SingletonSecond.CreateInstance();
            //            single.Show();
            //        });
            //    }
            //}

            //{
            //    //饿汉模式


            //    for (int i = 0; i < 5; i++)
            //    {
            //        Task.Run(() =>
            //        {
            //            SingletonThrid single = SingletonThrid.CreateInstance();
            //            single.Show();
            //        });
            //    }
            //}  
            #endregion
            #endregion
            #region 设计模式 装饰器模式 实现了AOP(洋葱模型或者叫俄罗斯套娃)
            {
                /// 组合+继承 装饰器的基类实现了AOP
                /// 组合
                /// 继承
                /// OOP--->AOP 
                /// 完成了功能的扩展，没有修改原有的代码，没有破坏原有的封装
                /// 功能扩展--> 动态可定制的功能扩展 ---> 组合+继承 的装饰器实现了
                /// 在不破坏封装的前提下，给对象扩展新的功能---AOP面向切面编程的核心思想
                /// 装饰器模式就是一种实现方式
                /// 1
                /// 既然我们解决了功能的动态扩展，那我们再程序设计时就有了新的做法
                /// 1、聚焦于核心业务逻辑，其他的交给AOP来做
                /// 2、对于公共逻辑可以集中管理，统一标准，方便项目管理

                //AbstractorStudent Student = new StudentVip()
                //{
                //    Id = 1,
                //    Name = "小苹果"
                //};

                ////AbstractorStudent student5 = new StudentDecoratorVideo(Student);//里氏替换原则
                //Student = new StudentDecoratorPriview(Student);
                //Student = new StudentDecoratorAnswer(Student);
                //Student = new StudentDecoratorVideo(Student);//通过继承方式
                //Student = new StudentDecoratorHomework(Student);
                //Student.Study();
                //这里搞定了一些BT需求，可以任意的扩展定制需求，要几个、什么顺序都可以随意
                /*既然解决了功能动态扩展，在程序设计时，有了新的做法
                 * 1、可以聚焦于核心业务，其他东西交给AOP来做
                 * 2、对于公共逻辑可以集中管理，统一标准，方便项目管理
                 
                 */
            }
            {
                //AbstractorStudent Student = new StudentVip()
                //{
                //    Id = 1,
                //    Name = "小苹果"
                //};
                //StudentCombination Student1 = new StudentCombination(Student);//通过组合方式
                //Student1.Study();
            }
            //{
            //    AbstractorStudent Student = new StudentFree()
            //    {
            //        Id = 2,
            //        Name = "下一站"
            //    };



            //}
            //{
            //    AbstractorStudent Student = new StudentVipStudy()
            //    {
            //        Id = 3,
            //        Name = "老鼠"
            //    };

            //}
            #endregion

            #region 设计模式六大原则

            SixDesignPrinciples sixDesign = new SixDesignPrinciples();
            //单一职责
            //sixDesign.SingleResponsibility();
            //sixDesign.LSPResponsibility();
            //sixDesign.DipResponsibility();

            #endregion

            // ClassOne one = new ClassOne();
            //var n1= one.Class1();
            // one.Class2();
            // Console.WriteLine(n1);


            // Console.WriteLine("Hello World!");
            // //CsharpSixInfo.Show();
            // string strTmp = "abcdefg某某某";

            // int i = System.Text.Encoding.Default.GetBytes(strTmp).Length;
            // strTmp = "某某某";

            // i = System.Text.Encoding.UTF32.GetBytes(strTmp).Length;
            // int j = strTmp.Length;

            //CsharpSevenInfo.Show();
            Console.ReadLine();
        }
    }

    class ClassOne
    {

        private static int count = 0;
        public int Class1()
        {
            return count++;
        }
        public int Class2()
        {
            return count++;
        }

    }

    class payments
    {
        public string payment { get; set; }
    }

    #region C#6语法
    /// <summary>
    /// C#6
    /// </summary>
    public static class CsharpSixInfo
    {

        public static void Show()
        {

            //1、只读自动属性
            {
                Student student = new Student("LIU", "Fang");

                string FullName = student.FullName;

                string FullName1 = student.ToString();
            }
            //2、using static 
            {
                StaticClass.Next();
                StaticClass.NextTo();
                Next();
                NextTo();
            }
            //null条件运算符
            {

                Student student = null;
                string fullName = student?.FullName;

                int? id = student?.Id;

                student = new Student("a", "cd");

                string testName = student?.TestName ?? "合理";
            }


            //4、字符串内插
            {
                string fName = "志存";
                string LName = "高远";
                string fullName = $"{{{fName}}}-{LName}-{fName}-{fName}:{LName}";
                Console.WriteLine(fullName);
            }

            //5、异常筛选器
            {
                // ExceptionShow();
            }
            //6、nameof
            {
                string className = "staticClass";
                string className1 = nameof(StaticClass);
                Console.WriteLine(className1);
                string className2 = nameof(CsharpSixInfo);
                Console.WriteLine(className2);
                string className3 = nameof(Student);
                Console.WriteLine(className3);
            }
            //7、事件
            {
                NotifyPropertyChanged notifyProperty = new NotifyPropertyChanged();
                notifyProperty.PropertyChanged += (object o, System.ComponentModel.PropertyChangedEventArgs b) => { };

                notifyProperty.LastName = "志存高远";
            }


            //8、使用索引器初始化关联集合
            {
                Dictionary<int, string> msg = new Dictionary<int, string>() {
                    { 404,"page not found"},
                    { 302,"page moved"},
                    { 500,"server can't come out to play"}
                };
                msg.Add(405, "nihao");
                msg.Remove(405, out string obj);
                Console.WriteLine(obj);

                Dictionary<int, string> msg1 = new Dictionary<int, string>()
                {
                    [404] = "page not found",
                    [302] = "page moved",
                    [500] = "server can't come out to play"
                };
                msg1.Add(405, "nihao");
                msg1.Remove(405, out string obj1);
                Console.WriteLine(obj1);
            }
        }


    }

    public class Student
    {
        public Student(string fname, string lname)
        {
            FirstName = fname;
            LastName = lname;
        }
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TestName { get; set; }
        //自动属性初始化表达式
        public ICollection<double> Grades { get; } = new List<double>();
        public string FullName => $"{FirstName}-{LastName}";

        public string FullName1
        {
            get
            {
                return string.Format("{0}-{1}", FirstName, LastName);
            }
        }

        //public override string ToString()
        //{
        //    return $"{FirstName}-{LastName}";

        //}

        public override string ToString() => $"{FirstName}-{LastName}";


    }
    public class NotifyPropertyChanged
    {
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (value != lastName)
                {
                    lastName = value;
                    PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(nameof(LastName)));
                }
            }
        }
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

    }
    public static class StaticClass
    {
        public static void Next()
        {
            Console.WriteLine("Next");
        }

        public static void NextTo()
        {
            Console.WriteLine("NextTo");
        }
        public static void ExceptionShow()
        {
            try
            {

                throw new Exception("志qq存qq高qq远qq");
            }
            catch (Exception ex) when (ex.Message.Contains("志存"))
            {
                throw;
            }
            finally
            {


            }
        }
    }
    #endregion

    #region C#7语法
    public static class CsharpSevenInfo
    {

        public static void Show()
        {
            //1、out变量
            {
                Console.WriteLine("请输入：");
                //不需要声明
                string input = Console.ReadLine();
                int.TryParse(input, out int result);
                Console.WriteLine(result);

                //out 修改时可以使var类型声明
                int.TryParse(input, out var result1);
                Console.WriteLine(result1);

                Dinfo dinfo = new Dinfo(122);
            }
            //2元祖
            {
                (string Alpha, string Bate) nameLetters = ("a", "b");
                nameLetters.Alpha = "aa";
                nameLetters.Bate = "bb";
                Console.WriteLine($"{nameLetters.Alpha }-{nameLetters.Bate }");

                var alphaStart = (Alpha: "a", Bate: "b");
                alphaStart.Alpha = "A+A";
                alphaStart.Bate = "B+B";
                Console.WriteLine($"{alphaStart.Alpha }-{alphaStart.Bate }");

                (int max, int min) = Range();
                Console.WriteLine($"{max }-{min }");


            }
            //3、模式
            {
                int input = 123;
                int sum = 234;
                if (input is int count)
                {
                    sum += count;
                }
            }
            {
                IEnumerable<object> enumerableList = new List<object>() {
                0,
                new List<int>(){ 1,2,3},
                100,
                null

                };
                //  var r = SumPositiveNumbers(enumerableList);
            }

            //4 本地方法
            {
                var rs = LocalFunction("志存高远");
                Console.WriteLine($"{rs }");

            }

            //命名实参
            {
                PrintParm(age: 12, name: "刘", sex: "男"); //命名实参可以不限顺序
            }

            //增强泛型约束  枚举类型约束  之前的五种（基类约束 where T：Student、接口约束 where T：IStudent、引用类型约束 where T：class、值类型约束where T：struct、无参类型约束 where T：new()）
            {

                var tes = new GerenicInfo<EnumInfo>();
                tes.Show(EnumInfo.one);
            }

            //通用的异步返回类型
            {
                //ValueTask<int> task = null;
                //await foreach (var item in task)
                //{ 

                //}
            }

        }

        private static (int max, int min) Range()
        {
            return (123, 234);
        }

        private static int SumPositiveNumbers(IEnumerable<object> sequence)
        {
            int sum = 0;
            foreach (var i in sequence)
            {
                switch (i)
                {
                    case 0: break;
                    case IEnumerable<int> childSequence:
                        {
                            foreach (var item in childSequence)
                            {
                                sum += (item > 0) ? item : 0;
                            }
                            break;
                        }
                    case int n when n > 0:
                        sum += n;
                        break;
                    case null:
                        throw new NullReferenceException("Null found in Sequence");

                    default:
                        throw new InvalidOperationException("Unrecognized Type");
                }
            }
            return sum;
        }

        private static string LocalFunction(string name)
        {
            return TestFun(name);

            string TestFun(string name1)
            {
                return name1;
            }

        }

        private static void PrintParm(string name, int age, string sex)
        {

            Console.WriteLine($"{name}-{age}-{sex}");

        }
    }
    public class Binfo
    {
        public Binfo(int i, out string j)
        {
            j = i.ToString();
        }
    }

    public class Dinfo : Binfo
    {
        public Dinfo(int i) : base(i, out var j)
        {
            Console.WriteLine($"the value of 'j' is {j}");
        }
    }

    public class GerenicInfo<T> where T : Enum
    {
        public void Show(T t)
        { Console.WriteLine($"the value {t}"); }
    }
    public enum EnumInfo
    {
        one,
        two,
        three
    }
    #endregion

    #region C#8 语法
    public static class CsharpEightInfo
    {


    }

    interface CustomInterface
    {
        void Show();

        //public void ShowInfo()
        //{
        //    Console.WriteLine("C#8可以接口实现具体方法");
        //}
    }
    #endregion



}
