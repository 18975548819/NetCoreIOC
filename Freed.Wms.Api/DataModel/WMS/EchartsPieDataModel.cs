using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.WMS
{
    /// <summary>
    /// Ecahrts饼状图
    /// </summary>
    public class EchartsPieDataModel
    {
        public List<string> legendData { get; set; }
        public List<SeriesData> seriesDatas { get; set; }

    }

    public class SeriesData
    {
        public string name { get; set; }
        public int value { get; set; }
    }
}
