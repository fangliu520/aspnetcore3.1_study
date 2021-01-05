using System;
using System.Collections.Generic;
using System.Text;
using AppNetCore.Utility.ConfigFile;
namespace ConsoleAppGrammar.CommandPattern
{
    /// <summary>
    /// 命令模式：
    /// 1、命令模式实现异步队列：比如买火车票，从直接下单-》异步等待下单
    /// 2、数据恢复、命令撤销，下单时候写入日志 ，当数据库挂了之后，可以从日志里面去查找，重新执行下命令，就可以吧数据写入数据库，防止数据丢失
    /// </summary>
    public class CommandPatternServer
    {
        public static void Show()
        {
            try
            {
                Document document = new Document();

                Console.WriteLine("请输入 r 或者 w 命令！");

                while (true)
                {
                    string input = Console.ReadLine();
                    //    if (input.Equals("r"))
                    //        document.Read();
                    //    else if (input.Equals("w"))
                    //        document.Write();
                    //    else
                    //        Console.WriteLine("Do nothing!");

                    //var conn_r = ConfigurationManager.GetNode("Command:R");
                    //{
                    //    if (conn_r.Equals("read"))
                    //        document.Read();
                    //    else if (conn_r.Equals("write"))
                    //        document.Write();
                    //    else
                    //        Console.WriteLine("Do nothing!");
                    //}
                    string action = ConfigurationManager.GetNode(input);
                    ///把命令和命令的执行者分开
                    //BaseCommand command = new ReadCommand();
                    BaseCommand command = (BaseCommand)Activator.CreateInstance(action.Split(',')[1], action.Split(',')[0]).Unwrap();
                    IReceive receive = new ReceiveNew();//new Receive();//也可以配置+反射
                    command.SetReceive(receive);
                    command.Set(document);
                    //command.Excute();
                    Invoker invoker = new Invoker(command);
                    invoker.Excute();
                }

               


            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
           
        }
    }
}
