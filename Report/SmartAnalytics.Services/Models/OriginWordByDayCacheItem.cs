using System;

namespace SmartAnalytics.Services.Models
{
    public class OriginWordByDayCacheItem
    {
        /// <summary>
        /// 网站域名
        /// </summary>
        public string SiteDomain { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string WordText { get; set; }

        /// <summary>
        /// 统计时间
        /// </summary>
        public DateTime TotalDate { get; set; }

        /// <summary>
        /// 百度搜索次数
        /// </summary>
        public int BaiDuTotalCount { get; set; }

        /// <summary>
        /// 好搜搜索次数
        /// </summary>
        public int HaoSouTotalCount { get; set; }

        /// <summary>
        /// 搜狗搜索次数
        /// </summary>
        public int SouGouTotalCount { get; set; }

        /// <summary>
        /// 谷歌搜索次数
        /// </summary>
        public int GoogleTotalCount { get; set; }

    }
}
