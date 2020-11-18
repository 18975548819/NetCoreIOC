using DataEntities.InterfaceEntities.WMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.QueryModel
{
    /// <summary>
    /// 出入库明细
    /// </summary>
    public class WmsEntryAndExitGoodsModel: IWmsEntryAndExitGoods
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 单据号
        /// </summary>
        public string DeliveryNo { get; set; }
        /// <summary>
        /// 料号
        /// </summary>
        public string MaterieId { get; set; }
        /// <summary>
        /// 仓位
        /// </summary>
        public string RepertoryId { get; set; }
        /// <summary>
        /// 储位编号
        /// </summary>
        public string StorageNo { get; set; }
        /// <summary>
        /// 储位名称
        /// </summary>
        public string StorageName { get; set; }
        /// <summary>
        /// 批次
        /// </summary>
        public string LotNumber { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string StaffNo { get; set; }
        /// <summary>
        /// 扫描时间
        /// </summary>
        public DateTime? ScanTime { get; set; }
        /// <summary>
        /// 入库类型
        /// </summary>
        public string StorageType { get; set; }
        /// <summary>
        /// 物料二维码
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 生产日期
        /// </summary>
        public string StorageTime { get; set; }
        /// <summary>
        /// 有效日期
        /// </summary>
        public string ValidTime { get; set; }
    }
}
