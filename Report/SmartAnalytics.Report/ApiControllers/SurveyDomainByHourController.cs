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
    public class SurveyDomainByHourController : ApiController
    {
        private readonly ISurveyService _surveyService = new SurveyService();

        /// <summary>
        /// /api/SurveyDomainByHour?date=2015-06-10
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IEnumerable<SurveyDomainByHourEChartsPageItem> Get(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
            }

            var queryResult = _surveyService.GetSurveyDomainByHourList(DateTime.Parse(date));

            var query = from p in queryResult
                        group p by p.SiteDomain into tempGroup
                        orderby tempGroup.Sum(p => p.PageView) descending
                        select new SurveyDomainByHourEChartsPageItem
                        {
                            SiteDomain = tempGroup.Key,
                            Data = tempGroup.OrderBy(p=>p.TotalHour).Select(p => p.PageView).ToList()
                        };

            return query.ToList();
        }
    }
}
