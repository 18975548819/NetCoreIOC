using DataEntities.InterfaceEntities.WMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceModel.WMS
{
    /// <summary>
    /// 入库物料信息
    /// </summary>
    public class WmsInStorageGoods:IWmsInStorageGoods
    {
        public int Id { get; set; }
        public string MaterieId { get; set; }
        public string Spec { get; set; }
        public string LotNumber { get; set; }
        public string StorageTime { get; set; }
        public string ValidTime { get; set; }
        public string Fnumber { get; set; }
        public decimal Qty { get; set; }
        public string DeliveryNo { get; set; }
        public DateTime ScanTime { get; set; }
        public string ScanStaffNo { get; set; }
        public string ScanStaffName { get; set; }
        public string InStorageType { get; set; }
        public string InStorageNo { get; set; }
        public string InStorageStaffNo { get; set; }
        public string InStorageStaffName { get; set; }
        public DateTime? InStorageTime { get; set; }
        public string MaterialCode { get; set; }
        public string RepertoryId { get; set; }
    }
}
