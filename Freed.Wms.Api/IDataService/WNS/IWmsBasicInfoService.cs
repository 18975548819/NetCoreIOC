using DataEntities.InterfaceEntities.WMS;
using DataEntities.QueryModel;
using DataModel.WMS;
using Freed.Common.Data;
using IDataService.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IDataService.WNS
{
    /// <summary>
    /// WMS基础信息数据操作接口定义
    /// </summary>
    public interface IWmsBasicInfoService: IIDependencyService
    {
        /// <summary>
        /// 获取储位信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IWmsStorage>>> GetWmsWmsStorageAllListAsync(QueryData<GetBaseInfoQuery> query);

        /// <summary>
        /// 库存物料个数统计
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<SeriesData>>> GetSeriesDataAsync(QueryData<GetBaseInfoQuery> query);

        /// <summary>
        /// 库存查询统计
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<SeriesData>>> GetSeriesDataKCAsync(QueryData<GetBaseInfoQuery> query);

        /// <summary>
        /// 获取所有的仓位名称
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IWmsShelves>>> GetWmsShelvesToRepertoryIdListAsync(QueryData<GetWmsInStorageGoodsQuery> query);

        #region 报表查询
        /// <summary>
        /// 获取出入库明细
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IWmsEntryAndExitGoods>>> GetIWmsEntryAndExitGoodsListAsync(QueryData<GetWmsInStorageGoodsQuery> query);


        /// <summary>
        /// 库存报表查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IWmsStock>>> GetIWmsStockListAsync(QueryData<GetWmsInStorageGoodsQuery> query);

        /// <summary>
        ///物料库存详情
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IWmsStock>>> GetIWmsStockDetailListAsync(QueryData<GetWmsInStorageGoodsQuery> query);
        #endregion
    }
}
