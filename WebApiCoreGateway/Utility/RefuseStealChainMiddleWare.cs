using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCoreGateway.Utility
{
    /// <summary>
    /// 防盗链中间件
    /// </summary>
    public class RefuseStealChainMiddleWare
    {
        private readonly RequestDelegate _next;
        public RefuseStealChainMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string url = httpContext.Request.Path.Value;
            if (!url.Contains(".jpg"))
            {
                await _next(httpContext);
            }
            string urlReferer = httpContext.Request.Headers["Referer"];

            if (string.IsNullOrWhiteSpace(urlReferer))//直接访问图片
            {
                await this.SetForbinddenImage(httpContext);
            }

            if (urlReferer.Contains("localhost"))//本地访问
            {
                await _next(httpContext);
            }
            else
            {
                await this.SetForbinddenImage(httpContext);
            }
        }
        /// <summary>
        /// 设置404图片
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private async Task SetForbinddenImage(HttpContext httpContext)
        {
            string defautImagePath = "Images/404.jpg";
            string path = Path.Combine(Directory.GetCurrentDirectory(), defautImagePath);
            FileStream fs = File.OpenRead(path);
            byte[] bytes = new byte[fs.Length];
            await fs.ReadAsync(bytes, 0, bytes.Length);
            await httpContext.Response.Body.WriteAsync(bytes, 0, bytes.Length);
        }
    }
}
