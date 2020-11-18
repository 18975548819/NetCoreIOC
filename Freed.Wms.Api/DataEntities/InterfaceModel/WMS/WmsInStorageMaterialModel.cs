using DataEntities.InterfaceEntities.WMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceModel.WMS
{
    public class WmsInStorageMaterialModel: IWmsInStorageMaterial
    {
        public int Id { get; set; }
        public string DeliveryNo { get; set; }
        public string MaterialId { get; set; }
        public decimal Qty { get; set; }
        public string UnitFid { get; set; }
        public string TransactionName { get; set; }
        public string CostCenterName { get; set; }
        public string WarehouseName { get; set; }
        public decimal ScanQty { get; set; }
        public int ScanPack { get; set; }
        public DateTime ScanTime { get; set; }
        public string ScanStaffNo { get; set; }
        public string ScanStaffName { get; set; }
        public DateTime CreateTime { get; set; }
        public string InStorageType { get; set; }
        public string RepertoryId { get; set; }
    }
}
