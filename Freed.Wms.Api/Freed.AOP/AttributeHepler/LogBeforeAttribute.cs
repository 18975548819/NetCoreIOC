using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Freed.FrameWork.AttributeHepler
{
    /// <summary>
    /// 定义特性
    /// </summary>
    public class LogBeforeAttribute : BaseInterceptorAttribute
    {

        public override Action Do(IInvocation invocation,Action action)
        {
            return () =>
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                action.Invoke();
                Console.WriteLine("This's LogBeforeAttribute Log......");
                stopwatch.Stop();
                Console.WriteLine("总共花费时长：" + stopwatch.ElapsedMilliseconds.ToString());
            };
        }

        //public override void Do(IInvocation invocation)
        //{
        //    Stopwatch stopwatch = new Stopwatch();
        //    stopwatch.Start();
        //    Console.WriteLine("This's LogBeforeAttribute Log......");
        //    stopwatch.Stop();
        //    Console.WriteLine("总共花费时长：" + stopwatch.ElapsedMilliseconds.ToString());
        //}
    }
}
