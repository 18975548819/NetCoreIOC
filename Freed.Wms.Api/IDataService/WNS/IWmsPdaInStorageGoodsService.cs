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
    /// 入库物料信息获取数据接口层
    /// </summary>
    public interface IWmsPdaInStorageGoodsService: IIDependencyService
    {
        /// <summary>
        /// 获取入库物料信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IWmsInStorageGoods>>> GetWmsInStorageGoodsListAsync(QueryData<GetWmsInStorageGoodsQuery> query);

        /// <summary>
        ///获取所有
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IWmsInStorageGoods>>> GetWmsInStorageGoodsAllListAsync(QueryData<GetWmsInStorageGoodsQuery> query);


        /// <summary>
        /// 获取近七天仓库库存趋势
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IWmsInStorageGoods>>> GetServerDayWmsInStorageGoodsKCAsync(QueryData<GetWmsInStorageGoodsQuery> query);

        /// <summary>
        /// 获取入库类型集合信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IWmsInStorageGoods>>> GetInStorageTypeListAsync(QueryData<GetWmsInStorageGoodsQuery> query);
    }
}
