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
    /// 出库物料
    /// </summary>
    [Route("v1/api/[controller]")]
    [ApiController]
    public class OutStorageGoodsController : BaseController
    {
        private IWmsPdaOutStorageGoodsManager _manager;
        private IWmsPdaConfigManager _configManager;

        public OutStorageGoodsController(IWmsPdaOutStorageGoodsManager manager, IWmsPdaConfigManager configManager) : base(configManager)
        {
            _manager = manager;
            _configManager = configManager;
        }

        /// <summary>
        /// 获取出库物料信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [Authorize, HttpPost, Route("get_OutStorageGoodsInfo_DvScrollBoard")]
        public async Task<IActionResult> GetOutStorageGoodsInfoDvScrollBoard(GetDvScrollBoardViewModel model)
        {
            GetWmsInStorageGoodsQuery getWmsInStorageGoods = new GetWmsInStorageGoodsQuery();
            getWmsInStorageGoods.StartScanTime = DateTime.Now.ToString("yyyy-MM-dd");
            getWmsInStorageGoods.EndScanTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            var query = new QueryData<GetWmsInStorageGoodsQuery>();
            query.Criteria = getWmsInStorageGoods;
            query.SqlConn = CurrentConnFactory;
            query.RepertoryId = CurrentUser.WmsRepertory;
            query.PageModel.PageIndex = model.PageIndex;
            query.PageModel.PageSize = model.PageSize;

            var result = await _manager.GetOutStorageGoodInfoByDvScrollBoardMaAsyn(query);

            return Ok(result);
        }

        /// <summary>
        /// 当天（动态环图）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [Authorize, HttpPost, Route("get_OutStorageGoodsInfo_DvActiveRingChart")]
        public async Task<IActionResult> GetOutStorageGoodsInfoDvActiveRingChart(GetDvScrollBoardViewModel model)
        {
            GetWmsInStorageGoodsQuery getWmsInStorageGoods = new GetWmsInStorageGoodsQuery();
            getWmsInStorageGoods.StartScanTime = DateTime.Now.ToString("yyyy-MM-dd");
            getWmsInStorageGoods.EndScanTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            var query = new QueryData<GetWmsInStorageGoodsQuery>();
            query.Criteria = getWmsInStorageGoods;
            query.SqlConn = CurrentConnFactory;
            query.RepertoryId = CurrentUser.WmsRepertory;
            query.PageModel.PageIndex = model.PageIndex;
            query.PageModel.PageSize = model.PageSize;

            var result = await _manager.GetOutStorageGoodInfoDvActiveRingChartMaAsyn(query);

            return Ok(result);
        }
    }
}
