using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAnalytics.Services.Models
{
    public class SurveyDomainByHourCacheItem
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
        /// 统计时段
        /// </summary>
        public int TotalHour { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int PageView { get; set; }

        /// <summary>
        /// 独立访客UV
        /// </summary>
        public int UniqueUser { get; set; }

        /// <summary>
        /// 新独立访客
        /// </summary>
        public int NewUniqueUser { get; set; }

        /// <summary>
        /// 新独立访客比率
        /// </summary>
        public float NewUniqueUserRate { get; set; }

        /// <summary>
        /// 独立访客IP
        /// </summary>
        public int UniqueIp { get; set; }

        /// <summary>
        /// 访问次数
        /// </summary>
        public int AccessNumber { get; set; }

        /// <summary>
        /// 人均浏览页数
        /// </summary>
        public float UserViewPageAverage { get; set; }

        /// <summary>
        /// 平均访问深度
        /// </summary>
        public float ViewPageDeptAverage { get; set; }

        /// <summary>
        /// 平均访问时长
        /// </summary>
        public int ViewPageTimeSpanAverage { get; set; }

        /// <summary>
        /// 跳出率
        /// </summary>
        public float BounceRate { get; set; }
    }
}
