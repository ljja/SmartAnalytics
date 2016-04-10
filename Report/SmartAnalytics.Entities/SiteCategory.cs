using System;
using System.ComponentModel.DataAnnotations;

namespace SmartAnalytics.Entities
{
    /// <summary>
    /// 网站分类表
    /// </summary>
    public class SiteCategory
    {
        /// <summary>
        /// 网站域名
        /// </summary>
        [Key]
        [StringLength(50)]
        public string Domain { get; set; }

        /// <summary>
        /// 网站名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string SiteName { get; set; }

        /// <summary>
        /// 网站描述
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Summary { get; set; }

        /// <summary>
        /// PR值
        /// </summary>
        [Required]
        public int Pr { get; set; }

        /// <summary>
        /// 点击数量
        /// </summary>
        [Required]
        public int Click { get; set; }

        /// <summary>
        /// 行业代码
        /// </summary>
        [Required]
        [StringLength(12)]
        public string IndustryCode { get; set; }

        /// <summary>
        /// 行政区划代码
        /// </summary>
        [Required]
        [StringLength(6)]
        public string CityAreaCode { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; }

    }
}
