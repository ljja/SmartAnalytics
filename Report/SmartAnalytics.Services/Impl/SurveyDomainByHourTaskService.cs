using System;
using System.Collections.Generic;
using EntityFramework.Extensions;
using log4net;
using SmartAnalytics.Cache;
using SmartAnalytics.Entities;
using SmartAnalytics.Services.Models;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Services.Impl
{
    public class SurveyDomainByHourTaskService : ServiceContext, ITaskService
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(SurveyDomainByHourTaskService));

        public string Command
        {
            get { return "SurveyDomain"; }
        }

        public string Name
        {
            get
            {
                return "受访分析-受访域名(每小时执行一次)";
            }
        }

        public SurveyDomainByHourTaskService()
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

            //respondents:domain:[年月日]:[小时]
            var redisKey = string.Format("respondents:domain:{0}:{1}", dateTime.ToString("yyyyMMdd"), dateTime.Hour.ToString("D2"));
            var redisBody = CacheContext.Get<List<SurveyDomainByHourCacheItem>>(redisKey);

            if (redisBody != null)
            {
                try
                {
                    //清理当前小时的全部数据
                    DbContext.SurveyDomain.Delete(p => p.TotalDate == day && p.TotalHour == hour);

                    //全新插入数据
                    foreach (var m in redisBody)
                    {
                        DbContext.SurveyDomain.Add(new SurveyDomain
                        {
                            SiteDomain = m.SiteDomain,
                            TotalDate = m.TotalDate,
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
                            BounceRate = m.BounceRate
                        });
                    }
                    DbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            }
        }
    }
}
