using System;
using System.ComponentModel.DataAnnotations;

namespace SmartAnalytics.Entities
{
    /// <summary>
    /// 来源页面
    /// </summary>
    public class OriginPage
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
        /// 统计小时
        /// </summary>
        [Required]
        public int TotalHour { get; set; }

        /// <summary>
        /// 来源网址
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public string OriginUrl { get; set; }

        /// <summary>
        /// 网站域名
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string OriginDomain { get; set; }

        /// <summary>
        /// 来源次数
        /// </summary>
        [Required]
        public int TotalCount { get; set; }
    }
}
