using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Freed.Wms.Api.Models
{
    /// <summary>
    /// 报表查询前端参数
    /// </summary>
    public class ReportQueryViewModel
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
        [Required]
        public int PageIndex { get; set; }
        [Required]
        public int PageSize { get; set; }
        /// <summary>
        /// 扫描时间
        /// </summary>
        public List<string> ScanTime { get; set; }

    }
}
