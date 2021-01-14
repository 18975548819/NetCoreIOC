using Castle.DynamicProxy;
using System;

namespace Freed.FrameWork.CustomAop
{
    /// <summary>
    /// 基本实现，在业务逻辑中实现AOP
    /// </summary>
    public class CustomAop : StandardInterceptor
    {
        /// <summary>
        /// 执行前
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PreProceed(IInvocation invocation)
        {
            Console.WriteLine("方法执行前。。。。。。。。。。");
        }

        /// <summary>
        /// 执行中
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PerformProceed(IInvocation invocation)
        {
            Console.WriteLine("执行方法：{0}",invocation.Method.Name);
            base.PerformProceed(invocation);
        }

        /// <summary>
        /// 执行后
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PostProceed(IInvocation invocation)
        {
            Console.WriteLine("方法执行后。。。。。。。。。");
        }
    }
}
