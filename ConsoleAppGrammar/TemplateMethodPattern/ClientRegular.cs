using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.TemplateMethodPattern
{
    /// <summary>
    /// 定期用户
    /// </summary>
    public class ClientRegular : BaseClient
    {
        public override double CalculateInterest(double balance)
        {
            return balance * 0.05;
        }

    }
}
