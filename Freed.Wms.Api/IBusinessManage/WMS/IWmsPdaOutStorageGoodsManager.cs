﻿using DataEntities.InterfaceEntities.WMS;
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
    /// 出库物料业务逻辑层接口定义
    /// </summary>
    public interface IWmsPdaOutStorageGoodsManager: IDependencyManager
    {
        Task<ErrData<DvScrollBoardModel>> GetOutStorageGoodInfoByDvScrollBoardMaAsyn(QueryData<GetWmsInStorageGoodsQuery> query);

        Task<ErrData<DvActiveRingChartModel>> GetOutStorageGoodInfoDvActiveRingChartMaAsyn(QueryData<GetWmsInStorageGoodsQuery> query);

        Task<ErrData<EchartsLineDataModels>> GetOutStorageGoodInfoByServerDayMaAsyn(QueryData<GetWmsInStorageGoodsQuery> query);

        /// <summary>
        /// 获取物料库存信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<IWmsOutStorageGoods>> GetOutStorageGoodListMaAsyn(QueryData<GetWmsInStorageGoodsQuery> query);
    }
}
