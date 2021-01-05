using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.TemplateMethodPattern
{
    public class ClientVip : BaseClient
    {
        public override double CalculateInterest(double balance)
        {
            return balance * 0.07;
        }

        public override void Show(string name, double balance, double interest)
        {
            Console.WriteLine($"尊贵的{name} VIP客户，你的账户余额：{balance},利息：{interest}");
            Console.WriteLine($"投资有风险，入行需谨慎！");
        }
    }
}
