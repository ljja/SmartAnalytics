using System.ComponentModel.DataAnnotations;

namespace SmartAnalytics.Report.Models
{
    public class DomainEditRequest : DomainCreateRequest
    {
        [Required]
        [Display(Name = "网站编号")]
        public int Id { get; set; }
    }
}