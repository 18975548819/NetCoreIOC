using DataEntities.InterfaceEntities.BasicInfo;
using DataEntities.InterfaceModel.BasicInfo;
using DataEntities.QueryModel.BasicInfo;
using Freed.Common.Data;
using IBusinessManage.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessManage.BasicInfo
{
    /// <summary>
    /// 功能模块业务逻辑接口定义
    /// </summary>
    public interface ISysModuleManager: IDependencyManager
    {
        /// <summary>
        /// 获取功能模块信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysModule>> GetSysModuleMainListMaAsync(QueryData<GetBaseInfoByModuleQuery> query);

        /// <summary>
        /// 获取功能模块和子模块
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<SysModuleListModel>> GetSysModuleListMaAsync(QueryData<GetBaseInfoByModuleQuery> query);
        /// <summary>
        /// 新增功能模块
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>

        Task<ErrData<bool>> InsertSysModulMaAsync(QueryData<SysModule> query);

        /// <summary>
        /// 修改功能模块
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> EidtSysModulMaAsync(QueryData<SysModule> query);

        /// <summary>
        /// 删除功能模块
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> DeleteSysModulMaAsync(QueryData<GetBaseInfoByModuleQuery> query);
    }
}
