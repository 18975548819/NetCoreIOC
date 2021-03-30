using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ServiceStack.Redis;

namespace Freed.Redis.ClientDome
{
    public class RedisUnitHepler
    {
        /// <summary>
        /// Redis的String读写操作
        /// </summary>
        public static void StringViod()
        {
            using (RedisClient client = new RedisClient("127.0.0.1", 6379))
            {
                //删除当前数据库中的所有Key,默认删除的是db0
                client.FlushDb();
                //删除所有数据库中的key
                client.FlushAll();
                #region 设置key的values
                client.Set<string>("name","zeng.tao");
                //第一种获取values方式
                Console.WriteLine(client.Get<string>("name"));
                //第二种获取values方式
                Console.WriteLine(client.GetValue("name"));
                Console.ReadLine();
                #endregion
            }
            return;
        }


        /// <summary>
        /// 秒杀场景模拟
        /// </summary>
        /// <param name="id"></param>
        /// <param name="minute"></param>
        public static void MiaoSha(string id,int minute)
        {
            var flag = true;
            while (flag)
            {
                if (DateTime.Now.Minute == minute)
                {
                    Console.WriteLine($"在{minute}分0秒正式开启秒杀！");
                    flag = false;
                    for (int i = 0; i < 10; i++)
                    {
                        string name = $"客户端{id}号：{i}";
                        Task.Run(() => {
                            using (RedisClient client = new RedisClient("127.0.0.1", 6379))
                            {
                                //取到值自减1
                                var num = client.Decr("number");
                                if (num < 0)
                                {
                                    Console.WriteLine(name + "抢购失败");
                                }
                                else
                                {
                                    Console.WriteLine(name + "************抢购成功*************");
                                }
                            }
                        });
                        Thread.Sleep(10);
                    }
                }
            }
            return;
        }
    }
}
