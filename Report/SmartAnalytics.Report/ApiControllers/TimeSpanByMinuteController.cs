using System.Collections.Generic;
using System.Web.Http;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;
using SmartAnalytics.Services.Models;

namespace SmartAnalytics.Report.ApiControllers
{
    public class TimeSpanByMinuteController : ApiController
    {
        private readonly IFlowService _flowService = new FlowService();

        public IEnumerable<TimeSpanByMinutePageItem> Get(string domain = "www.baidu.com", int minute = 30)
        {
            return _flowService.GetTimeSpanByMinuteList(domain, minute);
        }
    }
}
