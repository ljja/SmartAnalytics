namespace SmartAnalytics.Services.Models
{
    public class OriginWordPageItem
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
        /// 来访总次数
        /// </summary>
        public int TotalCount
        {
            get { return BaiDuTotalCount + HaoSouTotalCount + SouGouTotalCount + GoogleTotalCount; }
        }

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
