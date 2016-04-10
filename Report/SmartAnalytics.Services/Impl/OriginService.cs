using System;
using System.Collections.Generic;
using System.Linq;
using SmartAnalytics.Services.Models;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Services.Impl
{
    public class OriginService : ServiceContext, IOriginService
    {
        public List<CategoryListByDatePageItem> GetCategoryListByDate(string domain, DateTime date)
        {
            var query = from originCategory in DbContext.OriginCategory

                        where originCategory.TotalDate == date && originCategory.SiteDomain == domain

                        group originCategory by originCategory.IndustryCode into tempGroup

                        join industryCode in DbContext.IndustryCode on tempGroup.Key equals industryCode.CategoryCode into result1

                        from leftIndustryCode in result1.DefaultIfEmpty()

                        orderby tempGroup.Sum(p => p.TotalNumber) descending

                        select new CategoryListByDatePageItem
                        {
                            IndustryCode = tempGroup.Key,
                            IndustryCodeName = leftIndustryCode.CategoryName,

                            TotalNumber = tempGroup.Sum(p => p.TotalNumber),

                            PageView = tempGroup.Sum(p => p.PageView),
                            UniqueUser = tempGroup.Sum(p => p.UniqueUser),
                            NewUniqueUser = tempGroup.Sum(p => p.NewUniqueUser),
                            NewUniqueUserRate = tempGroup.Average(p => p.NewUniqueUserRate),
                            UniqueIp = tempGroup.Sum(p => p.UniqueIp),
                            AccessNumber = tempGroup.Sum(p => p.AccessNumber),
                            UserViewPageAverage = tempGroup.Average(p => p.UserViewPageAverage),
                            ViewPageDeptAverage = tempGroup.Average(p => p.ViewPageDeptAverage),
                            ViewPageTimeSpanAverage = (int)tempGroup.Average(p => p.ViewPageTimeSpanAverage),
                            BounceRate = tempGroup.Average(p => p.BounceRate),
                        };

            return query.ToList();
        }

        public List<CategoryListByDatePageItem> GetCategoryListByDate(string domain, DateTime date, string industryCode)
        {
            var query = from originCategory in DbContext.OriginCategory

                        where originCategory.TotalDate == date && originCategory.SiteDomain == domain

                        group originCategory by originCategory.IndustryCode into tempGroup

                        join industryCodeTable in DbContext.IndustryCode on tempGroup.Key equals industryCodeTable.CategoryCode into result1

                        from leftIndustryCode in result1.DefaultIfEmpty()

                        where leftIndustryCode.ParentCode == industryCode

                        orderby tempGroup.Sum(p => p.TotalNumber) descending

                        select new CategoryListByDatePageItem
                        {
                            IndustryCode = tempGroup.Key,
                            IndustryCodeName = leftIndustryCode.CategoryName,

                            TotalNumber = tempGroup.Sum(p => p.TotalNumber),

                            PageView = tempGroup.Sum(p => p.PageView),
                            UniqueUser = tempGroup.Sum(p => p.UniqueUser),
                            NewUniqueUser = tempGroup.Sum(p => p.NewUniqueUser),
                            NewUniqueUserRate = tempGroup.Average(p => p.NewUniqueUserRate),
                            UniqueIp = tempGroup.Sum(p => p.UniqueIp),
                            AccessNumber = tempGroup.Sum(p => p.AccessNumber),
                            UserViewPageAverage = tempGroup.Average(p => p.UserViewPageAverage),
                            ViewPageDeptAverage = tempGroup.Average(p => p.ViewPageDeptAverage),
                            ViewPageTimeSpanAverage = (int)tempGroup.Average(p => p.ViewPageTimeSpanAverage),
                            BounceRate = tempGroup.Average(p => p.BounceRate),
                        };

            return query.ToList();
        }

        public List<CategoryListByHourPageItem> GetCategoryListByHour(string domain, DateTime date)
        {
            var query = from originCategory in DbContext.OriginCategory

                        where originCategory.TotalDate == date && originCategory.SiteDomain == domain

                        group originCategory by new { originCategory.TotalHour, originCategory.IndustryCode } into tempGroup

                        join industryCode in DbContext.IndustryCode on tempGroup.Key.IndustryCode equals industryCode.CategoryCode into result1

                        from leftIndustryCode in result1.DefaultIfEmpty()

                        orderby tempGroup.Sum(p => p.TotalNumber) descending

                        select new CategoryListByHourPageItem
                        {
                            TotalHour = tempGroup.Key.TotalHour,
                            IndustryCode = tempGroup.Key.IndustryCode,

                            IndustryCodeName = leftIndustryCode.CategoryName,

                            TotalNumber = tempGroup.Sum(p => p.TotalNumber),

                            PageView = tempGroup.Sum(p => p.PageView),
                            UniqueUser = tempGroup.Sum(p => p.UniqueUser),
                            NewUniqueUser = tempGroup.Sum(p => p.NewUniqueUser),
                            NewUniqueUserRate = tempGroup.Average(p => p.NewUniqueUserRate),
                            UniqueIp = tempGroup.Sum(p => p.UniqueIp),
                            AccessNumber = tempGroup.Sum(p => p.AccessNumber),
                            UserViewPageAverage = tempGroup.Average(p => p.UserViewPageAverage),
                            ViewPageDeptAverage = tempGroup.Average(p => p.ViewPageDeptAverage),
                            ViewPageTimeSpanAverage = (int)tempGroup.Average(p => p.ViewPageTimeSpanAverage),
                            BounceRate = tempGroup.Average(p => p.BounceRate),
                        };

            return query.ToList();
        }

        public List<CategoryListByHourPageItem> GetCategoryListByHour(string domain, DateTime date, string industryCode)
        {
            var query = from originCategory in DbContext.OriginCategory

                        where originCategory.TotalDate == date && originCategory.SiteDomain == domain

                        group originCategory by new { originCategory.TotalHour, originCategory.IndustryCode } into tempGroup

                        join industryCodeTable in DbContext.IndustryCode on tempGroup.Key.IndustryCode equals industryCodeTable.CategoryCode into result1

                        from leftIndustryCode in result1.DefaultIfEmpty()

                        where leftIndustryCode.ParentCode == industryCode

                        orderby tempGroup.Sum(p => p.TotalNumber) descending

                        select new CategoryListByHourPageItem
                        {
                            TotalHour = tempGroup.Key.TotalHour,
                            IndustryCode = tempGroup.Key.IndustryCode,

                            IndustryCodeName = leftIndustryCode.CategoryName,

                            TotalNumber = tempGroup.Sum(p => p.TotalNumber),

                            PageView = tempGroup.Sum(p => p.PageView),
                            UniqueUser = tempGroup.Sum(p => p.UniqueUser),
                            NewUniqueUser = tempGroup.Sum(p => p.NewUniqueUser),
                            NewUniqueUserRate = tempGroup.Average(p => p.NewUniqueUserRate),
                            UniqueIp = tempGroup.Sum(p => p.UniqueIp),
                            AccessNumber = tempGroup.Sum(p => p.AccessNumber),
                            UserViewPageAverage = tempGroup.Average(p => p.UserViewPageAverage),
                            ViewPageDeptAverage = tempGroup.Average(p => p.ViewPageDeptAverage),
                            ViewPageTimeSpanAverage = (int)tempGroup.Average(p => p.ViewPageTimeSpanAverage),
                            BounceRate = tempGroup.Average(p => p.BounceRate),
                        };

            return query.ToList();
        }

        public List<OriginDomainListByDatePageItem> GetOriginDomainListByDate(string domain, DateTime date)
        {
            var query = from originCategory in DbContext.OriginCategory

                        where originCategory.TotalDate == date && originCategory.SiteDomain == domain

                        group originCategory by originCategory.OriginDomain into tempGroup

                        orderby tempGroup.Sum(p => p.TotalNumber) descending

                        select new OriginDomainListByDatePageItem
                        {
                            OriginDomain = tempGroup.Key,
                            TotalNumber = tempGroup.Sum(p => p.TotalNumber),
                            PageView = tempGroup.Sum(p => p.PageView),
                            UniqueUser = tempGroup.Sum(p => p.UniqueUser),
                            NewUniqueUser = tempGroup.Sum(p => p.NewUniqueUser),
                            NewUniqueUserRate = tempGroup.Average(p => p.NewUniqueUserRate),
                            UniqueIp = tempGroup.Sum(p => p.UniqueIp),
                            AccessNumber = tempGroup.Sum(p => p.AccessNumber),
                            UserViewPageAverage = tempGroup.Average(p => p.UserViewPageAverage),
                            ViewPageDeptAverage = tempGroup.Average(p => p.ViewPageDeptAverage),
                            ViewPageTimeSpanAverage = (int)tempGroup.Average(p => p.ViewPageTimeSpanAverage),
                            BounceRate = tempGroup.Average(p => p.BounceRate),
                        };

            return query.ToList();
        }

        public List<OriginDomainListByHourPageItem> GetOriginDomainListByHour(string domain, DateTime date)
        {
            var query = from originCategory in DbContext.OriginCategory

                        where originCategory.TotalDate == date && originCategory.SiteDomain == domain

                        group originCategory by new { originCategory.OriginDomain, originCategory.TotalHour } into tempGroup

                        orderby tempGroup.Sum(p => p.TotalNumber) descending

                        select new OriginDomainListByHourPageItem
                        {
                            OriginDomain = tempGroup.Key.OriginDomain,
                            TotalHour = tempGroup.Key.TotalHour,
                            TotalNumber = tempGroup.Sum(p => p.TotalNumber),
                            PageView = tempGroup.Sum(p => p.PageView),
                            UniqueUser = tempGroup.Sum(p => p.UniqueUser),
                            NewUniqueUser = tempGroup.Sum(p => p.NewUniqueUser),
                            NewUniqueUserRate = tempGroup.Average(p => p.NewUniqueUserRate),
                            UniqueIp = tempGroup.Sum(p => p.UniqueIp),
                            AccessNumber = tempGroup.Sum(p => p.AccessNumber),
                            UserViewPageAverage = tempGroup.Average(p => p.UserViewPageAverage),
                            ViewPageDeptAverage = tempGroup.Average(p => p.ViewPageDeptAverage),
                            ViewPageTimeSpanAverage = (int)tempGroup.Average(p => p.ViewPageTimeSpanAverage),
                            BounceRate = tempGroup.Average(p => p.BounceRate),
                        };

            return query.ToList();
        }

        public List<OriginWordPageItem> GetOriginWordList(string domain, DateTime date)
        {
            var query = from originWord in DbContext.OriginWord
                        where originWord.SiteDomain == domain && originWord.TotalDate == date
                        orderby originWord.WordText descending
                        select new OriginWordPageItem
                        {
                            SiteDomain = originWord.SiteDomain,
                            WordText = originWord.WordText,
                            BaiDuTotalCount = originWord.BaiDuTotalCount,
                            HaoSouTotalCount = originWord.HaoSouTotalCount,
                            SouGouTotalCount = originWord.SouGouTotalCount,
                            GoogleTotalCount = originWord.GoogleTotalCount
                        };

            return query.ToList();
        }

        public List<OriginPagePageItem> GetOriginPageListByDay(string domain, DateTime date)
        {
            var query = from originPage in DbContext.OriginPage
                        where originPage.SiteDomain == domain && originPage.TotalDate == date
                        orderby originPage.TotalCount descending
                        select new OriginPagePageItem
                        {
                            TotalCount = originPage.TotalCount,
                            OriginUrl = originPage.OriginUrl
                        };

            return query.ToList();
        }

        public List<OriginPageDomainPageItem> GetOriginPageDomainListByDay(string domain, DateTime date)
        {
            var query = from p in DbContext.OriginPage
                        where p.SiteDomain == domain && p.TotalDate == date
                        group p by p.OriginDomain into tempGroup
                        orderby tempGroup.Sum(p => p.TotalCount)
                        select new OriginPageDomainPageItem
                        {
                            OriginDomain = tempGroup.Key,
                            TotalCount = tempGroup.Sum(p => p.TotalCount)
                        };

            return query.ToList();
        }

        public List<OriginPageDomainHourPageItem> GetOriginPageDomainHourListByDay(string domain, DateTime date)
        {
            var query = from p in DbContext.OriginPage
                        where p.SiteDomain == domain && p.TotalDate == date
                        group p by new { p.OriginDomain, p.TotalHour } into tempGroup
                        orderby tempGroup.Sum(p => p.TotalCount)
                        select new OriginPageDomainHourPageItem
                        {
                            TotalHour = tempGroup.Key.TotalHour,
                            OriginDomain = tempGroup.Key.OriginDomain,
                            TotalCount = tempGroup.Sum(p => p.TotalCount)
                        };

            return query.ToList();
        }
    }
}
