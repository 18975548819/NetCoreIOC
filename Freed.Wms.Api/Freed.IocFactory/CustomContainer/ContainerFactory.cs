using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Freed.FrameWork.AttributeHepler;

namespace Freed.IocFactory.CustomContainer
{
    /// <summary>
    /// 用来生成对象
    /// </summary>
    public class ContainerFactory : IContainerFactory
    {
        private Dictionary<string, Type> containerDictionary = new Dictionary<string, Type>();

        /// <summary>
        /// 注册IOC
        /// </summary>
        /// <typeparam name="TFronm"></typeparam>
        /// <typeparam name="TTo"></typeparam>
        public void Register<TFronm, TTo>() where TTo : TFronm  //泛型：代表TTo是来自TFronm  约束
        {
            this.containerDictionary.Add(typeof(TFronm).FullName, typeof(TTo));
        }


        #region 使用递归获取   应对嵌套
        public TFronm Resolve<TFronm>()
        {
            return (TFronm)this.ResolveObject(typeof(TFronm));
        }


        /// <summary>
        /// 使用递归创建参数对象---实现无限层级
        /// </summary>
        /// <param name="abstractType"></param>
        /// <returns></returns>
        private object ResolveObject(Type abstractType)
        {
            string key = abstractType.FullName;
            Type type = this.containerDictionary[key];
            //var ctor = type.GetConstructors()[0];  //获取构造函数参数  / 获取第一个构造函数
            ConstructorInfo ctor = null;  //构造函数
            ctor = type.GetConstructors().OrderBy(c => c.GetParameters().Length).First();  //获取构造函数参数最多的一个
            //ctor = type.GetConstructors().OrderBy(c => c.IsDefined(typeof(特性名称))).FirstOrDefault();  //检查特性

            #region 准备构造函数的参数  构造参数注入
            List<object> paraList = new List<object>();
            foreach (var para in ctor.GetParameters())
            {
                Type paraType = para.ParameterType;  //获取参数的类型
                object paraInstance = this.ResolveObject(paraType);
                paraList.Add(paraInstance);
                //string paraaKey = paraType.FullName; //获取参数的完整名称
                //Type paraTargetType = this.containerDictionary[paraaKey];  //获取注册的资源信息
                //paraList.Add(Activator.CreateInstance(paraTargetType));  //保存生成的参数对象
            }
            #endregion

            //object oInstance = Activator.CreateInstance(type);
            object oInstance = Activator.CreateInstance(type, paraList.ToArray());


            #region 属性注入
            //foreach (var prop in abstractType.GetProperties().Where(p => p.IsDefined(typeof(BaseInterceptorAttribute),true)))
            //{
            //    Type propType = prop.PropertyType;  //获取特性Type
            //    object propInstance = this.ResolveObject(propType);  //获取特性特性
            //    prop.SetValue(oInstance, propInstance);  //给参数设置特性
            //}
            #endregion
            //return (TFronm)oInstance;
            return oInstance;
        }
        #endregion


        #region 普通获取
        /// <summary>
        /// 获取注册对象
        /// </summary>
        /// <typeparam name="TFronm"></typeparam>
        /// <returns></returns>
        //public TFronm Resolve<TFronm>()
        //{
        //    string key = typeof(TFronm).FullName;
        //    Type type = this.containerDictionary[key];
        //    var ctor = type.GetConstructors()[0];  //获取构造函数参数
        //    #region 准备构造函数的参数
        //    List<object> paraList = new List<object>();
        //    foreach (var para in ctor.GetParameters())
        //    {
        //        Type paraType = para.ParameterType;  //获取参数的类型
        //        string paraaKey = paraType.FullName; //获取参数的完整名称
        //        Type paraTargetType = this.containerDictionary[paraaKey];  //获取注册的资源信息
        //        paraList.Add(Activator.CreateInstance(paraTargetType));  //保存生成的参数对象
        //    }
        //    #endregion

        //    //object oInstance = Activator.CreateInstance(type);
        //    object oInstance = Activator.CreateInstance(type, paraList.ToArray());
        //    return (TFronm)oInstance;
        //}
        #endregion
    }
}
