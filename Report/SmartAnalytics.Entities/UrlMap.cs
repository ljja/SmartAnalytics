using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAnalytics.Entities
{
    /// <summary>
    /// 网址表
    /// </summary>
    public class UrlMap
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
        /// 网址名称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string UrlTitle { get; set; }

        /// <summary>
        /// 网址
        /// </summary>
        [StringLength(1024)]
        [Required]
        public string UrlAddress { get; set; }

        /// <summary>
        /// 添加日期
        /// </summary>
        [Required]
        public DateTime CreateTime { get; set; }

    }
}
