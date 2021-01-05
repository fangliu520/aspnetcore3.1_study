 /*************************************************************************************
   * CLR版本：        4.0.30319.42000
   * 类 名 称：       SixDesignPrinciples
   * 机器名称：       WIN-AQ2FSGMEQLL
   * 命名空间：       ConsoleAppGrammar
   * 文 件 名：       SixDesignPrinciples
   * 创建时间：       2020/12/22 20:39:23
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

namespace ConsoleAppGrammar
{
    public class SixDesignPrinciples
    {
        #region  单一职责原则
        public void SingleResponsibility()
        {
            AbstractAnimal animal = new Chiken();
            animal.Breath();
            animal.Action();

            animal = new Fish();
            animal.Breath();
            animal.Action();
        }
        #endregion

        #region 里氏替换原则
        public void LSPResponsibility()
        {
            {
                Chinese people = new Chinese();
                people.Traditional();
                DoChinese(people);
            }
            {
                Chinese people = new HuBei();
                people.Traditional();
                DoChinese(people);//people 的实例方法是Chinese

            }
            {
                var people = new HuBei();//执行左边的类的属性方法
                people.Traditional();
                DoChinese(people);
                people.SayHi();//people 的实例方法是Hubei
            }

        }
        private static void DoChinese(Chinese people)
        {
            Console.WriteLine($"{people.Id},{people.Name}{people.KuaiZi}");
            people.SayHi();
        }
        private static void DoHuBei(HuBei people)
        {
            Console.WriteLine($"{people.Id},{people.Name}{people.KuaiZi}");
            people.SayHi();
        }
        #endregion

        #region 依赖倒置
        public void DipResponsibility()
        {
            StudentTest student = new StudentTest()
            {
                Id = 1,
                Name = "小鸟班长"
            };
            {
                iPhone phone = new iPhone();
                student.PlayiPhone(phone);
                student.PlayT(phone);
                student.Play(phone);
            }
            {
                Honor phone = new Honor();
                student.PlayiPhone(phone);
                student.PlayT(phone);
                student.Play(phone);
            }
            /*依赖细节 高层就依赖了下层，这种不适合扩展 不推荐
            //用泛型+父类约束等同于用父类参数类型
            //一个方法满足了不同的扩展需求
            //面向抽象后，不能使用子类的特别内容，比如下面的小米手机的 手环Bracelet方法

            面向抽象不止一个类型，用的就是通用的功能，非通用的，那就不要面向抽象
            面向对象语言开发，就是类与类之间进行交互，如果高层直接依赖低层的细节，细节多变的，
            低层的修改会直接水波效应传递到高层最上层，一点细微的改变就会导致整个系统的从下往上的修改。非常不稳定

            面向抽象，高层不直接依赖低层，而是依赖抽象，抽象一般相对稳定，低层细节的扩展就不会影响高层，这样就能支撑层内部的横向扩展，不会影响其他地方，这样的程序架构就稳定

            依赖倒置（理论基础）--IOC控制反转（实践封装）---DI依赖注入（实现IOC的手段）
            */
            {
                Mi phone = new Mi();

                student.PlayT(phone);
                student.Play(phone);
            }
        }
        #endregion

        #region 接口隔离原则
        /*原则思想：类和类之间应该建立在最小接口的上。
          描述：类A通过接口依赖B，类C通过接口依赖D，如果接口类A和类B不是最小的接口，则依赖的类B和类D必须要实现他们不需要的方法。
          优点：提高程序的灵活度，提高内聚，减少对外交互，使得最小的接口做最多的事情。
          

      */

        public void ISPResponsibility()
        {
            /*
             举个手机例子：现在智能手机都有 map movie online game……
             是否是应该把他上升到AbstractPhone 父类中？
             不应该的，上升后，oldMan 老年机也是手机，但是不支持这些功能
             AbstractPhone 必须放手机都支持的功能

            不适合放在抽象类中，但是面向抽象编程：接口
             */
            {

            }

        }
        public void Video(IExtendVideo extend)
        {
            extend.Movie();
            extend.Photo();
        }


        public void Happy(IExtendHappy extend)
        {
            extend.Online();
            extend.Game();
        }

        #endregion
    }
    /*
     总结:
          单一职责原则告诉我们实现类要职责单一
          
          里氏替换原则告诉我们不要破坏继承体系
          
          依赖倒置原则告诉我们要面向接口编程
          
          接口隔离原则告诉我们在设计接口的时候要精简单一
          
          迪米特原则告诉我们要降低耦合
          
          开闭原则是总纲，告诉我们要对扩展开放，对修改关闭
         */

    #region 单一职责原则 （抽离）
    /*   单一职责原则（Single Responsibility Principle）
    定义：一个类只负责一个功能领域中的相应职责，或者可以定义为：就一个类而言，应该只有一个引起它变化的原因。
    问题由来：类T负责两个不同的职责：职责P1，职责P2。当由于职责P1需求发生改变而需要修改类T时，有 
    可能会导致原本运行正常的职责P2功能发生故障。
    循单一职责原的优点有：
    1.可以降低类的复杂度，一个类只负责一项职责，其逻辑肯定要比负责多项职责简单的多；
    2. 提高类的可读性，提高系统的可维护性；
    3.变更引起的风险降低，变更是必然的，如果单一职责原则遵守的好，当修改一个功能时，可以显著降低对其他功能的影响。


        总结：一个类只做一件事
     */
    public class Chiken : AbstractAnimal
    {
        public Chiken() : base("小鸡")
        {

        }
        public override void Action()
        {
            Console.WriteLine($"{base._name} flying");
        }

        public override void Breath()
        {
            Console.WriteLine($"{base._name} 呼吸空气");
        }
    }

    public class Fish : AbstractAnimal
    {
        public Fish() : base("鱼")
        {

        }
        public override void Action()
        {
            Console.WriteLine($"{base._name} swimming");
        }

        public override void Breath()
        {
            Console.WriteLine($"{base._name} 呼吸水");
        }
    }
    public abstract class AbstractAnimal
    {

        protected string _name = null;
        public AbstractAnimal(string name)
        {
            _name = name;
        }
        public abstract void Breath();
        public abstract void Action();
    }
    #endregion

    #region 里氏替换原则
    /*
     里氏代换原则(Liskov Substitution Principle, LSP)
     定义：里氏代换原则(Liskov Substitution Principle, LSP)：所有引用基类（父类）的地方必须能透明地使用其子类的对象。
     继承 ：子类拥有父类的一切属性和行为，任何父类出现的地方都可以用子类代替
     
    多态

    1、父类有的子类必须有的
    如果出现了子类没有的东西，就应该断掉继承
    （如果非要继承，那么最好在重新建立一个父类，只包含有的东西）
    2、子类可以有自己的属性和行为 
    子类出现的地方，父类不一定能代替
    3、父类实现的东西，子类不要再写了(不要new 隐藏）
    如果想修改父类的行为，通过abstract/virtaul

        总结：如何去完成类与类之间的继承
     */
    public class People
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public void Traditional()
        {
            Console.WriteLine("仁义礼智信，温良恭俭让");
        }
    }
    public class Chinese : People
    {
        public string KuaiZi { get; set; }
        public void SayHi()
        {
            Console.WriteLine("早上吃了吗");
        }

    }
    public class HuBei : Chinese
    {
        public string KuaiZi { get; set; }
        public void SayHi()
        {
            Console.WriteLine("过早了吗");
        }

    }
    #endregion

    #region 迪米特法则(最少知道原则)
    /*
     迪米特法则(Law of  Demeter, LoD)
     定义：迪米特法则(Law of  Demeter, LoD)：一个软件实体应当尽可能少地与其他实体发生相互作用。
     高内聚 低耦合
     降低类与类之间的耦合

    聚合》组合》关联》依赖（出现在方法内部）
    只与直接的朋友通信，尽量避免依赖更多的类型
    （基类库的BCL 框架内置的类型除外）

    总结：告诉我们类不要与别的类依赖的太多，只与最直接的朋友通信
     */
    #endregion

    #region 依赖倒置原则

    /*
     原则思想：高层次的模块不应该依赖于低层次的模块，他们都应该依赖于抽象，抽象不应该依赖于具体实现，具体实现应该依赖于抽象。
     描述：类A直接依赖类B，假如要将类A改为依赖类C，则必须通过修改类A的代码来达成。
     这种场景下，类A一般是高层模块，负责复杂的业务逻辑；类B和类C是低层模块，负责基本的原子操作；假如修改类A，会给程序带来不必要的风险。
     优点：可以减少需求变化带来的工作量，做并行开发更加友好。
     面向抽象编程：尽量的使用抽象，80%的设计模式都是面向抽象有关
     属性、字段、方法、返回值……尽量抽象
     */

    public class StudentTest
    {
        public int Id { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// 这种写法违背了依赖倒置原则
        /// </summary>
        /// <param name="phone"></param>

        public void PlayiPhone(iPhone phone)
        {
            Console.WriteLine($"这里是{phone}");
            phone.Call();
            phone.SendMsg();
        }
        public void PlayiPhone(Honor phone)
        {
            Console.WriteLine($"这里是{phone}");
            phone.Call();
            phone.SendMsg();
        }

        //通过泛型调用也可

        public void PlayT<T>(T t) where T : AbstractPhone
        {
            Console.WriteLine($"这里是{t}");
            t.Call();
            t.SendMsg();
        }

        /// <summary>
        /// 直接用抽象父类 面向抽象（推荐）
        /// </summary>
        /// <param name="phone"></param>
        public void Play(AbstractPhone phone)
        {
            Console.WriteLine($"这里是{phone}");
            phone.Call();
            phone.SendMsg();
        }
    }
    //抽象父类
    public abstract class AbstractPhone
    {
        /// <summary>
        /// 手机ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 手机品牌
        /// </summary>
        public string Brand { get; set; }
        /// <summary>
        /// 打电话
        /// </summary>
        public abstract void Call();
        /// <summary>
        /// 发短信
        /// </summary>
        public abstract void SendMsg();
    }

    public class iPhone : AbstractPhone
    {
        public override void Call()
        {
            Console.WriteLine($"我是{this.GetType().Name}手机打电话");
        }

        public override void SendMsg()
        {
            Console.WriteLine($"我是{this.GetType().Name}手机发短信");
        }
    }

    public class Honor : AbstractPhone
    {
        public override void Call()
        {
            Console.WriteLine($"我是{this.GetType().Name}手机打电话");
        }

        public override void SendMsg()
        {
            Console.WriteLine($"我是{this.GetType().Name}手机发短信");
        }
    }

    public class Mi : AbstractPhone
    {
        public override void Call()
        {
            Console.WriteLine($"我是{this.GetType().Name}手机打电话");
        }

        public override void SendMsg()
        {
            Console.WriteLine($"我是{this.GetType().Name}手机发短信");
        }

        public void Bracelet()
        {
            Console.WriteLine($"我是{this.GetType().Name}Bracelet");
        }
    }
    #endregion

    #region 接口隔离原则
    /*原则思想：类和类之间应该建立在最小接口的上。
      描述：类A通过接口依赖B，类C通过接口依赖D，如果接口类A和类B不是最小的接口，则依赖的类B和类D必须要实现他们不需要的方法。
      优点：提高程序的灵活度，提高内聚，减少对外交互，使得最小的接口做最多的事情。
      
        接口该如何定义：
        1、既不能是大而全，会强迫实现没有的东西，也会依赖自己不需要的东西

        2、也不能一个方法一个接口，这样的面向抽象没有意义
           按照功能的密不可分来定义接口
           而且是动态的，随着业务量的发展会有变化，但是在设计的时候，要留好提前量，避免抽象的变化
           没有标准答案，随着业务产品来调整
        3、接口合并 Map---定位、搜索、导航  这种属于固定步骤，业务细节尽量的内聚，在接口也不要暴露太多

  */

    /// <summary>
    /// 不应该用这个中大而全的接口
    /// </summary>
    //public interface IExtend
    //{
    //    void Photo();
    //    void Online();
    //    void Game();
    //    void Map();
    //    void Movie();
    //}
    public interface IExtendVideo
    {
        void Photo();

        void Movie();
    }

    public interface IExtendHappy : IExtendGame
    {
        void Online();

    }
    public interface IExtendGame
    {
        void Game();
    }



    public class OldMan : AbstractPhone
    {
        public override void Call()
        {
            Console.WriteLine($"我是{this.GetType().Name}手机打电话");
        }


        public override void SendMsg()
        {
            Console.WriteLine($"我是{this.GetType().Name}手机发短信");
        }
    }

    public class Camera : IExtendVideo
    {
        public void Movie()
        {
            Console.WriteLine($"我是{this.GetType().Name}Movie");
        }

        public void Photo()
        {
            Console.WriteLine($"我是{this.GetType().Name}Photo");
        }
    }

    public class PSP : IExtendGame
    {
        public void Game()
        {
            Console.WriteLine($"我是{this.GetType().Name}Game");
        }
    }
    #endregion

    #region 开闭原则（OCP）
    /*
     * 对扩展开放、对修改关闭
     * 修改：修改现有的代码（类）
     * 扩展：增加代码（类）
     * 面向对象语言是一种静态语言、最害怕变化，会波及很多东西，全面测试
     * 最理想的办法就是新增类，对原有的代码没有改动，原有的代码才是可信的
     * 开闭原则只是一个目标、没有任何的手段、也被成为总则
     * 其他的五个原则是建议，就是为了更好的做好OCP 开闭原则
     * 
     * 如果有功能增加、修改的需求
     * 修改现有方法（最low）-->增加方法-->增加类-->增加/替换类库（通过IOC、DI）
     原则思想：尽量通过扩展软件实体来解决需求变化，而不是通过修改已有的代码来完成变化
     描述：一个软件产品在生命周期内，都会发生变化，既然变化是一个既定的事实，我们就应该在设计的时候尽量适应这些变化，以提高项目的稳定性和灵活性。
     优点：单一原则告诉我们，每个类都有自己负责的职责，里氏替换原则不能破坏继承关系的体系。


     */


    #endregion
}
