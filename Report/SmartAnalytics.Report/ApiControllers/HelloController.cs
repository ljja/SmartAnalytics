using System;
using System.Globalization;
using System.Web.Http;

namespace SmartAnalytics.Report.ApiControllers
{
    public class HelloController : ApiController
    {
        public string Get()
        {
            return DateTime.Now.ToString(CultureInfo.InvariantCulture);
        }
    }
}