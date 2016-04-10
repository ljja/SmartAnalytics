using System;
using System.Collections.Generic;
using SmartAnalytics.Services.Models;

namespace SmartAnalytics.Services
{
    /// <summary>
    /// 访客分析
    /// </summary>
    public interface IVisitorService
    {
        /// <summary>
        /// 地域分布
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        List<RegionListByDayPageItem> GetRegionListByDay(string domain, DateTime date);

        /// <summary>
        /// 活跃度
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        List<VisitorActiveByDayPageItem> GetVisitorActiveListByDay(string domain, DateTime date);

        /// <summary>
        /// 忠诚度
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        List<VisitorLoyaltyByDayPageItem> GetVisitorLoyaltyListByDay(string domain, DateTime date);

        /// <summary>
        /// 新老访客
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        List<VisitorNewOldByDayPageItem> GetVisitorNewOldListByDay(string domain, DateTime date);


        /// <summary>
        /// 分辨率
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        List<VisitorResolutionByDayPageItem> GetVisitorResolutionListByDay(string domain, DateTime date);
    }
}
