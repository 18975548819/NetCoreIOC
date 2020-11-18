using DataEntities.InterfaceEntities.WMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceModel.WMS
{
    public class WmsShelvesModel: IWmsShelves
    {
        public string GUID { get; set; }
        public int ShelvesId { get; set; }
        public string RepertoryId { get; set; }
        public string ShelvesName { get; set; }
    }
}
