namespace SmartAnalytics.Services.Models
{
    public class CategoryListByHourPageItem : CategoryListByDatePageItem
    {
        /// <summary>
        /// 统计小时
        /// </summary>
        public int TotalHour { get; set; }
    }
}
