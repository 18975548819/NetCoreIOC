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
        Task<DataResult<List<IWmsPdaConfig>>> GetPdaConfigAllAsync(QueryData<WmsPdaConfigQuery> query);

        Task<DataResult<List<IWmsPdaUser>>> GetWmsPdaUserAllAsync(QueryData<WmsPdaUserQuery> query);
    }
}
