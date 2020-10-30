/*************************************************************************************
   * CLR版本：        4.0.30319.42000
   * 类 名 称：       ThreadServer
   * 机器名称：       WIN-AQ2FSGMEQLL
   * 命名空间：       ConsoleAppNetCore3
   * 文 件 名：       ThreadServer
   * 创建时间：       2020/10/23 8:42:57
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
using System.Threading;
namespace ConsoleAppNetCore3
{
    public class ThreadServer
    {
        public string A()
        {
            string ts = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Thread.Sleep(1000);
            Console.WriteLine($"我是A,ThreadID：{Thread.CurrentThread.ManagedThreadId},开始时间：{ts}，结束时间:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}");
            return $"我是A,ThreadID：{Thread.CurrentThread.ManagedThreadId},开始时间：{ts}，结束时间:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}";
        }
        public string B()
        {
            string ts = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Thread.Sleep(2000);
            return $"我是B,ThreadID：{Thread.CurrentThread.ManagedThreadId},开始时间：{ts}，结束时间:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}";
        }
        public string C()
        {
            string ts = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            Thread.Sleep(3000);
            return $"我是C,ThreadID：{Thread.CurrentThread.ManagedThreadId},开始时间：{ts}，结束时间:{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")}";
        }
    }
}
