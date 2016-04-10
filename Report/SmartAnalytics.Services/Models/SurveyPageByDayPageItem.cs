using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAnalytics.Services.Models
{
    /// <summary>
    /// 受访页面
    /// </summary>
   public  class SurveyPageByDayPageItem
    {
        /// <summary>
        /// 受访页面
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int PageView { get; set; }

        /// <summary>
        /// 独立访客UV
        /// </summary>
        public int UniqueUser { get; set; }

        /// <summary>
        /// 独立访客IP
        /// </summary>
        public int UniqueIp { get; set; }
    }
}
