using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freed.Wms.Api.Models.BasicInfoQuery
{
    public class SysModuleModel
    {
        public string ModuleName { get; set; }
        public string ModuleType { get; set; }
        public string ParentModuleNO { get; set; }
        public string ModuleLevel { get; set; }
        public string LeafFlag { get; set; }
        public string Icon { get; set; }
        public string FormName { get; set; }
        public string SortNumber { get; set; }
        public string IsEnable { get; set; }
        public string Remark { get; set; }
        public string iframe { get; set; }
    }
}
