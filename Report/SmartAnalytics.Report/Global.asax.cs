using System;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using StackExchange.Redis;
using SmartAnalytics.Cache;
using SmartAnalytics.Entities;

namespace SmartAnalytics.Report
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly static string RedisConnection = ConfigurationManager.AppSettings["RedisConnection"];
        private readonly ILog _logger = LogManager.GetLogger(typeof(MvcApplication));

        protected void Application_Start()
        {
            AppDomain.CurrentDomain.FirstChanceException += CurrentDomainOnFirstChanceException;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //禁用X-AspNetMvc-Version头
            MvcHandler.DisableMvcResponseHeader = true;

            Task.Factory.StartNew(() =>
            {
                Database.SetInitializer(new CreateDatabaseIfNotExists<ReportDbContext>());
                Database.SetInitializer(new InitData());
                var count = new ReportDbContext().User.Count();

                if (RedisContext.RedisDatabase == null)
                {
                    try
                    {
                        RedisContext.RedisDatabase = ConnectionMultiplexer.Connect(RedisConnection).GetDatabase();
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(ex);
                    }
                }
            });
        }

        private void CurrentDomainOnFirstChanceException(object sender, FirstChanceExceptionEventArgs e)
        {
            _logger.Error(e.Exception);
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            var lastError = Server.GetLastError().GetBaseException();
            {
                _logger.Error(lastError);
            }
        }
    }
}
