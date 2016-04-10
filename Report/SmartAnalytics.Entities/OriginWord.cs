using System;
using System.ComponentModel.DataAnnotations;

namespace SmartAnalytics.Entities
{
    /// <summary>
    /// 来源分析-搜索词
    /// </summary>
    public class OriginWord
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
        /// 关键词
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string WordText { get; set; }

        /// <summary>
        /// 百度搜索次数
        /// </summary>
        [Required]
        public int BaiDuTotalCount { get; set; }

        /// <summary>
        /// 好搜搜索次数
        /// </summary>
        [Required]
        public int HaoSouTotalCount { get; set; }

        /// <summary>
        /// 搜狗搜索次数
        /// </summary>
        [Required]
        public int SouGouTotalCount { get; set; }

        /// <summary>
        /// 谷歌搜索次数
        /// </summary>
        [Required]
        public int GoogleTotalCount { get; set; }
    }
}
