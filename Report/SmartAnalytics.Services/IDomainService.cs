using System.Collections.Generic;
using SmartAnalytics.Entities;
using SmartAnalytics.Services.Models;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Services
{
    public interface IDomainService
    {
        List<Domain> GetAllList();

        PagedResult<DomainPageItem> GetList(Paging paging);

        Domain Get(int id);

        void Create(Domain model);

        void Edit(Domain model);

        void Delete(int id);
    }
}
