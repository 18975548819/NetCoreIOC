using System;
using System.Collections.Generic;
using System.Text;

namespace Freed.FrameWork.CustomAop
{
    /// <summary>
    /// AOP测试对象
    /// </summary>
    public class CommonClass
    {
        /// <summary>
        /// 实现AOP的方法必须是虚方法：virtual
        /// </summary>
        public virtual void MethodInterceptor()
        {
            Console.WriteLine("This's Interceptor");
        }

        /// <summary>
        /// 无效AOP方法
        /// </summary>
        public void MethodNoInterceptor()
        {
            Console.WriteLine("This's without Interceptor");
        }
    }
}
