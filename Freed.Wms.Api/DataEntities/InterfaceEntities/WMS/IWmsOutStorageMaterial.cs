using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceEntities.WMS
{
    public interface IWmsOutStorageMaterial
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
        DateTime? ScanTime { get; set; }
        string ScanStaffNo { get; set; }
        string ScanStaffName { get; set; }
        DateTime CreateTime { get; set; }
        string OutStorageType { get; set; }
        string RepertoryId { get; set; }
        /// <summary>
        /// 批次号（主要应对复检时区分单据来自本地还是EAS或者SAP）复检只要提交一次这把当前提交当成一个批次
        /// </summary>
        string BatchNo { get; set; }
    }
}
