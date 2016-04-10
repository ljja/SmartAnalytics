using System;
using System.Collections.Generic;

namespace SmartAnalytics.Services.Models
{
    public class OriginPageByHourCacheItem
    {
        public string SiteDomain { get; set; }

        public DateTime TotalDate { get; set; }

        public int TotalHour { get; set; }

        public List<OriginPageByHourCacheItemData> Items { get; set; }

        public OriginPageByHourCacheItem()
        {
            Items = new List<OriginPageByHourCacheItemData>();
        }

    }

    public class OriginPageByHourCacheItemData
    {
        public string OriginUrl { get; set; }

        public string OriginDomain { get; set; }

        public int Count { get; set; }
    }
}
