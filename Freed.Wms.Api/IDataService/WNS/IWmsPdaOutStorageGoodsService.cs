using DataEntities.InterfaceEntities.WMS;
using DataEntities.QueryModel;
using Freed.Common.Data;
using IDataService.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IDataService.WNS
{
    /// <summary>
    /// 出库物料信息数据层接口定义
    /// </summary>
    public interface IWmsPdaOutStorageGoodsService:IIDependencyService
    {
        /// <summary>
        /// 获取出库物料信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IWmsOutStorageGoods>>> GetWmsOutStorageGoodsListAsync(QueryData<GetWmsInStorageGoodsQuery> query);

        /// <summary>
        /// 获取所有出库物料信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IWmsOutStorageGoods>>> GetWmsOutStorageGoodsAllListAsync(QueryData<GetWmsInStorageGoodsQuery> query);

        /// <summary>
        /// 获取各仓库物料出库信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IWmsOutStorageGoods>>> GetServerDayWmsOutStorageGoodsListAsync(QueryData<GetWmsInStorageGoodsQuery> query);

        /// <summary>
        /// 获取出库类型集合信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IWmsOutStorageGoods>>> GetOutStorageTypeListAsync(QueryData<GetWmsInStorageGoodsQuery> query);
    }
}
