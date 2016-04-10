namespace SmartAnalytics.Services.Models
{
    public class OriginPageDomainPageItem
    {
        /// <summary>
        /// 网站域名
        /// </summary>
        public string OriginDomain { get; set; }

        /// <summary>
        /// 来源次数
        /// </summary>
        public int TotalCount { get; set; }
    }
}
