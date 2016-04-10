using System;
using System.Linq;
using System.Web.Http;
using SmartAnalytics.Report.Models;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;

namespace SmartAnalytics.Report.ApiControllers
{
    public class NewOldVisitorController : ApiController
    {
        private readonly IVisitorService _visitorService = new VisitorService();

        public NewOldVisitorPageItem Get(string date = "", string domain = "www.baidu.com")
        {
            if (string.IsNullOrEmpty(date))
            {
                date = DateTime.Now.ToString("yyyy-MM-dd");
            }

            var pageResult = _visitorService.GetVisitorNewOldListByDay(domain, DateTime.Parse(date));

            return new NewOldVisitorPageItem
            {
                NewVisitorCount = pageResult.Where(p => p.IsNewVisitor).Sum(p => p.UniqueUser),
                OldVisitorCount = pageResult.Where(p => p.IsNewVisitor == false).Sum(p => p.UniqueUser)
            };
        }
    }
}
