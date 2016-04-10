using System;
using System.Collections.Generic;
using System.Linq;
using SmartAnalytics.Services.Models;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Services.Impl
{
    public class FlowService : ServiceContext, IFlowService
    {
        public List<TimeSpanByHourPageItem> GetTimeSpanByHourList(string domain, DateTime day)
        {
            var query = from m in DbContext.FlowVolumeByHour
                        where m.SiteDomain == domain && m.TotalDate == day
                        select new TimeSpanByHourPageItem
                        {
                            TotalHour = m.TotalHour,
                            PageView = m.PageView,
                            UniqueUser = m.UniqueUser,
                            NewUniqueUser = m.NewUniqueUser,
                            NewUniqueUserRate = m.NewUniqueUserRate,
                            UniqueIp = m.UniqueIp,
                            AccessNumber = m.AccessNumber,
                            UserViewPageAverage = m.UserViewPageAverage,
                            ViewPageDeptAverage = m.ViewPageDeptAverage,
                            ViewPageTimeSpanAverage = m.ViewPageTimeSpanAverage,
                            BounceRate = m.BounceRate,
                            IsEmpty = false
                        };

            var pageResult = query.ToList();

            //补齐24个小时段空数据
            for (var i = 0; i <= 23; i++)
            {
                if (pageResult.Count(p => p.TotalHour == i) == 0)
                {
                    pageResult.Add(new TimeSpanByHourPageItem
                    {
                        TotalHour = i,
                        IsEmpty = true
                    });
                }
            }


            return pageResult.OrderBy(p => p.TotalHour).ToList();
        }

        public List<TimeSpanByDayPageItem> GetTimeSpanByDayList(string domain, DateTime beginDate, DateTime endDate)
        {
            var query = from m in DbContext.FlowVolumeByDay
                        where m.SiteDomain == domain && m.TotalDate >= beginDate && m.TotalDate <= endDate
                        select new TimeSpanByDayPageItem
                        {
                            PageView = m.PageView,
                            UniqueUser = m.UniqueUser,
                            NewUniqueUser = m.NewUniqueUser,
                            NewUniqueUserRate = m.NewUniqueUserRate,
                            UniqueIp = m.UniqueIp,
                            AccessNumber = m.AccessNumber,
                            UserViewPageAverage = m.UserViewPageAverage,
                            ViewPageDeptAverage = m.ViewPageDeptAverage,
                            ViewPageTimeSpanAverage = m.ViewPageTimeSpanAverage,
                            BounceRate = m.BounceRate,
                            TotalDate = m.TotalDate,
                            IsEmpty = false
                        };

            var pageResult = query.ToList();

            var dayCount = (endDate - beginDate).Days;
            for (var i = 1; i <= dayCount; i++)
            {
                var stepDay = beginDate.AddDays(i);
                if (pageResult.Count(p => p.TotalDate == stepDay) == 0)
                {
                    pageResult.Add(new TimeSpanByDayPageItem { TotalDate = stepDay, IsEmpty = true });
                }
            }

            return pageResult.OrderBy(p => p.TotalDate).ToList();
        }

        public List<PredictTimeSpanByHourPageItem> GetPredictTimeSpanByHourList(string domain, DateTime day)
        {
            var query = from m in DbContext.PredictFlowVolumeByHour
                        where m.SiteDomain == domain && m.TotalDate == day
                        select new PredictTimeSpanByHourPageItem
                        {
                            TotalHour = m.TotalHour,
                            PageView = m.PageView,
                            UniqueUser = m.UniqueUser,
                            NewUniqueUser = m.NewUniqueUser,
                            NewUniqueUserRate = m.NewUniqueUserRate,
                            UniqueIp = m.UniqueIp,
                            AccessNumber = m.AccessNumber,
                            UserViewPageAverage = m.UserViewPageAverage,
                            ViewPageDeptAverage = m.ViewPageDeptAverage,
                            ViewPageTimeSpanAverage = m.ViewPageTimeSpanAverage,
                            BounceRate = m.BounceRate,
                            IsEmpty = false
                        };

            var pageResult = query.ToList();

            //补齐24个小时段空数据
            for (var i = 0; i <= 23; i++)
            {
                if (pageResult.Count(p => p.TotalHour == i) == 0)
                {
                    pageResult.Add(new PredictTimeSpanByHourPageItem
                    {
                        TotalHour = i,
                        IsEmpty = true
                    });
                }
            }

            return pageResult.OrderBy(p => p.TotalHour).ToList();
        }

        public List<TimeSpanByMinutePageItem> GetTimeSpanByMinuteList(string domain, int leftMinute)
        {
            var date = DateTime.Now.AddMinutes(-leftMinute);

            var query = from m in DbContext.FlowVolumeByMinute
                        where m.SiteDomain == domain && m.TotalDate >= date
                        orderby m.TotalDate ascending 
                        select new TimeSpanByMinutePageItem
                        {
                            TotalHour = m.TotalHour,
                            TotalMinute = m.TotalMinute,
                            PageView = m.PageView,
                            UniqueUser = m.UniqueUser,
                            NewUniqueUser = m.NewUniqueUser,
                            NewUniqueUserRate = m.NewUniqueUserRate,
                            UniqueIp = m.UniqueIp,
                            AccessNumber = m.AccessNumber,
                            UserViewPageAverage = m.UserViewPageAverage,
                            ViewPageDeptAverage = m.ViewPageDeptAverage,
                            ViewPageTimeSpanAverage = m.ViewPageTimeSpanAverage,
                            BounceRate = m.BounceRate,
                            IsEmpty = false
                        };

            return query.ToList();
        }
    }
}