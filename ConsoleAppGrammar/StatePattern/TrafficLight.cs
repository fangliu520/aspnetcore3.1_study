using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppGrammar.StatePattern
{
    /// <summary>
    /// 这种写法耦合度太高，需要单独拆分灯
    /// </summary>
    public class TrafficLight
    {
        public LightColor Color { get; set; }

        public void Show()
        {

            if (this.Color == LightColor.Green)
            {
                Console.WriteLine("绿灯行");
            }
            else if (Color == LightColor.Red)
            {
                Console.WriteLine("红灯停");
            }
            else if (Color == LightColor.Yellow)
            {
                Console.WriteLine("黄灯等一等");
            }
        }
        public void Turn()
        {

            if (this.Color == LightColor.Green)
            {
                Color = LightColor.Yellow;

            }
            else if (Color == LightColor.Red)
            {
                Color = LightColor.Green;
            }
            else if (Color == LightColor.Yellow)
            {
                Color = LightColor.Red;
            }
        }
       
    } 
    public enum LightColor
        {
            Red,
            Green,
            Yellow
        }
}
