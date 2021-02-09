using DataEntities.InterfaceEntities.BasicInfo;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceModel.BasicInfo
{
    public class SysModuleListModel
    {
        public int ID { get; set; }
        public string ModuleNO { get; set; }
        public string ModuleName { get; set; }
        public string ModuleType { get; set; }
        public string ParentModuleNO { get; set; }
        public int ModuleLevel { get; set; }
        public int LeafFlag { get; set; }
        public string Icon { get; set; }
        public string ButtonImg { get; set; }
        public string NodeImg { get; set; }
        public string SelectNodeImg { get; set; }
        public string FormName { get; set; }
        public int SortNumber { get; set; }
        public int IsEnable { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateTime { get; set; }
        public string Remark { get; set; }
        public int iframe { get; set; }
        //public bool hasChildren { get; set; }

        public List<ISysModule> children { get; set; }
    }
}
