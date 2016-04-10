namespace SmartAnalytics.Report.Areas.Flow.Models
{
    /// <summary>
    /// 关键指标
    /// </summary>
    public class PrimaryIndicators
    {
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
        public int UserViewPageAverage { get; set; }

        /// <summary>
        /// 平均访问深度
        /// </summary>
        public int ViewPageDeptAverage { get; set; }

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