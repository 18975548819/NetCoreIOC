using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceEntities.WMS
{
    public interface IWmsInStorageMaterial
    {
        int Id { get; set; }
        string DeliveryNo { get; set; }
        string MaterialId { get; set; }
        decimal Qty { get; set; }
        string UnitFid { get; set; }
        string TransactionName { get; set; }
        string CostCenterName { get; set; }
        string WarehouseName { get; set; }
        decimal ScanQty { get; set; }
        int ScanPack { get; set; }
        DateTime ScanTime { get; set; }
        string ScanStaffNo { get; set; }
        string ScanStaffName { get; set; }
        DateTime CreateTime { get; set; }
        string InStorageType { get; set; }
        string RepertoryId { get; set; }
    }
}
