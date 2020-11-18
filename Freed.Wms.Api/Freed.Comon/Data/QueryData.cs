using System;
using System.Collections.Generic;
using System.Text;

namespace Freed.Common.Data
{
    public class QueryData<C>
    {
        public QueryData()
        {
            PageModel = new PageModel();
        }

        /// <summary>
        /// 查询条件结构
        /// </summary>
        public C Criteria { get; set; }

        /// <summary>
        /// 查询分页信息
        /// </summary>
        public PageModel PageModel { get; set; }

        /// <summary>
        /// 当前登录用户
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string SqlConn { get; set; }
        /// <summary>
        /// 登录仓位
        /// </summary>
        public string RepertoryId { get; set; }

    }
}
