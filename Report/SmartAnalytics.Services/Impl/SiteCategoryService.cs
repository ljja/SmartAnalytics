using System;
using System.Linq;
using log4net;
using SmartAnalytics.Cache;
using SmartAnalytics.Services.Models;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Services.Impl
{
    public class SiteCategoryService : ServiceContext, ISiteCategoryService
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(SiteCategoryService));

        public SiteCategoryService()
        {
            SetCacheInstance(new RedisContext());
        }

        public void LoadDataToRedis()
        {
            var siteInfo = DbContext.SiteCategory.Select(p => new SiteCategoryCacheModel
            {
                Domain = p.Domain,
                IndustryCode = p.IndustryCode,
                CityAreaCode = p.CityAreaCode
            }).ToList();

            const string keyFormat = "conf:siteinfo:{0}";

            foreach (var m in siteInfo)
            {
                var cacheKey = string.Format(keyFormat, m.Domain);
                try
                {
                    CacheContext.Set(cacheKey, m);
                }
                catch (Exception e)
                {
                    _logger.Error(cacheKey);
                    _logger.Error(e);
                }
            }
        }
    }
}
