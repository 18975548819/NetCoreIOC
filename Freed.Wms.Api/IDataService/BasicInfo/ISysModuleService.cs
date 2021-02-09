using DataEntities.InterfaceEntities.BasicInfo;
using DataEntities.QueryModel.BasicInfo;
using Freed.Common.Data;
using IDataService.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IDataService.BasicInfo
{
    /// <summary>
    /// 功能模块数据操作接口定义
    /// </summary>
    public interface ISysModuleService: IIDependencyService
    {
        /// <summary>
        /// 获取功能模块主菜单信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysModule>>> GetSysModuleMainListAsync(QueryData<GetBaseInfoByModuleQuery> query);

        /// <summary>
        /// 新增功能模块信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> InsertSysModuleSaveAsync(QueryData<InsertSysModuleQuery> query);

        /// <summary>
        /// 修改功能模块
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> EidtSysModuleAsync(QueryData<InsertSysModuleQuery> query);

        /// <summary>
        /// 删除功能模块
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> DeleteSysModuleAsync(QueryData<GetBaseInfoByModuleQuery> query);
    }
}
