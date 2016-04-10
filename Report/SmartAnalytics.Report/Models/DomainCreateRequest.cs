using System.ComponentModel.DataAnnotations;

namespace SmartAnalytics.Report.Models
{
    public class DomainCreateRequest
    {
        [Required]
        [MaxLength(50)]
        [Display(Name = "网站域名")]
        public string SiteDomain { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "网站别名")]
        public string DomainAlias { get; set; }
    }
}