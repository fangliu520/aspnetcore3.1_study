using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreTest3._1.Utility.Filter
{
    /// <summary>
    /// 结果过滤器，可以对结果进行格式化、大小写转换等一系列操作。
    /// </summary>
    public class ResultFilterAttribute : Attribute, IResultFilter, IFilterMetadata, IOrderedFilter
    {
        public int Order => 0;

        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine($"This is {typeof(ResultFilterAttribute)} OnResultExecuted");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine($"This is {typeof(ResultFilterAttribute)} OnResultExecuting");
        }
    }
}
