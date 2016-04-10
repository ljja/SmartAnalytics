namespace SmartAnalytics.Services.Models
{
    /// <summary>
    /// 访客分析-访客忠诚度-访问深度分布
    /// </summary>
    public class VisitorActiveByDayPageItem
    {
        /// <summary>
        /// 访问深度
        /// </summary>
        public int Depth { get; set; }

        /// <summary>
        /// 访问深度次数
        /// </summary>
        public int DepthCount { get; set; }

        /// <summary>
        /// 百分比
        /// </summary>
        public float Percent { get; set; }
    }
}
