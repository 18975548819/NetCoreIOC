using DataEntities.InterfaceEntities.BasicInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.QueryModel.BasicInfo
{
    /// <summary>
    /// 新增功能模块信息
    /// </summary>
    public class InsertSysModuleQuery
    {
        public List<ISysModule> sysModule { get; set; }
    }
}
