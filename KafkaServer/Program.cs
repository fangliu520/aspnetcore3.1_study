using System;
using System.Threading.Tasks;

namespace KafkaServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            while (true)
            {
                Console.WriteLine("请输入发送的内容");
                var message = Console.ReadLine();
                string brokerList = "192.168.2.109:9093,192.168.2.109:9094,192.168.2.109:9095";
                ConfulentKafka.Produce(brokerList, "mjdtest", message);
            }
        }
    }
}
