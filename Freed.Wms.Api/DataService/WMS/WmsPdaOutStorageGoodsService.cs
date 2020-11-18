using DataEntities.InterfaceEntities.WMS;
using DataEntities.InterfaceModel.WMS;
using DataEntities.QueryModel;
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
    /// 物料出库数据操作实现层
    /// </summary>
    public class WmsPdaOutStorageGoodsService : IWmsPdaOutStorageGoodsService
    {
        /// <summary>
        /// 获取出库类型集合
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IWmsOutStorageGoods>>> GetOutStorageTypeListAsync(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new DataResult<List<IWmsOutStorageGoods>>();

            string condition = @" where 1=1 ";
            string sql = string.Format(@" select distinct(OutStorageType) from [dbo].[Wms_Pda_OutStorage_Goods] {0} ", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(query.SqlConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<WmsOutStorageGoodsModel>(dbConn, sql);
                    result.Data = modelList.ToList<IWmsOutStorageGoods>();
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
        /// 获取各仓库近七天物料出库信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IWmsOutStorageGoods>>> GetServerDayWmsOutStorageGoodsListAsync(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new DataResult<List<IWmsOutStorageGoods>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.StartScanTime) ? string.Empty : string.Format(" and ScanTime >= '{0}' and ScanTime <= '{1}'", query.Criteria.StartScanTime, query.Criteria.EndScanTime);
            condition += string.IsNullOrEmpty(query.Criteria.DeliveryNo) ? string.Empty : string.Format(" and DeliveryNo = '{0}'", query.Criteria.DeliveryNo);
            condition += string.IsNullOrEmpty(query.Criteria.MaterieId) ? string.Empty : string.Format(" and MaterieId = '{0}'", query.Criteria.MaterieId);
            condition += string.IsNullOrEmpty(query.RepertoryId) ? string.Empty : string.Format(" and RepertoryId = '{0}'", query.RepertoryId);
            string sql = string.Format(@"     select RepertoryId,ScanTime,SUM(Qty) as Qty from 
               (select RepertoryId,CONVERT(varchar(100), ScanTime, 23) as ScanTime, Qty from [dbo].[Wms_Pda_OutStorage_Goods] {0}') a
               group by RepertoryId,ScanTime order by RepertoryId,ScanTime ", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(query.SqlConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<WmsOutStorageGoodsModel>(dbConn, sql);
                    result.Data = modelList.ToList<IWmsOutStorageGoods>();
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = null;
                }
            }
            return result;
        }

        public async Task<DataResult<List<IWmsOutStorageGoods>>> GetWmsOutStorageGoodsAllListAsync(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new DataResult<List<IWmsOutStorageGoods>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.StartScanTime) ? string.Empty : string.Format(" and ScanTime >= '{0}' and ScanTime <= '{1}'", query.Criteria.StartScanTime, query.Criteria.EndScanTime);
            condition += string.IsNullOrEmpty(query.Criteria.DeliveryNo) ? string.Empty : string.Format(" and DeliveryNo = '{0}'", query.Criteria.DeliveryNo);
            condition += string.IsNullOrEmpty(query.Criteria.MaterieId) ? string.Empty : string.Format(" and MaterieId = '{0}'", query.Criteria.MaterieId);
            condition += string.IsNullOrEmpty(query.RepertoryId) ? string.Empty : string.Format(" and RepertoryId = '{0}'", query.RepertoryId);
            string sql = string.Format(@"SELECT [Id]
                              ,[MaterieId]
                              ,[Spec]
                              ,[LotNumber]
                              ,[StorageTime]
                              ,[ValidTime]
                              ,[Fnumber]
                              ,[Qty]
                              ,[DeliveryNo]
                              ,[ScanTime]
                              ,[ScanStaffNo]
                              ,[ScanStaffName]
                              ,[OutStorageType]
                              ,[OutStorageNo]
                              ,[MaterialCode]
                              ,[RepertoryId]
                              ,[AllocationRepertoryId]
                              ,[IsForceOut]
                          FROM [dbo].[Wms_Pda_OutStorage_Goods]  {0}", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(query.SqlConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<WmsOutStorageGoodsModel>(dbConn, sql);
                    result.Data = modelList.ToList<IWmsOutStorageGoods>();
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
        /// 获取出库物料信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IWmsOutStorageGoods>>> GetWmsOutStorageGoodsListAsync(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new DataResult<List<IWmsOutStorageGoods>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.StartScanTime) ? string.Empty : string.Format(" and ScanTime >= '{0}' and ScanTime <= '{1}'", query.Criteria.StartScanTime, query.Criteria.EndScanTime);
            condition += string.IsNullOrEmpty(query.Criteria.DeliveryNo) ? string.Empty : string.Format(" and DeliveryNo = '{0}'", query.Criteria.DeliveryNo);
            condition += string.IsNullOrEmpty(query.Criteria.MaterieId) ? string.Empty : string.Format(" and MaterieId = '{0}'", query.Criteria.MaterieId);
            condition += string.IsNullOrEmpty(query.RepertoryId) ? string.Empty : string.Format(" and RepertoryId = '{0}'", query.RepertoryId);
            string sql = string.Format(@"SELECT [Id]
                              ,[MaterieId]
                              ,[Spec]
                              ,[LotNumber]
                              ,[StorageTime]
                              ,[ValidTime]
                              ,[Fnumber]
                              ,[Qty]
                              ,[DeliveryNo]
                              ,[ScanTime]
                              ,[ScanStaffNo]
                              ,[ScanStaffName]
                              ,[OutStorageType]
                              ,[OutStorageNo]
                              ,[MaterialCode]
                              ,[RepertoryId]
                              ,[AllocationRepertoryId]
                              ,[IsForceOut]
                          FROM [dbo].[Wms_Pda_OutStorage_Goods]  {0}", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(query.SqlConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryPageAsync<WmsOutStorageGoodsModel>(dbConn, "Id desc", sql, query.PageModel);
                    result.Data = modelList.ToList<IWmsOutStorageGoods>();
                    result.PageInfo = query.PageModel;
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
