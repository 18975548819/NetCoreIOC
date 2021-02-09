using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freed.Wms.Api.Models.BasicInfoQuery
{
    public class GetSysModuleViewModel
    {
        public int ID { get; set; }
        public string ModuleNO { get; set; }
        public string ParentModuleNO { get; set; }
        public string ModuleType { get; set; }
        public int LeafFlag { get; set; }
    }
}
