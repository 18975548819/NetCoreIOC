using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceEntities
{
    /// <summary>
    /// 配置信息
    /// </summary>
    public interface IWmsPdaConfig
    {
        int Id { get; set; }
        string GroupType { get; set; }
        string GroupName { get; set; }
        string Source { get; set; }
        string DataBase { get; set; }
        string DataPort { get; set; }
        string Uid { get; set; }
        string Pwd { get; set; }
        string UnitFid { get; set; }
        string FeasId { get; set; }
        bool IsShow { get; set; }
        int Order { get; set; }
    }
}
