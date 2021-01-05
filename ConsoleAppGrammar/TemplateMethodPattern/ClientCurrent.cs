using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.TemplateMethodPattern
{
    /// <summary>
    /// 活期用户
    /// </summary>
    public class ClientCurrent:BaseClient
    {
        public override double CalculateInterest(double balance)
        {
            return balance * 0.03;
        }

       

    }
}
