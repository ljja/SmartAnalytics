using System.ComponentModel.DataAnnotations;

namespace SmartAnalytics.Entities
{
    /// <summary>
    /// 行业代码
    /// </summary>
    public class IndustryCode
    {
        /// <summary>
        /// 行业代码8902020299
        /// </summary>
        [Key]
        [StringLength(12)]
        public string CategoryCode { get; set; }

        /// <summary>
        /// 行业代码名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string CategoryName { get; set; }

        /// <summary>
        /// 父级行业代码
        /// </summary>
        [StringLength(12)]
        public string ParentCode { get; set; }
    }
}
