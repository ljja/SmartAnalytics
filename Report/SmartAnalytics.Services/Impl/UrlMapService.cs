using System.Linq;
using SmartAnalytics.Services.Models;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Services.Impl
{
    public class UrlMapService : ServiceContext, IUrlMapService
    {
        public PagedResult<UrlMapPageItem> GetList(Paging paging, string domain)
        {
            var queryPageResult = new PagedResult<UrlMapPageItem>
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
            };

            var query = from p in DbContext.UrlMap
                        where p.SiteDomain == domain
                        orderby p.CreateTime descending
                        select new UrlMapPageItem
                        {
                            UrlTitle = p.UrlTitle,
                            UrlAddress = p.UrlAddress
                        };

            queryPageResult.SizeCount = query.Count();
            queryPageResult.Result = query.Skip(paging.PageIndex * paging.PageSize).Take(paging.PageSize).ToList();

            return queryPageResult;
        }
    }
}
