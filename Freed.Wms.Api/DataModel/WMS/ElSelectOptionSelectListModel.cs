using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.WMS
{
    /// <summary>
    /// 出入库明细查询（仓位、出入库类型条件）
    /// </summary>
    public class ElSelectOptionSelectListModel
    {
        /// <summary>
        /// 仓位集合
        /// </summary>
        public List<ElSelectOptionModel> RepertoryIdList { get; set; }

        /// <summary>
        /// 出入库类型集合
        /// </summary>
        public List<ElSelectOptionModel> StorageTypeList { get; set; }
    }
}
