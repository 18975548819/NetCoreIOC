using DataEntities.InterfaceEntities;
using DataEntities.InterfaceModel;
using DataEntities.QueryModel;
using Freed.Common.Data;
using IBusinessManage;
using IDataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManage
{
    /// <summary>
    /// 配置信息操作业务逻辑层
    /// </summary>
    public class WmsPdaConfigManager : IWmsPdaConfigManager
    {
        private IWmsPdaConfigService _service;

        public WmsPdaConfigManager(IWmsPdaConfigService service)
        {
            _service = service;
        }

        /// <summary>
        ///获取仓库数据库地址
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ListResult<IWmsPdaConfig>> GetPdaConfigAllAsync(QueryData<WmsPdaConfigQuery> query)
        {
            var lr = new ListResult<IWmsPdaConfig>();
            var dt = DateTime.Now;

            var res = await _service.GetPdaConfigAllAsync(query);
            if (res.HasErr)
            {
                lr.SetInfo("查询异常", res.ErrCode);
            }
            else
            {
                var lst = new List<IWmsPdaConfig>();
                foreach (var item in res.Data)
                {
                    IWmsPdaConfig config = new WmsPdaConfigModel();
                    config = item;
                    lst.Add(config);
                }
                lr.PageModel = res.PageInfo;
                lr.SetData(lst);
            }

            lr.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return lr;
        }


        /// <summary>
        /// 根据分组标识获取数据库连接地址
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ErrData<string>> QueryWmsPdaConnAsync(WmsPdaConfigQuery query)
        {
            var result = new ErrData<string>();
            var dt = DateTime.Now;

            var queryEx = new QueryData<WmsPdaConfigQuery>();
            queryEx.Criteria = query;
            var res = await _service.GetPdaConfigAllAsync(queryEx);
            if (res.HasErr)
            {
                result.SetInfo("查询异常", res.ErrCode);
            }
            else
            {
                var info = res.Data.FirstOrDefault();
                if (info != null)
                {
                    string connStr = string.Format("Data Source={0};User ID={1};Password={2};initial catalog={3};Max Pool Size = 512;",
                        info.Source, info.Uid, info.Pwd, info.DataBase);
                    result.Data = connStr;
                }
                else
                {
                    result.SetInfo("登录厂区不存在", res.ErrCode);
                }
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }


        public async Task<ErrData<IWmsPdaConfig>> QueryWmsPdaConfigAsync(WmsPdaConfigQuery query)
        {
            var result = new ErrData<IWmsPdaConfig>();
            var dt = DateTime.Now;

            var queryEx = new QueryData<WmsPdaConfigQuery>();
            queryEx.Criteria = query;
            var res = await _service.GetPdaConfigAllAsync(queryEx);
            if (res.HasErr)
            {
                result.SetInfo("查询异常", res.ErrCode);
            }
            else
            {
                var info = res.Data.FirstOrDefault();
                if (info != null)
                {
                    result.SetInfo(info, "成功", 200);
                }
                else
                {
                    result.SetInfo("登录厂区不存在", res.ErrCode);
                }
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }


        public async Task<ErrData<IWmsPdaUser>> LoginAsync(QueryData<WmsPdaUserQuery> query)
        {
            var result = new ErrData<IWmsPdaUser>();
            var dt = DateTime.Now;

            //根据厂区获取数据库连接串
            var connQuery = new WmsPdaConfigQuery();
            connQuery.GroupType = query.Criteria.GroupType;
            var conn = await QueryWmsPdaConnAsync(connQuery);
            query.SqlConn = conn.Data;

            var res = await _service.GetWmsPdaUserAllAsync(query);
            if (res.HasErr)
            {
                result.SetInfo("查询异常", res.ErrCode);
            }
            else
            {
                var info = res.Data.FirstOrDefault();
                if (info != null)
                {
                    result.SetInfo(info, "登录成功！", 200);
                }
                else
                {
                    result.SetInfo(null, "用户名或密码错误！", -101);
                }
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }
    }
}
