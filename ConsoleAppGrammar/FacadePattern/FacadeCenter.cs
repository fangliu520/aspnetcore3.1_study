using ConsoleAppGrammar.FacadePattern.Logistics;
using ConsoleAppGrammar.FacadePattern.Order;
using ConsoleAppGrammar.FacadePattern.Storage;
using ConsoleAppGrammar.FacadePattern.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.FacadePattern
{
   public class FacadeCenter
    {
        private FacadeCenter() { }
        private static FacadeCenter _facadeCenter = new FacadeCenter();

        public static FacadeCenter CreateInstance()
        {
            return _facadeCenter;
        }
        /// <summary>
        /// 门面模式通过是单例的
        /// 门面模式不能直接新增一个物流或者仓储检测方法，只能通过调用物流系统或者仓储系统的检测方法，这是门面模式的严格界定
        /// 用于分层
        /// 门面模式就是为了屏蔽上端知道太多细节
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="productId"></param>
        public void NewOrder(int userId, int productId)
        {
            IUserSystem iUser = new UserSystem();
            ILogisticsSystem iLogistics = new LogisticsSystem();
            IStorageSystem iStorage = new StorageSystem();
            IOrderSystem iOrder = new OrderSystem();
           
            if (!iUser.CheckUser(userId))
            {
                Console.WriteLine("用户检测失败");
            }
            else if (!iStorage.CheckStorage(productId))
            {
                Console.WriteLine("仓储检测失败");
            }
            else if (!iLogistics.CheckLogistics(userId, productId))
            {
                Console.WriteLine("物流检测失败");
            }
            else if (!iOrder.CheckOrder(userId, productId))
            {
                Console.WriteLine("订单检测失败");
            }
            else
            {
                iOrder.NewOrder(userId, productId);
                iLogistics.NewLogistics(userId, productId);
            }

        }
    }
}
