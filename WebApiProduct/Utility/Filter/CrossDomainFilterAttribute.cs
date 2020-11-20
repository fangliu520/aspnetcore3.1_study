using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiProduct.Utility.Filter
{
    /// <summary>
    /// 解决跨域Filter
    /// </summary>
    public class CrossDomainFilterAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");//解决跨域问题
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
          
        }
    }
}
