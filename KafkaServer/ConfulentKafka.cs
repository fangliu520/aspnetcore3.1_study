using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
namespace KafkaServer
{
     class ConfulentKafka
    {
        public static async Task Produce(string brokerList, string topicName, string content)
        {

            var config = new ProducerConfig() {
                BootstrapServers = brokerList,
                // ack ---注意注意注意。。
                // 代码封装的原因，消费端也可以写，实际没有用到===
                // 数据写入保存机制--- 保证数据不丢失，而且会影响到我们性能，
                // 越高级别数据不丢失，则写入的性能越差
                Acks = Acks.All,
                //幂等性 保证数据不丢失
                 EnableIdempotence=true,
                //信息发送完，多久吧数据发送到broker里面去
                LingerMs = 100,
                //控制条数，当内存的数据条数达到了，立刻马上发送过去
                // 只要上面的两个要求符合一个，则后台线程立刻马上把数据推送给broker
                // 可以看到发送的偏移量，如果没有偏移量，则就是没有写成功
                BatchNumMessages = 2,
                //补偿重试，发送失败了则重试 
                //  Partitioner = Partitioner.Random
                MessageSendMaxRetries=3

            };
            using (var producer = new ProducerBuilder<string, string>(config).Build()) 
            {
                Console.WriteLine("\n-----------------------------------------------------------------------");
                Console.WriteLine($"Producer {producer.Name} producing on topic {topicName}.");
                Console.WriteLine("-----------------------------------------------------------------------");
                try {
                    // 建议使用异步，传说性能比较好
                    // Key 注意是做负载均衡，注意： 比如，有三个节点，一个topic，创建了三个分区。。一个节点一个分区，但是，如果你在写入的数据的时候，没有写key,这样会导致，所有的数据存放到一个分区上面。。。
                    //ps：如果用了分区，打死也要写key .根据自己的业务，可以提前配置好，
                    // key的随机数，可以根据业务，搞一个权重，如果节点的资源不一样，合理利用资源，可以去写一个
                    var deliveryReport = await producer.ProduceAsync(topicName, new Message<string, string> { Key = new Random().Next(0, 9).ToString(), Value = content });
                    Console.WriteLine($"delivered to: {deliveryReport.TopicPartitionOffset}");
                }
                catch (ProduceException<string, string> e) {
                    Console.WriteLine($"failed to deliver message: {e.Message}|{e.StackTrace} [{e.Error.Code}]");
                }

            }
        }
    }
}
