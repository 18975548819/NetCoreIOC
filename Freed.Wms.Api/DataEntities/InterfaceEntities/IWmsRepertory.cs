using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceEntities
{
    public interface IWmsRepertory
    {
        string GUID { get; set; }
        string RepertoryId { get; set; }
        string RepertoryName { get; set; }
        string Company { get; set; }
        string Address { get; set; }
        string Factory { get; set; }
        string LastModified { get; set; }
        string IsBond { get; set; }
    }
}
