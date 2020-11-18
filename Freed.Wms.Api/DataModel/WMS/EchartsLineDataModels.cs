using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.WMS
{
    /// <summary>
    /// Echarts图表折线图数据对象
    /// </summary>
    public class EchartsLineDataModels
    {
        public string echartsName { get; set; }
        public List<string> xAxisData { get; set; }
        public List<int> yAxisData1 { get; set; }
        public List<int> yAxisData2 { get; set; }
        /// <summary>
        ///多个yAxisData数据
        /// </summary>
        public List<Dictionary<string, List<int>>> yAxisDataMultiple { get; set; }
    }
}
