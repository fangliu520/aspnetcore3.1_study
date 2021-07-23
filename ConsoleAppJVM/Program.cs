using System;
using BenchmarkDotNet;
namespace ConsoleAppJVM
{
    class Program
    {
        static void Main(string[] args)
        {
            //  BenchmarkDotNet.Running.BenchmarkRunner.Run<MD5Test>();
            //int x = -10;
            //int y = -3;
            //int r = x % y;
            //Console.WriteLine(r);//负数取模 值为1

            int r = f(5);//1 2 3 5 8
            Console.WriteLine(r);


             r = sum(100);//1 2 3 4 5
            Console.WriteLine(r);
            Console.WriteLine("ok");
            Console.ReadLine();
        }


        private static int f(int n)
        {
            if (n == 1)
                return 1;
            else if (n == 2)
            {
                return 2;
            }
            else
                return f(n-1) + f(n - 2);

        }

        private static int sum(int n)
        {
            if (n == 1)
                return 1;
          
            else
                return sum(n - 1) + n;

        }

    }
}
