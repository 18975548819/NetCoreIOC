using DataEntities.InterfaceEntities.BasicInfo;
using DataEntities.InterfaceModel.BasicInfo;
using DataEntities.QueryModel.BasicInfo;
using Freed.Common.Data;
using Freed.Common.Helpers;
using IDataService.BasicInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService.BasicInfo
{
    /// <summary>
    /// 功能模块数据操作接口实现
    /// </summary>
    public class SysModuleService : ISysModuleService
    {
        /// <summary>
        /// 删除功能模块
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<int>> DeleteSysModuleAsync(QueryData<GetBaseInfoByModuleQuery> query)
        {
            var result = new DataResult<int>();
            string sqlWhere = " where 1 = 1 ";
            sqlWhere += string.Format(" and ID = {0}", query.Criteria.ID);
            sqlWhere += string.IsNullOrEmpty(query.Criteria.ModuleNO) ? string.Empty : string.Format(" and ModuleNO = '{0}'", query.Criteria.ModuleNO);
            sqlWhere += string.IsNullOrEmpty(query.Criteria.ParentModuleNO) ? string.Empty : string.Format(" and ParentModuleNO = '{0}'", query.Criteria.ParentModuleNO);



            string sql = string.Format(@"  delete from Sys_Module {0} ", sqlWhere);
            string sql2 = string.Format(@"  delete from Sys_Module where 1 = 1 and  ParentModuleNO = '{0}'", query.Criteria.ParentModuleNO);  //删除子模块

            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                IDbTransaction transaction = dbConn.BeginTransaction();
                try
                {
                    if (!string.IsNullOrEmpty(query.Criteria.ParentModuleNO))
                    {
                        result.Data = await MssqlHelper.ExecuteSqlAsync(dbConn, sql2, null, transaction);
                        if (result.Data > 0)
                        {
                            result.Data = await MssqlHelper.ExecuteSqlAsync(dbConn, sql, null, transaction);
                        }
                    }
                    else
                    {
                        result.Data = await MssqlHelper.ExecuteSqlAsync(dbConn, sql, null, transaction);
                    }
                    if (result.Data <= 0)
                    {
                        transaction.Rollback();
                        result.SetErr("操作异常", result.Data);
                        return result;
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
            {
                result.SetErr(ex, -500);
            }
        }
            return result;
        }

        /// <summary>
        /// 修改功能模块
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<int>> EidtSysModuleAsync(QueryData<InsertSysModuleQuery> query)
        {
            var result = new DataResult<int>();
            string sql = string.Format(@"  update Sys_Module set ModuleName = @ModuleName,LeafFlag = @LeafFlag,FormName = @FormName,FormName = @FormName,SortNumber = @SortNumber,IsEnable = @IsEnable,Remark = @Remark where ID = @ID");

            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                IDbTransaction transaction = dbConn.BeginTransaction();
                try
                {
                    foreach (var item in query.Criteria.sysModule)
                    {
                        result.Data = await MssqlHelper.ExecuteSqlAsync(dbConn, sql, item, transaction);
                        if (result.Data <= 0)
                        {
                            transaction.Rollback();
                            result.SetErr("操作异常", result.Data);
                            return result;
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取功能模块主菜单
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<List<ISysModule>>> GetSysModuleMainListAsync(QueryData<GetBaseInfoByModuleQuery> query)
        {
            var result = new DataResult<List<ISysModule>>();

            string condition = @" where 1=1 ";
            condition += string.IsNullOrEmpty(query.Criteria.ModuleNO) ? string.Empty : string.Format(" and ModuleNO = '{0}'", query.Criteria.ModuleNO);
            condition += string.IsNullOrEmpty(query.Criteria.ParentModuleNO) ? string.Empty : string.Format(" and ParentModuleNO = '{0}'", query.Criteria.ParentModuleNO);
            condition += string.IsNullOrEmpty(query.Criteria.ModuleType) ? string.Empty : string.Format(" and ModuleType = '{0}'", query.Criteria.ModuleType);
            string sql = @"SELECT [ID]
                          ,[ModuleNO]
                          ,[ModuleName]
                          ,[ModuleType]
                          ,[ParentModuleNO]
                          ,[ModuleLevel]
                          ,[LeafFlag]
                          ,[Icon]
                          ,[ButtonImg]
                          ,[NodeImg]
                          ,[SelectNodeImg]
                          ,[FormName]
                          ,[SortNumber]
                          ,[IsEnable]
                          ,[CreateName]
                          ,[CreateTime]
                          ,[Remark]
                          ,[iframe]
                      FROM [dbo].[Sys_Module] "
                + condition;
            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                try
                {
                    var modelList = await MssqlHelper.QueryPageAsync<SysModule>(dbConn, " ID", sql, query.PageModel);
                    result.Data = modelList.ToList<ISysModule>();
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

        /// <summary>
        /// 新增功能模块
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<DataResult<int>> InsertSysModuleSaveAsync(QueryData<InsertSysModuleQuery> query)
        {
            var result = new DataResult<int>();
            string sql = string.Format(@"  insert into Sys_Module(ModuleNO,ModuleName,ModuleType,ParentModuleNO,ModuleLevel,LeafFlag,Icon,ButtonImg,NodeImg,SelectNodeImg,FormName,SortNumber,IsEnable,CreateName,CreateTime,Remark,iframe) values(@ModuleNO,@ModuleName,@ModuleType,@ParentModuleNO,@ModuleLevel,@LeafFlag,@Icon,@ButtonImg,@NodeImg,@SelectNodeImg,@FormName,@SortNumber,@IsEnable,@CreateName,@CreateTime,@Remark,@iframe)");

            string sqlMax = string.Format(@"SELECT top 1 [ID]
                                          ,[ModuleNO]
                                          ,[ModuleName]
                                          ,[ModuleType]
                                          ,[ParentModuleNO]
                                          ,[ModuleLevel]
                                          ,[LeafFlag]
                                          ,[Icon]
                                          ,[ButtonImg]
                                          ,[NodeImg]
                                          ,[SelectNodeImg]
                                          ,[FormName]
                                          ,[SortNumber]
                                          ,[IsEnable]
                                          ,[CreateName]
                                          ,[CreateTime]
                                          ,[Remark]
                                          ,[iframe]
                                      FROM [CNCPROCommon].[dbo].[Sys_Module] where ParentModuleNO = '' and ModuleType = 'Floder' order by ModuleNO desc");

            using (IDbConnection dbConn = MssqlHelper.OpenMsSqlConnection(MssqlHelper.GetConn))
            {
                IDbTransaction transaction = dbConn.BeginTransaction();
                try
                {
                    foreach (var item in query.Criteria.sysModule)
                    {
                        if (item.ParentModuleNO == "")
                        {
                            var model = await MssqlHelper.QueryFirstOrDefaultAsync<SysModule>(dbConn, sqlMax, null, transaction);
                            if (model != null)
                            {
                                int moduleNo = Convert.ToInt32(model.ModuleNO);
                                item.ModuleNO = (moduleNo + 1).ToString().PadLeft(3, '0');
                            }
                        }

                        result.Data = await MssqlHelper.ExecuteSqlAsync(dbConn, sql, item, transaction);
                        if (result.Data <= 0)
                        {
                            transaction.Rollback();
                            result.SetErr("操作异常", result.Data);
                            return result;
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    result.SetErr(ex, -500);
                }
            }
            return result;
        }
    }
}
