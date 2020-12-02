using DataEntities.InterfaceEntities.WMS;
using DataEntities.QueryModel;
using DataModel.WMS;
using Freed.Common.Data;
using IBusinessManage.WMS;
using IDataService.WNS;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessManage.WMS
{
    public class WmsPdaInStorageGoodsManager : IWmsPdaInStorageGoodsManager
    {
        private IWmsPdaInStorageGoodsService _service;

        private IWmsPdaOutStorageGoodsService _serviceOut;

        public WmsPdaInStorageGoodsManager(IWmsPdaInStorageGoodsService service, IWmsPdaOutStorageGoodsService serviceOut)
        {
            this._service = service;
            this._serviceOut = serviceOut;
        }

        /// <summary>
        /// 当天物料入库信息（动态环图）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ErrData<DvActiveRingChartModel>> GetInStorageGoodInfoByDvActiveRingChartMaAsyn(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new ErrData<DvActiveRingChartModel>();
            var dt = DateTime.Now;
            DvActiveRingChartModel dvActiveRingChart = new DvActiveRingChartModel();

            var res = await _service.GetWmsInStorageGoodsAllListAsync(query);
            if (res.HasErr)
            {
                result.SetInfo("查询异常", res.ErrCode);
            }
            else
            {
                if (res.Data != null && res.Data.Count > 0)
                {
                    List<ActiveRingChartData> chartDatas = new List<ActiveRingChartData>();
                    var groups = res.Data.GroupBy(p => p.InStorageType);
                    foreach (var gp in groups)
                    {
                        ActiveRingChartData chartData = new ActiveRingChartData();
                        chartData.name = gp.Key.ToString();
                        chartData.value = gp.Count();
                        chartDatas.Add(chartData);
                    }
                    dvActiveRingChart.data = chartDatas;
                }
                result.SetInfo(dvActiveRingChart, "成功", 200);
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        /// <summary>
        /// 获取当前入库物料信息（轮播表）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ErrData<DvScrollBoardModel>> GetInStorageGoodInfoByDvScrollBoardMaAsyn(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new ErrData<DvScrollBoardModel>();
            var dt = DateTime.Now;

            var res = await _service.GetWmsInStorageGoodsListAsync(query);
            if (res.HasErr)
            {
                result.SetInfo("查询异常", res.ErrCode);
            }
            else
            {
                DvScrollBoardModel dvScrollBoard = new DvScrollBoardModel();
                List<string> hd = new List<string>() { "物料号", "数量", "入库类型"};
                dvScrollBoard.header = hd;
                dvScrollBoard.index = false;

                var info = res.Data.Take(query.PageModel.PageSize);
                if (info != null && info.Count() > 0)
                {

                    List<List<string>> ob = new List<List<string>>();
                    foreach (var item in info)
                    {
                        List<string> obData = new List<string>();
                        obData.Add(item.MaterieId);
                        obData.Add(item.Qty.ToString());
                        obData.Add(item.InStorageType);
                        ob.Add(obData);
                    }
                    dvScrollBoard.data = ob;

                    result.SetInfo(dvScrollBoard, "成功", 200);
                }
                else
                {
                    result.SetInfo(null, "获取物料入库明细失败", -102);
                }
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        /// <summary>
        /// 获取近七天物料入库信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ErrData<EchartsLineDataModels>> GetInStorageGoodInfoByServerDayMaAsyn(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new ErrData<EchartsLineDataModels>();
            var dt = DateTime.Now;


            EchartsLineDataModels echartsLine = new EchartsLineDataModels();
            List<string> xData = new List<string>();
            for (int i = 6; i >= 0; i--)
            {
                xData.Add(DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd"));
            }
            echartsLine.xAxisData = xData;

            //入
            var res = await _service.GetWmsInStorageGoodsAllListAsync(query);
            if (res.HasErr)
            {
                result.SetInfo("查询异常", res.ErrCode);
            }
            else
            {
                var info = res.Data;
                if (info != null && info.Count() > 0)
                {
                    List<int> yData = new List<int>();
                    foreach (var item in xData)
                    {
                        int count = info.Where(g => g.ScanTime >= Convert.ToDateTime(item) && g.ScanTime <= Convert.ToDateTime(item).AddDays(1)).Count();
                        yData.Add(count);
                    }
                    echartsLine.yAxisData1 = yData;
                }
                else
                {
                    //result.SetInfo(null, "获取失败", -102);
                }
            }

            //出
            var resOut = await _serviceOut.GetWmsOutStorageGoodsAllListAsync(query);
            if (resOut.HasErr)
            {
                result.SetInfo("查询异常", resOut.ErrCode);
            }
            else
            {
                var infoOut = resOut.Data;
                if (infoOut != null && infoOut.Count() > 0)
                {
                    List<int> yDataOut = new List<int>();
                    foreach (var item in xData)
                    {
                        int count = infoOut.Where(g => g.ScanTime >= Convert.ToDateTime(item) && g.ScanTime <= Convert.ToDateTime(item).AddDays(1)).Count();
                        yDataOut.Add(count);
                    }
                    echartsLine.yAxisData2 = yDataOut;
                }
                else
                {
                    //result.SetInfo(null, "获取失败", -102);
                }
            }

            result.SetInfo(echartsLine, "成功", 200);
            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        /// <summary>
        /// 获取入库物料信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ListResult<IWmsInStorageGoods>> GetInStorageGoodListMaAsyn(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new ListResult<IWmsInStorageGoods>();
            var dt = DateTime.Now;

            try
            {
                var res = await _service.GetWmsInStorageGoodsListAsync(query);
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
                            result.Results.Add(item);
                        }
                    }
                    result.SetInfo("成功", 200);
                }
            }
            catch (Exception ex)
            {
                result.SetInfo(ex.ToString(),-500);
            }
            return result;
        }
    }
}
