using System.Threading;
using System.Web.Mvc;
using SmartAnalytics.Report.Filters;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;

namespace SmartAnalytics.Report.Controllers
{
    [UserAuthorization]
    public class ToolController : Controller
    {
        private readonly ISiteCategoryService _siteCategoryService = new SiteCategoryService();

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult ReLoadSiteCategoryCache()
        {
            new Thread(_siteCategoryService.LoadDataToRedis).Start();

            return RedirectToAction("Index");
        }
    }
}