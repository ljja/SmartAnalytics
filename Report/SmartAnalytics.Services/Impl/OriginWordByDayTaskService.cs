using System;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SmartAnalytics.Cache;
using SmartAnalytics.Entities;
using SmartAnalytics.Services.Models;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Services.Impl
{
    public class OriginWordByDayTaskService : ServiceContext, ITaskService
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(OriginWordByDayTaskService));

        public string Command
        {
            get { return "OriginWordDay"; }
        }

        public string Name
        {
            get { return "搜索词-按天统计(每日执行一次)"; }
        }

        public void Exec(string[] args)
        {
            var keyPath = "referrer:keyword";

            string messageBody = null;

            do
            {
                try
                {
                    messageBody = RedisContext.RedisDatabase.ListRightPop(keyPath);

                    if (string.IsNullOrEmpty(messageBody)) break;

                    var m = JsonConvert.DeserializeObject<OriginWordByDayCacheItem>(messageBody, new IsoDateTimeConverter { DateTimeFormat = "yyyyMMdd" });

                    if (m == null) continue;

                    DbContext.OriginWord.Add(new OriginWord
                    {
                        SiteDomain = m.SiteDomain,
                        TotalDate = m.TotalDate,
                        WordText = m.WordText,
                        BaiDuTotalCount = m.BaiDuTotalCount,
                        HaoSouTotalCount = m.HaoSouTotalCount,
                        SouGouTotalCount = m.SouGouTotalCount,
                        GoogleTotalCount = m.GoogleTotalCount
                    });
                    DbContext.SaveChanges();

                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                }
            } while (string.IsNullOrEmpty(messageBody) == false);

        }
    }
}
