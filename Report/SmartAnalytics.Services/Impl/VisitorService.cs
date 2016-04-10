using System;
using System.Collections.Generic;
using System.Linq;
using SmartAnalytics.Services.Models;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Services.Impl
{
    public class VisitorService : ServiceContext, IVisitorService
    {
        public List<RegionListByDayPageItem> GetRegionListByDay(string domain, DateTime date)
        {
            var query = from p in DbContext.VisitorRegion
                        from ipInfo in DbContext.IpAddressArea

                        where p.SiteDomain == domain && p.TotalDate == date && p.UserIpAddress == ipInfo.Ip

                        group p by ipInfo.Region into tempGroup
                        orderby tempGroup.Count() descending

                        select new RegionListByDayPageItem
                        {
                            Region = tempGroup.Key,
                            PageView = tempGroup.Sum(p => p.PageView),
                            UniqueUser = tempGroup.Sum(p => p.UniqueUser),

                            NewUniqueUser = tempGroup.Sum(p => p.NewUniqueUser),
                            NewUniqueUserRate = tempGroup.Average(p => p.NewUniqueUserRate),
                            UniqueIp = tempGroup.Sum(p => p.UniqueIp),

                            AccessNumber = tempGroup.Sum(p => p.AccessNumber),
                            UserViewPageAverage = tempGroup.Average(p => p.UserViewPageAverage),
                            ViewPageDeptAverage = tempGroup.Average(p => p.ViewPageDeptAverage),
                            ViewPageTimeSpanAverage = (int)tempGroup.Average(p => p.ViewPageTimeSpanAverage),
                            BounceRate = tempGroup.Average(p => p.BounceRate),
                        };


            return query.ToList();
        }

        public List<VisitorActiveByDayPageItem> GetVisitorActiveListByDay(string domain, DateTime date)
        {
            var query = from p in DbContext.VisitorActive
                        where p.SiteDomain == domain && p.TotalDate == date
                        group p by p.Depth into tempGroup
                        orderby tempGroup.Key ascending
                        select new VisitorActiveByDayPageItem
                        {
                            Depth = tempGroup.Key,
                            DepthCount = tempGroup.Sum(p => p.DepthCount)
                        };

            //返回前100条
            var queryResult = query.Take(100).ToList();

            var sum = queryResult.Sum(p => p.DepthCount);

            foreach (var m in queryResult)
            {
                // ReSharper disable once PossibleLossOfFraction
                m.Percent = m.DepthCount / (float)sum;
            }

            return queryResult;
        }

        public List<VisitorLoyaltyByDayPageItem> GetVisitorLoyaltyListByDay(string domain, DateTime date)
        {
            var query = from p in DbContext.VisitorLoyalty
                        where p.SiteDomain == domain && p.TotalDate == date
                        group p by p.FrequencyCount into tempGroup
                        orderby tempGroup.Key ascending
                        select new VisitorLoyaltyByDayPageItem
                        {
                            FrequencyCount = tempGroup.Key,
                            UniqueUser = tempGroup.Sum(p => p.UniqueUser),
                            PageView = tempGroup.Sum(p => p.PageView)
                        };

            var queryResult = query.Take(100).ToList();

            var sumUniqueUser = queryResult.Sum(p => p.UniqueUser);
            var sumPageView = queryResult.Sum(p => p.PageView);

            foreach (var m in queryResult)
            {
                m.UniqueUserRate = m.UniqueUser / (float)sumUniqueUser;
                m.PageViewRate = m.PageView / (float)sumPageView;
            }

            return queryResult;
        }

        public List<VisitorNewOldByDayPageItem> GetVisitorNewOldListByDay(string domain, DateTime date)
        {
            var query = from p in DbContext.VisitorNewOld
                        where p.SiteDomain == domain && p.TotalDate == date
                        select new VisitorNewOldByDayPageItem
                        {
                            IsNewVisitor = p.IsNewVisitor,
                            PageView = p.PageView,
                            UniqueUser = p.UniqueUser,
                            UniqueIp = p.UniqueIp,
                            AccessNumber = p.AccessNumber,
                            UserViewPageAverage = p.UserViewPageAverage,
                            ViewPageDeptAverage = p.ViewPageDeptAverage,
                            ViewPageTimeSpanAverage = p.ViewPageTimeSpanAverage,
                            BounceRate = p.BounceRate
                        };

            return query.ToList();
        }

        public List<VisitorResolutionByDayPageItem> GetVisitorResolutionListByDay(string domain, DateTime date)
        {
            var query = from p in DbContext.VisitorResolution
                        where p.SiteDomain == domain && p.TotalDate == date
                        orderby p.PageView descending
                        select new VisitorResolutionByDayPageItem
                        {
                            Resolution = p.Resolution,
                            PageView = p.PageView,
                            UniqueUser = p.UniqueUser,
                            NewUniqueUser = p.NewUniqueUser,
                            NewUniqueUserRate = p.NewUniqueUserRate,
                            UniqueIp = p.UniqueIp,
                            AccessNumber = p.AccessNumber,
                            UserViewPageAverage = p.UserViewPageAverage,
                            ViewPageDeptAverage = p.ViewPageDeptAverage,
                            ViewPageTimeSpanAverage = p.ViewPageTimeSpanAverage,
                            BounceRate = p.BounceRate
                        };

            return query.ToList();
        }
    }
}
