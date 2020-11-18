using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceEntities.WMS
{
    public interface IWmsShelves
    {
        string GUID { get; set; }
        int ShelvesId { get; set; }
        string RepertoryId { get; set; }
        string ShelvesName { get; set; }
    }
}
