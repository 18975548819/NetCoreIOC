using System;
using System.Collections.Generic;
using System.Text;

namespace DataModel.WMS
{
    public class DvActiveRingChartModel
    {
        public List<ActiveRingChartData> data { get; set; }
    }

    public class ActiveRingChartData
    {
        public string name { get; set; }
        public int value { get; set; }
    }
}
