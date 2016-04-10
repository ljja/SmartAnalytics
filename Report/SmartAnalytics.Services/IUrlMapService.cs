using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAnalytics.Services.Models;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Services
{
    public interface IUrlMapService
    {
        PagedResult<UrlMapPageItem> GetList(Paging paging, string domain);
    }
}
