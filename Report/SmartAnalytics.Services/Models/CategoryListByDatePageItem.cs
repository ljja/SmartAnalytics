namespace SmartAnalytics.Services.Models
{
    public class CategoryListByDatePageItem
    {
        /// <summary>
        /// 来访次数
        /// </summary>
        public int TotalNumber { get; set; }

        /// <summary>
        /// 行业代码
        /// </summary>
        public string IndustryCode { get; set; }

        /// <summary>
        /// 行业代码名称
        /// </summary>
        public string IndustryCodeName { get; set; }

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
