using ConsoleAppGrammar.DesignerPatter.BridgePattern.Bridge;
using ConsoleAppGrammar.DesignerPatter.BridgePattern.Bridge.System;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.DesignerPatter.BridgePattern
{
    /// <summary>
    /// 桥接模式（结构型设计模式） 多重继承 变化封装 解决多维度的变化
    /// </summary>
    public class BridgePatternServer
    {
        public void Show()
        {

            #region 没用桥接模式前的代码
            //{
            //    BasePhone phone = new iPhone();
            //    phone.Call();
            //    phone.Text();
            //    Console.WriteLine("********************");
            //}

            //{
            //    BasePhone phone = new Galaxy();
            //    phone.Call();
            //    phone.Text();
            //    Console.WriteLine("********************");
            //}
            //{
            //    BasePhone phone = new GalaxyIOS();
            //    phone.Call();
            //    phone.Text();
            //    Console.WriteLine("********************");
            //}
            //{
            //    BasePhone phone = new iPhoneAndroid();
            //    phone.Call();
            //    phone.Text();
            //    Console.WriteLine("********************");
            //} 
            #endregion
            ///一下是用桥接模式优化的代码
            ISystem android = new AndroidSystem();
            ISystem IOS = new IOSSystem();
            ISystem WinPhone = new WinPhoneSystem();
            Console.WriteLine("********************");
            {
                BasePhoneBridge phone = new iPhoneBridge();
                phone.SystemVersion = IOS;
                phone.Call();
                phone.Text();              

            }
            Console.WriteLine("********************");
            {
                BasePhoneBridge phone = new GalaxyBridge();
                phone.SystemVersion = android;
                phone.Call();
                phone.Text();

            }

            Console.WriteLine("********************");
            {
                BasePhoneBridge phone = new WinPhoneBridge();
                phone.SystemVersion = WinPhone;
                phone.Call();
                phone.Text();

            }

            Console.WriteLine("********************");
            {
                BasePhoneBridge phone = new iPhoneBridge();
                phone.SystemVersion = android;
                phone.Call();
                phone.Text();

            }
        }
    }
}
