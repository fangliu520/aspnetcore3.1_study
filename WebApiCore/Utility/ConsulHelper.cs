using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Consul;
namespace WebApiCore.Utility
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
                    Name = "MJD",//分组
                    Address =ip, //"127.0.0.1",
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
            catch (Exception e) {
                Console.WriteLine($"Consule注册：{e.Message}|{e.StackTrace}");
            }
        }
    }
}
