using Freed.Common.Helpers;
using IDataService.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Base
{
    public class SqlConnectionModel: ISqlConnection
    {
        
        public string CommonConnStr { get; set; }
        public string EasOrclConnStr { get; set; }

        public bool InitService(ISqlConnection connModel)
        {
            MssqlHelper.ConnCommon = connModel.CommonConnStr;
            return true;
        }
    }
}
