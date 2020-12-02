using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Freed.Wms.Api.Models
{
    /// <summary>
    /// 基本信息查询前端参数接收
    /// </summary>
    public class BaseInfoViewModel
    {
        /// <summary>
        /// 物料号
        /// </summary>
        public string MaterieId { get; set; }
        /// <summary>
        /// 仓位
        /// </summary>
        public string RepertoryId { get; set; }
        [Required]
        public int PageIndex { get; set; }
        [Required]
        public int PageSize { get; set; }
    }
}
