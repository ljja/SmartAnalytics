using System;

namespace SmartAnalytics.Services.Models
{
    public class VisitorActiveByDayCacheItem
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
        /// 访问深度
        /// </summary>
        public int Depth { get; set; }

        /// <summary>
        /// 访问次数
        /// </summary>
        public int DepthCount { get; set; }
    }
}
