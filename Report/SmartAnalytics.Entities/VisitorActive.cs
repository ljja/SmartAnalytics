using System;
using System.ComponentModel.DataAnnotations;

namespace SmartAnalytics.Entities
{
    /// <summary>
    /// 访客分析-访客忠诚度-访问深度分布
    /// </summary>
    public class VisitorActive
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
        /// 访问深度
        /// </summary>
        [Required]
        public int Depth { get; set; }
        
        /// <summary>
        /// 访问次数
        /// </summary>
        [Required]
        public int DepthCount { get; set; }

    }
}
