using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAnalytics.Report.Areas.Survey.Models
{
    public class HeatMapRenderImageRequest
    {
        [Required]
        [MaxLength(10)]
        public string BeginTime { get; set; }

        [Required]
        [MaxLength(10)]
        public string EndTime { get; set; }

        [Required]
        [MaxLength(4000)]
        public string Url { get; set; }

        [Required]
        [Range(640, int.MaxValue)]
        public int ScreenWidth { get; set; }

        [Required]
        [Range(480, int.MaxValue)]
        public int ScreenHeight { get; set; }
    }
}
