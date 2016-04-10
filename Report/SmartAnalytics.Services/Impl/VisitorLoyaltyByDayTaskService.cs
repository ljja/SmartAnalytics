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
    /// <summary>
    /// 访客忠诚度
    /// </summary>
    public class VisitorLoyaltyByDayTaskService : ServiceContext, ITaskService
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(VisitorLoyaltyByDayTaskService));

        public string Command
        {
            get
            {
                return "VisitorLoyalty";
            }
        }

        public string Name
        {
            get
            {
                return "访客分析-忠诚度(每日执行一次)";
            }
        }

        public VisitorLoyaltyByDayTaskService()
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
                var redisKey = string.Format("visitor:loyalty:{0}:{1}", date.ToString("yyyyMMdd"), domain);
                var redisBody = CacheContext.Get<List<VisitorLoyaltyByDayCacheItem>>(redisKey);
                if (redisBody != null)
                {
                    var lockDomain = domain;
                    var query = from p in DbContext.VisitorLoyalty
                                where p.SiteDomain == lockDomain && p.TotalDate == date
                                select p.Id;

                    var queryCount = query.Count();
                    if (queryCount > 0)
                    {
                        DbContext.VisitorLoyalty.Delete(p => p.SiteDomain == lockDomain && p.TotalDate == date);
                    }

                    foreach (var m in redisBody)
                    {
                        DbContext.VisitorLoyalty.Add(new VisitorLoyalty
                        {
                            SiteDomain = m.SiteDomain,
                            TotalDate = m.TotalDate,
                            FrequencyCount = m.FrequencyCount,
                            UniqueUser = m.UniqueUser,
                            PageView = m.PageView
                        });
                    }
                    DbContext.SaveChanges();
                }
            }
        }
    }
}
