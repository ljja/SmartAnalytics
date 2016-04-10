using System;
using System.Linq;
using EntityFramework.Extensions;
using SmartAnalytics.Cache;
using SmartAnalytics.Entities;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Services.Impl
{
    public class TimeSpanByDayTaskService : ServiceContext, ITaskService
    {
        public string Command
        {
            get
            {
                return "FlowDay";
            }
        }

        public string Name
        {
            get
            {
                return "流量统计-按日统计(每天执行一次)";
            }
        }

        public TimeSpanByDayTaskService()
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
                
            }
        }

        private void SyncRedisCache(DateTime date)
        {
            var siteDomains = DbContext.Domain.Select(p => p.SiteDomain).ToList();

            foreach (var domain in siteDomains)
            {
                var redisKey = string.Format("flow:day:{0}:{1}", date.ToString("yyyyMMdd"), domain);
                var redisBody = CacheContext.Get<FlowVolumeByDay>(redisKey);

                if (redisBody != null)
                {
                    var lockDomain = domain;
                    var query = from p in DbContext.FlowVolumeByDay
                                where p.SiteDomain == lockDomain && p.TotalDate == date
                                select p.Id;

                    var queryCount = query.Count();
                    if (queryCount == 0)
                    {
                        //Insert
                        DbContext.FlowVolumeByDay.Add(new FlowVolumeByDay
                        {
                            SiteDomain = domain,
                            TotalDate = date,
                            PageView = redisBody.PageView,
                            UniqueUser = redisBody.UniqueUser,
                            NewUniqueUser = redisBody.NewUniqueUser,
                            NewUniqueUserRate = redisBody.NewUniqueUserRate,
                            UniqueIp = redisBody.UniqueIp,
                            AccessNumber = redisBody.AccessNumber,
                            UserViewPageAverage = redisBody.UserViewPageAverage,
                            ViewPageDeptAverage = redisBody.ViewPageDeptAverage,
                            ViewPageTimeSpanAverage = redisBody.ViewPageTimeSpanAverage,
                            BounceRate = redisBody.BounceRate
                        });

                        DbContext.SaveChanges();
                    }
                    else
                    {
                        //update
                        DbContext.FlowVolumeByDay.Update(
                            p => p.SiteDomain == lockDomain && p.TotalDate == date,
                            m => new FlowVolumeByDay
                            {
                                PageView = redisBody.PageView,
                                UniqueUser = redisBody.UniqueUser,
                                NewUniqueUser = redisBody.NewUniqueUser,
                                NewUniqueUserRate = redisBody.NewUniqueUserRate,
                                UniqueIp = redisBody.UniqueIp,
                                AccessNumber = redisBody.AccessNumber,
                                UserViewPageAverage = redisBody.UserViewPageAverage,
                                ViewPageDeptAverage = redisBody.ViewPageDeptAverage,
                                ViewPageTimeSpanAverage = redisBody.ViewPageTimeSpanAverage,
                                BounceRate = redisBody.BounceRate
                            });
                    }
                }
            }
        }
    }
}
