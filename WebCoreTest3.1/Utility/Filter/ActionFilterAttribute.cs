using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreTest3._1.Utility.Filter
{
    /// <summary>
    /// 可以通过ActionFilter 拦截 每个执行的方法进行一系列的操作，比如：执行操作日志、参数验证，权限控制 等一系列操作。
    /// </summary>
    public class ActionFilterAttribute : Attribute, IActionFilter, IFilterMetadata, IOrderedFilter
    {
        public int Order => 0;

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"This is {typeof(ActionFilterAttribute)} OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"This is {typeof(ActionFilterAttribute)} OnActionExecuting");
        }
    }
}
