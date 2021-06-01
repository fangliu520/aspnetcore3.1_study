using System;
using System.Collections.Generic;

namespace KafkaClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string brokerList = "192.168.2.109:9093,192.168.2.109:9094,192.168.2.109:9095";
            var topics = new List<string> { "mjdtest" };
            Console.WriteLine("请输入组名称");
            string groupname = Console.ReadLine();
            ConfulentKafka.Consumer(brokerList, topics, groupname);
        }
    }
}
