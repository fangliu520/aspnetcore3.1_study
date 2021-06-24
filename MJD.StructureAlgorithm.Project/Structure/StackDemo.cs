using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MJD.StructureAlgorithm.Project.Structure
{
    public  class StackDemo
    {
        public static void show()
        {
            /*先进后出
              Stack<T>：FILO可以用链表实现，
              C#用的是数组Capacity—TrimExcessStack实际上是对数组的一个封装
            */
            {
               // Console.WriteLine("***************Stack<T>******************");
               // Stack<string> numStack = new Stack<string>();
               // numStack.Push("1");
               // numStack.Push("2");
               // numStack.Push("3");
               // numStack.Push("4");

               // foreach (string number in numStack)
               // {
               //     Console.WriteLine(number);
               // }
               // Console.WriteLine($"Pop '{numStack.Pop()}'");//获取并移除
               // Console.WriteLine($"Peek at next item to dequeue: { numStack.Peek()}");//获取不移除
               //// Console.WriteLine($"Pop '{numStack.Pop()}'");
               // Console.WriteLine("***************Stack移除后******************");
               // foreach (string number in numStack)
               // {
               //     Console.WriteLine(number);
               // }
               // Stack<string> stackCopy = new Stack<string>(numStack.ToArray());
               // foreach (string number in stackCopy)
               // {
               //     Console.WriteLine(number);
               // }
               // stackCopy.TrimExcess();//清理空白的空间

               // Console.WriteLine($"stackCopy.Contains(\"3\") = {stackCopy.Contains("3")}");
               // stackCopy.Clear();
               // Console.WriteLine($"stackCopy.Count = {stackCopy.Count}");
            }
            {
                //进制转换
                // BinaryConversion(5, 2);

                //回文检测

                ReplyDetection("上海自来水来自海上");

            }
        }

        /// <summary>
        ///   
        /// </summary>
        /// <param name="number">原进制的值</param>
        /// <param name="b">目标进制</param>
        private static void BinaryConversion(int number, int format)
        {
            Stack<int> targetStack = new Stack<int>();
            do
            {
                targetStack.Push(number % format);
                number = number / format;
            }
            while (number != 0);

            while (targetStack.Count > 0)
            {
                Console.Write(targetStack.Pop());
            }
        }

        private static bool ReplyDetection(string content)
        {

            foreach (var item in content)
            {
                Console.WriteLine(item);
            }
            return false;
        }
    }
}
