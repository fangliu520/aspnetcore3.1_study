using ConsoleAppGrammar.FacadePattern.Logistics;
using ConsoleAppGrammar.FacadePattern.Order;
using ConsoleAppGrammar.FacadePattern.Storage;
using ConsoleAppGrammar.FacadePattern.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.FacadePattern
{
    /// <summary>
    /// 门面模式Facade
    /// </summary>
    public class FacadeServer
    {
        public void Show()
        {


            int userId = 123;
            int productId = 23;
            FacadeCenter facade = FacadeCenter.CreateInstance();
            facade.NewOrder(userId, productId);
            {
                //IUserSystem iUser = new UserSystem();
                //ILogisticsSystem iLogistics = new LogisticsSystem();
                //IStorageSystem iStorage = new StorageSystem();
                //IOrderSystem iOrder = new OrderSystem();
                //if (!iUser.CheckUser(userId))
                //{
                //    Console.WriteLine("用户检测失败");
                //}
                //else if (!iStorage.CheckStorage(productId))
                //{
                //    Console.WriteLine("仓储检测失败");
                //}
                //else if (!iLogistics.CheckLogistics(userId, productId))
                //{
                //    Console.WriteLine("物流检测失败");
                //}
                //else if (!iOrder.CheckOrder(userId, productId))
                //{
                //    Console.WriteLine("订单检测失败");
                //}
                //else
                //{
                //    iOrder.NewOrder(userId, productId);
                //    iLogistics.NewLogistics(userId, productId);
                //}

            }


        }
    }
}
