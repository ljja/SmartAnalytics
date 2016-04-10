using System.Collections.Generic;

namespace SmartAnalytics.Report.Models
{
    public class OriginDomainListByHourEChartsPageItem
    {
        /// <summary>
        /// 行业代码
        /// </summary>
        public string OriginDomain { get; set; }

        /// <summary>
        /// 时段数据列表
        /// </summary>
        public List<int> Data { get; set; }

        public OriginDomainListByHourEChartsPageItem()
        {
            Data = new List<int>();
        }
    }
}