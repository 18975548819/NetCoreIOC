using Freed.Consul.ClientDome.Utility;
using System;

namespace Freed.Consul.ClientDome
{
    /// <summary>
    /// Consul客户端测试
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var consulPath = ConsulHelper.GetConsulInfoList();  //consul发现
            string url = "";
            foreach (var item in consulPath.Result)
            {
                url = item;
                Console.WriteLine(item);
            }
            if (!string.IsNullOrEmpty(url))
            {
                url ="http://"+ url + "/Test/Get";
                string msg = ApiHelper.InvokeApi(url); //获取接口数据
                Console.WriteLine(msg.ToString());
            }
            Console.WriteLine("Consul获取成功！");
            Console.ReadLine();
        }
    }
}
