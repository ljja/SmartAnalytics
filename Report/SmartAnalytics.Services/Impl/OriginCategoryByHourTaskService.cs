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
    public class OriginCategoryByHourTaskService : ServiceContext, ITaskService
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(OriginCategoryByHourTaskService));

        public string Command
        {
            get { return "OriginCategoryHour"; }
        }

        public string Name
        {
            get { return "来源分类-按小时统计(每小时执行一次)"; }
        }

        public OriginCategoryByHourTaskService()
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
            catch (Exception exception)
            {
                _logger.Error(exception);
            }
        }

        /// <summary>
        /// 
        /// KEY:referrer:category:[年月日]:[小时]:[域名]
        /// 
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
                var redisKey = string.Format("referrer:category:{0}:{1}:{2}", dateTime.ToString("yyyyMMdd"), dateTime.Hour, domain);
                var redisBody = CacheContext.Get<List<OriginCategoryByHourCacheItem>>(redisKey);
                if (redisBody != null && redisBody.Any())
                {
                    foreach (var item in redisBody)
                    {
                        var lockDomain = domain;
                        var cacheItem = item;
                        var query = from p in DbContext.OriginCategory
                                    where p.SiteDomain == lockDomain &&
                                        p.TotalDate == day &&
                                        p.TotalHour == hour &&
                                        p.OriginDomain == cacheItem.OriginDomain &&
                                        p.IndustryCode == cacheItem.IndustryCode
                                    select p.Id;

                        var queryCount = query.Count();
                        if (queryCount == 0)
                        {
                            #region Insert

                            DbContext.OriginCategory.Add(new OriginCategory
                            {
                                //时间维度
                                SiteDomain = domain,
                                TotalDate = day,
                                TotalHour = hour,

                                //来源分类指标
                                OriginDomain = cacheItem.OriginDomain,
                                IndustryCode = cacheItem.IndustryCode,
                                TotalNumber = cacheItem.TotalNumber,

                                //通用指标
                                PageView = cacheItem.PageView,
                                UniqueUser = cacheItem.UniqueUser,
                                NewUniqueUser = cacheItem.NewUniqueUser,
                                NewUniqueUserRate = cacheItem.NewUniqueUserRate,
                                UniqueIp = cacheItem.UniqueIp,
                                AccessNumber = cacheItem.AccessNumber,
                                UserViewPageAverage = cacheItem.UserViewPageAverage,
                                ViewPageDeptAverage = cacheItem.ViewPageDeptAverage,
                                ViewPageTimeSpanAverage = cacheItem.ViewPageTimeSpanAverage,
                                BounceRate = cacheItem.BounceRate
                            });
                            DbContext.SaveChanges();

                            #endregion
                        }
                        else
                        {
                            #region update

                            DbContext.OriginCategory.Update(
                               p => p.SiteDomain == lockDomain &&
                                        p.TotalDate == day &&
                                        p.TotalHour == hour &&
                                        p.OriginDomain == cacheItem.OriginDomain &&
                                        p.IndustryCode == cacheItem.IndustryCode,
                               m => new OriginCategory
                               {
                                   //分类指标
                                   TotalNumber = cacheItem.TotalNumber,

                                   //通用指标
                                   PageView = cacheItem.PageView,
                                   UniqueUser = cacheItem.UniqueUser,
                                   NewUniqueUser = cacheItem.NewUniqueUser,
                                   NewUniqueUserRate = cacheItem.NewUniqueUserRate,
                                   UniqueIp = cacheItem.UniqueIp,
                                   AccessNumber = cacheItem.AccessNumber,
                                   UserViewPageAverage = cacheItem.UserViewPageAverage,
                                   ViewPageDeptAverage = cacheItem.ViewPageDeptAverage,
                                   ViewPageTimeSpanAverage = cacheItem.ViewPageTimeSpanAverage,
                                   BounceRate = cacheItem.BounceRate
                               });

                            #endregion
                        }
                    }
                }
            }
        }
    }
}
