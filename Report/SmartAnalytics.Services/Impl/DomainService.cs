using System.Collections.Generic;
using System.Linq;
using EntityFramework.Extensions;
using SmartAnalytics.Entities;
using SmartAnalytics.Services.Models;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Services.Impl
{
    public class DomainService : ServiceContext, IDomainService
    {
        public List<Domain> GetAllList()
        {
            return DbContext.Domain.OrderByDescending(p => p.SiteDomain).ToList();
        }

        public PagedResult<DomainPageItem> GetList(Paging paging)
        {
            var queryPageResult = new PagedResult<DomainPageItem>
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
            };

            var query = from m in DbContext.Domain
                        orderby m.Id descending
                        select new DomainPageItem
                        {
                            DomainAlias = m.DomainAlias,
                            SiteDomain = m.SiteDomain,
                            Id = m.Id
                        };

            queryPageResult.SizeCount = query.Count();
            queryPageResult.Result = query.Skip(paging.PageIndex * paging.PageSize).Take(paging.PageSize).ToList();

            return queryPageResult;
        }

        public Domain Get(int id)
        {
            return DbContext.Domain.FirstOrDefault(p => p.Id == id);
        }

        public void Create(Domain model)
        {
            DbContext.Domain.Add(model);
            DbContext.SaveChanges();
        }

        public void Edit(Domain model)
        {
            DbContext.Domain.Update(p => p.Id == model.Id, m => new Domain { SiteDomain = model.SiteDomain, DomainAlias = model.DomainAlias });
        }

        public void Delete(int id)
        {
            DbContext.Domain.Delete(p => p.Id == id);
        }
    }
}