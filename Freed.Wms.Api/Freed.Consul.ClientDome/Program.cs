using Castle.DynamicProxy;
using Freed.Consul.ClientDome.Utility;
using Freed.FrameWork.CustomAop;
using System;
using ITestHepler;
using TestHepler;
using Freed.IocFactory.CustomIoc;
using Freed.IocFactory.CustomContainer;

namespace Freed.Consul.ClientDome
{
    /// <summary>
    /// Consul客户端测试
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            //自定义IOC
            {
                //ICustomTest customTest = ObjectFactory.CreaterTest();
                //customTest = (ICustomTest)customTest.AOP(typeof(ICustomTest));
                //string dt =  customTest.GetDateTime();
                //Console.WriteLine(dt);
            }

            //IOC工厂模式注册
            {
                IContainerFactory container = new ContainerFactory();  
                container.Register<ICustomTest, CustomTest>();
                ICustomTest customTest = container.Resolve<ICustomTest>();
                string dt =  customTest.GetDateTime();
                Console.WriteLine(dt);
            }


            //测试AOP，自定义AOP测试
            {
                #region 对象中实现AOP注入
                //ProxyGenerator generator = new ProxyGenerator();  //动态代理
                //CustomAop customAop = new CustomAop();  //注入对象
                //CommonClass common = generator.CreateClassProxy<CommonClass>(customAop);//创建对象
                //common.MethodNoInterceptor(); //无效AOP方法
                //common.MethodInterceptor();//有效AOP方法
                #endregion


                #region 接口中实现AOP注入
                //ICustomTest custom = new CustomTest();
                //custom = (ICustomTest)custom.AOP(typeof(ICustomTest)); //注入
                //string dt = custom.GetDateTime();
                //Console.WriteLine(dt);
                #endregion

            }

            //consul接口调用测试
            {
                //var consulPath = ConsulHelper.GetConsulInfoList();  //consul发现
                //string url = "";
                //foreach (var item in consulPath.Result)
                //{
                //    url = item;
                //    Console.WriteLine(item);
                //}
                //if (!string.IsNullOrEmpty(url))
                //{
                //    url = "http://" + url + "/Test/Get";
                //    string msg = ApiHelper.InvokeApi(url); //获取接口数据
                //    Console.WriteLine(msg.ToString());
                //}
                //Console.WriteLine("Consul获取成功！");
            }

            Console.ReadLine();
        }
    }
}
