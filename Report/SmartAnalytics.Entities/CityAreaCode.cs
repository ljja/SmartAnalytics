using System.ComponentModel.DataAnnotations;

namespace SmartAnalytics.Entities
{
    /// <summary>
    /// 县及县以上行政区划代码(截止2014年10月31日)
    /// </summary>
    public class CityAreaCode
    {
        /// <summary>
        /// 行政区划代码
        /// </summary>
        [Key]
        [StringLength(6)]
        public string AreaCode { get; set; }

        /// <summary>
        /// 行政区官方名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string AreaName { get; set; }
    }
}
