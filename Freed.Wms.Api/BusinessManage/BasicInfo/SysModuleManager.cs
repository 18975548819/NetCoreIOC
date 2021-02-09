using DataEntities.InterfaceEntities.BasicInfo;
using DataEntities.InterfaceModel.BasicInfo;
using DataEntities.QueryModel.BasicInfo;
using Freed.Common.Data;
using IBusinessManage.BasicInfo;
using IDataService.BasicInfo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManage.BasicInfo
{
    /// <summary>
    /// 功能模块业务逻辑接口实现
    /// </summary>
    public class SysModuleManager : ISysModuleManager
    {
        private ISysModuleService _service;

        public SysModuleManager(ISysModuleService service)
        {
            _service = service;
        }

        /// <summary>
        /// 删除功能模块
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ErrData<bool>> DeleteSysModulMaAsync(QueryData<GetBaseInfoByModuleQuery> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;
            try
            {
                var res = await _service.DeleteSysModuleAsync(query);
                if (res.HasErr)
                {
                    result.SetInfo(res.ErrMsg, res.ErrCode);
                    result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                    return result;
                }
                else
                {
                    if (res.Data > 0)
                    {
                        result.SetInfo("删除功能模块成功！", 200);

                    }
                    else
                    {
                        result.SetInfo("删除功能模块失败！", -102);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        /// <summary>
        /// 修改功能模块
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ErrData<bool>> EidtSysModulMaAsync(QueryData<SysModule> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;
            try
            {
                InsertSysModuleQuery insertSysModule = new InsertSysModuleQuery();
                List<ISysModule> sysModules = new List<ISysModule>();
                query.Criteria.CreateName = query.UserName;
                query.Criteria.CreateTime = DateTime.Now;
                sysModules.Add(query.Criteria);
                insertSysModule.sysModule = sysModules;

                var queryR = new QueryData<InsertSysModuleQuery>();
                queryR.Criteria = insertSysModule;

                var res = await _service.EidtSysModuleAsync(queryR);
                if (res.HasErr)
                {
                    result.SetInfo(res.ErrMsg, res.ErrCode);
                    result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                    return result;
                }
                else
                {
                    if (res.Data > 0)
                    {
                        result.SetInfo("修改功能模块成功！", 200);

                    }
                    else
                    {
                        result.SetInfo("修改功能模块失败！", -102);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        /// <summary>
        /// 获取功能模块和子模块
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ListResult<SysModuleListModel>> GetSysModuleListMaAsync(QueryData<GetBaseInfoByModuleQuery> query)
        {
            var lr = new ListResult<SysModuleListModel>();
            var dt = DateTime.Now;

            var res = await GetSysModuleMainListMaAsync(query);
            if (res.HasErr)
            {
                lr.SetInfo(res.Msg,res.Code);
                lr.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                return lr;
            }
            else
            {
                var lst = new List<SysModuleListModel>();
                foreach (var item in res.Results)
                {
                    SysModuleListModel sysModuleList = new SysModuleListModel();
                    sysModuleList.ButtonImg = item.ButtonImg;
                    sysModuleList.CreateName = item.CreateName;
                    sysModuleList.CreateTime = item.CreateTime;
                    sysModuleList.FormName = item.FormName;
                    //if (query.Criteria.ModuleType == "Floder")
                    //{
                    //    sysModuleList.hasChildren = true;
                    //}
                    //else
                    //{
                    //    sysModuleList.hasChildren = false;
                    //}
                    sysModuleList.Icon = item.Icon;
                    sysModuleList.ID = item.ID;
                    sysModuleList.iframe = item.iframe;
                    sysModuleList.IsEnable = item.IsEnable;
                    sysModuleList.LeafFlag = item.LeafFlag;
                    sysModuleList.ModuleLevel = item.ModuleLevel;
                    sysModuleList.ModuleName = item.ModuleName;
                    sysModuleList.ModuleNO = item.ModuleNO;
                    sysModuleList.ModuleType = item.ModuleType;
                    sysModuleList.NodeImg = item.NodeImg;
                    sysModuleList.ParentModuleNO = item.ParentModuleNO;
                    sysModuleList.Remark = item.Remark;
                    sysModuleList.SelectNodeImg = item.SelectNodeImg;
                    sysModuleList.SortNumber = item.SortNumber;

                    var querChild = new QueryData<GetBaseInfoByModuleQuery>();
                    GetBaseInfoByModuleQuery getBaseInfo = new GetBaseInfoByModuleQuery();
                    getBaseInfo.ParentModuleNO = item.ModuleNO;
                    getBaseInfo.ModuleType = "Tab";
                    querChild.Criteria = getBaseInfo;
                    var res2 = await GetSysModuleMainListMaAsync(querChild);
                    if (res2.HasErr)
                    {
                        lr.SetInfo(res2.Msg, res2.Code);
                        lr.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                        return lr;
                    }
                    var childList = new List<ISysModule>();
                    foreach (var child in res2.Results)
                    {
                        //child.hasChildren = false;
                        childList.Add(child);
                    }
                    sysModuleList.children = childList;
                    lst.Add(sysModuleList);
                }

                lr.SetData(lst);
            }

            lr.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return lr;
        }

        /// <summary>
        /// 获取功能模块信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ListResult<ISysModule>> GetSysModuleMainListMaAsync(QueryData<GetBaseInfoByModuleQuery> query)
        {
            var lr = new ListResult<ISysModule>();
            var dt = DateTime.Now;

            var res = await _service.GetSysModuleMainListAsync(query);
            if (res.HasErr)
            {
                lr.SetInfo("查询异常", res.ErrCode);
            }
            else
            {
                var lst = new List<ISysModule>();
                foreach (var item in res.Data)
                {
                    ISysModule module = new SysModule();
                    module = item;
                    lst.Add(module);
                }
                lr.PageModel = res.PageInfo;
                lr.SetData(lst);
            }

            lr.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return lr;
        }

        /// <summary>
        /// 新增功能模块
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ErrData<bool>> InsertSysModulMaAsync(QueryData<SysModule> query)
        {
            var result = new ErrData<bool>();
            var dt = DateTime.Now;
            try
            {
                InsertSysModuleQuery insertSysModule = new InsertSysModuleQuery();
                List<ISysModule> sysModules = new List<ISysModule>();
                query.Criteria.CreateName = query.UserName;
                query.Criteria.CreateTime = DateTime.Now;
                sysModules.Add(query.Criteria);
                {
                    //SysModule module = new SysModule();
                    //module.ButtonImg = query.Criteria.ButtonImg;
                    //module.CreateName = query.UserName;
                    //module.CreateTime = DateTime.Now;
                    //module.FormName = query.Criteria.FormName;
                    //module.Icon = query.Criteria.Icon;
                    //module.iframe = query.Criteria.iframe;
                    //module.IsEnable = query.Criteria.IsEnable;
                    //module.LeafFlag = query.Criteria.LeafFlag;
                    //module.ModuleLevel = query.Criteria.ModuleLevel;
                    //module.ModuleName = query.Criteria.ModuleName;
                    //module.ModuleNO = query.Criteria.ModuleNO;
                    //module.ModuleType = query.Criteria.ModuleType;
                    //module.NodeImg = query.Criteria.NodeImg;
                    //module.ParentModuleNO = query.Criteria.ParentModuleNO;
                    //module.Remark = query.Criteria.Remark;
                    //module.SelectNodeImg = query.Criteria.SelectNodeImg;
                    //module.SortNumber = query.Criteria.SortNumber;
                    //sysModules.Add(module);
                }
                insertSysModule.sysModule = sysModules;

                var queryR = new QueryData<InsertSysModuleQuery>();
                queryR.Criteria = insertSysModule;

                var res = await _service.InsertSysModuleSaveAsync(queryR);
                if (res.HasErr)
                {
                    result.SetInfo(res.ErrMsg, res.ErrCode);
                    result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
                    return result;
                }
                else
                {
                    if (res.Data > 0)
                    {
                        result.SetInfo("新增功能模块成功！", 200);

                    }
                    else
                    {
                        result.SetInfo("新增功能模块失败！", -102);
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }
    }
}
