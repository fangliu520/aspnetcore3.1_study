﻿using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaClient
{
    class ConfulentKafka
    {
		public static void Consumer(string brokerlist, List<string> topicname, string groupname)
		{

			var mode = "subscribe";
			var brokerList = brokerlist;
			var topics = topicname;
			Console.WriteLine($"Started consumer, Ctrl-C to stop consuming");
			CancellationTokenSource cts = new CancellationTokenSource();
			Console.CancelKeyPress += (_, e) =>
			{
				e.Cancel = true; // prevent the process from terminating.
				cts.Cancel();
			};

			switch (mode)
			{
				case "subscribe":
					Run_Consume(brokerList, topics, groupname);
					break;
				case "manual":
					//Run_ManualAssign(brokerList, topics, cts.Token);
					break;
				default:
					//PrintUsage();
					break;
			}
		}
		/// <summary>
		///  消费端拿到数据,告诉kafka数据我已经消费完了
		/// </summary>
		/// <param name="brokerList"></param>
		/// <param name="topics"></param>
		/// <param name="group"></param>
		public static void Run_Consume(string brokerList, List<string> topics, string group)
        {
			var config = new ConsumerConfig
			{
				BootstrapServers = brokerList,
				GroupId = group,

				EnableAutoCommit = false,

				AutoOffsetReset = AutoOffsetReset.Earliest,

				//	EnablePartitionEof = true,
				//PartitionAssignmentStrategy = PartitionAssignmentStrategy.Range,
				//FetchMaxBytes =,
				//FetchWaitMaxMs=1,   

				//SessionTimeoutMs = 6000,
				//MaxPollIntervalMs = 5000,



			};
			const int commitPeriod = 5;
			// 提交偏移量的时候,也可以批量去提交
			using (var consumer = new ConsumerBuilder<Ignore, string>(config)
				.SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
				.SetPartitionsAssignedHandler((c, partitions) =>
				{
					//自定义存储偏移量
					// 1.每次消费完成，把相应的分区id和offset写入到mysql数据库存储
					//2.从指定分区和偏移量开始拉取数据
					//分配的时候调用
					Console.WriteLine($"Assigned partitions: [{string.Join(", ", partitions)}]");
					#region 指定分区消费
					// 之前可以自动均衡,现在不可以了 
					//List<TopicPartitionOffset> topics = new List<TopicPartitionOffset>();
					//// 我当前读取所有的分区里面的从10开始
					//foreach (var item in partitions)
					//{
					//	topics.Add(new TopicPartitionOffset(item.Topic, item.Partition, new Offset(10)));
					//}

					//return topics;
					#endregion
				})
				.SetPartitionsRevokedHandler((c, partitions) =>
				{
					//新加入消费者的时候调用 
					Console.WriteLine($"Revoking assignment: [{string.Join(", ", partitions)}]");
				})
				.Build())
			{
				//消费者会影响在平衡分区，当同一个组新加入消费者时，分区会在分配
				consumer.Subscribe(topics);
				try
				{

					while (true)
					{

						try
						{
							var consumeResult = consumer.Consume();

							if (consumeResult.IsPartitionEOF)
							{
								continue;
							}
							Console.WriteLine($": {consumeResult.TopicPartitionOffset}::{consumeResult.Message.Value}");


							if (consumeResult.Offset % commitPeriod == 0)
							{
								try
								{
									consumer.Commit(consumeResult);
									Console.WriteLine("提交");
								}
								catch (KafkaException e)
								{
									Console.WriteLine($"Commit error: {e.Error.Reason}");
								}
							}
						}
						catch (ConsumeException e)
						{
							Console.WriteLine($"Consume error: {e.Error.Reason}");
						}
					}
				}
				catch (OperationCanceledException)
				{
					Console.WriteLine("Closing consumer.");
					consumer.Close();
				}
			}
		}
	}
}
