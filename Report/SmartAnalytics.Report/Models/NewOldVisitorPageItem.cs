using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAnalytics.Report.Models
{
    public class NewOldVisitorPageItem
    {
        /// <summary>
        /// 新访客总数
        /// </summary>
        public int NewVisitorCount { get; set; }

        /// <summary>
        /// 老访客总数
        /// </summary>
        public int OldVisitorCount { get; set; }
    }
}
