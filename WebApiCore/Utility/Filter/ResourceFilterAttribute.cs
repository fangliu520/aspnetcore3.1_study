using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore.Utility.Filter
{
    public class ResourceFilterAttribute : Attribute, IResourceFilter, IFilterMetadata, IOrderedFilter
    {
        public int Order => 0;

        private static Dictionary<string, object> dic = new Dictionary<string, object>();
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            //操作一个缓存实例
            string key = context.HttpContext.Request.Path;           
            dic[key] = (ObjectResult)context.Result;
            Console.WriteLine($"This is {typeof(ResourceFilterAttribute)} OnResourceExecuted");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string key = context.HttpContext.Request.Path;
            if (dic.ContainsKey(key))
            {
                context.Result = (ObjectResult)dic[key];
            }
            Console.WriteLine($"This is {typeof(ResourceFilterAttribute)} OnResourceExecuting");
        }
    }
}
