using DataEntities.InterfaceEntities.WMS;
using DataEntities.QueryModel;
using DataModel.WMS;
using Freed.Common.Data;
using IBusinessManage.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessManage.WMS
{
    /// <summary>
    /// WMS基础数据业务逻辑接口定义
    /// </summary>
    public interface IWmsBasicInfoManager: IDependencyManager
    {
        /// <summary>
        /// 获取储位信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<IWmsStorage>> GetWmsWmsStorageAllListMsAsync(QueryData<GetBaseInfoQuery> query);

        //各仓位物料数量
        Task<ErrData<EchartsPieDataModel>> GetEchartsPieDataMsAsync(QueryData<GetBaseInfoQuery> query);

        /// <summary>
        /// 物料库存数量统计
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<EchartsPieDataModel>> GetEchartsPieDataKCMsAsync(QueryData<GetBaseInfoQuery> query);

        /// <summary>
        ///获取仓库近七天库存趋势
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<EchartsLineDataModels>> GetServerDayInAndOutWarehouseTrendsMsAsync(QueryData<GetWmsInStorageGoodsQuery> query);

        /// <summary>
        /// 获取所有仓位和出入库类型
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<ElSelectOptionSelectListModel>> GetRepertoryIdAndStorageTypeListMsAsync(QueryData<GetWmsInStorageGoodsQuery> query);

        #region 报表查询
        /// <summary>
        /// 获取出入库明细
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<IWmsEntryAndExitGoods>> GetWmsEntryAndExitGoodsMsAsync(QueryData<GetWmsInStorageGoodsQuery> query);

        /// <summary>
        /// 库存报表查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<IWmsStock>> GetIWmsStockMsAsync(QueryData<GetWmsInStorageGoodsQuery> query);
        #endregion
    }
}
