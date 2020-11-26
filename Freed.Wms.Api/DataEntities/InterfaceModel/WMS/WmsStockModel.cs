using DataEntities.InterfaceEntities.WMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceModel.WMS
{
    /// <summary>
    /// 库存
    /// </summary>
    public class WmsStockModel: IWmsStock
    {
        public int Id { get; set; }
        public string MaterieId { get; set; }
        public string RepertoryId { get; set; }
        /// <summary>
        /// 唯一码
        /// </summary>
        public string Fnumber { get; set; }
        public string LotNumber { get; set; }
        public string StorageTime { get; set; }
        public string ValidTime { get; set; }
        public string InStorageNo { get; set; }
        public string StorageName { get; set; }
        public decimal Qty { get; set; }
        public bool? hasChildren { get; set; }
    }
}
