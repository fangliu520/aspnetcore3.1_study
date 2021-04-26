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

        #region 日志组件引用方法一
                //.ConfigureLogging((context, loggingBuilder) =>
                //{
                //    loggingBuilder.AddFilter("System", LogLevel.Warning);//过滤掉System
                //    loggingBuilder.AddFilter("Microsoft", LogLevel.Warning);//过滤掉Microsoft            
                //    loggingBuilder.AddLog4Net($"log4net.config");//配置文件
                //}) 
        #endregion
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
