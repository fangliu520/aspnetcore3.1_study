using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.FacadePattern.Order
{
    public class OrderSystem : IOrderSystem
    {
        public bool CheckOrder(int userId, int productId)
        {
            return true;
        }

        public void NewOrder(int userId, int productId)
        {
            Console.WriteLine($"{userId} 给商品{productId} 下单");
        }
    }
}
