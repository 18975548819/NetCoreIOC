using DataEntities.InterfaceEntities.WMS;
using DataEntities.QueryModel;
using DataModel.WMS;
using Freed.Common.Data;
using IBusinessManage.WMS;
using IDataService.WNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManage.WMS
{
    /// <summary>
    /// WMS基础数据业务逻辑接口实现
    /// </summary>
    public class WmsBasicInfoManager : IWmsBasicInfoManager
    {
        private IWmsBasicInfoService _service;
        private IWmsPdaInStorageGoodsService _inService;
        private IWmsPdaOutStorageGoodsService _outService;

        public WmsBasicInfoManager(IWmsBasicInfoService service, IWmsPdaInStorageGoodsService inService, IWmsPdaOutStorageGoodsService outService)
        {
            _service = service;
            _inService = inService;
            _outService = outService;
        }

        /// <summary>
        /// 获取各仓库物料库存数量
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ErrData<EchartsPieDataModel>> GetEchartsPieDataKCMsAsync(QueryData<GetBaseInfoQuery> query)
        {
            var result = new ErrData<EchartsPieDataModel>();
            var dt = DateTime.Now;

            var res = await _service.GetSeriesDataKCAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(res.ErrMsg, res.ErrCode);
                return result;
            }
            else
            {
                if (res.Data.Count > 0)
                {
                    EchartsPieDataModel echartsPieData = new EchartsPieDataModel();
                    List<string> nData = new List<string>();
                    List<SeriesData> vData = new List<SeriesData>();
                    foreach (var item in res.Data)
                    {
                        nData.Add(item.name);
                        vData.Add(item);
                    }
                    echartsPieData.legendData = nData;
                    echartsPieData.seriesDatas = vData;
                    result.SetInfo(echartsPieData, "成功", 200);
                }
                else
                {
                    result.SetInfo(null, "暂时未获取到仓库物料数量信息", 200);
                }
            }
            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        /// <summary>
        /// 仓库物料数量获取
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ErrData<EchartsPieDataModel>> GetEchartsPieDataMsAsync(QueryData<GetBaseInfoQuery> query)
        {
            var result = new ErrData<EchartsPieDataModel>();
            var dt = DateTime.Now;

            var res = await _service.GetSeriesDataAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(res.ErrMsg, res.ErrCode);
                return result;
            }
            else
            {
                if (res.Data.Count > 0)
                {
                    EchartsPieDataModel echartsPieData = new EchartsPieDataModel();
                    List<string> nData = new List<string>();
                    List<SeriesData> vData = new List<SeriesData>();
                    foreach (var item in res.Data)
                    {
                        nData.Add(item.name);
                        vData.Add(item);
                    }
                    echartsPieData.legendData = nData;
                    echartsPieData.seriesDatas = vData;
                    result.SetInfo(echartsPieData, "成功", 200);
                }
                else
                {
                    result.SetInfo(null, "暂时未获取到仓库物料数量信息", 200);
                }
            }
            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        /// <summary>
        /// 获取所有仓库和出入库类型
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ErrData<ElSelectOptionSelectListModel>> GetRepertoryIdAndStorageTypeListMsAsync(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new ErrData<ElSelectOptionSelectListModel>();
            var dt = DateTime.Now;

            ElSelectOptionSelectListModel elSelectOptionSelectListModel = new ElSelectOptionSelectListModel();
            List<ElSelectOptionModel> repertoryList = new List<ElSelectOptionModel>();
            List<ElSelectOptionModel> storageTypeList = new List<ElSelectOptionModel>();

            var res = await _service.GetWmsShelvesToRepertoryIdListAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(res.ErrMsg, res.ErrCode);
                return result;
            }
            else
            {
                if (res.Data.Count > 0)
                {
                    foreach (var item in res.Data)
                    {
                        ElSelectOptionModel elSelectOptionRep = new ElSelectOptionModel();
                        elSelectOptionRep.label = item.RepertoryId;
                        elSelectOptionRep.value = Guid.NewGuid().ToString();
                        repertoryList.Add(elSelectOptionRep);
                    }
                    elSelectOptionSelectListModel.RepertoryIdList = repertoryList;
                }
            }

            var resIn = await _inService.GetInStorageTypeListAsync(query);
            if (resIn.HasErr)
            {
                result.SetInfo(resIn.ErrMsg, resIn.ErrCode);
                return result;
            }
            else
            {
                if (resIn.Data.Count > 0)
                {
                    foreach (var itemIn in resIn.Data)
                    {
                        ElSelectOptionModel elSelectOptionIn = new ElSelectOptionModel();
                        elSelectOptionIn.label = itemIn.InStorageType;
                        elSelectOptionIn.value = Guid.NewGuid().ToString();
                        storageTypeList.Add(elSelectOptionIn);
                    }
                }
            }

            var resOut = await _outService.GetOutStorageTypeListAsync(query);
            if (resOut.HasErr)
            {
                result.SetInfo(resOut.ErrMsg, resOut.ErrCode);
                return result;
            }
            else
            {
                if (resOut.Data.Count > 0)
                {
                    foreach (var itemOut in resIn.Data)
                    {
                        ElSelectOptionModel elSelectOptionOut = new ElSelectOptionModel();
                        elSelectOptionOut.label = itemOut.InStorageType;
                        elSelectOptionOut.value = Guid.NewGuid().ToString();
                        storageTypeList.Add(elSelectOptionOut);
                    }
                }
            }

            elSelectOptionSelectListModel.StorageTypeList = storageTypeList;

            result.SetInfo(elSelectOptionSelectListModel, "成功", 200);
            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        /// <summary>
        /// 获取近七天仓库库存趋势
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ErrData<EchartsLineDataModels>> GetServerDayInAndOutWarehouseTrendsMsAsync(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new ErrData<EchartsLineDataModels>();
            var dt = DateTime.Now;
            EchartsLineDataModels echartsLineData = new EchartsLineDataModels();
            List<string> reportyList = new List<string>();
            List<int> yAxisData1 = new List<int>();
            List<WarehouseTrendsModel> warehousesLsit = new List<WarehouseTrendsModel>();
            List<Dictionary<string, List<int>>> multipleList = new List<Dictionary<string, List<int>>>();
            try
            {
                echartsLineData.echartsName = "仓库近七天物料进出趋势";
                List<string> xData = new List<string>();
                for (int i = 6; i >= 0; i--)
                {
                    xData.Add(DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd"));
                }
                echartsLineData.xAxisData = xData;

                foreach (var item in xData)
                {
                    GetWmsInStorageGoodsQuery inStorageGoodsQuery = new GetWmsInStorageGoodsQuery();
                    inStorageGoodsQuery.StartScanTime = item;
                    inStorageGoodsQuery.RepertoryId = query.RepertoryId;
                    query.Criteria = inStorageGoodsQuery;
                    var inRes = await _inService.GetServerDayWmsInStorageGoodsKCAsync(query);
                    if (inRes.HasErr)
                    {
                        result.SetInfo(inRes.ErrMsg, inRes.ErrCode);
                        return result;
                    }
                    if (inRes.Data.Count > 0)
                    {
                        //先获取仓库名称集合
                        foreach (var goods in inRes.Data)
                        {
                            if (!reportyList.Exists(e => e == goods.RepertoryId))
                            {
                                reportyList.Add(goods.RepertoryId);
                            }
                            WarehouseTrendsModel warehouse = new WarehouseTrendsModel();
                            warehouse.DataTime = item;
                            warehouse.RepertoryId = goods.RepertoryId;
                            warehouse.Num = Convert.ToInt32(goods.Qty);
                            warehousesLsit.Add(warehouse);
                        }
                    }

                }

                foreach (var reporty in reportyList)
                {
                    Dictionary<string, List<int>> valuePairs = new Dictionary<string, List<int>>();
                    List<int> nhNum = new List<int>();
                    for (int i = 0; i < xData.Count; i++)
                    {
                        var obs = warehousesLsit.Where(w => w.RepertoryId == reporty && w.DataTime == xData[i]).FirstOrDefault();
                        nhNum.Add(Convert.ToInt32(obs.Num));
                    }
                    valuePairs.Add(reporty, nhNum);
                    multipleList.Add(valuePairs);
                }

                echartsLineData.yAxisDataMultiple = multipleList;
                result.SetInfo(echartsLineData, "成功", 200);
            }
            catch (Exception ex)
            {
                result.SetInfo(ex.ToString(), -500);
            }
            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        /// <summary>
        /// 获取储位信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ListResult<IWmsStorage>> GetWmsWmsStorageAllListMsAsync(QueryData<GetBaseInfoQuery> query)
        {
            var result = new ListResult<IWmsStorage>();
            var dt = DateTime.Now;

            var res = await _service.GetWmsWmsStorageAllListAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(res.ErrMsg, res.ErrCode);
                return result;
            }
            else
            {
                if (res.Data != null && res.Data.Count > 0)
                {
                    foreach (var item in res.Data)
                    {
                        result.Results.Add(item);
                    }
                }
            }
            result.SetInfo("成功", 200);
            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        #region 报表查询
        /// <summary>
        /// 获取出入库明细
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ListResult<IWmsEntryAndExitGoods>> GetWmsEntryAndExitGoodsMsAsync(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new ListResult<IWmsEntryAndExitGoods>();
            var dt = DateTime.Now;

            var res = await _service.GetIWmsEntryAndExitGoodsListAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(res.ErrMsg, res.ErrCode);
                return result;
            }
            else
            {
                if (res.Data != null && res.Data.Count > 0)
                {
                    foreach (var item in res.Data)
                    {
                        result.Results.Add(item);
                    }
                }
            }
            result.PageModel = res.PageInfo;
            result.SetInfo("成功", 200);
            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        /// <summary>
        /// 库存报表查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ListResult<IWmsStock>> GetIWmsStockMsAsync(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new ListResult<IWmsStock>();
            var dt = DateTime.Now;

            var res = await _service.GetIWmsStockListAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(res.ErrMsg, res.ErrCode);
                return result;
            }
            else
            {
                if (res.Data != null && res.Data.Count > 0)
                {
                    foreach (var item in res.Data)
                    {
                        result.Results.Add(item);
                    }
                }
            }
            result.PageModel = res.PageInfo;
            result.SetInfo("成功", 200);
            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }
        #endregion
    }
}
