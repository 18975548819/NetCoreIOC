using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Freed.Wms.Api.Models
{
    public class LoginViewModel
    {
        /// <summary>
        /// 登录名
        /// </summary>
        [Required]
        public string LoginName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }
        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin { get; set; }
        /// <summary>
        /// 登录厂区
        /// </summary>
        [Required]
        public string GroupType { get; set; }
        /// <summary>
        /// 登录仓库
        /// </summary>
        [Required]
        public string WmsRepertory { get; set; }
        /// <summary>
        /// 是否SAP过账
        /// </summary>
        public string IsSapPosting { get; set; }
    }
}
