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
    /// 出库物料业务逻辑实现层
    /// </summary>
    public class WmsPdaOutStorageGoodsManager : IWmsPdaOutStorageGoodsManager
    {
        private IWmsPdaOutStorageGoodsService _service;

        public WmsPdaOutStorageGoodsManager(IWmsPdaOutStorageGoodsService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取出库物料信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ErrData<DvScrollBoardModel>> GetOutStorageGoodInfoByDvScrollBoardMaAsyn(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new ErrData<DvScrollBoardModel>();
            var dt = DateTime.Now;

            var res = await _service.GetWmsOutStorageGoodsListAsync(query);
            if (res.HasErr)
            {
                result.SetInfo("查询异常", res.ErrCode);
            }
            else
            {
                DvScrollBoardModel dvScrollBoard = new DvScrollBoardModel();
                List<string> hd = new List<string>() { "物料号", "数量", "出库类型" };
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
                        obData.Add(item.OutStorageType);
                        ob.Add(obData);
                    }
                    dvScrollBoard.data = ob;

                    result.SetInfo(dvScrollBoard, "成功", 200);
                }
                else
                {
                    result.SetInfo(null, "获取物料出库明细失败", -102);
                }
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        public async Task<ErrData<EchartsLineDataModels>> GetOutStorageGoodInfoByServerDayMaAsyn(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new ErrData<EchartsLineDataModels>();
            var dt = DateTime.Now;

            var res = await _service.GetWmsOutStorageGoodsAllListAsync(query);
            if (res.HasErr)
            {
                result.SetInfo("查询异常", res.ErrCode);
            }
            else
            {
                EchartsLineDataModels echartsLine = new EchartsLineDataModels();
                List<string> xData = new List<string>();
                for (int i = 6; i >= 0; i--)
                {
                    xData.Add(DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd"));
                }
                echartsLine.xAxisData = xData;

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
                    result.SetInfo(echartsLine, "成功", 200);
                }
                else
                {
                    result.SetInfo(null, "获取失败", -102);
                }
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }

        /// <summary>
        /// 当天（动态环图）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ErrData<DvActiveRingChartModel>> GetOutStorageGoodInfoDvActiveRingChartMaAsyn(QueryData<GetWmsInStorageGoodsQuery> query)
        {
            var result = new ErrData<DvActiveRingChartModel>();
            var dt = DateTime.Now;
            DvActiveRingChartModel dvActiveRingChart = new DvActiveRingChartModel();

            var res = await _service.GetWmsOutStorageGoodsAllListAsync(query);
            if (res.HasErr)
            {
                result.SetInfo("查询异常", res.ErrCode);
            }
            else
            {
                if (res.Data != null && res.Data.Count > 0)
                {
                    List<ActiveRingChartData> chartDatas = new List<ActiveRingChartData>();
                    var groups = res.Data.GroupBy(p => p.OutStorageType);
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
    }
}
