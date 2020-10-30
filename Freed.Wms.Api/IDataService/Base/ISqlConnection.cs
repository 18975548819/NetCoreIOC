using System;
using System.Collections.Generic;
using System.Text;

namespace IDataService.Base
{
    public interface ISqlConnection : IIDependencyService
    {
        #region 数据库连接串

        string CommonConnStr { get; set; }
        string EasOrclConnStr { get; set; }

        #endregion

        bool InitService(ISqlConnection connModel);
    }
}
