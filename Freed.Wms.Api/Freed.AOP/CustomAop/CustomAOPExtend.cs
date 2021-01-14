using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Freed.FrameWork.CustomAop
{
    /// <summary>
    /// AOP注入扩展对象
    /// </summary>
    public static class CustomAOPExtend
    {
        public static object AOP(this object t,Type interfaceType)
        {
            ProxyGenerator generator = new ProxyGenerator();  //动态代理
            CustomInterfaceAop interfaceAop = new CustomInterfaceAop();  //注入对象
            t = generator.CreateInterfaceProxyWithTarget(interfaceType,t,interfaceAop);
            return t;
        }
    }
}
