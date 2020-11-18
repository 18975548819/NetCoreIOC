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
    public class WmsPdaStorageMaterialManager : IWmsPdaStorageMaterialManager
    {
        private IWmsInStorageMaterialService _inService;
        private IWmsOutStorageMaterialService _outService;

        public WmsPdaStorageMaterialManager(IWmsInStorageMaterialService inService, IWmsOutStorageMaterialService outService)
        {
            _inService = inService;
            _outService = outService;
        }

        /// <summary>
        /// 近七天单据出入库
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<ErrData<EchartsLineDataModels>> GetStorageMaterialInfoByServerDayMaAsyn(QueryData<GetWmsInStorageMaterialQuery> query)
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


            //入库单据
            var res = await _inService.GetWmsInStorageMaterialAllListAsync(query);
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
                    result.SetInfo(echartsLine, "成功", 200);
                }
                else
                {
                    result.SetInfo(null, "入库单据信息获取失败", -102);
                }
            }


            //出库单据
            var resOut = await _outService.GetWmsOutStorageMaterialAllListAsync(query);
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
                    result.SetInfo(echartsLine, "成功", 200);
                }
                else
                {
                    result.SetInfo(null, "出库单据信息获取失败", -102);
                }
            }

            result.ExpandSeconds = (DateTime.Now - dt).TotalSeconds;
            return result;
        }
    }
}
