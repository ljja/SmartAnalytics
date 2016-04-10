using SmartAnalytics.Entities;
using SmartAnalytics.Services.Models;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Services
{
    public interface IUserManageService
    {
        PagedResult<UserManagePageItem> GetList(Paging paging);

        User Get(string username = "");

        bool Edit(User model);

        bool Create(User model);

        bool Delete(string username);

        bool ExistsUser(string username);
    }
}