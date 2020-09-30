
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPICore3.Models
{
    public class CheckTokenFilter : Attribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string Token = context.HttpContext.Request.Headers["token"];
            if (string.IsNullOrEmpty(Token))
            {
                context.Result = new JsonResult(new Result() { Code = "10002", Msg = "Token参数不能为空！", Status = Status.Error });
            }
            else if (Token.Trim() != "123456")
            {
                context.Result = new JsonResult(new Result() { Code = "10002", Msg = "accessToken无效！", Status = Status.Error });
            }


        }

    }
}
