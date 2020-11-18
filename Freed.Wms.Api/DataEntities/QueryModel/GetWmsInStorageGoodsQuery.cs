using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.QueryModel
{
    public class GetWmsInStorageGoodsQuery
    {
        /// <summary>
        ///单据号
        /// </summary>
        public string DeliveryNo { get; set; }
        /// <summary>
        /// 物料号
        /// </summary>
        public string MaterieId { get; set; }
        /// <summary>
        /// 开始扫描时间
        /// </summary>
        public string StartScanTime { get; set; }
        /// <summary>
        /// 结束扫描时间
        /// </summary>
        public string EndScanTime { get; set; }
        /// <summary>
        /// 仓位
        /// </summary>
        public string RepertoryId { get; set; }
        /// <summary>
        /// 出入库类型
        /// </summary>
        public string StorageType { get; set; }
    }
}
