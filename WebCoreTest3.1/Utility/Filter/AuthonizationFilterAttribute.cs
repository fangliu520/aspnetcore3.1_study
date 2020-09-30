using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace WebCoreTest3._1.Utility.Filter
{
    /// <summary>
    /// 权限控制过滤器
    ///通过 Authonization Filter 可以实现复杂的权限角色认证、登陆授权等操作
    /// </summary>
    public class AuthonizationFilterAttribute : Attribute, IAuthorizationFilter, IFilterMetadata, IOrderedFilter
    {
        public int Order => 0;

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //这里可以做复杂的权限控制操作
            if (context.HttpContext.User.Identity.Name != "1") //简单的做一个示范
            {
                //未通过验证则跳转到无权限提示页
                RedirectToActionResult content = new RedirectToActionResult("NoAuth", "Exception", null);
                context.Result = content;
            }
        }
    }
}
