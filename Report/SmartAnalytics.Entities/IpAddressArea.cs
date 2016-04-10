using System.ComponentModel.DataAnnotations;

namespace SmartAnalytics.Entities
{
    /// <summary>
    /// IP区域表
    /// </summary>
    public class IpAddressArea
    {
        /// <summary>
        /// Ip地址
        /// </summary>
        [Key]
        [MaxLength(15)]
        public string Ip { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        [MaxLength(50)]
        public string Country { get; set; }

        /// <summary>
        /// 国家代码
        /// </summary>
        [MaxLength(50)]
        public string CountryCode { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [MaxLength(50)]
        public string Area { get; set; }

        /// <summary>
        /// 区域代码
        /// </summary>
        [MaxLength(50)]
        public string AreaCode { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [MaxLength(50)]
        public string Region { get; set; }

        /// <summary>
        /// 省份代码
        /// </summary>
        [MaxLength(50)]
        public string RegionCode { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [MaxLength(50)]
        public string City { get; set; }

        /// <summary>
        /// 城市代码
        /// </summary>
        [MaxLength(50)]
        public string CityCode { get; set; }

        /// <summary>
        /// 乡镇
        /// </summary>
        [MaxLength(50)]
        public string County { get; set; }

        /// <summary>
        /// 乡镇代码
        /// </summary>
        [MaxLength(50)]
        public string CountyCode { get; set; }

        /// <summary>
        /// 运营商
        /// </summary>
        [MaxLength(50)]
        public string Isp { get; set; }

        /// <summary>
        /// 运营商代码
        /// </summary>
        [MaxLength(50)]
        public string IspCode { get; set; }
    }
}