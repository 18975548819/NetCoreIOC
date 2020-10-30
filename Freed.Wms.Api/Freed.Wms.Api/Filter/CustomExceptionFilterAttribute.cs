using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freed.Wms.Api.Filter
{
    public class CustomExceptionFilterAttribute:ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //如果发生异常未处理是执行下面操作
            if (!context.ExceptionHandled)
            {
                context.Result = new JsonResult(new
                {
                    Code = -500,
                    HasErr = true,
                    Msg = "系统发生未处理异常，请联系开发人员",
                    Data = ""
                });
                context.ExceptionHandled = true;
            }
        }
    }
}
