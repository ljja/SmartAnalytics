namespace SmartAnalytics.Services.Models
{
    public class VisitorLoyaltyByDayPageItem
    {
        /// <summary>
        /// 日访问频度
        /// </summary>
        public int FrequencyCount { get; set; }

        /// <summary>
        /// 独立访客UV
        /// </summary>
        public int UniqueUser { get; set; }

        /// <summary>
        /// 独立访客UV比率
        /// </summary>
        public float UniqueUserRate { get; set; }

        /// <summary>
        /// 浏览次数PV
        /// </summary>
        public int PageView { get; set; }

        /// <summary>
        /// 浏览次数PV比率
        /// </summary>
        public float PageViewRate { get; set; }
    }
}
