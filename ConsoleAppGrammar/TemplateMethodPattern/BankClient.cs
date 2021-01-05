using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.TemplateMethodPattern
{
    public class BankClient
    {
        public void Query(int id, string name, string pwd)
        {
            if (this.CheckUser(id, name))
            {
                double balance = this.QueryBalance(id);
                double interest = this.CalculateInterest(balance);
                this.Show(name, balance, interest);
            }
            else
            {
                Console.WriteLine("账号密码错误");
            }
        }

        private void Show(string name, double balance, double interest)
        {
            Console.WriteLine($"尊敬的{name}客户，你的账户余额：{balance},利息：{interest}");
        }

        private double CalculateInterest( double balance)
        {
            return balance * 0.03;
        }

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
