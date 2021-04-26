using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebCoreTest3._1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

        #region ��־������÷���һ
                //.ConfigureLogging((context, loggingBuilder) =>
                //{
                //    loggingBuilder.AddFilter("System", LogLevel.Warning);//���˵�System
                //    loggingBuilder.AddFilter("Microsoft", LogLevel.Warning);//���˵�Microsoft            
                //    loggingBuilder.AddLog4Net($"log4net.config");//�����ļ�
                //}) 
        #endregion
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
