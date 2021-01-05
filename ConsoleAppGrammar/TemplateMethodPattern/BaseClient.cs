using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.TemplateMethodPattern
{
    public abstract class BaseClient
    {
        public void Query(int id, string name, string pwd)
        {
            if (this.CheckUser(id, name))
            {
                double balance = this.QueryBalance(id);
                double interest = this.CalculateInterest(balance);//抽象方法由子类实现，初始化实例的时候指定子类
                this.Show(name, balance, interest);//抽象/虚方法 初始化构造子类的时候，如果对该虚方法有重写的话，则此处调用子类的重写的方法。
            }
            else
            {
                Console.WriteLine("账号密码错误");
            }
        }

        public virtual void Show(string name, double balance, double interest)
        {
            Console.WriteLine($"尊敬的{name}客户，你的账户余额：{balance},利息：{interest}");
        }
        public abstract double CalculateInterest(double balance);
        /// <summary>
        /// 查询余额
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private double QueryBalance(int id)
        {
            return new Random().Next(1000, 1000000);
        }
        /// <summary>
        /// 用户检测
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool CheckUser(int id, string name)
        {
            return DateTime.Now < DateTime.Now.AddDays(1);
        }


    }
}
