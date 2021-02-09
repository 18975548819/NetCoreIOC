using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceEntities.BasicInfo
{
    /// <summary>
    /// 功能模块
    /// </summary>
    public interface ISysModule
    {
        int ID { get; set; }
        string ModuleNO { get; set; }
        string ModuleName { get; set; }
        string ModuleType { get; set; }
        string ParentModuleNO { get; set; }
        int ModuleLevel { get; set; }
        int LeafFlag { get; set; }
        string Icon { get; set; }
        string ButtonImg { get; set; }
        string NodeImg { get; set; }
        string SelectNodeImg { get; set; }
        string FormName { get; set; }
        int SortNumber { get; set; }
        int IsEnable { get; set; }
        string CreateName { get; set; }
        DateTime CreateTime { get; set; }
        string Remark { get; set; }
        int iframe { get; set; }
        //bool hasChildren { get; set; }
    }
}
