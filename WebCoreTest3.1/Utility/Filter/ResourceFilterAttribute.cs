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

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            Console.WriteLine($"This is {typeof(ResourceFilterAttribute)} OnResourceExecuted");
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            Console.WriteLine($"This is {typeof(ResourceFilterAttribute)} OnResourceExecuting");
        }
    }
}
