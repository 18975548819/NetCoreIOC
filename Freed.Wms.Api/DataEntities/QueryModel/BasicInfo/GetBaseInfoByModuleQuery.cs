using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.QueryModel.BasicInfo
{
    /// <summary>
    /// 获取基础信息-功能模块参数对象
    /// </summary>
    public class GetBaseInfoByModuleQuery
    {
        public int ID { get; set; }
        public string ModuleNO { get; set; }
        public string ParentModuleNO { get; set; }
        public string ModuleType { get; set; }
        public int LeafFlag { get; set; }
    }
}
