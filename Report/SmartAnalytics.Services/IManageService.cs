using SmartAnalytics.Services.Sessions;

namespace SmartAnalytics.Services
{
    public interface IManageService
    {
        void LoginOut(string username, string ipAddress);

        UserSession GetUserSession(string username, string password, string ipAddress);
    }
}