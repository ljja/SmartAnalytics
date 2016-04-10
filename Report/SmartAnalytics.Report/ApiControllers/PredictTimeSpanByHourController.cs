using System;
using System.Collections.Generic;
using System.Web.Http;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;
using SmartAnalytics.Services.Models;

namespace SmartAnalytics.Report.ApiControllers
{
    /// <summary>
    /// 流量趋势
    /// </summary>
    public class PredictTimeSpanByHourController : ApiController
    {
        private readonly IFlowService _flowService = new FlowService();

        public IEnumerable<PredictTimeSpanByHourPageItem> Get(string date, string domain = "www.baidu.com")
        {
            if (string.IsNullOrEmpty(date))
            {
                date = DateTime.Now.ToString("yyyy-MM-dd");
            }

            return _flowService.GetPredictTimeSpanByHourList(domain, DateTime.Parse(date));
        }
    }
}
