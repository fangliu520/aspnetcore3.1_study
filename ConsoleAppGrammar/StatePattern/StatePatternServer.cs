using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.StatePattern
{
    /// <summary>
    /// 状态模式
    /// 哪些场景可以用到
    /// 电流 -公交卡 进站 出站 -投票 0-5 有效 无效
    /// </summary>
    public class StatePatternServer
    {
        public static void Show()
        {


            {
                TrafficLight light = new TrafficLight()
                {
                    Color = LightColor.Green
                };
                light.Show();
                light.Turn();
                light.Show();
                light.Turn();
                light.Show();
                light.Turn();
            }
            {
                ///这种事有问题的，灯没有qiehan
                Console.WriteLine("****************************");
                LightBase light = new LightGreen();
                light.Show();
                light.Turn();
                light.Show();
                light.Turn();
                light.Show();
                light.Turn();
            }

            {

                Console.WriteLine("_________________________");
                LightBase light = new LightGreen();
                Context context = new Context()
                {
                    CurrentLight = light
                };
                context.Show();
                context.Turn();
                context.Show();
                context.Turn();
                context.Show();
                context.Turn();

            }
        }
    }
}
