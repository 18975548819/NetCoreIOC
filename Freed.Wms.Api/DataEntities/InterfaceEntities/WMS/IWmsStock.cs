using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceEntities.WMS
{
    /// <summary>
    /// 库存
    /// </summary>
    public interface IWmsStock
    {
        string MaterieId { get; set; }
        string RepertoryId { get; set; }
        /// <summary>
        /// 唯一码
        /// </summary>
        string Fnumber { get; set; }
        string LotNumber { get; set; }
        string StorageTime { get; set; }
        string ValidTime { get; set; }
        string InStorageNo { get; set; }
        string StorageName { get; set; }
        decimal Qty { get; set; }
    }
}
