using DataEntities.InterfaceEntities.WMS;
using DataEntities.QueryModel;
using Freed.Common.Data;
using IDataService.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IDataService.WNS
{
    public interface IWmsInStorageMaterialService:IIDependencyService
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<IWmsInStorageMaterial>>> GetWmsInStorageMaterialAllListAsync(QueryData<GetWmsInStorageMaterialQuery> query);
    }
}
