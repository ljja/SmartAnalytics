using System;
using System.ComponentModel.DataAnnotations;

namespace SmartAnalytics.Entities
{
    /// <summary>
    /// 访客分析-系统环境-终端类型
    /// </summary>
    public class VisitorTerminal
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
        /// 终端类型
        /// </summary>
        [Required]
        [StringLength(50)]
        public string TerminalType { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        [Required]
        public int PageView { get; set; }

        /// <summary>
        /// 独立访客UV
        /// </summary>
        [Required]
        public int UniqueUser { get; set; }

        /// <summary>
        /// 新独立访客
        /// </summary>
        [Required]
        public int NewUniqueUser { get; set; }

        /// <summary>
        /// 新独立访客比率
        /// </summary>
        [Required]
        public float NewUniqueUserRate { get; set; }

        /// <summary>
        /// 独立访客IP
        /// </summary>
        [Required]
        public int UniqueIp { get; set; }

        /// <summary>
        /// 访问次数
        /// </summary>
        [Required]
        public int AccessNumber { get; set; }

        /// <summary>
        /// 人均浏览页数
        /// </summary>
        [Required]
        public float UserViewPageAverage { get; set; }

        /// <summary>
        /// 平均访问深度
        /// </summary>
        [Required]
        public float ViewPageDeptAverage { get; set; }

        /// <summary>
        /// 平均访问时长
        /// </summary>
        [Required]
        public int ViewPageTimeSpanAverage { get; set; }

        /// <summary>
        /// 跳出率
        /// </summary>
        [Required]
        public float BounceRate { get; set; }
    }
}
