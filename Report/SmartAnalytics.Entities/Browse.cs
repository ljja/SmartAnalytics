using System.ComponentModel.DataAnnotations;

namespace SmartAnalytics.Entities
{
    /// <summary>
    /// 浏览器表
    /// </summary>
    public class Browse
    {
        /// <summary>
        /// 主键自增
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 浏览器友好名称
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string BrowseName { get; set; }

        /// <summary>
        /// 浏览器UserAgent
        /// </summary>
        [Required]
        [MaxLength(1024)]
        public string UserAgent { get; set; }

        /// <summary>
        /// 浏览器UserAgent md5哈希
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string UserAgentHash { get; set; }
    }
}