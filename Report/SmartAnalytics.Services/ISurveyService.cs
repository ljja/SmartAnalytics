using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartAnalytics.Services.Models;
using SmartAnalytics.Services.Util;

namespace SmartAnalytics.Services
{
    public interface ISurveyService
    {
        List<SurveyDomainByDayPageItem> GetSurveyDomainByDayList(DateTime day);

        List<SurveyDomainByHourPageItem> GetSurveyDomainByHourList(DateTime day);

        List<SurveyPageByDayPageItem> GetSurveyPageByDayList(DateTime day, string siteDomain);

        PagedResult<UserLocalPageItem> GetList(Paging paging, string siteDomain);
    }
}
