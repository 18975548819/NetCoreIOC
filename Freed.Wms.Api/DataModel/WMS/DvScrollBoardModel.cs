using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.WMS
{
    /// <summary>
    /// 轮播表数据
    /// </summary>
    public class DvScrollBoardModel
    {
        /// <summary>
        /// 表头
        /// </summary>
        public List<string> header { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public List<List<string>> data { get; set; }
        /// <summary>
        /// 显示行号
        /// </summary>
        public bool index { get; set; }
    }
}
