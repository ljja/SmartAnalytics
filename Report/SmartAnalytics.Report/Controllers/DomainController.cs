using System.Web.Mvc;
using SmartAnalytics.Entities;
using SmartAnalytics.Report.Filters;
using SmartAnalytics.Report.Models;
using SmartAnalytics.Services;
using SmartAnalytics.Services.Impl;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Report.Controllers
{
    [UserAuthorization]
    public class DomainController : Controller
    {
        private readonly IDomainService _domainService = new DomainService();

        public ActionResult Index(int pageIndex = 0, int pageSize = 20)
        {
            var paging = new Paging { PageIndex = pageIndex, PageSize = pageSize };

            var pageResult = _domainService.GetList(paging);

            return View(pageResult);
        }

        public ActionResult Create()
        {
            return View(new DomainCreateRequest());
        }

        [HttpPost]
        [ValidateModelState]
        public ActionResult Create(DomainCreateRequest model)
        {
            _domainService.Create(new Domain { SiteDomain = model.SiteDomain, DomainAlias = model.DomainAlias });

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id = 0)
        {
            if (id <= 0) return RedirectToAction("Index");

            var model = _domainService.Get(id);

            var viewModel = new DomainEditRequest
            {
                SiteDomain = model.SiteDomain,
                DomainAlias = model.DomainAlias,
                Id = model.Id
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateModelState]
        public ActionResult Edit(DomainEditRequest model)
        {
            _domainService.Edit(new Domain
            {
                SiteDomain = model.SiteDomain,
                DomainAlias = model.DomainAlias,
                Id = model.Id
            });

            return RedirectToAction("Index");
        }
    }
}