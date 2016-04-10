using System;
using System.Collections.Generic;
using SmartAnalytics.Services.Models;

namespace SmartAnalytics.Services
{
    /// <summary>
    /// 来源分析服务接口
    /// </summary>
    public interface IOriginService
    {
        List<CategoryListByDatePageItem> GetCategoryListByDate(string domain, DateTime date);

        List<CategoryListByDatePageItem> GetCategoryListByDate(string domain, DateTime date, string industryCode);

        List<CategoryListByHourPageItem> GetCategoryListByHour(string domain, DateTime date);

        List<CategoryListByHourPageItem> GetCategoryListByHour(string domain, DateTime date, string industryCode);

        List<OriginDomainListByDatePageItem> GetOriginDomainListByDate(string domain, DateTime date);

        List<OriginDomainListByHourPageItem> GetOriginDomainListByHour(string domain, DateTime date);

        List<OriginWordPageItem> GetOriginWordList(string domain, DateTime date);

        List<OriginPagePageItem> GetOriginPageListByDay(string domain, DateTime date);

        List<OriginPageDomainPageItem> GetOriginPageDomainListByDay(string domain, DateTime date);

        List<OriginPageDomainHourPageItem> GetOriginPageDomainHourListByDay(string domain, DateTime date);
    }
}
