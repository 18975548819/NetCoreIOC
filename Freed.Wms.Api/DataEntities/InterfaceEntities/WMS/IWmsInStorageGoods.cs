using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceEntities.WMS
{
    /// <summary>
    /// 入库物料信息
    /// </summary>
    public interface IWmsInStorageGoods
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
        string InStorageType { get; set; }
        string InStorageNo { get; set; }
        string InStorageStaffNo { get; set; }
        string InStorageStaffName { get; set; }
        DateTime? InStorageTime { get; set; }
        string MaterialCode { get; set; }
        string RepertoryId { get; set; }
    }
}
