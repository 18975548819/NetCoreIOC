using DataEntities.InterfaceEntities.WMS;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataEntities.InterfaceModel.WMS
{
    /// <summary>
    /// 储位信息
    /// </summary>
    public class WmsStorage: IWmsStorage
    {

        public string GUID { get; set; }

        public string StorageId { get; set; }

        public string ShelvesId { get; set; }

        public string Storage_x { get; set; }

        public string Storage_y { get; set; }

        public string StorageName { get; set; }

        public int IsBond { get; set; }
        /// <summary>
        /// 规格最大数
        /// </summary>
        public int RangeMax { get; set; }
        /// <summary>
        /// 规格最小数
        /// </summary>
        public int RangeMin { get; set; }
    }
}
