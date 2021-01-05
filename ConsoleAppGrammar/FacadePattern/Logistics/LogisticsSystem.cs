using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.FacadePattern.Logistics
{
    public class LogisticsSystem : ILogisticsSystem
    {
        public bool CheckLogistics(int userId, int productId)
        {
            return true;
        }

        public void NewLogistics(int userId, int productId)
        {
            Console.WriteLine($"{userId} 给商品{productId} 下物流单");
        }
    }
}
