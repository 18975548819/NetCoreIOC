using DataEntities.QueryModel;
using DataModel.WMS;
using Freed.Common.Data;
using IBusinessManage.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IBusinessManage.WMS
{
    public interface IWmsPdaStorageMaterialManager:IDependencyManager
    {
        Task<ErrData<EchartsLineDataModels>> GetStorageMaterialInfoByServerDayMaAsyn(QueryData<GetWmsInStorageMaterialQuery> query);
    }
}
