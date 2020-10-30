using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceEntities
{
    public interface IWmsPdaUser
    {
        int ID { get; set; }
        string UFnumber { get; set; }
        string UFname { get; set; }
        string UPassword { get; set; }
        string GroupType { get; set; }
        string WarehouseID { get; set; }
        string WmsRepertory { get; set; }
        string IsSapPosting { get; set; }
    }
}
