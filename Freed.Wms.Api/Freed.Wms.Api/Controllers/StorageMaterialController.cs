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
    /// 单据
    /// </summary>
    [Route("v1/api/[controller]")]
    [ApiController]
    public class StorageMaterialController : BaseController
    {
        private IWmsPdaStorageMaterialManager _manager;
        private IWmsPdaConfigManager _configManager;

        public StorageMaterialController(IWmsPdaStorageMaterialManager manager, IWmsPdaConfigManager configManager) : base(configManager)
        {
            _manager = manager;
            _configManager = configManager;
        }

        /// <summary>
        /// 获取出入库单据信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [Authorize,HttpPost, Route("get_StorageMaterialInfo")]
        public async Task<IActionResult> GetStorageMaterialInfo(GetDvScrollBoardViewModel model)
        {
            GetWmsInStorageMaterialQuery getWmsInStorageMaterial = new GetWmsInStorageMaterialQuery();
            getWmsInStorageMaterial.StartScanTime = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
            getWmsInStorageMaterial.EndScanTime = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            var query = new QueryData<GetWmsInStorageMaterialQuery>();
            query.Criteria = getWmsInStorageMaterial;
            query.SqlConn = CurrentConnFactory;
            query.RepertoryId = CurrentUser.WmsRepertory;
            query.PageModel.PageIndex = model.PageIndex;
            query.PageModel.PageSize = model.PageSize;

            var result = await _manager.GetStorageMaterialInfoByServerDayMaAsyn(query);

            return Ok(result);
        }
    }
}
