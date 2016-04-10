using System;

namespace SmartAnalytics.Services.Models
{
    public class VisitorLoyaltyByDayCacheItem
    {
        /// <summary>
        /// 网站域名
        /// </summary>
        public string SiteDomain { get; set; }

        /// <summary>
        /// 统计日期
        /// </summary>
        public DateTime TotalDate { get; set; }

        /// <summary>
        /// 日访问频度
        /// </summary>
        public int FrequencyCount { get; set; }

        /// <summary>
        /// 独立访客UV
        /// </summary>
        public int UniqueUser { get; set; }

        /// <summary>
        /// 浏览次数PV
        /// </summary>
        public int PageView { get; set; }
    }
}
