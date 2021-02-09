using DataEntities.InterfaceModel.BasicInfo;
using DataEntities.QueryModel.BasicInfo;
using Freed.Common.Data;
using Freed.Wms.Api.Controllers.Base;
using Freed.Wms.Api.Models.BasicInfoQuery;
using IBusinessManage;
using IBusinessManage.BasicInfo;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freed.Wms.Api.Controllers.BasicInfo
{
    /// <summary>
    /// 功能模块
    /// </summary>
    [Route("v1/api/[controller]")]
    [ApiController]
    public class SysModuleController : BaseController
    {
        private ISysModuleManager _manager;
        private IWmsPdaConfigManager _configManager;

        public SysModuleController(ISysModuleManager manager, IWmsPdaConfigManager configManager) : base(configManager)
        {
            _manager = manager;
            _configManager = configManager;
        }

        /// <summary>
        /// 获取功能模块（主功能模块）
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("get_SysModuleMainList")]
        public async Task<IActionResult> GetSysModuleMainList(GetSysModuleViewModel model)
        {
            GetBaseInfoByModuleQuery getBaseInfoByModule = new GetBaseInfoByModuleQuery();
            getBaseInfoByModule.ModuleType = model.ModuleType;
            getBaseInfoByModule.ModuleNO = model.ModuleNO;
            var query = new QueryData<GetBaseInfoByModuleQuery>();
            query.Criteria = getBaseInfoByModule;

            var result = await _manager.GetSysModuleMainListMaAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 获取功能模块和子模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("get_SysModuleList")]
        public async Task<IActionResult> GetSysModuleList(GetSysModuleViewModel model)
        {
            GetBaseInfoByModuleQuery getBaseInfoByModule = new GetBaseInfoByModuleQuery();
            getBaseInfoByModule.ModuleType = model.ModuleType;
            getBaseInfoByModule.ModuleNO = model.ModuleNO;
            var query = new QueryData<GetBaseInfoByModuleQuery>();
            query.Criteria = getBaseInfoByModule;

            var result = await _manager.GetSysModuleListMaAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 获取子功能模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("get_SysModuleChildList")]
        public async Task<IActionResult> GetSysModuleChildList(GetSysModuleViewModel model)
        {
            GetBaseInfoByModuleQuery getBaseInfoByModule = new GetBaseInfoByModuleQuery();
            getBaseInfoByModule.ModuleType = model.ModuleType;
            getBaseInfoByModule.ParentModuleNO = model.ParentModuleNO;
            var query = new QueryData<GetBaseInfoByModuleQuery>();
            query.Criteria = getBaseInfoByModule;

            var result = await _manager.GetSysModuleMainListMaAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 新增功能模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("add_SysModule")]
        public async Task<IActionResult> InsertSysModule(SysModuleViewModel model)
        {
            SysModule module = new SysModule();
            module.FormName = model.sysModule.FormName;
            module.Icon = model.sysModule.Icon;
            if (model.sysModule.IsEnable == "True")
            {
                module.IsEnable = 1;
            }
            else
            {
                module.IsEnable = 0;
            }
            if (model.sysModule.LeafFlag == "True")
            {
                module.LeafFlag = 0;
            }
            else
            {
                module.LeafFlag = 1;
            }
            module.ModuleLevel = Convert.ToInt32(model.sysModule.ModuleLevel);
            module.ModuleName = model.sysModule.ModuleName;
            module.ModuleType = model.sysModule.ModuleType;
            if (model.sysModule.ModuleType == "Floder")
            {
                module.ModuleNO = model.sysModule.SortNumber.PadLeft(3, '0');
                module.ParentModuleNO = "";
            }
            else
            {
                module.ModuleNO = model.sysModule.ParentModuleNO.PadLeft(3, '0') + "_" + model.sysModule.SortNumber.PadLeft(3, '0');
                module.ParentModuleNO = model.sysModule.ParentModuleNO.PadLeft(3,'0');
            }
            module.Remark = model.sysModule.Remark;
            module.SortNumber = Convert.ToInt32(model.sysModule.SortNumber);

            var query = new QueryData<SysModule>();
            query.Criteria = module;
            query.UserName = CurrentUser.UserNo;
            var result = await _manager.InsertSysModulMaAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 修改功能模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("eidt_SysModule")]
        public async Task<IActionResult> eidtSysModule(SysModuleViewModel model)
        {
            SysModule module = new SysModule();
            module.FormName = model.sysModule.FormName;
            module.Icon = model.sysModule.Icon;
            if (model.sysModule.IsEnable == "True")
            {
                module.IsEnable = 1;
            }
            else
            {
                module.IsEnable = 0;
            }
            if (model.sysModule.LeafFlag == "True")
            {
                module.LeafFlag = 0;
            }
            else
            {
                module.LeafFlag = 1;
            }
            module.ModuleLevel = Convert.ToInt32(model.sysModule.ModuleLevel);
            module.ModuleName = model.sysModule.ModuleName;
            module.ModuleType = model.sysModule.ModuleType;
            if (model.sysModule.ModuleType == "Floder")
            {
                module.ModuleNO = model.sysModule.SortNumber.PadLeft(3, '0');
                module.ParentModuleNO = "";
            }
            else
            {
                module.ModuleNO = model.sysModule.ParentModuleNO.PadLeft(3, '0') + "_" + model.sysModule.SortNumber.PadLeft(3, '0');
                module.ParentModuleNO = model.sysModule.ParentModuleNO.PadLeft(3, '0');
            }
            module.Remark = model.sysModule.Remark;
            module.SortNumber = Convert.ToInt32(model.sysModule.SortNumber);

            var query = new QueryData<SysModule>();
            query.Criteria = module;
            query.UserName = CurrentUser.UserNo;
            var result = await _manager.EidtSysModulMaAsync(query);

            return Ok(result);
        }


        /// <summary>
        /// 删除功能模块
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("delete_SysModule")]
        public async Task<IActionResult> DeleteSysModule(GetSysModuleViewModel model)
        {
            GetBaseInfoByModuleQuery getBaseInfoByModule = new GetBaseInfoByModuleQuery();
            getBaseInfoByModule.ModuleNO = model.ModuleNO;
            getBaseInfoByModule.ParentModuleNO = model.ParentModuleNO;
            getBaseInfoByModule.ID = model.ID;
            var query = new QueryData<GetBaseInfoByModuleQuery>();
            query.Criteria = getBaseInfoByModule;
            var result = await _manager.DeleteSysModulMaAsync(query);

            return Ok(result);
        }
    }
}
