using System;
using System.ComponentModel.DataAnnotations;

namespace SmartAnalytics.Entities
{
    /// <summary>
    /// 访客分析-访客忠诚度-日访问频度
    /// </summary>
    public class VisitorLoyalty
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 网站域名
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string SiteDomain { get; set; }

        /// <summary>
        /// 统计日期
        /// </summary>
        [Required]
        public DateTime TotalDate { get; set; }

        /// <summary>
        /// 日访问频度
        /// </summary>
        [Required]
        public int FrequencyCount { get; set; }

        /// <summary>
        /// 独立访客UV
        /// </summary>
        [Required]
        public int UniqueUser { get; set; }

        /// <summary>
        /// 浏览次数PV
        /// </summary>
        [Required]
        public int PageView { get; set; }

    }
}
