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
    /// 物料入库业务逻辑层接口定义
    /// </summary>
    public interface IWmsPdaInStorageGoodsManager: IDependencyManager
    {
        /// <summary>
        /// 当天入库物料信息返回（轮播表）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<DvScrollBoardModel>> GetInStorageGoodInfoByDvScrollBoardMaAsyn(QueryData<GetWmsInStorageGoodsQuery> query);
        
        /// <summary>
        /// 获取入库物料信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<IWmsInStorageGoods>> GetInStorageGoodListMaAsyn(QueryData<GetWmsInStorageGoodsQuery> query);

        /// <summary>
        /// 当天入库物料信息返回（动态环图）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<DvActiveRingChartModel>> GetInStorageGoodInfoByDvActiveRingChartMaAsyn(QueryData<GetWmsInStorageGoodsQuery> query);

        /// <summary>
        /// 获取近七天入库物料信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<EchartsLineDataModels>> GetInStorageGoodInfoByServerDayMaAsyn(QueryData<GetWmsInStorageGoodsQuery> query);
    }
}
