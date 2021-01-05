using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.FacadePattern.Order
{
    public interface IOrderSystem
    {
        bool CheckOrder(int userId, int productId);
        void NewOrder(int userId, int productId);
    }
}
