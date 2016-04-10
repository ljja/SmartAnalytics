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

    public class OriginPageController : ApiController
    {
        private readonly IOriginService _originService = new OriginService();

        [AcceptVerbs("GET")]
        [Route("api/OriginPage/day")]
        public IEnumerable<OriginPageDomainPageItem> GetListByDay(string date, string domain = "www.baidu.com")
        {
            if (string.IsNullOrEmpty(date))
            {
                date = DateTime.Now.ToString("yyyy-MM-dd");
            }

            return _originService.GetOriginPageDomainListByDay(domain, DateTime.Parse(date));
        }

        [AcceptVerbs("GET")]
        [Route("api/OriginPage/hour")]
        public IEnumerable<OriginPageDomainHourEChartsPageItem> GetListByHour(string date, string domain = "www.baidu.com")
        {
            if (string.IsNullOrEmpty(date))
            {
                date = DateTime.Now.ToString("yyyy-MM-dd");
            }

            var queryResult = _originService.GetOriginPageDomainHourListByDay(domain, DateTime.Parse(date));

            var originDomainList = queryResult.GroupBy(p => p.OriginDomain)
                .Select(p => new OriginPageDomainHourEChartsPageItem
                {
                    OriginDomain = p.Key
                })
                .OrderByDescending(p => p.OriginDomain)
                .ToList();

            //补全24个小时段数据
            foreach (var m in originDomainList)
            {
                for (var i = 0; i <= 23; i++)
                {
                    var hourModel = queryResult.FirstOrDefault(p => p.TotalHour == i && p.OriginDomain == m.OriginDomain);
                    if (hourModel == null)
                    {
                        m.Data.Add(0);
                    }
                    else
                    {
                        m.Data.Add(hourModel.TotalCount);
                    }
                }
            }

            return originDomainList;
        }
    }
}
