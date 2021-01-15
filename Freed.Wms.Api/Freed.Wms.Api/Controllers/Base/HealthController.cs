using Freed.CacheFactory.Unility;
using IBusinessManage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freed.Wms.Api.Controllers.Base
{
    /// <summary>
    /// 健康检查
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private  IConfiguration _configuration;
        private IMemoryCache _iMemoryCache;
        private ICustomMemoryCache _customMemoryCache;

        public HealthController(IConfiguration configuration, IMemoryCache memoryCache,ICustomMemoryCache customMemoryCache)
        {
            _configuration = configuration;
            _iMemoryCache = memoryCache;
            _customMemoryCache = customMemoryCache;
        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            Console.WriteLine($"心跳健康检查:{_configuration["port"]}端口正常！");
            return Ok($"心跳健康检查:{_configuration["port"]}端口正常！");
        }


        [HttpGet]
        [Route("GetSystemDateTime")]
        public IActionResult GetSystemDateTime()
        {
            string dt = "";
            #region MemoryCache 缓存
            string key = $"Test";
            if (this._iMemoryCache.TryGetValue<string>(key, out string time))
            {
                dt = time;
            }
            else
            {
                time = DateTime.Now.ToString();
                this._iMemoryCache.Set(key, time, TimeSpan.FromSeconds(120));
                dt = time;
            }
            #endregion

            #region 自定义缓存（第三方缓存）
            //string key = $"Test";
            //if (this._customMemoryCache.TryGetValue(key, out object time))
            //{
            //    dt = time.ToString();
            //}
            //else
            //{
            //    time = DateTime.Now.ToString();
            //    this._customMemoryCache.Set(key, time);
            //    dt = time.ToString();
            //}
            #endregion

            return Ok($"当前系统时间：{dt}");
        }
    }
}
