using System;
using System.Linq;
using EntityFramework.Extensions;
using SmartAnalytics.Cache;
using SmartAnalytics.Entities;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Services.Impl
{
    public class TimeSpanByHourTaskService : ServiceContext, ITaskService
    {
        public string Command
        {
            get
            {
                return "FlowHour";
            }
        }

        public string Name
        {
            get { return "流量统计-按小时统计(每小时执行一次)"; }
        }

        public TimeSpanByHourTaskService()
        {
            SetCacheInstance(new RedisContext());
        }

        public void Exec(string[] args)
        {
            if (args == null || args.Length == 0 || string.IsNullOrEmpty(args[0]))
            {
                var lastHour = DateTime.Now.AddHours(-1);

                SyncRedisCache(lastHour);

                return;
            }

            try
            {
                var hourCount = int.Parse(args[0]);

                for (var i = 0; i <= hourCount; i++)
                {
                    var hour = DateTime.Now.AddHours(-i);

                    SyncRedisCache(hour);
                }
            }
            catch { }
        }

        private void SyncRedisCache(DateTime dateTime)
        {
            var day = DateTime.Parse(dateTime.ToString("yyyy-MM-dd"));
            var hour = dateTime.Hour;

            var siteDomains = DbContext.Domain.Select(p => p.SiteDomain).ToList();
            foreach (var domain in siteDomains)
            {
                var redisKey = string.Format("flow:hour:{0}:{1}:{2}", dateTime.ToString("yyyyMMdd"), dateTime.Hour, domain);
                var redisBody = CacheContext.Get<FlowVolumeByHour>(redisKey);

                if (redisBody != null)
                {
                    var lockDomain = domain;
                    var query = from p in DbContext.FlowVolumeByHour
                                where p.SiteDomain == lockDomain && p.TotalDate == day && p.TotalHour == hour
                                select p.Id;

                    var queryCount = query.Count();
                    if (queryCount == 0)
                    {
                        //Insert
                        DbContext.FlowVolumeByHour.Add(new FlowVolumeByHour
                        {
                            SiteDomain = domain,
                            TotalDate = day,
                            TotalHour = hour,
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
                        DbContext.FlowVolumeByHour.Update(
                            p => p.SiteDomain == lockDomain && p.TotalDate == day && p.TotalHour == hour,
                            m => new FlowVolumeByHour
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
