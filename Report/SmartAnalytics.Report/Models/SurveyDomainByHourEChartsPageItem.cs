using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartAnalytics.Report.Models
{
    public class SurveyDomainByHourEChartsPageItem
    {
        /// <summary>
        /// 受访域名
        /// </summary>
        public string SiteDomain { get; set; }

        /// <summary>
        /// 时段数据列表
        /// </summary>
        public List<int> Data { get; set; }

        public SurveyDomainByHourEChartsPageItem()
        {
            Data = new List<int>();
        }
    }
}