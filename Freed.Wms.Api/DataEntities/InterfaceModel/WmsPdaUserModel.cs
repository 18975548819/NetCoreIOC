using DataEntities.InterfaceEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceModel
{
    public class WmsPdaUserModel : IWmsPdaUser
    {
        public int ID { get; set; }
        public string UFnumber { get; set; }
        public string UFname { get; set; }
        public string UPassword { get; set; }
        public string GroupType { get; set; }
        public string WarehouseID { get; set; }
        public string WmsRepertory { get; set; }
        public string IsSapPosting { get; set; }
    }
}
