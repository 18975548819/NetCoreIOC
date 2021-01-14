using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Castle.DynamicProxy;

namespace Freed.FrameWork.AttributeHepler
{
    //[AttributeUsage(AttributeTargets.Method)]  //代表方法注入
    /// <summary>
    /// 基础特性
    /// </summary>
    public abstract class BaseInterceptorAttribute:Attribute
    {
        /// <summary>
        /// 进行组装
        /// </summary>
        /// <param name="invocation"></param>
        /// <param name="action"></param>
        public abstract Action Do(IInvocation invocation, Action action);

        /// <summary>
        /// 不进行组装
        /// </summary>
        /// <param name="invocation"></param>
        //public abstract void Do(IInvocation invocation);
    }
}
