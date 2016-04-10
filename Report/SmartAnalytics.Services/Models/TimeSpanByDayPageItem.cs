using System;

namespace SmartAnalytics.Services.Models
{
    public class TimeSpanByDayPageItem
    {
        /// <summary>
        /// 日期段
        /// </summary>
        public DateTime TotalDate { get; set; }

        /// <summary>
        /// 日期段格式化版本yyyy-MM-dd
        /// </summary>
        public string TotalDateString
        {
            get
            {
                return TotalDate.ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// 浏览次数
        /// </summary>
        public int PageView { get; set; }

        /// <summary>
        /// 独立访客UV
        /// </summary>
        public int UniqueUser { get; set; }

        /// <summary>
        /// 新独立访客
        /// </summary>
        public int NewUniqueUser { get; set; }

        /// <summary>
        /// 新独立访客比率
        /// </summary>
        public float NewUniqueUserRate { get; set; }

        /// <summary>
        /// 独立访客IP
        /// </summary>
        public int UniqueIp { get; set; }

        /// <summary>
        /// 访问次数
        /// </summary>
        public int AccessNumber { get; set; }

        /// <summary>
        /// 人均浏览页数
        /// </summary>
        public float UserViewPageAverage { get; set; }

        /// <summary>
        /// 平均访问深度
        /// </summary>
        public float ViewPageDeptAverage { get; set; }

        /// <summary>
        /// 平均访问时长
        /// </summary>
        public int ViewPageTimeSpanAverage { get; set; }

        /// <summary>
        /// 跳出率
        /// </summary>
        public float BounceRate { get; set; }

        /// <summary>
        /// 是否空数据项
        /// </summary>
        public bool IsEmpty { get; set; }


        public TimeSpanByDayPageItem()
        {
            PageView = 0;
            UniqueUser = 0;
            NewUniqueUser = 0;
            NewUniqueUserRate = 0f;
            UniqueIp = 0;
            AccessNumber = 0;
            UserViewPageAverage = 0f;
            ViewPageDeptAverage = 0f;
            ViewPageTimeSpanAverage = 0;
            BounceRate = 0f;
            IsEmpty = false;
        }
    }
}