using System;
using System.Collections.Generic;
using System.Linq;
using EntityFramework.Extensions;
using log4net;
using SmartAnalytics.Cache;
using SmartAnalytics.Entities;
using SmartAnalytics.Services.Models;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Services.Impl
{
    public class VisitorRegionByDayTaskService : ServiceContext, ITaskService
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(VisitorRegionByDayTaskService));

        public string Command
        {
            get
            {
                return "VisitorRegion";
            }
        }

        public string Name
        {
            get
            {
                return "访客分析-地域分布(每日执行一次)";
            }
        }

        public VisitorRegionByDayTaskService()
        {
            SetCacheInstance(new RedisContext());
        }

        public void Exec(string[] args)
        {
            if (args == null || args.Length == 0 || string.IsNullOrEmpty(args[0]))
            {
                var today = DateTime.Parse(DateTime.Now.AddDays(-1).ToString("yy-MM-dd"));

                SyncRedisCache(today);

                return;
            }

            try
            {
                var leftDayCount = int.Parse(args[0]);

                for (var i = 0; i <= leftDayCount; i++)
                {
                    var day = DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd");

                    SyncRedisCache(DateTime.Parse(day));
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }

        private void SyncRedisCache(DateTime date)
        {
            var siteDomains = DbContext.Domain.Select(p => p.SiteDomain).ToList();

            foreach (var domain in siteDomains)
            {
                var redisKey = string.Format("visitor:region:{0}:{1}", date.ToString("yyyyMMdd"), domain);
                var redisBody = CacheContext.Get<List<VisitorRegionByDayCacheItem>>(redisKey);
                if (redisBody != null)
                {
                    foreach (var m in redisBody)
                    {
                        var lockDomain = domain;
                        var query = from p in DbContext.VisitorRegion
                                    where p.SiteDomain == lockDomain && p.TotalDate == date && p.UserIpAddress == m.UserIpAddress
                                    select p.Id;

                        var queryCount = query.Count();
                        if (queryCount == 0)
                        {
                            //Insert
                            DbContext.VisitorRegion.Add(new VisitorRegion
                            {
                                SiteDomain = m.SiteDomain,
                                TotalDate = m.TotalDate,
                                UserIpAddress = m.UserIpAddress,
                                PageView = m.PageView,
                                UniqueUser = m.UniqueUser,
                                NewUniqueUser = m.NewUniqueUser,
                                NewUniqueUserRate = m.NewUniqueUserRate,
                                UniqueIp = m.UniqueIp,
                                AccessNumber = m.AccessNumber,
                                UserViewPageAverage = m.UserViewPageAverage,
                                ViewPageDeptAverage = m.ViewPageDeptAverage,
                                ViewPageTimeSpanAverage = m.ViewPageTimeSpanAverage,
                                BounceRate = m.BounceRate
                            });

                            DbContext.SaveChanges();
                        }
                        else
                        {
                            //update
                            var m1 = m;
                            var m2 = m;
                            DbContext.VisitorRegion.Update(p => m2.UserIpAddress != null && (p.SiteDomain == lockDomain && p.TotalDate == date && p.UserIpAddress == m2.UserIpAddress), q => new VisitorRegion
                            {
                                PageView = m1.PageView,
                                UniqueUser = m1.UniqueUser,
                                NewUniqueUser = m1.NewUniqueUser,
                                NewUniqueUserRate = m1.NewUniqueUserRate,
                                UniqueIp = m1.UniqueIp,
                                AccessNumber = m1.AccessNumber,
                                UserViewPageAverage = m1.UserViewPageAverage,
                                ViewPageDeptAverage = m1.ViewPageDeptAverage,
                                ViewPageTimeSpanAverage = m1.ViewPageTimeSpanAverage,
                                BounceRate = m1.BounceRate
                            });
                        }
                    }
                }
            }
        }
    }
}
