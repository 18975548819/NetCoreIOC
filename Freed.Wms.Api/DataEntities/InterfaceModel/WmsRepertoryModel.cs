using DataEntities.InterfaceEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceModel
{
    public class WmsRepertoryModel: IWmsRepertory
    {
        public string GUID { get; set; }
        public string RepertoryId { get; set; }
        public string RepertoryName { get; set; }
        public string Company { get; set; }
        public string Address { get; set; }
        public string Factory { get; set; }
        public string LastModified { get; set; }
        public string IsBond { get; set; }
    }
}
