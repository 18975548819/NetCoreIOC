using DataEntities.InterfaceEntities;
using DataEntities.QueryModel;
using Freed.Common.Data;
using IBusinessManage.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessManage
{
    /// <summary>
    /// 配置信息业务处理逻辑层
    /// </summary>
    public interface IWmsPdaConfigManager:IDependencyManager
    {
        /// <summary>
        /// 获取仓库数据库地址信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<IWmsPdaConfig>> GetPdaConfigAllAsync(QueryData<WmsPdaConfigQuery> query);

        /// <summary>
        /// 根据登录仓库拼接查询字符串
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<string>> QueryWmsPdaConnAsync(WmsPdaConfigQuery query);


        /// <summary>
        /// 根据登录仓库获取对应配置信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<IWmsPdaConfig>> QueryWmsPdaConfigAsync(WmsPdaConfigQuery query);

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<IWmsPdaUser>> LoginAsync(QueryData<WmsPdaUserQuery> query);
    }
}
