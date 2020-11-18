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
    /// 入库物料
    /// </summary>
    [Route("v1/api/[controller]")]
    [ApiController]
    public class InStorageGoodsController : BaseController
    {
        private IWmsPdaInStorageGoodsManager _manager;
        private IWmsPdaOutStorageGoodsManager _outManager;
        private IWmsPdaConfigManager _configManager;

        public InStorageGoodsController(IWmsPdaInStorageGoodsManager manager, IWmsPdaConfigManager configManager, IWmsPdaOutStorageGoodsManager outManager) : base(configManager)
        {
            _manager = manager;
            _outManager = outManager;
            _configManager = configManager;
        }


        /// <summary>
        /// 获取入库物料信息（轮播表）
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize,HttpPost, Route("get_InStorageGoodsInfo_DvScrollBoard")]
        public async Task<IActionResult> GetInStorageGoodsInfoDvScrollBoard(GetDvScrollBoardViewModel model)
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

            var result = await _manager.GetInStorageGoodInfoByDvScrollBoardMaAsyn(query);

            return Ok(result);
        }



        [Authorize, HttpPost, Route("get_InStorageGoodsInfo_DvActiveRingChart")]
        public async Task<IActionResult> GetInStorageGoodsInfoDvActiveRingChart(GetDvScrollBoardViewModel model)
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

            var result = await _manager.GetInStorageGoodInfoByDvActiveRingChartMaAsyn(query);

            return Ok(result);
        }






        [Authorize, HttpPost, Route("get_InStorageGoodsInfo_ServerData")]
        public async Task<IActionResult> GetInStorageGoodsInfoServerData(GetDvScrollBoardViewModel model)
        {
            GetWmsInStorageGoodsQuery getWmsInStorageGoods = new GetWmsInStorageGoodsQuery();
            getWmsInStorageGoods.StartScanTime = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
            getWmsInStorageGoods.EndScanTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            var query = new QueryData<GetWmsInStorageGoodsQuery>();
            query.Criteria = getWmsInStorageGoods;
            query.SqlConn = CurrentConnFactory;
            query.RepertoryId = CurrentUser.WmsRepertory;
            query.PageModel.PageIndex = model.PageIndex;
            query.PageModel.PageSize = model.PageSize;

            var result = await _manager.GetInStorageGoodInfoByServerDayMaAsyn(query);
            return Ok(result);
        }
    }
}
