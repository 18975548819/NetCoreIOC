﻿using DataEntities.InterfaceEntities.WMS;
using DataEntities.InterfaceModel.WMS;
using DataEntities.QueryModel;
using Freed.Common.Data;
using Freed.Common.Helpers;
using IDataService.Base;
using IDataService.WNS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.WMS
{
    public class WmsInStorageMaterialService : IWmsInStorageMaterialService
    {
        public async Task<DataResult<List<IWmsInStorageMaterial>>> GetWmsInStorageMaterialAllListAsync(QueryData<GetWmsInStorageMaterialQuery> query)
        {
            var result = new DataResult<List<IWmsInStorageMaterial>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.StartScanTime) ? string.Empty : string.Format(" and ScanTime >= '{0}' and ScanTime <= '{1}'", query.Criteria.StartScanTime, query.Criteria.EndScanTime);
            condition += string.IsNullOrEmpty(query.Criteria.DeliveryNo) ? string.Empty : string.Format(" and DeliveryNo = '{0}'", query.Criteria.DeliveryNo);
            condition += string.IsNullOrEmpty(query.Criteria.MaterieId) ? string.Empty : string.Format(" and MaterialId = '{0}'", query.Criteria.MaterieId);
            condition += string.IsNullOrEmpty(query.RepertoryId) ? string.Empty : string.Format(" and RepertoryId = '{0}'", query.RepertoryId);
            string sql = string.Format(@"SELECT [Id]
                                      ,[DeliveryNo]
                                      ,[MaterialId]
                                      ,[Qty]
                                      ,[UnitFid]
                                      ,[TransactionName]
                                      ,[CostCenterName]
                                      ,[WarehouseName]
                                      ,[ScanQty]
                                      ,[ScanPack]
                                      ,[ScanTime]
                                      ,[ScanStaffNo]
                                      ,[ScanStaffName]
                                      ,[CreateTime]
                                      ,[InStorageType]
                                      ,[RepertoryId]
                                  FROM [dbo].[Wms_Pda_InStorage_Material]  {0}", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(query.SqlConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<WmsInStorageMaterialModel>(dbConn, sql);
                    result.Data = modelList.ToList<IWmsInStorageMaterial>();
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = null;
                }
            }
            return result;
        }
    }
}
