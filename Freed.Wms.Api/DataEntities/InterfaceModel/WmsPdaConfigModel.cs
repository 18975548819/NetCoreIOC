using DataEntities.InterfaceEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceModel
{
    /// <summary>
    /// 配置信息实体
    /// </summary>
    public class WmsPdaConfigModel : IWmsPdaConfig
    {
        public int Id { get; set; }
        public string GroupType { get; set; }
        public string GroupName { get; set; }
        public string Source { get; set; }
        public string DataBase { get; set; }
        public string DataPort { get; set; }
        public string Uid { get; set; }
        public string Pwd { get; set; }
        public string UnitFid { get; set; }
        public string FeasId { get; set; }
        public bool IsShow { get; set; }
        public int Order { get; set; }
    }
}
