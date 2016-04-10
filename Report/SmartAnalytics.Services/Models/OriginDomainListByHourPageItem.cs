namespace SmartAnalytics.Services.Models
{
    public class OriginDomainListByHourPageItem : OriginDomainListByDatePageItem
    {
        /// <summary>
        /// 统计小时
        /// </summary>
        public int TotalHour { get; set; }
    }
}
