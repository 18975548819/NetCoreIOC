using Castle.DynamicProxy;
using Freed.FrameWork.AttributeHepler;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Freed.FrameWork.CustomAop
{
    /// <summary>
    /// 实现接口AOP注入,使用特性的方式注入
    /// </summary>
    public class CustomInterfaceAop : StandardInterceptor
    {
        #region 组装
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
            //按顺序注入AOP
            {
                //获取方法标记的特性
                var method = invocation.Method;
                Action action = () => base.PerformProceed(invocation);  //组装
                if (method.IsDefined(typeof(BaseInterceptorAttribute), true))//获取特性
                {
                    #region 获取多个特性
                    //foreach (var attribute in method.GetCustomAttributes<BaseInterceptorAttribute>())
                    foreach (var attribute in method.GetCustomAttributes<BaseInterceptorAttribute>().ToArray().Reverse())  //顺序控制   特性标记越往下越靠近核心业务
                    {
                        action = attribute.Do(invocation, action);  //进行组装
                        //attribute.Do(invocation); //不进行组装
                    }
                    #endregion

                    #region 获取单个特性
                    //var attribute = method.GetCustomAttribute<BaseInterceptorAttribute>();
                    //attribute.Do();
                    #endregion
                }
                //Console.WriteLine("执行方法：{0}", invocation.Method.Name);
                //base.PerformProceed(invocation);
                action.Invoke();
            }
        }

        /// <summary>
        /// 执行后
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PostProceed(IInvocation invocation)
        {
            Console.WriteLine("方法执行后。。。。。。。。。");
        }
        #endregion

        #region 正常使用
        ///// <summary>
        ///// 执行前
        ///// </summary>
        ///// <param name="invocation"></param>
        //protected override void PreProceed(IInvocation invocation)
        //{
        //    Console.WriteLine("方法执行前。。。。。。。。。。");
        //}

        ///// <summary>
        ///// 执行中
        ///// </summary>
        ///// <param name="invocation"></param>
        //protected override void PerformProceed(IInvocation invocation)
        //{
        //    //获取方法标记的特性
        //    var method = invocation.Method;

        //    if (method.IsDefined(typeof(BaseInterceptorAttribute), true))//获取特性
        //    {
        //        #region 获取多个特性
        //        foreach (var attribute in method.GetCustomAttributes<BaseInterceptorAttribute>())
        //        {
        //            attribute.Do();
        //        }
        //        #endregion

        //        #region 获取单个特性
        //        //var attribute = method.GetCustomAttribute<BaseInterceptorAttribute>();
        //        //attribute.Do();
        //        #endregion
        //    }
        //    Console.WriteLine("执行方法：{0}", invocation.Method.Name);
        //    base.PerformProceed(invocation);
        //}

        ///// <summary>
        ///// 执行后
        ///// </summary>
        ///// <param name="invocation"></param>
        //protected override void PostProceed(IInvocation invocation)
        //{
        //    Console.WriteLine("方法执行后。。。。。。。。。");
        //}
        #endregion
    }
}
