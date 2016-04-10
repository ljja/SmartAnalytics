using System;
using System.Data.Entity.Validation;
using System.Linq;
using EntityFramework.Extensions;
using log4net;
using SmartAnalytics.Cache;
using SmartAnalytics.Entities;
using SmartAnalytics.Services.Models;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Services.Impl
{
    public class OriginPageByHourTaskService : ServiceContext, ITaskService
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(OriginPageByHourTaskService));

        public string Command
        {
            get
            {
                return "OriginPageHour";
            }
        }

        public string Name
        {
            get { return "来源页面-按小时统计(每小时执行一次)"; }
        }

        public OriginPageByHourTaskService()
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
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        /// <summary>
        /// referrer:page:[年月日]:[小时]:[域名]
        /// </summary>
        /// <param name="dateTime"></param>
        private void SyncRedisCache(DateTime dateTime)
        {
            var day = DateTime.Parse(dateTime.ToString("yyyy-MM-dd"));
            var hour = dateTime.Hour;

            var siteDomains = DbContext.Domain.Select(p => p.SiteDomain).ToList();
            foreach (var domain in siteDomains)
            {
                //来源分类统计结果
                var redisKey = string.Format("referrer:page:{0}:{1}:{2}", dateTime.ToString("yyyyMMdd"), dateTime.Hour.ToString("D2"), domain);
                var redisBody = CacheContext.Get<OriginPageByHourCacheItem>(redisKey);
                if (redisBody != null)
                {
                    var lockDomain = domain;
                    var query = from p in DbContext.OriginPage
                                where p.SiteDomain == lockDomain && p.TotalDate == day && p.TotalHour == hour
                                select p.Id;

                    var queryCount = query.Count();
                    if (queryCount > 0)
                    {
                        DbContext.OriginPage.Delete(p => p.SiteDomain == lockDomain && p.TotalDate == day && p.TotalHour == hour);
                    }

                    //add
                    foreach (var m in redisBody.Items)
                    {
                        if (string.IsNullOrEmpty(m.OriginDomain) == false && m.OriginDomain.Length > 50)
                        {
                            m.OriginDomain = m.OriginDomain.Substring(0, 50);
                        }

                        if (string.IsNullOrEmpty(m.OriginUrl) == false && m.OriginUrl.Length > 1024)
                        {
                            m.OriginUrl = m.OriginUrl.Substring(0, 1024);
                        }

                        DbContext.OriginPage.Add(new OriginPage
                        {
                            SiteDomain = redisBody.SiteDomain,
                            TotalDate = redisBody.TotalDate,
                            TotalHour = redisBody.TotalHour,
                            OriginDomain = m.OriginDomain,
                            OriginUrl = m.OriginUrl,
                            TotalCount = m.Count
                        });
                    }

                    try
                    {
                        DbContext.SaveChanges();
                    }
                    catch (DbEntityValidationException entityValidationException)
                    {
                        _logger.Error(entityValidationException);
                    }
                }
            }
        }
    }
}
