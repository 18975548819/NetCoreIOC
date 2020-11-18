using DataEntities.InterfaceEntities.WMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceModel.WMS
{
    public class WmsOutStorageGoodsModel: IWmsOutStorageGoods
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
        public string OutStorageType { get; set; }
        public string OutStorageNo { get; set; }
        public string MaterialCode { get; set; }
        public string RepertoryId { get; set; }
        public string AllocationRepertoryId { get; set; }
        /// <summary>
        /// 是否强制出库
        /// </summary>
        public int IsForceOut { get; set; }
    }
}
