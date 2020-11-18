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
    /// 物料入库数据接口实现
    /// </summary>
    public class WmsPdaInStorageGoodsService : IWmsPdaInStorageGoodsService
    {
        /// <summary>
        /// 获取入库类型集合
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IWmsInStorageGoods>>> GetInStorageTypeListAsync(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new DataResult<List<IWmsInStorageGoods>>();

            string condition = @" where 1=1 ";
            string sql = string.Format(@"select distinct(InStorageType) as InStorageType from [dbo].[Wms_Pda_InStorage_Goods]  {0}", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(query.SqlConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<WmsInStorageGoods>(dbConn, sql);
                    result.Data = modelList.ToList<IWmsInStorageGoods>();
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
        /// 获取近七天仓库库存趋势
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IWmsInStorageGoods>>> GetServerDayWmsInStorageGoodsKCAsync(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new DataResult<List<IWmsInStorageGoods>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.StartScanTime) ? string.Empty : string.Format(" and ScanTime <= '{0}'", query.Criteria.StartScanTime);
            condition += string.IsNullOrEmpty(query.Criteria.DeliveryNo) ? string.Empty : string.Format(" and DeliveryNo = '{0}'", query.Criteria.DeliveryNo);
            condition += string.IsNullOrEmpty(query.Criteria.MaterieId) ? string.Empty : string.Format(" and MaterieId = '{0}'", query.Criteria.MaterieId);
            condition += string.IsNullOrEmpty(query.Criteria.RepertoryId) ? string.Empty : string.Format(" and RepertoryId = '{0}'", query.Criteria.RepertoryId);
            string sql = string.Format(@"select a.RepertoryId,ISNULL((a.Qty - b.Qty), 0) as Qty from
					(select RepertoryId,SUM(Qty) as Qty from [dbo].[Wms_Pda_InStorage_Goods] {0} group by RepertoryId) a  left join
					(select RepertoryId,SUM(Qty) as Qty from [dbo].[Wms_Pda_OutStorage_Goods] {0} group by RepertoryId) b on a.RepertoryId = b.RepertoryId", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(query.SqlConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<WmsInStorageGoods>(dbConn, sql);
                    result.Data = modelList.ToList<IWmsInStorageGoods>();
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                    result.Data = null;
                }
            }
            return result;
        }

        public async Task<DataResult<List<IWmsInStorageGoods>>> GetWmsInStorageGoodsAllListAsync(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new DataResult<List<IWmsInStorageGoods>>();

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
                          ,[InStorageType]
                          ,[InStorageNo]
                          ,[InStorageStaffNo]
                          ,[InStorageStaffName]
                          ,[InStorageTime]
                          ,[MaterialCode]
                          ,[RepertoryId]
                      FROM [dbo].[Wms_Pda_InStorage_Goods]  {0}", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(query.SqlConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryListAsync<WmsInStorageGoods>(dbConn, sql);
                    result.Data = modelList.ToList<IWmsInStorageGoods>();
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
        /// 获取入库物料信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IWmsInStorageGoods>>> GetWmsInStorageGoodsListAsync(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new DataResult<List<IWmsInStorageGoods>>();

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
                          ,[InStorageType]
                          ,[InStorageNo]
                          ,[InStorageStaffNo]
                          ,[InStorageStaffName]
                          ,[InStorageTime]
                          ,[MaterialCode]
                          ,[RepertoryId]
                      FROM [dbo].[Wms_Pda_InStorage_Goods]  {0}", condition);
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(query.SqlConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryPageAsync<WmsInStorageGoods>(dbConn, "Id desc", sql, query.PageModel);
                    result.Data = modelList.ToList<IWmsInStorageGoods>();
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
