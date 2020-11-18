using DataEntities.InterfaceEntities.WMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceModel.WMS
{
    public class WmsOutStorageMaterialModel: IWmsOutStorageMaterial
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
        public DateTime? ScanTime { get; set; }
        public string ScanStaffNo { get; set; }
        public string ScanStaffName { get; set; }
        public DateTime CreateTime { get; set; }
        public string OutStorageType { get; set; }
        public string RepertoryId { get; set; }
        /// <summary>
        /// 批次号（主要应对复检时区分单据来自本地还是EAS或者SAP）复检只要提交一次这把当前提交当成一个批次
        /// </summary>
        public string BatchNo { get; set; }
    }
}
