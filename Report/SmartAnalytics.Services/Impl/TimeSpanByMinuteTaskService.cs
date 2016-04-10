using System;
using System.Linq;
using EntityFramework.Extensions;
using SmartAnalytics.Cache;
using SmartAnalytics.Entities;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Services.Impl
{
    public class TimeSpanByMinuteTaskService : ServiceContext, ITaskService
    {
        public string Command
        {
            get { return "FlowMinute"; }
        }

        public string Name
        {
            get { return "流量统计-按分钟统计(每分钟执行一次)"; }
        }


        public TimeSpanByMinuteTaskService()
        {
            SetCacheInstance(new RedisContext());
        }

        public void Exec(string[] args)
        {
            if (args == null || args.Length == 0 || string.IsNullOrEmpty(args[0]))
            {
                var today = DateTime.Parse(DateTime.Now.AddMinutes(-1).ToString("yyyy-MM-dd HH:mm"));
                SyncRedisCache(today);
                return;
            }

            try
            {
                var leftDayCount = int.Parse(args[0]);
                for (var i = 0; i <= leftDayCount; i++)
                {
                    var day = DateTime.Now.AddMinutes(-i).ToString("yyyy-MM-dd HH:mm");
                    SyncRedisCache(DateTime.Parse(day));
                }
            }
            catch (Exception e)
            {
            }
        }


        /// <summary>
        /// yyyy-mm-dd hh:mm
        /// </summary>
        /// <param name="date"></param>
        private void SyncRedisCache(DateTime date)
        {
            var siteDomains = DbContext.Domain.Select(p => p.SiteDomain).ToList();
            foreach (var domain in siteDomains)
            {
                var redisKey = string.Format("flow:rt:{0}:{1}:{2}:{3}", date.ToString("yyyyMMdd"), date.Hour.ToString("d2"), date.Minute.ToString("d2"), domain);
                var redisBody = CacheContext.Get<FlowVolumeByMinute>(redisKey);
                if (redisBody != null)
                {
                    var lockDomain = domain;
                    var query = from p in DbContext.FlowVolumeByMinute
                                where p.SiteDomain == lockDomain && p.TotalDate == date && p.TotalHour == date.Hour && p.TotalMinute == date.Minute
                                select p.Id;

                    var queryCount = query.Count();
                    if (queryCount == 0)
                    {
                        //Insert
                        DbContext.FlowVolumeByMinute.Add(new FlowVolumeByMinute
                        {
                            SiteDomain = domain,
                            TotalDate = date,
                            TotalHour = redisBody.TotalHour,
                            TotalMinute = redisBody.TotalMinute,
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
                        DbContext.FlowVolumeByMinute.Update(
                            p => p.SiteDomain == lockDomain && p.TotalDate == date && p.TotalHour == date.Hour && p.TotalMinute == date.Minute,
                            m => new FlowVolumeByMinute
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
