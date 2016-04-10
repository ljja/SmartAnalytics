using System;
using System.Linq;
using SmartAnalytics.Services.Sessions;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Services.Impl
{
    public sealed class ManageService : ServiceContext, IManageService
    {

        public void LoginOut(string username, string ipAddress)
        {
        }

        public UserSession GetUserSession(string username, string password, string ipAddress)
        {
            if (DbContext.User.Any(p => p.UserName == username && p.UserPwd == password && p.IsEnable))
            {
                return new UserSession
                {
                    LoginDateTime = DateTime.Now,
                    LoginIpAddress = ipAddress,
                    UserName = username,
                };
            }

            return null;
        }
    }
}