using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using SmartAnalytics.Report.Models;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;
using SmartAnalytics.Services.Models;

namespace SmartAnalytics.Report.ApiControllers
{
    public class OriginCategoryController : ApiController
    {
        private readonly IOriginService _originService = new OriginService();

        [AcceptVerbs("GET")]
        [Route("api/OriginCategory/day")]
        public IEnumerable<CategoryListByDatePageItem> GetListByDay(string date, string domain = "www.baidu.com", string industryCode = "")
        {
            if (string.IsNullOrEmpty(date))
            {
                date = DateTime.Now.ToString("yyyy-MM-dd");
            }

            if (string.IsNullOrEmpty(industryCode))
            {
                return _originService.GetCategoryListByDate(domain, DateTime.Parse(date));
            }
            else
            {
                return _originService.GetCategoryListByDate(domain, DateTime.Parse(date), industryCode);
            }
        }

        [AcceptVerbs("GET")]
        [Route("api/OriginCategory/hour")]
        public IEnumerable<CategoryListByHourEChartsPageItem> GetListByHour(string date, string domain = "www.baidu.com", string industryCode = "")
        {
            if (string.IsNullOrEmpty(date))
            {
                date = DateTime.Now.ToString("yyyy-MM-dd");
            }

            List<CategoryListByHourPageItem> queryResult;

            if (string.IsNullOrEmpty(industryCode))
            {
                queryResult = _originService.GetCategoryListByHour(domain, DateTime.Parse(date));
            }
            else
            {
                queryResult = _originService.GetCategoryListByHour(domain, DateTime.Parse(date), industryCode);
            }

            var industryCodeList = queryResult.GroupBy(p => new { p.IndustryCode, p.IndustryCodeName })
                .Select(p => new CategoryListByHourEChartsPageItem
                {
                    IndustryCode = p.Key.IndustryCode,
                    IndustryCodeName = p.Key.IndustryCodeName
                })
                .OrderByDescending(p => p.IndustryCode)
                .ToList();

            foreach (var m in industryCodeList)
            {
                for (var i = 0; i < 23; i++)
                {
                    var hourModel = queryResult.FirstOrDefault(p => p.TotalHour == i && p.IndustryCode == m.IndustryCode);
                    if (hourModel == null)
                    {
                        m.Data.Add(0);
                    }
                    else
                    {
                        m.Data.Add(hourModel.TotalNumber);
                    }
                }
            }

            return industryCodeList;
        }
    }
}
