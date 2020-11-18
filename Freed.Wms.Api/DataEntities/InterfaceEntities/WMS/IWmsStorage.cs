using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceEntities.WMS
{
    /// <summary>
    /// 储位信息
    /// </summary>
    public interface IWmsStorage
    {

        string GUID { get; set; }

        string StorageId { get; set; }

        string ShelvesId { get; set; }

        string Storage_x { get; set; }

        string Storage_y { get; set; }

        string StorageName { get; set; }

        int IsBond { get; set; }

        /// <summary>
        /// 规格最大数
        /// </summary>
        int RangeMax { get; set; }
        /// <summary>
        /// 规格最小数
        /// </summary>
        int RangeMin { get; set; }
    }
}
