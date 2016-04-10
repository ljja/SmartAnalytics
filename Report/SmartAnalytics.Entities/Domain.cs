using System.ComponentModel.DataAnnotations;

namespace SmartAnalytics.Entities
{
    /// <summary>
    ///  统计域名表
    /// </summary>
    public class Domain
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string SiteDomain { get; set; }

        [Required]
        [MaxLength(100)]
        public string DomainAlias { get; set; }
    }
}