using DataEntities.InterfaceEntities;
using DataEntities.QueryModel;
using Freed.Common.Data;
using IDataService.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IDataService
{
    /// <summary>
    /// 配置信息数据操作接口定义
    /// </summary>
    public interface IWmsPdaConfigService : IIDependencyService
    {
        /// <summary>
        /// 获取仓位的数据库连接信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IWmsPdaConfig>>> GetPdaConfigAllAsync(QueryData<WmsPdaConfigQuery> query);

        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IWmsPdaUser>>> GetWmsPdaUserAllAsync(QueryData<WmsPdaUserQuery> query);

        /// <summary>
        /// 获取仓位信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IWmsRepertory>>> GetWmsRepertoryAllAsync(QueryData<WmsRepertoryQuery> query);
    }
}
