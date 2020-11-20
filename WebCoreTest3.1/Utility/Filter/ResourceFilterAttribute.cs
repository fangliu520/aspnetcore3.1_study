using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebCoreTest3._1.Utility.Filter
{

    /// <summary>
    /// 资源过滤器
    ///可以通过Resource Filter 进行资源缓存、防盗链等操作。
    /// </summary>
    public class ResourceFilterAttribute : Attribute, IResourceFilter, IFilterMetadata, IOrderedFilter
    {
        public int Order => 0;

        private static Dictionary<string, object> dic = new Dictionary<string, object>();
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //操作一个缓存实例
            string key = context.HttpContext.Request.Path;
            dic[key] = context.Result;
            Console.WriteLine($"This is {typeof(ResourceFilterAttribute)} OnResourceExecuted");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string key = context.HttpContext.Request.Path;
            if (dic.ContainsKey(key))
            {
                context.Result = (IActionResult)dic[key];
            }
            Console.WriteLine($"This is {typeof(ResourceFilterAttribute)} OnResourceExecuting");
        }
    }
}
