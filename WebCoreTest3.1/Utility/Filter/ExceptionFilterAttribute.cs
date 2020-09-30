using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreTest3._1.Utility.Filter
{
    public class ExceptionFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.ExceptionFilterAttribute
    {
        private ILogger<ExceptionFilterAttribute> _logger;
        //构造注入日志组件
        public ExceptionFilterAttribute(ILogger<ExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }
        public override void OnException(ExceptionContext context)
        {
            //日志收集
            _logger.LogError(context.Exception, context?.Exception?.Message ?? "异常");
            if (!context.ExceptionHandled)
            {
                Console.WriteLine($"ExceptionFilter异常：{context.HttpContext.Request.Path} {context.Exception.Message} {context.Exception.StackTrace}");

                context.Result = new JsonResult(new
                {

                    Result = true,
                    Msg = "发生异常，请联系管理员！"
                });
                context.ExceptionHandled = true;
            }
            //base.OnException(context);
        }
    }
}
