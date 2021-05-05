using System;
using BenchmarkDotNet;
namespace ConsoleAppJVM
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkDotNet.Running.BenchmarkRunner.Run<MD5Test>();
            Console.WriteLine("ok");
            Console.ReadLine();
        }
    }
}
