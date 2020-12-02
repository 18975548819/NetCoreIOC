using DataEntities.InterfaceEntities.WMS;
using DataEntities.InterfaceModel.WMS;
using DataEntities.QueryModel;
using DataModel.WMS;
using Freed.Common.Data;
using Freed.Common.Helpers;
using IDataService.WNS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.WMS
{
    /// <summary>
    /// WMS基础数据操作接口实现
    /// </summary>
    public class WmsBasicInfoService : IWmsBasicInfoService
    {
        /// <summary>
        /// 仓库物料数量统计
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<SeriesData>>> GetSeriesDataAsync(QueryData<GetBaseInfoQuery> query)
        {
            var result = new DataResult<List<SeriesData>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.RepertoryId) ? string.Empty : string.Format(" and InRepertoryId = '{0}' ", query.Criteria.RepertoryId);

            string sql = string.Format(@"select  InRepertoryId as name,COUNT(*) as value from V_Wms_Pda_StorageList {0} group by  InRepertoryId", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(query.SqlConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<SeriesData>(dbConn, sql);
                    result.Data = modelList.ToList<SeriesData>();
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = null;
                }
            }
            return result;
        }

        /// <summary>
        /// 物料库存统计
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<SeriesData>>> GetSeriesDataKCAsync(QueryData<GetBaseInfoQuery> query)
        {
            var result = new DataResult<List<SeriesData>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.MaterieId) ? string.Empty : string.Format(" and MaterieId = '{0}'", query.Criteria.MaterieId);
            condition += string.IsNullOrEmpty(query.Criteria.RepertoryId) ? string.Empty : string.Format(" and InRepertoryId = '{0}'", query.Criteria.RepertoryId);
            string sql = string.Format(@"select MaterieId as name,sum(Qty) as value from [dbo].[V_Wms_Pda_StorageList] {0} group by MaterieId ", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(query.SqlConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryPageAsync<SeriesData>(dbConn, "value desc", sql, query.PageModel);
                    result.Data = modelList.ToList<SeriesData>();
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = null;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取所有仓位名称
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IWmsShelves>>> GetWmsShelvesToRepertoryIdListAsync(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new DataResult<List<IWmsShelves>>();

            string condition = @" where 1=1 ";
            string sql = string.Format(@"select distinct(RepertoryId) from ls_wms_Shelves {0}", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(query.SqlConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<WmsShelvesModel>(dbConn, sql);
                    result.Data = modelList.ToList<IWmsShelves>();
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = null;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取储位信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IWmsStorage>>> GetWmsWmsStorageAllListAsync(QueryData<GetBaseInfoQuery> query)
        {
            var result = new DataResult<List<IWmsStorage>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.RepertoryId) ? string.Empty : string.Format(" and RepertoryId = '{0}'", query.RepertoryId);
            string sql = string.Format(@"select a.GUID, StorageId, a.ShelvesId, Storage_x, Storage_y, StorageName,a.IsBond,a.RangeMax,a.RangeMin
                from ls_wms_Storage a 
                left join dbo.ls_wms_Shelves b
                on a.ShelvesId=b.ShelvesId  {0}", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(query.SqlConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<WmsStorage>(dbConn, sql);
                    result.Data = modelList.ToList<IWmsStorage>();
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = null;
                }
            }
            return result;
        }

        #region 报表查询
        /// <summary>
        /// 获取出入库明细
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IWmsEntryAndExitGoods>>> GetIWmsEntryAndExitGoodsListAsync(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new DataResult<List<IWmsEntryAndExitGoods>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.DeliveryNo) ? string.Empty : string.Format(" and DeliveryNo like '%{0}%' ", query.Criteria.DeliveryNo);
            condition += string.IsNullOrEmpty(query.Criteria.MaterieId) ? string.Empty : string.Format(" and MaterieId like '%{0}%' ", query.Criteria.MaterieId);
            condition += string.IsNullOrEmpty(query.Criteria.StartScanTime) ? string.Empty : string.Format(" and CONVERT(varchar,ScanTime,23)>='{0}' ", query.Criteria.StartScanTime);
            condition += string.IsNullOrEmpty(query.Criteria.EndScanTime) ? string.Empty : string.Format(" and CONVERT(varchar,ScanTime,23)<='{0}' ", query.Criteria.EndScanTime);
            condition += string.IsNullOrEmpty(query.Criteria.StorageType) ? string.Empty : string.Format(" and StorageType = '{0}' ", query.Criteria.StorageType);
            condition += string.IsNullOrEmpty(query.Criteria.RepertoryId) ? string.Empty : string.Format(" and RepertoryId = '{0}' ", query.Criteria.RepertoryId);

            string sql = string.Format(@"SELECT * FROM 
	                (SELECT [Id],[DeliveryNo],[MaterieId],[RepertoryId],[InStorageNo] StorageNo,[LotNumber],[Qty],[InStorageStaffNo] StaffNo,  ScanTime,[InStorageType] StorageType,[MaterialCode],[StorageTime],[ValidTime]
	                FROM [dbo].[Wms_Pda_InStorage_Goods]
	                UNION ALL
	                SELECT [Id],[DeliveryNo],[MaterieId],[RepertoryId],[OutStorageNo] StorageNo,[LotNumber],[Qty],[ScanStaffNo] StaffNo,[ScanTime],[OutStorageType] StorageType,[MaterialCode],[StorageTime],[ValidTime]
	                FROM [dbo].[Wms_Pda_OutStorage_Goods]) t  {0}", condition);
            string sqlsto = @"SELECT Top 1 StorageName FROM [dbo].[ls_wms_Storage] WHERE StorageId=@StorageNo";
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(query.SqlConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryPageAsync<WmsEntryAndExitGoodsModel>(dbConn, "Id desc", sql, query.PageModel);
                    result.Data = modelList.ToList<IWmsEntryAndExitGoods>();
                    result.PageInfo.PageIndex = query.PageModel.PageIndex;
                    result.PageInfo.PageSize = query.PageModel.PageSize;
                    int totalCount = await MssqlHelper.QueryCountAsync(dbConn, sql);
                    result.PageInfo.TotalCount = totalCount;
                    if (result.Data.Count > 0)
                    {
                        foreach (var item in result.Data)
                        {
                            item.StorageName = await MssqlHelper.QueryFirstAsync<string>(dbConn, sqlsto, new { StorageNo = item.StorageNo });
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = null;
                }
            }
            return result;
        }

        /// <summary>
        /// 库存报表查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IWmsStock>>> GetIWmsStockListAsync(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new DataResult<List<IWmsStock>>();

            string condition = @" WHERE 1=1 and QTY > 0 ";
            condition += string.IsNullOrEmpty(query.Criteria.MaterieId) ? string.Empty : string.Format(" and MaterieId like '%{0}%'  ", query.Criteria.MaterieId);
            condition += string.IsNullOrEmpty(query.Criteria.RepertoryId) ? string.Empty : string.Format(" and InRepertoryId like '%{0}%' ", query.Criteria.RepertoryId);

            string sql = string.Format(@"select MaterieId,InRepertoryId as RepertoryId,sum(Qty) as Qty from [dbo].[V_Wms_Pda_StorageList] {0} group by MaterieId,InRepertoryId", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(query.SqlConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryPageAsync<WmsStockModel>(dbConn, "MaterieId asc", sql, query.PageModel);
                    result.Data = modelList.ToList<IWmsStock>();
                    result.PageInfo.PageIndex = query.PageModel.PageIndex;
                    result.PageInfo.PageSize = query.PageModel.PageSize;
                    int totalCount = await MssqlHelper.QueryCountAsync(dbConn, sql);
                    result.PageInfo.TotalCount = totalCount;
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = null;
                }
            }
            return result;
        }

        /// <summary>
        /// 物料库存详细查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IWmsStock>>> GetIWmsStockDetailListAsync(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new DataResult<List<IWmsStock>>();

            string condition = @" WHERE 1=1 and QTY > 0 ";
            condition += string.IsNullOrEmpty(query.Criteria.MaterieId) ? string.Empty : string.Format(" and MaterieId like '%{0}%'  ", query.Criteria.MaterieId);
            condition += string.IsNullOrEmpty(query.Criteria.RepertoryId) ? string.Empty : string.Format(" and InRepertoryId like '%{0}%' ", query.Criteria.RepertoryId);
            //成品库存视图
            string sql = string.Format(@"select MaterieId,InRepertoryId as RepertoryId,LotNumber,StorageTime,ValidTime,InStorageNo,Qty,Fnumber from [dbo].[V_Wms_Pda_FinishedProductList] {0}", condition);

            //原材料试图库存
            string sql2 = string.Format(@"select [MaterieId]
                                              ,[LotNumber]
                                              ,[InStorageNo]
                                              ,[Qty]
                                              ,[StorageTime]
                                              ,[InRepertoryId]
                                              ,[OutRepertoryId]
                                              ,[InStorageName]
                                              ,[OnStorageName] from [dbo].[V_Wms_Pda_StorageList] {0}", condition);



            string sqlsto = @"SELECT Top 1 StorageName FROM [dbo].[ls_wms_Storage] WHERE StorageId=@StorageNo";

            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(query.SqlConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryPageAsync<WmsStockModel>(dbConn, "MaterieId asc", sql, query.PageModel);
                    string fnumberV = modelList.Select(s => s.Fnumber).FirstOrDefault();
                    if (string.IsNullOrEmpty(fnumberV))
                    {
                        modelList = await MssqlHelper.QueryPageAsync<WmsStockModel>(dbConn, "MaterieId asc", sql2, query.PageModel);
                    }
                    result.Data = modelList.ToList<IWmsStock>();
                    if (result.Data.Count > 0)
                    {
                        foreach (var item in result.Data)
                        {
                            item.StorageName = await MssqlHelper.QueryFirstAsync<string>(dbConn, sqlsto, new { StorageNo = item.InStorageNo });
                        }
                    }
                    result.PageInfo.PageIndex = query.PageModel.PageIndex;
                    result.PageInfo.PageSize = query.PageModel.PageSize;
                    int totalCount = await MssqlHelper.QueryCountAsync(dbConn, sql);
                    result.PageInfo.TotalCount = totalCount;
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = null;
                }
            }
            return result;
        }


        #endregion

    }
}
