using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceEntities.WMS
{
    /// <summary>
    /// 出入库明细
    /// </summary>
    public interface IWmsEntryAndExitGoods
    {
        /// <summary>
        /// 编号
        /// </summary>
        int Id { get; set; }
        /// <summary>
        /// 单据号
        /// </summary>
        string DeliveryNo { get; set; }
        /// <summary>
        /// 料号
        /// </summary>
        string MaterieId { get; set; }
        /// <summary>
        /// 仓位
        /// </summary>
        string RepertoryId { get; set; }
        /// <summary>
        /// 储位编号
        /// </summary>
        string StorageNo { get; set; }
        /// <summary>
        /// 储位名称
        /// </summary>
        string StorageName { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        string LotNumber { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        decimal Qty { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        string StaffNo { get; set; }
        /// <summary>
        /// 扫描时间
        /// </summary>
        DateTime? ScanTime { get; set; }
        /// <summary>
        /// 入库类型
        /// </summary>
        string StorageType { get; set; }
        /// <summary>
        /// 物料二维码
        /// </summary>
        string MaterialCode { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        string StorageTime { get; set; }
        /// <summary>
        /// 有效日期
        /// </summary>
        string ValidTime { get; set; }
    }
}
