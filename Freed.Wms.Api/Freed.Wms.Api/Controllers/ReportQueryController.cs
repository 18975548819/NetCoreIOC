using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataEntities.QueryModel;
using Freed.Common.Data;
using Freed.Wms.Api.Controllers.Base;
using Freed.Wms.Api.Models;
using IBusinessManage;
using IBusinessManage.WMS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Freed.Wms.Api.Controllers
{
    /// <summary>
    /// 报表查询
    /// </summary>
    [Route("v1/api/[controller]")]
    [ApiController]
    public class ReportQueryController : BaseController
    {
        private IWmsBasicInfoManager _manager;
        private IWmsPdaConfigManager _configManager;

        public ReportQueryController(IWmsBasicInfoManager manager, IWmsPdaConfigManager configManager) : base(configManager)
        {
            _manager = manager;
            _configManager = configManager;
        }

        /// <summary>
        /// 获取出入库明细
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("get_WmsEntryAndExitGoods")]
        public async Task<IActionResult> GetWmsEntryAndExitGoodsList(ReportQueryViewModel model)
        {
            GetWmsInStorageGoodsQuery infoQuery = new GetWmsInStorageGoodsQuery();
            infoQuery.DeliveryNo = model.DeliveryNo;
            infoQuery.MaterieId = model.MaterieId;
            infoQuery.RepertoryId = model.RepertoryId;
            infoQuery.StorageType = model.StorageType;
            if (model.ScanTime.Count > 0)
            {
                infoQuery.StartScanTime = model.ScanTime[0];
                infoQuery.EndScanTime = model.ScanTime[1];
            }


            var query = new QueryData<GetWmsInStorageGoodsQuery>();
            query.Criteria = infoQuery;
            query.SqlConn = CurrentConnFactory;
            query.RepertoryId = CurrentUser.WmsRepertory;
            if (!string.IsNullOrEmpty(model.DeliveryNo) || !string.IsNullOrEmpty(model.MaterieId))
            {
                query.PageModel.PageIndex = 1;
                query.PageModel.PageSize = model.PageSize;
            }
            else
            {
                query.PageModel.PageIndex = model.PageIndex;
                query.PageModel.PageSize = model.PageSize;
            }

            var result = await _manager.GetWmsEntryAndExitGoodsMsAsync(query);

            return Ok(result);
        }


        /// <summary>
        /// 库存报表查询
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize, HttpPost, Route("get_WmsStock")]
        public async Task<IActionResult> GetIWmsStockList(ReportQueryViewModel model)
        {
            GetWmsInStorageGoodsQuery infoQuery = new GetWmsInStorageGoodsQuery();
            infoQuery.DeliveryNo = model.DeliveryNo;
            infoQuery.MaterieId = model.MaterieId;
            infoQuery.RepertoryId = model.RepertoryId;
            infoQuery.StorageType = model.StorageType;
            if (model.ScanTime.Count > 0)
            {
                infoQuery.StartScanTime = model.ScanTime[0];
                infoQuery.EndScanTime = model.ScanTime[1];
            }


            var query = new QueryData<GetWmsInStorageGoodsQuery>();
            query.Criteria = infoQuery;
            query.SqlConn = CurrentConnFactory;
            query.RepertoryId = CurrentUser.WmsRepertory;
            if (!string.IsNullOrEmpty(model.DeliveryNo) || !string.IsNullOrEmpty(model.MaterieId))
            {
                query.PageModel.PageIndex = 1;
                query.PageModel.PageSize = model.PageSize;
            }
            else
            {
                query.PageModel.PageIndex = model.PageIndex;
                query.PageModel.PageSize = model.PageSize;
            }

            var result = await _manager.GetIWmsStockMsAsync(query);

            return Ok(result);
        }
    }
}
