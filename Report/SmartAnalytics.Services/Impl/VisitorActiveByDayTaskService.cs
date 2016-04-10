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
    /// 访客活跃度
    /// </summary>
    public class VisitorActiveByDayTaskService : ServiceContext, ITaskService
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(VisitorActiveByDayTaskService));

        public string Command
        {
            get
            {
                return "VisitorActive";
            }
        }

        public string Name
        {
            get
            {
                return "访客分析-活跃度(每日执行一次)";
            }
        }

        public VisitorActiveByDayTaskService()
        {
            SetCacheInstance(new RedisContext());
        }

        public void Exec(string[] args)
        {
            if (args == null || args.Length == 0 || string.IsNullOrEmpty(args[0]))
            {
                var today = DateTime.Parse(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));

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
                var redisKey = string.Format("visitor:active:{0}:{1}", date.ToString("yyyyMMdd"), domain);
                var redisBody = CacheContext.Get<List<VisitorActiveByDayCacheItem>>(redisKey);
                if (redisBody != null)
                {
                    var lockDomain = domain;
                    var query = from p in DbContext.VisitorActive
                                where p.SiteDomain == lockDomain && p.TotalDate == date
                                select p.Id;

                    var queryCount = query.Count();
                    if (queryCount > 0)
                    {
                        DbContext.VisitorActive.Delete(p => p.SiteDomain == lockDomain && p.TotalDate == date);
                    }

                    foreach (var m in redisBody)
                    {
                        DbContext.VisitorActive.Add(new VisitorActive
                        {
                            SiteDomain = m.SiteDomain,
                            TotalDate = m.TotalDate,
                            Depth = m.Depth,
                            DepthCount = m.DepthCount
                        });
                    }
                    DbContext.SaveChanges();
                }
            }
        }
    }
}
