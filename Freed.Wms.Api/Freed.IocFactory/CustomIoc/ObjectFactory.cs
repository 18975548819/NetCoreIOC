using ITestHepler;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Freed.IocFactory.CustomIoc
{ 
    /// <summary>
    /// 创建对象工厂
    /// </summary>
    public class ObjectFactory
    {
        public static ICustomTest CreaterTest()
        {
            ICustomTest customTest = null;

            //反射
            Assembly assembly = Assembly.Load("TestHepler");
            Type type = assembly.GetType("TestHepler.CustomTest");
            customTest = (ICustomTest)Activator.CreateInstance(type);
            return customTest;
        }
    }
}
