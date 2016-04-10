using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAnalytics.Cache;
using SmartAnalytics.Entities;
using SmartAnalytics.Services.Models;
using SmartAnalytics.Services.Util;
using EntityFramework.Extensions;

namespace SmartAnalytics.Services.Impl
{
    public class SurveyPageByDayTaskService : ServiceContext, ITaskService
    {

        public string Command
        {
            get { return "SurveyPage"; }
        }

        public string Name
        {
            get { return "受访分析-受访页面(每天执行一次)"; }
        }

        public SurveyPageByDayTaskService()
        {
            SetCacheInstance(new RedisContext());
        }

        public void Exec(string[] args)
        {
            if (args == null || args.Length == 0 || string.IsNullOrEmpty(args[0]))
            {
                var lastDay = DateTime.Parse(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd"));

                SyncRedisCache(lastDay);

                return;
            }

            try
            {
                var DayCount = int.Parse(args[0]);

                for (var i = 0; i <= DayCount; i++)
                {
                    var day = DateTime.Parse(DateTime.Now.AddDays(-i).ToString("yyyy-MM-dd"));

                    SyncRedisCache(day);
                }
            }
            catch { }
        }
        /// <summary>
        /// 解析redis数据放入数据库
        /// </summary>
        /// <param name="dateTime"></param>
        private void SyncRedisCache(DateTime dateTime)
        {
            //var day = DateTime.Parse(dateTime.ToString("yyyy-MM-dd"));
            //取得domain
            var siteDomains = DbContext.Domain.Select(p => p.SiteDomain).ToList();
            foreach (var domain in siteDomains)
            {
                //respondents:page:[年月日]:[域名]
                var redisKey = string.Format("respondents:page:{0}:{1}", dateTime.ToString("yyyyMMdd"), domain);
                var redisBody = CacheContext.Get<List<SurveyPageByDayCacheItem>>(redisKey);
                if (redisBody != null)
                {
                    //清理当前日期的全部数据
                    var lockDomain = domain;
                    var query = from p in DbContext.SurveyPage
                                where p.SiteDomain == lockDomain && p.TotalDate == dateTime
                                select p.Id;

                    var queryCount = query.Count();
                    if (queryCount > 0)
                    {
                        DbContext.SurveyPage.Delete(p => p.SiteDomain == lockDomain && p.TotalDate == dateTime);
                    }

                    foreach (var m in redisBody)
                    {
                        DbContext.SurveyPage.Add(new SurveyPage
                        {
                            SiteDomain = m.SiteDomain,
                            TotalDate = m.TotalDate,
                            Url = m.Url,
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
            }
        }
    }
}
