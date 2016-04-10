using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAnalytics.Services.Models
{
    public class SurveyDomainByHourPageItem
    {
        /// <summary>
        /// 网站域名
        /// </summary>
        public string SiteDomain { get; set; }

        /// <summary>
        /// 统计时段
        /// </summary>
        public int TotalHour { get; set; }

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

        /// <summary>
        /// 是否空数据项
        /// </summary>
        public bool IsEmpty { get; set; }

        public SurveyDomainByHourPageItem()
        {
            SiteDomain = String.Empty;
            TotalHour = 0;
            PageView = 0;
            UniqueUser = 0;
            UniqueIp = 0;
            IsEmpty = false;
        }

    }
}
