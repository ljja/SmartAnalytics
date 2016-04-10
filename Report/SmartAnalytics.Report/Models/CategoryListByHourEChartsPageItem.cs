using System.Collections.Generic;

namespace SmartAnalytics.Report.Models
{
    public class CategoryListByHourEChartsPageItem
    {
        /// <summary>
        /// 行业代码
        /// </summary>
        public string IndustryCode { get; set; }

        /// <summary>
        /// 行业代码名称
        /// </summary>
        public string IndustryCodeName { get; set; }

        /// <summary>
        /// 时段数据列表
        /// </summary>
        public List<int> Data { get; set; }

        public CategoryListByHourEChartsPageItem()
        {
            Data = new List<int>();
        }
    }
}