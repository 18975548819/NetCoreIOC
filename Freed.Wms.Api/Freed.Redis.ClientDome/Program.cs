using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freed.Redis.ClientDome
{
    class Program
    {
        static void Main(string[] args)
        {
            //Redis的String使用
            //{
            //    RedisUnitHepler.StringViod();
            //}

            //秒杀
            {
                try
                {
                    string guidStr = Guid.NewGuid().ToString();
                    int minut = 15;
                    using (RedisClient client = new RedisClient("127.0.0.1", 6379))
                    {
                        client.Set<int>("number", 10);
                    }
                    RedisUnitHepler.MiaoSha(guidStr, minut);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.ReadLine();
                }
            }
        }
    }
}
