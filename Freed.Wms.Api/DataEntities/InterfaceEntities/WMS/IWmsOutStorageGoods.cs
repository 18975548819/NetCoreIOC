using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceEntities.WMS
{
    /// <summary>
    /// 出库物料信息
    /// </summary>
    public interface IWmsOutStorageGoods
    {
        int Id { get; set; }
        string MaterieId { get; set; }
        string Spec { get; set; }
        string LotNumber { get; set; }
        string StorageTime { get; set; }
        string ValidTime { get; set; }
        string Fnumber { get; set; }
        decimal Qty { get; set; }
        string DeliveryNo { get; set; }
        DateTime ScanTime { get; set; }
        string ScanStaffNo { get; set; }
        string ScanStaffName { get; set; }
        string OutStorageType { get; set; }
        string OutStorageNo { get; set; }
        string MaterialCode { get; set; }
        string RepertoryId { get; set; }
        string AllocationRepertoryId { get; set; }
        /// <summary>
        /// 是否强制出库
        /// </summary>
        int IsForceOut { get; set; }
    }
}
