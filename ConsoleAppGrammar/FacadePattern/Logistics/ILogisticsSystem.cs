using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.FacadePattern.Logistics
{
    public interface ILogisticsSystem
    {
        bool CheckLogistics(int userId, int productId);
        void NewLogistics(int userId, int productId);
    }
}
