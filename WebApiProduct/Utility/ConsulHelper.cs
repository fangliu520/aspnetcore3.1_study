using Consul;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiProduct.Utility
{
    public static class ConsulHelper
    {
        public static void ConsulRegist(this IConfiguration configuration)
        {
            try
            {
                string ip = configuration["ip"];
                string port = configuration["port"];
                string weight = configuration["weight"];
                string consulAddress = configuration["ConsulAddress"];
                string consulCenter = configuration["ConsuleCenter"];
                ConsulClient client = new ConsulClient(c =>
                {
                    c.Address = new Uri(consulAddress);// new Uri("http://localhost:8500");
                    c.Datacenter = consulCenter;//"dc1";
                });
                client.Agent.ServiceRegister(new AgentServiceRegistration()
                {
                    ID = $"service {ip}:{port}",//唯一
                    Name = "MJD_Product",//分组
                    Address = ip, //"127.0.0.1",
                    Port = int.Parse(port),
                    Tags = new string[] { weight.ToString() },//null,
                    Check = new AgentServiceCheck()
                    {
                        Interval = TimeSpan.FromSeconds(10),
                        HTTP = $"http://{ip}:{port}/api/Healthcheck",
                        Timeout = TimeSpan.FromSeconds(5),
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(20)
                    }
                });
                Console.WriteLine($"{ip}:{port}-weight:{weight}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"webapiproduct|Consule注册：{e.Message}|{e.StackTrace}");
            }

            //ConsulClient client = new ConsulClient(c =>
            //{
            //    c.Address = new Uri("http://localhost:8500");
            //    c.Datacenter = "dc1";
            //});
            //string ip = configuration["ip"];
            //int port = Convert.ToInt32(configuration["Port"]);
            //client.Agent.ServiceRegister(new AgentServiceRegistration()
            //{
            //    ID = $"service{Guid.NewGuid()}",
            //    Name = "webapiproduct",
            //    Address = "127.0.0.1",
            //    Port = port,
            //    Tags = null,
            //    Check = new AgentServiceCheck()
            //    {
            //        Interval = TimeSpan.FromSeconds(10),
            //        HTTP = $"http://{ip}:{port}/api/Healthcheck",
            //        Timeout = TimeSpan.FromSeconds(5),
            //        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5)
            //    }
            //});

        }
    }
}
