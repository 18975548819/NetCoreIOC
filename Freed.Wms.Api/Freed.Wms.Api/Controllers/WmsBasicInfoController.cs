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
    /// 基础信息
    /// </summary>
    [Route("v1/api/[controller]")]
    [ApiController]
    public class WmsBasicInfoController : BaseController
    {
        private IWmsBasicInfoManager _manager;
        private IWmsPdaConfigManager _configManager;

        public WmsBasicInfoController(IWmsBasicInfoManager manager, IWmsPdaConfigManager configManager) : base(configManager)
        {
            _manager = manager;
            _configManager = configManager;
        }

        /// <summary>
        /// 获取储位信息
        /// </summary>
        /// <returns></returns>
        [Authorize, HttpPost, Route("get_wms_storage")]
        public async Task<IActionResult> GetWmsStorageInfo()
        {
            GetBaseInfoQuery infoQuery = new GetBaseInfoQuery();

            var query = new QueryData<GetBaseInfoQuery>();
            query.Criteria = infoQuery;
            query.SqlConn = CurrentConnFactory;
            query.RepertoryId = CurrentUser.WmsRepertory;

            var result = await _manager.GetWmsWmsStorageAllListMsAsync(query);

            return Ok(result);
        }


        /// <summary>
        /// 仓库物料数量获取
        /// </summary>
        /// <returns></returns>
        [Authorize, HttpPost, Route("get_EchartsPieData")]
        public async Task<IActionResult> GetEchartsPieData(BaseInfoViewModel model)
        {
            GetBaseInfoQuery infoQuery = new GetBaseInfoQuery();
            infoQuery.MaterieId = model.MaterieId;
            infoQuery.RepertoryId = model.RepertoryId;

            var query = new QueryData<GetBaseInfoQuery>();
            query.Criteria = infoQuery;
            query.SqlConn = CurrentConnFactory;
            query.RepertoryId = CurrentUser.WmsRepertory;
            query.PageModel.PageIndex = model.PageIndex;
            query.PageModel.PageSize = model.PageSize;

            var result = await _manager.GetEchartsPieDataMsAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 库存统计
        /// </summary>
        /// <returns></returns>
        [Authorize, HttpPost, Route("get_EchartsPieDataKC")]
        public async Task<IActionResult> GetEchartsPieDataKC(BaseInfoViewModel model)
        {
            GetBaseInfoQuery infoQuery = new GetBaseInfoQuery();
            infoQuery.MaterieId = model.MaterieId;
            infoQuery.RepertoryId = model.RepertoryId;

            var query = new QueryData<GetBaseInfoQuery>();
            query.Criteria = infoQuery;
            query.SqlConn = CurrentConnFactory;
            query.RepertoryId = CurrentUser.WmsRepertory;
            query.PageModel.PageIndex = model.PageIndex;
            query.PageModel.PageSize = model.PageSize;
            var result = await _manager.GetEchartsPieDataKCMsAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 获取仓位集合和出入库类型
        /// </summary>
        /// <returns></returns>
        [Authorize, HttpPost, Route("get_RepertoryIdAndStorageTypeList")]
        public async Task<IActionResult> GetRepertoryIdAndStorageTypeList()
        {
            GetWmsInStorageGoodsQuery infoQuery = new GetWmsInStorageGoodsQuery();

            var query = new QueryData<GetWmsInStorageGoodsQuery>();
            query.Criteria = infoQuery;
            query.SqlConn = CurrentConnFactory;
            query.RepertoryId = CurrentUser.WmsRepertory;

            var result = await _manager.GetRepertoryIdAndStorageTypeListMsAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 获取近七天仓库库存趋势
        /// </summary>
        /// <returns></returns>
        [Authorize, HttpPost, Route("get_ServerDayInAndOutWarehouseTrends")]
        public async Task<IActionResult> GetServerDayInAndOutWarehouseTrends()
        {
            GetWmsInStorageGoodsQuery infoQuery = new GetWmsInStorageGoodsQuery();

            var query = new QueryData<GetWmsInStorageGoodsQuery>();
            query.Criteria = infoQuery;
            query.SqlConn = CurrentConnFactory;
            query.RepertoryId = CurrentUser.WmsRepertory;

            var result = await _manager.GetServerDayInAndOutWarehouseTrendsMsAsync(query);

            return Ok(result);
        }
    }
}
