using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.QueryModel
{
    public class GetWmsInStorageMaterialQuery
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
    }
}
