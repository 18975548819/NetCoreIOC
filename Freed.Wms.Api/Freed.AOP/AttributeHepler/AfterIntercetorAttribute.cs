using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freed.FrameWork.AttributeHepler
{
    public class AfterIntercetorAttribute : BaseInterceptorAttribute
    {
        public override Action Do(IInvocation invocation, Action action)
        {
            return () =>
            {
                Console.WriteLine("This's AfterIntercetorAttribute Log.........");
                action.Invoke();
            };
        }

        //public override void Do(IInvocation invocation)
        //{
        //    Console.WriteLine("This's AfterIntercetorAttribute Log.........");
        //}
    }
}
